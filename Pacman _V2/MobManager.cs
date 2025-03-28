using Pacman_V2;

namespace Pacman__V2
{
    public class MobManager
    {
        private List<Mob> mobs;
        private int moveDelay;
        private const int MOVE_INTERVAL = 2;

        public MobManager()
        {
            mobs = new List<Mob>();
        }

        public void ResetWithPositions((int X, int Y)[] positions)
        {
            mobs.Clear();
            foreach (var pos in positions)
            {
                mobs.Add(new Mob(new Position(pos.X, pos.Y)));
            }
            moveDelay = 0;
        }

        public IEnumerable<Position> GetMobPositions()
        {
            return mobs.Select(m => m.Position);
        }

        public void MoveMobs(Position playerPos, Board board)
        {
            moveDelay++;
            if (moveDelay < MOVE_INTERVAL)
            {
                return;
            }
            moveDelay = 0;

            foreach (var mob in mobs)
            {
                MoveSingleMob(mob, playerPos, board);
            }
        }

        private void MoveSingleMob(Mob mob, Position playerPos, Board board)
        {
            int deltaX = playerPos.X - mob.Position.X;
            int deltaY = playerPos.Y - mob.Position.Y;

            Position newPosition = mob.Position;

            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                // Probeer horizontaal te bewegen
                newPosition = TryMove(mob.Position, new Position(
                    mob.Position.X + Math.Sign(deltaX),
                    mob.Position.Y
                ), board);

                // Als horizontaal niet lukt, probeer verticaal
                if (newPosition == mob.Position)
                {
                    newPosition = TryMove(mob.Position, new Position(
                        mob.Position.X,
                        mob.Position.Y + Math.Sign(deltaY)
                    ), board);
                }
            }
            else
            {
                // Probeer verticaal te bewegen
                newPosition = TryMove(mob.Position, new Position(
                    mob.Position.X,
                    mob.Position.Y + Math.Sign(deltaY)
                ), board);

                // Als verticaal niet lukt, probeer horizontaal
                if (newPosition == mob.Position)
                {
                    newPosition = TryMove(mob.Position, new Position(
                        mob.Position.X + Math.Sign(deltaX),
                        mob.Position.Y
                    ), board);
                }
            }

            mob.Position = newPosition;
        }

        private Position TryMove(Position current, Position desired, Board board)
        {
            if (board.IsWalkable(desired) && !GetMobPositions().Any(p => p == desired))
            {
                return desired;
            }
            return current;
        }
    }
}