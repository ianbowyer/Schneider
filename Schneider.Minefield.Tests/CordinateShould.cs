namespace Schneider.Minefield.Tests
{
    using FluentAssertions;

    public class CordinateShould
    {
        [Fact]
        public void MatchEqual()
        {
            // Arrange
            var cord1 = new Coordinate(8, 8);
            var cord2 = new Coordinate(8, 8);

            // Act
            var actual = cord1.Equals(cord2);

            // Actual
            actual.Should().BeTrue();
        }

        [Theory]
        [InlineData(7, 8)]
        [InlineData(8, 7)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void MatchNotEqual(int x, int y)
        {
            // Arrange
            var cord1 = new Coordinate(8, 8);
            var cord2 = new Coordinate(x, y);

            // Act
            var actual = cord1.Equals(cord2);

            // Actual
            actual.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseIfNull()
        {
            // Arrange
            var cord1 = new Coordinate(8, 8);

            // Act
            var actual = cord1.Equals(null);

            // Actual
            actual.Should().BeFalse();
        }

        [Fact]
        public void FindUsingAny()
        {
            // Arrange
            var cords = new List<Coordinate>
            {
                new(1,2),
                new(1,3),
                new(1,4),
                new(1,5),
            };

            var tofind = new Coordinate(1, 3);

            // Act
            var actual = cords.Contains(tofind);

            // Actual
            actual.Should().BeTrue();
        }
    }
}