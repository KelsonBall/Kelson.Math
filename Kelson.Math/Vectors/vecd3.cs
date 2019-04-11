using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kelson.Math.Vectors
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct vecd3
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public vecd3(double x, double y, double z) =>
            (X, Y, Z) = (x, y, z);

        public vecd3(vecd3Ref vec) =>
            (X, Y, Z) = vec;

        public void Deconstruct(out double x, out double y, out double z) =>
            (x, y, z) = (X, Y, Z);        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => System.Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd3 Cross(in vecd3 other)
            => new vecd3(Y * other.Z - Z * other.Y,
                            Z * other.X - X * other.Z,
                            X * other.Y - Y * other.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in vecd3 other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd3 Add(in vecd3 other) => new vecd3(X + other.X, Y + other.Y, Z + other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd3 Sub(in vecd3 other) => new vecd3(X - other.X, Y - other.Y, Z - other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd3 Scale(double scalar) => new vecd3(X * scalar, Y * scalar, Z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd3 Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in vecd3 other) => System.Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in vecd3 other, in vecd3 normal)
        {
            if (normal.Cross(this).Dot(other) > 0)
                return AngularMagnitude(other);
            else
                return -AngularMagnitude(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd3(in (double x, double y, double z) t) => new vecd3(t.x, t.y, t.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd3(in (float x, float y, float z) t) => new vecd3(t.x, t.y, t.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd3(Tuple<double, double, double> t) => new vecd3(t.Item1, t.Item2, t.Item3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd3(Tuple<float, float, float> t) => new vecd3(t.Item1, t.Item2, t.Item3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd3(double[] t)
        {
            if (t.Length != 3)
                throw new InvalidOperationException($"Array length must be 3 to cast to {nameof(vecd3)}");
            return new vecd3(t[0], t[1], t[2]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd3(float[] t)
        {
            if (t.Length != 3)
                throw new InvalidOperationException($"Array length must be 3 to cast to {nameof(vecd3)}");
            return new vecd3(t[0], t[1], t[2]);
        }

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (vecd3* data = &this)
                return new ReadOnlySpan<float>(data, 3);
        }

        public static vecd3 operator -(vecd3 a, vecd3 b) => a.Sub(b);
        public static vecd3 operator -(vecd3 a) => a.Scale(-1);
        public static vecd3 operator +(vecd3 a, vecd3 b) => a.Add(b);
        public static double operator *(vecd3 a, vecd3 b) => a.Dot(b);
        public static vecd3 operator *(vecd3 a, double s) => a.Scale(s);
        public static vecd3 operator *(double s, vecd3 b) => b.Scale(s);
        public static vecd3 operator /(vecd3 a, double s) => a.Scale(1 / s);

        public override string ToString() => $"<{X},{Y},{Z}>";
    }


    [StructLayout(LayoutKind.Sequential)]
    public readonly ref struct vecd3Ref
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public vecd3Ref(double x, double y, double z) =>
            (X, Y, Z) = (x, y, z);

        public void Deconstruct(out double x, out double y, out double z) =>
            (x, y, z) = (X, Y, Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double MagnitudeSquared() => Dot(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Magnitude() => System.Math.Sqrt(Dot(this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd3Ref Cross(in vecd3Ref other)
            => new vecd3Ref(Y * other.Z - Z * other.Y,
                            Z * other.X - X * other.Z,
                            X * other.Y - Y * other.X);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Dot(in vecd3Ref other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd3Ref Add(in vecd3Ref other) => new vecd3Ref(X + other.X, Y + other.Y, Z + other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd3Ref Sub(in vecd3Ref other) => new vecd3Ref(X - other.X, Y - other.Y, Z - other.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd3Ref Scale(double scalar) => new vecd3Ref(X * scalar, Y * scalar, Z * scalar);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public vecd3Ref Unit() => Scale(1d / Magnitude());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double AngularMagnitude(in vecd3Ref other) => System.Math.Acos(Dot(other) / (Magnitude() * other.Magnitude()));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Angle(in vecd3Ref other, in vecd3Ref normal)
        {
            if (normal.Cross(this).Dot(other) > 0)
                return AngularMagnitude(other);
            else
                return -AngularMagnitude(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd3Ref(in (double x, double y, double z) t) => new vecd3Ref(t.x, t.y, t.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator vecd3Ref(in (float x, float y, float z) t) => new vecd3Ref(t.x, t.y, t.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd3Ref(ReadOnlySpan<double> t)
        {
            if (t.Length != 3)
                throw new InvalidOperationException($"Array length must be 3 to cast to {nameof(vecd3Ref)}");
            return new vecd3Ref(t[0], t[1], t[2]);
        }        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator vecd3Ref(ReadOnlySpan<float> t)
        {
            if (t.Length != 3)
                throw new InvalidOperationException($"Array length must be 3 to cast to {nameof(vecd3Ref)}");
            return new vecd3Ref(t[0], t[1], t[2]);
        }

        public unsafe ReadOnlySpan<float> AsSpan()
        {
            fixed (vecd3Ref* data = &this)
                return new ReadOnlySpan<float>(data, 3);
        }

        public static vecd3Ref operator -(vecd3Ref a, vecd3Ref b) => a.Sub(b);
        public static vecd3Ref operator -(vecd3Ref a) => a.Scale(-1);
        public static vecd3Ref operator +(vecd3Ref a, vecd3Ref b) => a.Add(b);
        public static double operator *(vecd3Ref a, vecd3Ref b) => a.Dot(b);
        public static vecd3Ref operator *(vecd3Ref a, double s) => a.Scale(s);
        public static vecd3Ref operator *(double s, vecd3Ref b) => b.Scale(s);
        public static vecd3Ref operator /(vecd3Ref a, double s) => a.Scale(1 / s);

        public override string ToString() => $"<{X},{Y},{Z}>";
    }
}
