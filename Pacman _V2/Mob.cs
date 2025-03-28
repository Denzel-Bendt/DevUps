namespace Pacman__V2
{
    public class Mob
    {
        public Position Position { get; set; }

        public Mob(Position startPosition)
        {
            Position = startPosition;
        }
    }
}