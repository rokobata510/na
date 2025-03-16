using System;
using UnityEngine;

public class NodeVector3 : AOverridenVector3
{
    public static readonly float lengthOfOneStep = 0.5f;
    public override float X
    {
        get
        {
            return x;
        }

        protected set => x = Math.Sign(value) * lengthOfOneStep;
    }

    public override float Y
    {
        get => y;
        protected set => y = Math.Sign(value) * lengthOfOneStep;
    }

    public override float Z
    {
        get => z;
        protected set => z = Math.Sign(value) * lengthOfOneStep;
    }

    public override Vector3 Vector
    {
        get => vector;
        set
        {
            vector = new(Math.Sign(value.x) * lengthOfOneStep, Math.Sign(value.y) * lengthOfOneStep, Math.Sign(value.z) * lengthOfOneStep);
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }
    }

    public NodeVector3(Vector3 v) : base(v) { }

    public NodeVector3(float x, float y, float z) : base(x, y, z) { }


    public NodeVector3(float x, float y) : base(x, y) { }
    public NodeVector3() : base() { }



    public static implicit operator NodeVector3(Vector3 v) => new(v);
    public static implicit operator NodeVector3(Vector2 v) => new(v);
    public static explicit operator NodeVector3(NormalizedVector3 v) => new(v.Vector);
    public static explicit operator NodeVector3(UnnormalizedVector3 v) => new(v.Vector);
}

