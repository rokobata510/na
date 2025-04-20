using System;
using UnityEngine;

public abstract class AOverridenVector3
{
    protected float x = 0;
    public virtual float X
    {
        get => x;
        protected set => x = value;
    }

    protected float y = 0;
    public virtual float Y
    {
        get => y;
        protected set => y = value;
    }

    protected float z = 0;
    public virtual float Z
    {
        get => z;
        protected set => z = value;
    }

    protected Vector3 vector;
    public virtual Vector3 Vector
    {
        get => vector;
        set
        {
            x = value.x;
            y = value.y;
            z = value.z;
            vector = value;
        }
    }

    public float Magnitude => vector.magnitude;

    public NormalizedVector3 Normalized => new(vector.normalized);
    public override string ToString()
    {
        return Vector.ToString();
    }

    protected AOverridenVector3(Vector3 vector)
    {

        Vector = vector;
        X = vector.x;
        Y = vector.y;
        Z = vector.z;
    }

    protected AOverridenVector3(float x, float y, float z) : this(new Vector3(x, y, z)) { }

    protected AOverridenVector3(float x, float y) : this(x, y, 0) { }

    protected AOverridenVector3() : this(0, 0, 0) { }

    public UnnormalizedVector3 Round(int decimalPoints = 0) => decimalPoints switch
    {
        < 0 => throw new ArgumentException("decimalPoints must be greater than or equal to 0"),
        0 => new ((int)x,(int)y),
        > 15 => throw new ArgumentException("decimalPoints must be less than or equal to 15"),
        _ => new UnnormalizedVector3((float)Math.Round(X, decimalPoints), (float)Math.Round(Y, decimalPoints), (float)Math.Round(Z, decimalPoints))
    };

    public override bool Equals(object obj) =>
        obj is UnnormalizedVector3 vector &&
        X == vector.X &&
        Y == vector.Y &&
        Z == vector.Z;

    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(X);
        hash.Add(Y);
        hash.Add(Z);
        hash.Add(Vector);
        hash.Add(Magnitude);
        hash.Add(Normalized);
        return hash.ToHashCode();
    }

    public static implicit operator Vector3(AOverridenVector3 v) => v.Vector;

    public static implicit operator Vector2(AOverridenVector3 v) => new(v.X, v.Y);

    public static UnnormalizedVector3 operator *(AOverridenVector3 a, float b) => new(a.X * b, a.Y * b, a.Z * b);

    public static UnnormalizedVector3 operator *(float b, AOverridenVector3 a) => new(a.X * b, a.Y * b, a.Z * b);

    public static UnnormalizedVector3 operator *(Vector3 a, AOverridenVector3 b) => new(a.x * b.X, a.y * b.Y, a.z * b.Z);

    public static UnnormalizedVector3 operator *(AOverridenVector3 a, Vector3 b) => new(a.X * b.x, a.Y * b.y, a.Z * b.z);

    public static UnnormalizedVector3 operator +(AOverridenVector3 a, AOverridenVector3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static UnnormalizedVector3 operator -(AOverridenVector3 a, AOverridenVector3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public static UnnormalizedVector3 operator /(AOverridenVector3 a, float b) => new(a.X / b, a.Y / b, a.Z / b);

    public static UnnormalizedVector3 operator -(AOverridenVector3 a) => new(-a.X, -a.Y, -a.Z);


    public static bool operator !=(AOverridenVector3 a, AOverridenVector3 b) => !(a == b);
    private const float Epsilon = 0.01f;

    public static bool operator ==(AOverridenVector3 a, AOverridenVector3 b) => Math.Abs(a.X - b.X) < Epsilon && Math.Abs(a.Y - b.Y) < Epsilon && Math.Abs(a.Z - b.Z) < Epsilon;
}

