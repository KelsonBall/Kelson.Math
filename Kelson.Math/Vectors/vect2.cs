namespace Kelson.Math.Vectors
{
    public abstract class vect2<T, TVec, TOps> where TVec : vect2<T, TVec, TOps>, ICreatable<T, TVec>, new() where TOps : IArithmeticInterface<T, TVec>, new()
    {       
        public readonly T X;
        public readonly T Y;

        public static readonly TOps Ops = new TOps();
        
        protected vect2(T x, T y)
        {
            (X, Y) = (x, y);            
        }

        public void Deconstruct(out T x, out T y) =>
            (x, y) = (X, Y);
        
        public T MagnitudeSquared() => Dot((TVec)this);

        
        public T Magnitude() => Ops.sqrt(Dot((TVec)this));

        
        public T Dot(in TVec other) => Ops.add(Ops.multiply(X, other.X), Ops.multiply(Y, other.Y));

        
        public TVec Add(in TVec other) => Ops.@new(Ops.add(X, other.X), Ops.add(Y, other.Y));

        
        public TVec Sub(in TVec other) => Ops.@new(Ops.subtract(X, other.X), Ops.subtract(Y, other.Y));

        
        public TVec Scale(T scalar) => Ops.@new(Ops.multiply(X, scalar), Ops.multiply(Y, scalar));

        
        public TVec Unit() => Scale(Ops.divide(Ops.one(), Magnitude()));

        
        public T AngularMagnitude(in TVec other) => Ops.acos(Ops.divide(Dot(other), Ops.multiply(Magnitude(), other.Magnitude())));

        
        public T Angle(in TVec other) => Ops.subtract(Ops.atan2(other.Y, other.X), Ops.atan2(Y, X));       

        public static TVec operator -(vect2<T, TVec, TOps> a, vect2<T, TVec, TOps> b) => a.Sub((TVec)b);
        public static TVec operator -(vect2<T, TVec, TOps> a) => a.Scale(Ops.minus(Ops.one()));
        public static TVec operator +(vect2<T, TVec, TOps> a, vect2<T, TVec, TOps> b) => a.Add((TVec)b);
        public static T operator *(vect2<T, TVec, TOps> a, vect2<T, TVec, TOps> b) => a.Dot((TVec)b);
        public static TVec operator *(vect2<T, TVec, TOps> a, T s) => a.Scale(s);
        public static TVec operator *(T s, vect2<T, TVec, TOps> b) => b.Scale(s);
        public static TVec operator /(vect2<T, TVec, TOps> a, T s) => a.Scale(Ops.divide(Ops.one(), s));

        public static implicit operator vect2<T, TVec, TOps>((T a, T b) ab) => Ops.@new(ab.a, ab.b);

        public override string ToString() => $"<{X}, {Y}>";
    }
}
