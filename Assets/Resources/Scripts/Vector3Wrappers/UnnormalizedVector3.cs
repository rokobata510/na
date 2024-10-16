using System;
using UnityEngine;

public class UnnormalizedVector3 : AOverridenVector3
{
    public UnnormalizedVector3 RoundedToHalves => new((float)(Math.Round(X * 2) / 2), (float)(Math.Round(Y * 2) / 2));
    public UnnormalizedVector3(Vector3 vector) : base(vector) { }
    public UnnormalizedVector3(float x, float y, float z) : base(x, y, z) { }
    public UnnormalizedVector3(float x, float y) : base(x, y) { }
    public UnnormalizedVector3() : base() { }

    public static implicit operator UnnormalizedVector3(Vector3 v) => new(v);
    public static implicit operator UnnormalizedVector3(Vector2 v) => new(v);
    public static explicit operator UnnormalizedVector3(NormalizedVector3 v) => new(v.Vector);
    public static explicit operator UnnormalizedVector3(NodeVector3 v) => new(v.Vector);

}
