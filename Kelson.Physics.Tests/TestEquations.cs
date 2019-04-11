using Xunit;
using static Kelson.Physics.Unit;
using static Kelson.Physics.Equations;
using static Kelson.Physics.Constants;
using FluentAssertions;

namespace Kelson.Physics.Tests
{
    public class TestEquations
    {
        [Fact]
        public void EarthMoonGravForce()
        {
            var force = GraviatationalForce(Earth.MASS, Moon.MASS, Moon.EARTH_DISTANCE);
            force.Value.Should().BeApproximately(2e20, 1e19);
            force.Units.Should().BeEquivalentTo(Force);
        }

        [Fact]
        public void EarthSurfaceGravity()
        {
            var accel = GravitationalAcceleration(Earth.MASS, Earth.RADIUS);
            accel.Value.Should().BeApproximately(Earth.SURFACE_GRAVITY.Value, 0.1);
            accel.Units.Should().BeEquivalentTo(Acceleration);
        }

        [Fact]
        public void EarthSurfaceGravityFromMiles()
        {
            var accel = GravitationalAcceleration(Earth.MASS, (3958.8, Miles));
            accel.Value.Should().BeApproximately(Earth.SURFACE_GRAVITY.Value, 0.1);
            accel.Units.Should().BeEquivalentTo(Acceleration);
        }

        [Fact]
        public void MoonSurfaceGravity()
        {
            var accel = GravitationalAcceleration(Moon.MASS, Moon.RADIUS);
            accel.Value.Should().BeApproximately(Moon.SURFACE_GRAVITY.Value, 0.1);
            accel.Units.Should().BeEquivalentTo(Acceleration);
        }
    }
}
