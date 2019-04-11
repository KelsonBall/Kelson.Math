using static Kelson.Physics.Unit;
using static Kelson.Physics.Constants;
namespace Kelson.Physics
{
    public static class Equations
    {
        public static Scalar GraviatationalForce(Scalar mass1, Scalar mass2, Scalar distance)
        {
            Assert(mass1.Units, Kilograms);
            Assert(mass2.Units, Kilograms);
            Assert(distance.Units, Meters);            
            return GRAV * ((mass1 * mass2) / (distance * distance));
        }

        public static Scalar GravitationalAcceleration(Scalar mass, Scalar distance)
        {
            Assert(mass.Units, Kilograms);
            Assert(distance.Units, Meters);
            return GRAV * (mass / (distance * distance));
        }
    }
}
