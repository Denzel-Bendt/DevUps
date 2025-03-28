using Xunit;
using Pacman_V2;
using Pacman__V2; // Namespace from main project

namespace SL2A.DevOps.Tests
{
    public class PositionTests
    {
        [Theory]
        [InlineData(1, 1, Direction.Right, 2, 1)]
        [InlineData(5, 5, Direction.Left, 4, 5)]
        public void Move_UpdatesPositionCorrectly(int startX, int startY, 
                                               Direction direction,
                                               int expectedX, int expectedY)
        {
            // Arrange
            var position = new Position(startX, startY);
            
            // Act
            var newPosition = position.Move(direction);
            
            // Assert
            Assert.Equal(expectedX, newPosition.X);
            Assert.Equal(expectedY, newPosition.Y);
        }

        [Fact]
        public void Positions_AreEqual_WhenCoordinatesMatch()
        {
            // Arrange
            var pos1 = new Position(3, 4);
            var pos2 = new Position(3, 4);
            
            // Act & Assert
            Assert.True(pos1 == pos2);
            Assert.False(pos1 != pos2);
        }
    }
}