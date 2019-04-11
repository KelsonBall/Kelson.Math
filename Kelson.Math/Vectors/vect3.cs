namespace Kelson.Math.Vectors
{
    public abstract class vect3<T, TVec, TOps> where TVec : vect3<T, TVec, TOps>, ICreatable<T, TVec>, new() where TOps : IArithmeticInterface<T, TVec>, new()
    {
        public readonly T X;
        public readonly T Y;
        public readonly T Z;

        public static readonly TOps Ops = new TOps();

        protected vect3(T x, T y, T z) =>        
            (X, Y, Z) = (x, y, z);        

        public void Deconstruct(out T x, out T y, out T z) =>
            (x, y, z) = (X, Y, Z);

        public T MagnitudeSquared() => Dot((TVec)this);


        public T Magnitude() => Ops.sqrt(Dot((TVec)this));


        public T Dot(in TVec other) => Ops.add(Ops.add(Ops.multiply(X, other.X), Ops.multiply(Y, other.Y)), Ops.multiply(Z, other.Z));


        public TVec Add(in TVec other) => Ops.@new(Ops.add(X, other.X), Ops.add(Y, other.Y), Ops.add(Z, other.Z));


        public TVec Sub(in TVec other) => Ops.@new(Ops.subtract(X, other.X), Ops.subtract(Y, other.Y), Ops.subtract(Z, other.Z));


        public TVec Scale(T scalar) => Ops.@new(Ops.multiply(X, scalar), Ops.multiply(Y, scalar), Ops.multiply(Z, scalar));


        public TVec Unit() => Scale(Ops.divide(Ops.one(), Magnitude()));        

        public static TVec operator -(vect3<T, TVec, TOps> a, vect3<T, TVec, TOps> b) => a.Sub((TVec)b);
        public static TVec operator -(vect3<T, TVec, TOps> a) => a.Scale(Ops.minus(Ops.one()));
        public static TVec operator +(vect3<T, TVec, TOps> a, vect3<T, TVec, TOps> b) => a.Add((TVec)b);
        public static T operator *(vect3<T, TVec, TOps> a, vect3<T, TVec, TOps> b) => a.Dot((TVec)b);
        public static TVec operator *(vect3<T, TVec, TOps> a, T s) => a.Scale(s);
        public static TVec operator *(T s, vect3<T, TVec, TOps> b) => b.Scale(s);
        public static TVec operator /(vect3<T, TVec, TOps> a, T s) => a.Scale(Ops.divide(Ops.one(), s));

        public static implicit operator vect3<T, TVec, TOps>((T a, T b, T c) abc) => Ops.@new(abc.a, abc.b, abc.c);

        public override string ToString() => $"<{X}, {Y}, {Z}>";
    }
}
