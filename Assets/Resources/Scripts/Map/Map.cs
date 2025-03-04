using Codice.Client.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public bool generateAll;
    public int MaxRows = 100;
    private int BossRow => MaxRows + 1;
    public int MiddleColumn => MaxColumns / 2;
    public int MaxColumns = 7;
    public GameObject EnemyNodePrefab;
    public float basicWeight;
    public GameObject eliteNodePrefab;
    public float eliteWeight;
    public GameObject mysteryNodePrefab;
    public float mysteryWeight;
    public GameObject treasureNodePrefab;
    public float treasureWeight;
    public GameObject shopNodePrefab;
    public float shopWeight;
    public GameObject bossNodePrefab;
    public GameObject linePrefab;
    public float nodeGenerationChance = 0.5f;
    public float routeGenerationChance = 0.5f;
    private static List<AMapNode> nodes = new();
    public AMapNode playerOccupiedNode;
    AMapNode lastHoveredNode = null;
    private bool playerHasEnteredCurrentEncounter = true;
    public static List<AMapNode> Nodes { get => nodes; set => nodes = value; }
    private static Map instance;


    public void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        if (MaxColumns % 2 == 0)
        {
            throw new ArgumentException("MaxColumns must be odd");
        }
        nodes = new();
        GenerateBossNode();
        GenerateMap();
        GenerateFallbackRoutes();
        DiscardOrphanNodes();
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name != "Map")
        {
            return;
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        Unhover(hit);
        Hover(hit);
        HandlePlayerMovement(hit);
        if (Input.GetKeyDown(KeyCode.Space) && !playerHasEnteredCurrentEncounter)
        {
            playerHasEnteredCurrentEncounter = true;
            playerOccupiedNode.EnterEncounter();
        }
    }
    public void OnDestroy()
    {
        instance = null;
    }
    private void HandlePlayerMovement(RaycastHit2D hit)
    {
        if
        (
            !Input.GetMouseButtonDown(0) ||
            hit.collider == null ||
            !hit.collider.TryGetComponent(out AMapNode hitNode) ||
            (
                playerOccupiedNode != null &&
                !playerOccupiedNode.RoutesFromHere.Contains(hitNode)
            ) ||
            (
                playerOccupiedNode == null &&
                hitNode.row != 1
            ) ||
            playerHasEnteredCurrentEncounter == false
        )
        {
            return;
        }
        if (playerOccupiedNode != null)
        {
            playerOccupiedNode.events.OnPlayerLeft.Invoke();
        }
        if(hitNode == null)
        {
            Console.WriteLine("hitNode is null");
        }
        playerOccupiedNode = hitNode;
        playerOccupiedNode.events.OnPlayerOccupied.Invoke();
        playerHasEnteredCurrentEncounter = false;
    }

    private void Hover(RaycastHit2D hit)
    {
        if (hit.collider == null || !hit.collider.TryGetComponent(out AMapNode hitNode) || hitNode == playerOccupiedNode)
        {
            return;
        }
        lastHoveredNode = hitNode;
        hitNode.events.OnHover.Invoke();
    }

    private void Unhover(RaycastHit2D hit)
    {
        if(hit.collider != null)
        {
            if(hit.collider.TryGetComponent(out AMapNode hitNode)&&hitNode == playerOccupiedNode)
            {
                return;
            }
        }
        

        if (lastHoveredNode == null)
        {
            return;
        }

        if (hit.collider == lastHoveredNode.GetComponent<Collider2D>())
        {
            return;
        }

        if (lastHoveredNode == playerOccupiedNode)
        {
            return;
        }
        lastHoveredNode.events.OnUnhover.Invoke();
        lastHoveredNode = null;

    }

    private void DiscardOrphanNodes()
    {
        int failsafe = 0;
        while (GetNodesWithNoRouteLeadingToThem().Count > 0 && failsafe < 1000)
        {
            failsafe++;
            foreach (AMapNode node in GetNodesWithNoRouteLeadingToThem())
            {
                foreach (GameObject line in node.LinesFromHere)
                {
                    Destroy(line);
                }
                Destroy(node.gameObject);
                nodes.Remove(node);
            }
        }
    }

    private void GenerateFallbackRoutes()
    {
        foreach (AMapNode node in GetNodesWithNoRouteLeadingToThem())
        {
            ConnectToAFallbackNode(node);
        }
        void ConnectToAFallbackNode(AMapNode node)
        {
            for (int row = node.row; row >= 1; row--)
            {
                AMapNode targetNode = null;
                targetNode = TryFindingFallbackNode(node, row, targetNode);
                if (targetNode != null)
                {
                    AddToRoutes(node, targetNode);
                    break;
                }
            }
        }
        AMapNode TryFindingFallbackNode(AMapNode node, int row, AMapNode targetNode)
        {
            for (int i = 0; i < 3; i++)
            {
                (int, int) offset = NodeOffsetForFallback(i);
                targetNode = Nodes.Find(targetNode => targetNode.column == node.column + offset.Item1 && targetNode.row == row + offset.Item2);
                if (targetNode != null)
                {
                    return targetNode;
                }
            }
            return null;
        }
        (int, int) NodeOffsetForFallback(int nodeOffsetCounter)
        {
            return (nodeOffsetCounter % 3) switch
            {
                0 => (0, -1),
                1 => (-1, 0),
                2 => (1, 0),
                _ => throw new InvalidOperationException(),
            };
        }
    }


    private static List<AMapNode> GetNodesWithNoRouteLeadingToThem()
    {
        List<AMapNode> NodesWithARouteLeadingToThem = new();
        foreach (AMapNode node in Nodes)
        {
            if (node.row == 1)
            {
                if (!NodesWithARouteLeadingToThem.Contains(node))
                {
                    NodesWithARouteLeadingToThem.Add(node);
                }
            }
            foreach (AMapNode routeTarget in node.RoutesFromHere)
            {

                if (!NodesWithARouteLeadingToThem.Contains(routeTarget))
                {
                    NodesWithARouteLeadingToThem.Add(routeTarget);
                }

            }
        }
        return Nodes.Except(NodesWithARouteLeadingToThem).ToList();
    }

    private void GenerateMap()
    {
        for (int currentRow = MaxRows; currentRow > 0; currentRow--)
        {
            GenerateRow(currentRow);
        }
    }

    private void GenerateBossNode()
    {
        AMapNode bossNode = Instantiate(bossNodePrefab, new UnnormalizedVector3(MiddleColumn, MaxRows + 1, 0), Quaternion.identity).GetComponent<AMapNode>();
        bossNode.row = MaxRows + 1;
        bossNode.column = MiddleColumn;
        bossNode.seed = WorldRandomStream.Range(0, int.MaxValue);
        bossNode.gameObject.transform.SetParent(transform);
        nodes.Add(bossNode);
    }
    private void GenerateRow(int currentRow)
    {
        for (int currentColumn = 0; currentColumn < MaxColumns; currentColumn++)
        {
            if (WorldRandomStream.Range(0f, 1f) > nodeGenerationChance && !generateAll)
            {
                continue;
            }
            AMapNode generatedNode = InstantiateNode(currentRow, currentColumn);
            ConnectNodeToLastRow(generatedNode);
        }
    }

    private void ConnectNodeToLastRow(AMapNode currentNode)
    {
        bool hasAConnection = false;
        for (int columnOffset = -1; columnOffset <= 1; columnOffset++)
        {
            AMapNode targetNode = Nodes.Find(node => node.row == currentNode.row + 1 && node.column == currentNode.column + columnOffset);
            if (TryConnect(currentNode, targetNode))
                hasAConnection = true;
        }
        if (!hasAConnection)
        {
            ConnectToFallback(currentNode);
        }
    }

    private void ConnectToFallback(AMapNode currentNode)
    {
        for (int rowOffset = 1; currentNode.row + rowOffset < BossRow; rowOffset++)
        {
            AMapNode targetNode = nodes.Find(node => node.row == currentNode.row + rowOffset && node.column == currentNode.column);
            if (AddNonBossNodeToRoutesFromThisNode(currentNode, targetNode))
            {
                return;
            }
        }
        AddBossNodeToRoutesFromThisNode(currentNode);
    }
    private bool TryConnect(AMapNode currentNode, AMapNode targetNode)
    {
        if (LastRowIsBoss(currentNode.row))
        {
            AddBossNodeToRoutesFromThisNode(currentNode);
            return false;
        }
        else
        {
            return TryAddingNonBossNodeToRoutesFromThisNode(currentNode, targetNode);
        }
        bool TryAddingNonBossNodeToRoutesFromThisNode(AMapNode currentNode, AMapNode targetNode)
        {
            if (WorldRandomStream.Value() > routeGenerationChance && !generateAll)
            {
                return false;
            }

            return AddNonBossNodeToRoutesFromThisNode(currentNode, targetNode);
        }
    }
    private bool AddNonBossNodeToRoutesFromThisNode(AMapNode currentNode, AMapNode targetNode)
    {
        if (LastRowNodeExists(targetNode) && RouteAvoidsIntersections(currentNode, targetNode))
        {
            AddToRoutes(currentNode, targetNode);
            return true;
        }
        return false;

        static bool LastRowNodeExists(AMapNode lastRowNode) => lastRowNode != null;
        static bool RouteAvoidsIntersections(AMapNode originNode, AMapNode targetNode)
        {
            int direction = targetNode.column.CompareTo(originNode.column);
            if (direction == 0)
            {
                return true;
            }
            AMapNode nextNode = nodes.Find(node => node.row == originNode.row && node.column == originNode.column + direction);
            AMapNode nodeAbove = nodes.Find(node => node.row == originNode.row + 1 && node.column == originNode.column);
            return nodeAbove == null || nextNode == null || !nextNode.RoutesFromHere.Contains(nodeAbove);
        }
    }
    private void AddToRoutes(AMapNode currentNode, AMapNode targetNode)
    {
        //TODO: ne kelljen itt ellenőrizni, hogy már benne van-e (csak a boss négyszeres csatlakozás miatt kell)
        if (currentNode.RoutesFromHere.Contains(targetNode))
        {
            return;
        }
        currentNode.LinesFromHere.Add(InstantiateLine(currentNode.transform.position, targetNode.transform.position));
        currentNode.RoutesFromHere.Add(targetNode);
    }
    private void AddBossNodeToRoutesFromThisNode(AMapNode currentNode) => AddToRoutes(currentNode, nodes.Find(node => node.row == BossRow && node.column == MiddleColumn));
    private bool LastRowIsBoss(int currentRow) => currentRow + 1 == BossRow;
    private AMapNode InstantiateNode(int currentRow, int currentColumn)
    {
        AMapNode newNode = Instantiate(PickNodeType(), new UnnormalizedVector3(currentColumn + UnityEngine.Random.Range(-100, 100) * 0.002f, currentRow + UnityEngine.Random.Range(-100, 100) * 0.002f, 0), Quaternion.identity).GetComponent<AMapNode>();
        newNode.row = currentRow;
        newNode.column = currentColumn;
        newNode.seed = WorldRandomStream.Range(0, int.MaxValue);
        nodes.Add(newNode);
        newNode.gameObject.transform.SetParent(transform);
        return newNode;
    }

    private GameObject PickNodeType()
    {
        float randomValue = WorldRandomStream.Value() * (basicWeight + eliteWeight + mysteryWeight + treasureWeight + shopWeight);
        if (randomValue < basicWeight)
        {
            return EnemyNodePrefab;
        }
        if (randomValue < basicWeight + eliteWeight)
        {
            return eliteNodePrefab;
        }
        if (randomValue < basicWeight + eliteWeight + mysteryWeight)
        {
            return mysteryNodePrefab;
        }
        if (randomValue < basicWeight + eliteWeight + mysteryWeight + treasureWeight)
        {
            return treasureNodePrefab;
        }
        return shopNodePrefab;
    }

    private GameObject InstantiateLine(Vector3 start, Vector3 end)
    {
        GameObject line = Instantiate(linePrefab, start, Quaternion.identity);
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        line.transform.SetParent(transform);
        return line;
    }
}