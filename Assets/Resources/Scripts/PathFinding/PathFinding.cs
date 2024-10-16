using System;
using System.Collections.Generic;
using UnityEngine;


public static class PathFinding
{
    public static List<UnnormalizedVector3> FindPathAwayFromTarget(Vector3 origin, Vector3 target, int gCostLimit = 20, int nodeCountLimit = 100)
    {
        return FindPath(origin, target, (A, B) => -GetDistance(A, B), gCostLimit, nodeCountLimit);
    }
    public static List<UnnormalizedVector3> FindPathTowardsTarget(Vector3 origin, Vector3 target, int gCostLimit = 20, int nodeCountLimit = 100)
    {
        return FindPath(origin, target, GetDistance, gCostLimit, nodeCountLimit);
    }
    public static List<UnnormalizedVector3> FindPath(Vector3 origin, Vector3 target, Func<Node, Node, float> heuristic, int gCostLimit = 20, int nodeCountLimit = 100)
    {
        Node targetNode = new(target.x, target.y, 0, 0, null);
        Node startNode = new(origin.x, origin.y, 0, 0, null);
        if (!targetNode.IsWalkable || startNode == targetNode)
        {
            return new List<UnnormalizedVector3> { new() };
        }
        List<Node> openSet = new();
        HashSet<Node> closedSet = new();
        openSet.Add(startNode);
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
                targetNode.parent = currentNode.parent;
                targetNode.neighbours = currentNode.neighbours;
                return RetracePath(startNode, targetNode);
            }
            currentNode.neighbours = new List<Node>
            {
                new(currentNode.X + 0.5f, currentNode.Y,0, 0, currentNode),
                new(currentNode.X - 0.5f, currentNode.Y  ,0, 0, currentNode),
                new(currentNode.X, currentNode.Y + 0.5f, 0, 0, currentNode),
                new(currentNode.X, currentNode.Y - 0.5f, 0, 0, currentNode),
                new(currentNode.X +0.5f, currentNode.Y + 0.5f, 0, 0, currentNode),
                new(currentNode.X - 0.5f, currentNode.Y + 0.5f, 0, 0, currentNode),
                new(currentNode.X + 0.5f, currentNode.Y - 0.5f, 0, 0, currentNode),
                new(currentNode.X - 0.5f, currentNode.Y - 0.5f, 0, 0, currentNode)
            };
            foreach (Node neighbour in currentNode.neighbours)
            {
                if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                {
                    continue;
                }
                float newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = (int)newMovementCostToNeighbour;
                    neighbour.hCost = (int)heuristic(neighbour, targetNode);
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
    private static float GetDistance(Node nodeA, Node nodeB)
    {
        float dstX = Mathf.Abs(nodeA.X - nodeB.X);
        float dstY = Mathf.Abs(nodeA.Y - nodeB.Y);
        return Mathf.Sqrt(dstX * dstX + dstY * dstY);
    }


}

