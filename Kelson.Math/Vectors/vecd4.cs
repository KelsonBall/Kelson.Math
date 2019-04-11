using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Math.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct vecd4
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public readonly double W;

        public vecd4(double x, double y, double z, double w) =>
            (X, Y, Z, W) = (x, y, z, w);

        public vecd4(vecd4Ref vec) =>
            (X, Y, Z, W) = vec;

        public void Deconstruct(out double x, out double y, out double z, out double w) =>        
            (x, y, z, w) = (X, Y, Z, W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => System.Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in vecd4 other) => (X * other.X) + (Y * other.Y) + (Z * other.Z) + (W * other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd4 Add(in vecd4 other) => new vecd4(X + other.X, Y + other.Y, Z + other.Z, W + other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd4 Sub(in vecd4 other) => new vecd4(X - other.X, Y - other.Y, Z - other.Z, W - other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd4 Scale(double scalar) => new vecd4(X * scalar, Y * scalar, Z * scalar, W * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd4 Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in vecd4 other) => System.Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd4(in (float x, float y, float z, float w) t) => new vecd4(t.x, t.y, t.z, t.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd4(Tuple<double, double, double, double> t) => new vecd4(t.Item1, t.Item2, t.Item3, t.Item4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd4(Tuple<float, float, float, float> t) => new vecd4(t.Item1, t.Item2, t.Item3, t.Item4);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd4(double[] t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException($"Array length must be 4 to cast to {nameof(vecd4)}");
            return new vecd4(t[0], t[1], t[2], t[3]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd4(float[] t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException($"Array length must be 4 to cast to {nameof(vecd4)}");
            return new vecd4(t[0], t[1], t[2], t[3]);
        }

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (vecd4* data = &this)
                return new ReadOnlySpan<float>(data, 4);
        }

        public static vecd4 operator -(vecd4 a, vecd4 b) => a.Sub(b);
        public static vecd4 operator -(vecd4 a) => a.Scale(-1);
        public static vecd4 operator +(vecd4 a, vecd4 b) => a.Add(b);
        public static double operator *(vecd4 a, vecd4 b) => a.Dot(b);
        public static vecd4 operator *(vecd4 a, double s) => a.Scale(s);
        public static vecd4 operator *(double s, vecd4 b) => b.Scale(s);
        public static vecd4 operator /(vecd4 a, double s) => a.Scale(1 / s);

        public override string ToString() => $"<{X},{Y},{Z},{W}>";
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct vecd4Ref
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public readonly double W;

        public vecd4Ref(double x, double y, double z, double w) =>
            (X, Y, Z, W) = (x, y, z, w);

        public void Deconstruct(out double x, out double y, out double z, out double w) =>
            (x, y, z, w) = (X, Y, Z, W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => System.Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in vecd4Ref other) => (X * other.X) + (Y * other.Y) + (Z * other.Z) + (W * other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd4Ref Add(in vecd4Ref other) => new vecd4Ref(X + other.X, Y + other.Y, Z + other.Z, W + other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd4Ref Sub(in vecd4Ref other) => new vecd4Ref(X - other.X, Y - other.Y, Z - other.Z, W - other.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd4Ref Scale(double scalar) => new vecd4Ref(X * scalar, Y * scalar, Z * scalar, W * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd4Ref Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in vecd4Ref other) => System.Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd4Ref(in (float x, float y, float z, float w) t) => new vecd4Ref(t.x, t.y, t.z, t.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd4Ref(double[] t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException($"Array length must be 4 to cast to {nameof(vecd4Ref)}");
            return new vecd4Ref(t[0], t[1], t[2], t[3]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd4Ref(float[] t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException($"Array length must be 4 to cast to {nameof(vecd4Ref)}");
            return new vecd4Ref(t[0], t[1], t[2], t[3]);
        }

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (vecd4Ref* data = &this)
                return new ReadOnlySpan<float>(data, 4);
        }

        public static vecd4Ref operator -(vecd4Ref a, vecd4Ref b) => a.Sub(b);
        public static vecd4Ref operator -(vecd4Ref a) => a.Scale(-1);
        public static vecd4Ref operator +(vecd4Ref a, vecd4Ref b) => a.Add(b);
        public static double operator *(vecd4Ref a, vecd4Ref b) => a.Dot(b);
        public static vecd4Ref operator *(vecd4Ref a, double s) => a.Scale(s);
        public static vecd4Ref operator *(double s, vecd4Ref b) => b.Scale(s);
        public static vecd4Ref operator /(vecd4Ref a, double s) => a.Scale(1 / s);

        public override string ToString() => $"<{X},{Y},{Z},{W}>";
    }
}
