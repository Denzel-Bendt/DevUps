using Pacman_V2;

namespace Pacman__V2
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position Move(Direction direction)
        {
            return direction switch
            {
                Direction.Up => new Position(X, Y - 1),
                Direction.Down => new Position(X, Y + 1),
                Direction.Left => new Position(X - 1, Y),
                Direction.Right => new Position(X + 1, Y),
                _ => this
            };
        }

        public static bool operator ==(Position a, Position b)
            => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Position a, Position b)
            => !(a == b);

        public override bool Equals(object? obj)  // Added nullability marker
            => obj is Position position && this == position;

        public override int GetHashCode()
            => HashCode.Combine(X, Y);
    }
}