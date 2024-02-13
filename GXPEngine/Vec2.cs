using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    // TODO: Implement Length, Normalize, Normalized, SetXY methods (see Assignment 1)

    public float Length()
    {
        // TODO: return the vector length
        return Mathf.Sqrt(x * x + y * y);
    }
    public Vec2 Normalized()
    {
        return new Vec2(x / Length(), y / Length());
    }

    public void SetXY(float eX, float eY)
    {
        x = eX;
        y = eY;
    }
    public void Normalize()
    {
        if (x == 0 && y == 0) return;
        x = x / Length();
        y = y / Length();
    }

    // TODO: Implement subtract, scale operators

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }
    public static Vec2 operator *(Vec2 left, float scalar)
    {
        return new Vec2(left.x * scalar, left.y * scalar);
    }
    public static Vec2 operator *(float scalar, Vec2 right)
    {
        return new Vec2(scalar * right.x, scalar * right.y);
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }
}
