using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Math.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct vecd2
    {
        public readonly double X;
        public readonly double Y;

        public vecd2(double x, double y) =>
            (X, Y) = (x, y);

        public vecd2(vecd2Ref vec) =>
            (X, Y) = vec;

        public void Deconstruct(out double x, out double y) =>
            (x, y) = (X, Y);

        public vecd2Ref AsRefStruct() => new vecd2Ref(in this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => System.Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in vecd2 other) => (X * other.X) + (Y * other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd2 Add(in vecd2 other) => new vecd2(X + other.X, Y + other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd2 Sub(in vecd2 other) => new vecd2(X - other.X, Y - other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd2 Scale(double scalar) => new vecd2(X * scalar, Y * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd2 Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in vecd2 other) => System.Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in vecd2 other) => System.Math.Atan2(other.Y, other.X) - System.Math.Atan2(Y, X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd2(in (double x, double y) t) => new vecd2(t.x, t.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd2(in (float x, float y) t) => new vecd2(t.x, t.y);        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd2(double[] t)
        {
            if (t.Length != 2)
                throw new InvalidOperationException("Array length must be 2 to cast to vecd2");
            return new vecd2(t[0], t[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd2(float[] t)
        {
            if (t.Length != 2)
                throw new InvalidOperationException("Array length must be 2 to cast to vecd2");
            return new vecd2(t[0], t[1]);
        }

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (vecd2* data = &this)
                return new ReadOnlySpan<float>(data, 2);
        }

        public static vecd2 operator -(vecd2 a, vecd2 b) => a.Sub(b);
        public static vecd2 operator -(vecd2 a) => a.Scale(-1);
        public static vecd2 operator +(vecd2 a, vecd2 b) => a.Add(b);
        public static double operator *(vecd2 a, vecd2 b) => a.Dot(b);
        public static vecd2 operator *(vecd2 a, double s) => a.Scale(s);
        public static vecd2 operator *(double s, vecd2 b) => b.Scale(s);
        public static vecd2 operator /(vecd2 a, double s) => a.Scale(1 / s);

        public override string ToString() => $"<{X},{Y}>";        
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct vecd2Ref
    {
        public readonly double X;
        public readonly double Y;

        public vecd2Ref(double x, double y) =>
            (X, Y) = (x, y);

        public vecd2Ref(in vecd2 vec) =>
            (X, Y) = vec;

        public void Deconstruct(out double x, out double y) =>
            (x, y) = (X, Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => System.Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in vecd2Ref other) => (X * other.X) + (Y * other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd2Ref Add(in vecd2Ref other) => new vecd2Ref(X + other.X, Y + other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd2Ref Sub(in vecd2Ref other) => new vecd2Ref(X - other.X, Y - other.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd2Ref Scale(double scalar) => new vecd2Ref(X * scalar, Y * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd2Ref Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in vecd2Ref other) => System.Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in vecd2Ref other) => System.Math.Atan2(other.Y, other.X) - System.Math.Atan2(Y, X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd2Ref(in (double x, double y) t) => new vecd2Ref(t.x, t.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd2Ref(in (float x, float y) t) => new vecd2Ref(t.x, t.y);

        public static implicit operator vecd2(vecd2Ref vec) => new vecd2(vec);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd2Ref(double[] t)
        {
            if (t.Length != 2)
                throw new InvalidOperationException("Array length must be 2 to cast to vecd2Ref");
            return new vecd2Ref(t[0], t[1]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd2Ref(float[] t)
        {
            if (t.Length != 2)
                throw new InvalidOperationException("Array length must be 2 to cast to vecd2Ref");
            return new vecd2Ref(t[0], t[1]);
        }

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (vecd2Ref* data = &this)
                return new ReadOnlySpan<float>(data, 2);
        }

        public static vecd2Ref operator -(vecd2Ref a, vecd2Ref b) => a.Sub(b);
        public static vecd2Ref operator -(vecd2Ref a) => a.Scale(-1);
        public static vecd2Ref operator +(vecd2Ref a, vecd2Ref b) => a.Add(b);
        public static double operator *(vecd2Ref a, vecd2Ref b) => a.Dot(b);
        public static vecd2Ref operator *(vecd2Ref a, double s) => a.Scale(s);
        public static vecd2Ref operator *(double s, vecd2Ref b) => b.Scale(s);
        public static vecd2Ref operator /(vecd2Ref a, double s) => a.Scale(1 / s);

        public override string ToString() => $"<{X},{Y}>";
    }
}
