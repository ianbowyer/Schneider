namespace Schneider.Minefield.Tests
{
    using FluentAssertions;

    public class MinefieldShould
    {
        [Theory]
        [InlineData(2, 2, 4)]
        [InlineData(3, 3, 9)]
        [InlineData(4, 4, 16)]
        public void ShouldCalculateTheCorrectMaxSquares(int x, int y, int expected)
        {
            // Arrange
            var minefield = new Minefield(x, y);

            // Act
            minefield.PlantMines(expected);
        }

        [Theory]
        [InlineData(2, 2, 5)]
        [InlineData(3, 3, 10)]
        [InlineData(4, 4, 17)]
        public void ShouldThrowExceptionIfTooManyMines(int x, int y, int expected)
        {
            // Arrange
            var minefield = new Minefield(x, y);

            // Act
            Assert.Throws<ArgumentException>(() => minefield.PlantMines(expected));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        [InlineData(50)]
        [InlineData(60)]
        [InlineData(64)]
        public void PlantTheCorrectNumberOfMines(int numberOfMines)
        {
            // Arrange
            var sut = new Minefield(8, 8);

            // Act
            sut.PlantMines(numberOfMines);

            // Assert
            sut.MinesLeft.Should().Be(numberOfMines);
        }

        [Theory]
        [InlineData(65)]
        public void ThrowExceptionIfTooManyMinesAreRequested(int numberOfMines)
        {
            // Arrange
            var sut = new Minefield(8, 8);

            // Act
            Assert.Throws<ArgumentException>(() => sut.PlantMines(numberOfMines));

            // Assert
        }

        [Theory]
        [InlineData(3, 3, MoveOutcome.MineExplodes)]
        [InlineData(1, 1, MoveOutcome.NoMine)]
        public void GetTheCorrectStatusOfaMine(int x, int y, MoveOutcome expected)
        {
            // Arrange
            var sut = new Minefield(8, 8);
            sut.PlantMine(new Coordinate(3, 3));

            // Act
            var actual = sut.GetStatus(new Coordinate(x, y));

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void PlantAllMines()
        {
            // Arrange
            var sut = new Minefield(8, 8);
            sut.PlantMines(8 * 8);

            var expected = "MMMMMMMM\r\nMMMMMMMM\r\nMMMMMMMM\r\nMMMMMMMM\r\nMMMMMMMM\r\nMMMMMMMM\r\nMMMMMMMM\r\nMMMMMMMM\r\n";

            // Act
            var actual = sut.ShowMap();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void ShowMap()
        {
            // Arrange
            var sut = new Minefield(8, 8);
            sut.PlantMine(new Coordinate(3, 3));

            var expected = "00000000\r\n00000000\r\n00000000\r\n00000000\r\n000M0000\r\n00000000\r\n00000000\r\n00000000\r\n";

            // Act
            var actual = sut.ShowMap();

            // Assert
            actual.Should().Be(expected);
        }
    }
}