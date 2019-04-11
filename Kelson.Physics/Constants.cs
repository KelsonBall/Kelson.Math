using static Kelson.Physics.Unit;

namespace Kelson.Physics
{
    public static class Constants
    {
        public static readonly Scalar GRAV = (6.67408e-11, Volume / (Kilograms * Seconds * Seconds));

        public static class Earth
        {
            public static readonly Scalar MASS = (5.9722e24, Kilograms);

            public static readonly Scalar RADIUS = (6.378137e6, Meters);

            public static readonly Scalar SURFACE_GRAVITY = (9.80, Acceleration);
        }

        public static class Moon
        {
            public static readonly Scalar MASS = (7.34767309e22, Kilograms);

            public static readonly Scalar RADIUS = (1.7381e6, Meters);

            public static readonly Scalar SURFACE_GRAVITY = (1.62, Acceleration);

            public static readonly Scalar EARTH_DISTANCE = (3.844e8, Meters);
        }
    }
}
