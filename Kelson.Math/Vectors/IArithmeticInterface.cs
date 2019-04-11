namespace Kelson.Math.Vectors
{
    public interface IArithmeticInterface<T, TVec> where TVec : ICreatable<T, TVec>, new()
    {
        TVec @new (params T[] v);
        T zero    ();
        T one     ();
        T multiply(T a, T b);
        T divide  (T a, T b);
        T add     (T a, T b);
        T subtract(T a, T b);
        T acos    (T a);
        T asin    (T a);
        T atan    (T a);
        T atan2   (T a, T b);
        T pow     (T a, T b);
        T minus   (T a);
        T sqrt    (T a);        
    }
}
