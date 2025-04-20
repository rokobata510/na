using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    private static readonly float raycastBoxSides = 0.5f;
    private static readonly float raycastBoxDiagonal = raycastBoxSides * MathF.Sqrt(8);
    protected UnnormalizedVector3 vector = new();
    public float X => vector.X;
    public float Y => vector.Y;

    public List<GameObject> collidesWith = new();

    public Node parent;
    public List<Node> neighbours;
    public float gCost;
    public float hCost;
    public float FCost => gCost + hCost;


    public Node(Vector2 vector, float gCost, float hCost, Node parent)
    {
        this.vector = new UnnormalizedVector3((float)(Math.Round(vector.x * 2) / 2), (float)(Math.Round(vector.y * 2) / 2));
        this.gCost = gCost;
        this.hCost = hCost;
        this.parent = parent;
        collidesWith = Physics2D.OverlapBoxAll(new Vector2(X, Y), new Vector2(raycastBoxSides, raycastBoxSides), 0)
            .Select(hit => hit.gameObject)
            .ToList();
    }
    public Node(float x, float y, float gCost, float hCost, Node parent) : this(new Vector2(x, y), gCost, hCost, parent) { }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Node otherNode = (Node)obj;
        return Vector2.Distance(vector, otherNode.vector) < 0.1f;
    }

    public bool IsWalkableForGameObject(GameObject gameObject)
    {
        bool isWalkable = collidesWith.All(collision => Physics2D.GetIgnoreLayerCollision(gameObject.layer, collision.layer) || collision == gameObject || collision.layer == LayerMask.NameToLayer("Player"));
        if (isWalkable)
            Debug.DrawRay(new Vector2(X - raycastBoxDiagonal / 2, Y - raycastBoxDiagonal / 2), new Vector2(raycastBoxSides, raycastBoxSides), Color.green);
        else
            Debug.DrawRay(new Vector2(X - raycastBoxDiagonal / 2, Y - raycastBoxDiagonal / 2), new Vector2(raycastBoxSides, raycastBoxSides), Color.red);
        return isWalkable;
    }
    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }


}

