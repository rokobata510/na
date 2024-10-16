using System;
using UnityEngine;

public class NormalizedVector3 : AOverridenVector3
{
    public override Vector3 Vector
    {
        get => vector.normalized;
        set
        {
            vector = value.normalized;
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }
    }

    public NormalizedVector3(Vector3 v) => Vector = v.normalized;

    public NormalizedVector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }


    public NormalizedVector3(float x, float y) => new NormalizedVector3(x, y, 0);
    public NormalizedVector3() => new NormalizedVector3(0, 0, 0);

    public static implicit operator NormalizedVector3(Vector3 v) => new(v);
    public static implicit operator NormalizedVector3(Vector2 v) => new(v);
    public static explicit operator NormalizedVector3(NodeVector3 v) => new(v.Vector);
    public static explicit operator NormalizedVector3(UnnormalizedVector3 v) => new(v.Vector);
}

