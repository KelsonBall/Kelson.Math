using FluentAssertions;
using Kelson.Math.Matricies;
using Xunit;

namespace Kelson.Math.Tests
{
    public class TestMat3
    {
        [Theory]
        [InlineData(
            new double[] 
            {
                1, 0, 5,
                2, 1, 6,
                3, 4, 0
            }, 
            new double[] 
            {
                -24,  18, 5,
                 20, -15,-4,
                -5,   4,  1
            })]
        [InlineData(
            new double[]
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            },
            new double[]
            {
                -3, 6, -3,
                6, -12, 6,
                -3, 6, -3,
            })]
        public void DetermineAdjugateComponents(double[] a, double[] result)
        {
            var matA = new mat3(a);
            var expected = new mat3(result);
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    matA.AdjugateComponent(r, c).Should().BeApproximately(expected[r, c], double.Epsilon);
        }
    }
}
