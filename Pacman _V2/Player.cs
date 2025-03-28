using Pacman__V2;

namespace Pacman_V2
{


    public class Player
    {
        public Position Position { get; private set; }
        public int Score { get; private set; }

        public void Reset(int x, int y)
        {
            Position = new Position(x, y);
            Score = 0;
        }

        public void SetPosition(int x, int y)
        {
            Position = new Position(x, y);
        }

        public Position GetNextPosition(Direction direction)
        {
            return Position.Move(direction);
        }

        public void Move(Direction direction)
        {
            Position = GetNextPosition(direction);
        }

        public void AddScore(int points)
        {
            Score += points;
        }
    }
}