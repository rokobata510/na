using System;
using System.Collections.Generic;
using UnityEngine;


public static class PathFinding
{
    public static List<UnnormalizedVector3> FindPathAwayFromTarget(GameObject origin, GameObject target, int gCostLimit = 20, int nodeCountLimit = 100)
    {
        return FindPath(origin, target, (A, B) => -GetDistance(A, B), gCostLimit, nodeCountLimit);
    }
    public static List<UnnormalizedVector3> FindPathTowardsTarget(GameObject origin, GameObject target, int gCostLimit = 20, int nodeCountLimit = 100)
    {
        return FindPath(origin, target, GetDistance, gCostLimit, nodeCountLimit);
    }
    public static List<UnnormalizedVector3> FindPath(GameObject origin, GameObject target, Func<Node, Node, float> heuristic, int gCostLimit = 20, int nodeCountLimit = 100)
    {
        Node targetNode = new(target.transform.position.x, target.transform.position.y, 0, 0, null);
        Node startNode = new(origin.transform.position.x, origin.transform.position.y, 0, 0, null);

        if (!targetNode.IsWalkableForGameObject(origin) || startNode == targetNode)
        {
            return new List<UnnormalizedVector3> { new() };
        }

        List<Node> openSet = new();
        HashSet<Node> closedSet = new();
        Dictionary<(float, float), Node> nodeLookup = new();

        openSet.Add(startNode);
        nodeLookup[(startNode.X, startNode.Y)] = startNode;

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode?.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode.Equals(targetNode) || openSet.Count > nodeCountLimit)
            {
                targetNode = currentNode;
                return RetracePath(startNode, targetNode);
            }

            foreach (var direction in new (float, float)[]
            {
                (0.5f, 0f),
                (-0.5f, 0f),
                (0f, 0.5f),
                (0f, -0.5f),

                (0.5f, 0.5f),
                (-0.5f, 0.5f),
                (0.5f, -0.5f),
                (-0.5f, -0.5f)
            })
            {
                float newX = currentNode.X + direction.Item1;
                float newY = currentNode.Y + direction.Item2;

                if (!nodeLookup.TryGetValue((newX, newY), out Node neighbour))
                {
                    neighbour = new Node(newX, newY, float.MaxValue, 0, currentNode);
                    nodeLookup[(newX, newY)] = neighbour;
                }

                if (!neighbour.IsWalkableForGameObject(origin) || closedSet.Contains(neighbour))
                {
                    continue;
                }

                float newMovementCostToNeighbour = currentNode.gCost + heuristic(currentNode, neighbour);

                if (newMovementCostToNeighbour < neighbour.gCost)
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = heuristic(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour) && neighbour.gCost < gCostLimit)
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        return new List<UnnormalizedVector3> { new() };
    }

    private static List<UnnormalizedVector3> RetracePath(Node startNode, Node endNode)
    {
        List<UnnormalizedVector3> path = new();
        Node currentNode = endNode;
        while (!currentNode.Equals(startNode))
        {
            Debug.DrawLine(new UnnormalizedVector3(currentNode.X, currentNode.Y), new UnnormalizedVector3(currentNode.parent.X, currentNode.parent.Y), Color.green, 1);
            path.Add(new UnnormalizedVector3(currentNode.X - currentNode.parent.X, currentNode.Y - currentNode.parent.Y, 0));
            currentNode = currentNode.parent;
        }
        path.Reverse();
        if (path.Count > 0)
        {
            return path;
        }
        else
        {
            return new List<UnnormalizedVector3> { new() };
        }
    }
    private static float diagonalMultiplier = MathF.Sqrt(2);
    private static float GetDistance(Node nodeA, Node nodeB)
    {
        bool isDiagonal = nodeA.X != nodeB.X && nodeA.Y != nodeB.Y;
        float dstX = Mathf.Abs(nodeA.X - nodeB.X);
        float dstY = Mathf.Abs(nodeA.Y - nodeB.Y);
        if (isDiagonal)
        {
            return Mathf.Sqrt(dstX * dstX + dstY * dstY) * (diagonalMultiplier);
        }
        else
        {
            return Mathf.Sqrt(dstX * dstX + dstY * dstY) * (1);
        }
    }


}

