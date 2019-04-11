using Kelson.Math.Vectors;
using static Kelson.Physics.Unit;

namespace Kelson.Physics
{
    public class ScalarOps<TVec> : IArithmeticInterface<Scalar, TVec> where TVec : ICreatable<Scalar, TVec>, new()
    {
        public Scalar acos(Scalar a) => (System.Math.Acos(a.Value), a.Units);        

        public Scalar add(Scalar a, Scalar b) => a + b;        

        public Scalar asin(Scalar a)=> (System.Math.Asin(a.Value), a.Units);        

        public Scalar atan(Scalar a) => (System.Math.Atan(a.Value), a.Units);

        public Scalar atan2(Scalar a, Scalar b)
        {
            Assert(a.Units, b.Units);
            return (System.Math.Atan2(a.Value, b.Value), a.Units.CoreUnits());
        }

        public Scalar divide(Scalar a, Scalar b) => a / b;

        public Scalar minus(Scalar a) => -a;

        public Scalar multiply(Scalar a, Scalar b) => a * b;

        public TVec @new(params Scalar[] s) => new TVec().CreateWith(s);
        
        public Scalar one() => (1, One);

        public Scalar pow(Scalar a, Scalar b)
        {
            Assert(a.Units, b.Units);
            return (System.Math.Pow(a.Value, b.Value), a.Units.CoreUnits());
        }

        public Scalar sqrt(Scalar a) => (System.Math.Sqrt(a.Value), a.Units.CoreUnits());

        public Scalar subtract(Scalar a, Scalar b) => a - b;

        public Scalar zero() => (0, One);
    }
}
