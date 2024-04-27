using System;

namespace Blackguard.Utilities;

public struct Point {
    public int X;
    public int Y;

    public Point(int x, int y) {
        X = x;
        Y = y;
    }

    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator +(Point a, int b) => new(a.X + b, a.Y + b);

    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator -(Point a, int b) => new(a.X - b, a.Y - b);

    public static Point operator *(Point a, Point b) => new(a.X * b.X, a.Y * b.Y);
    public static Point operator *(Point a, float b) => new((int)(a.X * b), (int)(a.Y * b));

    public static Point operator /(Point a, Point b) => new((int)((float)a.X / b.X), (int)((float)a.Y / b.Y));
    public static Point operator /(Point a, float b) => new((int)(a.X / b), (int)(a.Y / b));

    public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Point a, Point b) => a.X != b.X || a.Y != b.Y;

    public readonly override bool Equals(object? obj) {
        if (obj is not Point b)
            return false;
        else
            return this == b;
    }

    public readonly override int GetHashCode() {
        return HashCode.Combine(X, Y);
    }

    public readonly override string ToString() {
        return $"({X}, {Y})";
    }
}
