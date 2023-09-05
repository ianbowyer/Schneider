namespace Schneider.Minefield.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FluentAssertions;

    public class MinefieldCoreShould
    {
        [Fact]
        public void ExplodeWhenMovingRightOnToAnActiveMine()
        {
            // Arrange
            var minefield = new Minefield(8, 8);
            minefield.PlantMine(new Coordinate(1, 1));

            var sut = new MinefieldCore();
            sut.CurrentMinefield = minefield;
            sut.CurrentPosition = new Coordinate(0, 1);

            var expected = MoveOutcome.MineExplodes;
            // Act
            var actual = sut.Move(Direction.Right);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void ExplodeWhenMovingLeftOnToAnActiveMine()
        {
            // Arrange
            var minefield = new Minefield(8, 8);
            minefield.PlantMine(new Coordinate(1, 1));

            var sut = new MinefieldCore();
            sut.CurrentMinefield = minefield;
            sut.CurrentPosition = new Coordinate(2, 1);

            var expected = MoveOutcome.MineExplodes;
            // Act
            var actual = sut.Move(Direction.Left);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void ExplodeWhenMovingUpOnToAnActiveMine()
        {
            // Arrange
            var minefield = new Minefield(8, 8);
            minefield.PlantMine(new Coordinate(1, 1));

            var sut = new MinefieldCore();
            sut.CurrentMinefield = minefield;
            sut.CurrentPosition = new Coordinate(1, 0);

            var expected = MoveOutcome.MineExplodes;
            // Act
            var actual = sut.Move(Direction.Up);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void ExplodeWhenMovingDownOnToAnActiveMine()
        {
            // Arrange

            var minefield = new Minefield(8, 8);
            minefield.PlantMine(new Coordinate(1, 1));

            var sut = new MinefieldCore();
            sut.CurrentMinefield = minefield;
            sut.CurrentPosition = new Coordinate(1, 2);

            var expected = MoveOutcome.MineExplodes;

            // Act
            var actual = sut.Move(Direction.Down);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void NotExplodeWhenMovingDownOnToAnExplodedMine()
        {
            // Arrange
            var minefield = new Minefield(8, 8);
            minefield.PlantExplodedMine(new Coordinate(1, 1));

            var sut = new MinefieldCore();
            sut.CurrentMinefield = minefield;
            sut.CurrentPosition = new Coordinate(1, 2);

            var expected = MoveOutcome.ExplodedMine;

            // Act
            var actual = sut.Move(Direction.Down);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void NoMineWhenMovingToAnEmptySqaure()
        {
            // Arrange

            var minefield = new Minefield(8, 8);
            minefield.PlantExplodedMine(new Coordinate(1, 1));

            var sut = new MinefieldCore();
            sut.CurrentMinefield = minefield;
            sut.CurrentPosition = new Coordinate(1, 2);

            var expected = MoveOutcome.NoMine;
            // Act
            var actual = sut.Move(Direction.Right);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void CompleteTheGameWhenReachingTheTopSide()
        {
            // Arrange
            var minefield = new Minefield(8, 8);

            var sut = new MinefieldCore();
            sut.CurrentMinefield = minefield;
            sut.CurrentPosition = new Coordinate(3, 6);

            var expected = MoveOutcome.Completed;
            // Act
            var actual = sut.Move(Direction.Up);

            // Assert
            expected.Should().Be(actual);
        }

        [Fact]
        public void NotMoveOutSideTheMinefield()
        {
            // Arrange
            var minefield = new Minefield(8, 8);

            var sut = new MinefieldCore();
            sut.CurrentMinefield = minefield;
            sut.CurrentPosition = new Coordinate(3, 7);

            var expected = MoveOutcome.NotAllowed;
            // Act
            var actual = sut.Move(Direction.Up);

            // Assert
            expected.Should().Be(actual);
        }
    }
}