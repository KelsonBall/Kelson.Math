using Xunit;
using static Kelson.Physics.Unit;
using FluentAssertions;

namespace Kelson.Physics.Tests
{
    public class TestArithmetic
    {
        const double within_reason = 0.0001;
        const double almost_exactly = double.Epsilon;

        [Fact]
        public void AcclerationOverTime()
        {
            Scalar a = (9.8, Acceleration); // falling (on earth)            
            var v = a * (10.0, Seconds); // falling for 10 seconds

            v.Value.Should().BeApproximately(98, almost_exactly);
            v.Units.Should().BeEquivalentTo(Velocity);
        }

        [Fact]
        public void ZeroTo60()
        {
            Scalar speed = (60, Miles / Hours);

            speed.Value
                .Should().BeApproximately(26.8224, within_reason); // m/s normalized

            Scalar time = (3.5, Seconds);
            var accel = speed / time;
            accel.Value.Should().BeApproximately(7.6635, within_reason);
            accel.Units.Should().BeEquivalentTo(Acceleration);

            (Miles / Hours).FromSI(accel.Value)
                .Should().BeApproximately(17.142857, within_reason); // mph per second
        }

        [Fact]
        public void MixedTimeUnitAcceleration()
        {
            Scalar accel = (5, Miles / Hours / Seconds); // 5 mph per second
            var speed = accel * (5, Seconds); // for 5 seconds
            Scalar expected = (25, Miles / Hours); // 5 * 5 mph

            speed.Value.Should().BeApproximately(expected.Value, within_reason);
            speed.Units.Should().BeEquivalentTo(Velocity);
        }
    }
}
