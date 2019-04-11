using Kelson.Math.Vectors;
using static Kelson.Physics.Unit;

namespace Kelson.Physics
{    
    public class Scalar2 : vect2<Scalar, Scalar2, ScalarOps<Scalar2>>, ICreatable<Scalar, Scalar2>
    {
        public Scalar2() : base((0, One), (0, One)) { }

        public Scalar2 CreateWith(params Scalar[] values) => new Scalar2(values[0], values[1]);

        public Scalar2(Scalar x, Scalar y) : base(x, y) { }
    }

    public class Scalar3 : vect3<Scalar, Scalar3, ScalarOps<Scalar3>>, ICreatable<Scalar, Scalar3>
    {
        public Scalar3() : base((0, One), (0, One), (0, One)) { }

        public Scalar3 CreateWith(params Scalar[] values) => new Scalar3(values[0], values[1], values[2]);

        public Scalar3(Scalar x, Scalar y, Scalar z) : base(x, y, z) { }
    }
}
