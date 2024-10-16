using System;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private static readonly float RaycastBoxSides = 0.1f;
    private static readonly float RaycastBoxDiagonal = RaycastBoxSides*MathF.Sqrt(8);
    protected UnnormalizedVector3 vector = new();
    public float X => vector.X;
    public float Y => vector.Y;
    public bool isWall = false;
    public bool isEnemy = false;
    public bool IsWalkable => !isWall && !isEnemy;
    public Node parent;
    public List<Node> neighbours;
    public float gCost;
    public float hCost;
    public float FCost => gCost + hCost;


    public Node(Vector2 vector, float gCost, float hCost, Node parent)
    {
        this.vector = new UnnormalizedVector3((float)(Math.Round(vector.x*2)/2), (float)(Math.Round(vector.y * 2) / 2));
        this.gCost = gCost;
        this.hCost = hCost;
        this.parent = parent;
        CheckWalkability();
    }
    public Node(float x, float y, float gCost, float hCost, Node parent) : this(new Vector2(x, y), gCost, hCost, parent) { }
    void CheckWalkability()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(X - RaycastBoxSides, Y + RaycastBoxSides), new Vector2(1f,-1f),RaycastBoxDiagonal);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                isEnemy = true;
            }
            if (hit.collider.gameObject.CompareTag("Walls"))
            {

                isWall = true;
            }
            
        }
        if (IsWalkable == true)
        {
            Debug.DrawLine(new Vector2(X - RaycastBoxSides, Y + RaycastBoxSides), new Vector2(X + RaycastBoxSides, Y - RaycastBoxSides), Color.yellow, 1);
        }
        else
        {
            Debug.DrawLine(new Vector2(X + RaycastBoxSides, Y + RaycastBoxSides), new Vector2(X - RaycastBoxSides, Y - RaycastBoxSides), Color.magenta, 1);
        }
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Node otherNode = (Node)obj;
        return Vector2.Distance(vector, otherNode.vector) < 0.1f;
    }
    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }


}

