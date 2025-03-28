using Pacman__V2;

namespace Pacman_V2
{
    public class Board
    {
        private char[,] maze;
        private bool[,] dots;
        private int totalDots;
        private int currentLevel = 1;
        public const int MAX_LEVEL = 3;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int CurrentLevel => currentLevel;

        public void Initialize()
        {
            maze = currentLevel switch
            {
                1 => CreateLevel1(),
                2 => CreateLevel2(),
                3 => CreateLevel3(),
                //_ => CreateLeveli()
            };

            Width = maze.GetLength(1);
            Height = maze.GetLength(0);
            InitializeDots();
        }

        private void InitializeDots()
        {
            dots = new bool[Height, Width];
            totalDots = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (maze[y, x] == ' ')
                    {
                        dots[y, x] = true;
                        totalDots++;
                    }
                }
            }
        }

        public void Draw(Position playerPos, IEnumerable<Position> mobPositions, int score)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Level: {currentLevel} - Score: {score}");

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Position currentPos = new Position(x, y);

                    if (currentPos == playerPos)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('P');
                    }
                    else if (mobPositions.Any(m => m == currentPos))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('M');
                    }
                    else if (maze[y, x] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('█');
                    }
                    else if (dots[y, x])
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('·');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        // Level layouts van vorige code blijven hetzelfde
        private char[,] CreateLevel1()
        {
            return new char[,]
            {
        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
        {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
        {'#',' ','#','#',' ','#','#',' ',' ','#','#',' ','#','#','#'},
        {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
        {'#',' ','#','#',' ','#','#',' ',' ','#','#',' ','#',' ','#'},
        {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
        {'#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#'},
        {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
        {'#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#',' ','#'},
        {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
        {'#',' ','#','#',' ','#','#',' ',' ','#','#',' ','#',' ','#'},
        {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
        {'#',' ','#','#',' ','#','#',' ',' ','#','#',' ','#','#','#'},
        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
            };
        }
        private char[,] CreateLevel2()
        {
            return new char[,]
            {
        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
        {'#',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ','#'},
        {'#',' ','#','#',' ','#',' ','#',' ','#',' ','#','#',' ','#'},
        {'#',' ',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ','#'},
        {'#',' ','#',' ','#','#',' ','#',' ','#','#',' ','#',' ','#'},
        {'#',' ','#',' ',' ',' ',' ','#',' ',' ',' ',' ','#',' ','#'},
        {'#',' ','#',' ','#','#','#','#','#','#','#',' ','#',' ','#'},
        {'#',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ','#'},
        {'#',' ','#',' ','#','#','#','#','#','#','#',' ','#',' ','#'},
        {'#',' ','#',' ',' ',' ',' ','#',' ',' ',' ',' ','#',' ','#'},
        {'#',' ','#',' ','#','#',' ','#',' ','#','#',' ','#',' ','#'},
        {'#',' ',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ','#'},
        {'#',' ','#','#',' ','#',' ','#',' ','#',' ','#','#',' ','#'},
        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
            };
        }
        private char[,] CreateLevel3()
        {
            return new char[,]
            {
        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
        {'#',' ',' ','#',' ',' ',' ','#','#','#',' ',' ',' ',' ','#'},
        {'#',' ','#','#',' ','#','#',' ','#',' ',' ','#','#',' ','#'},
        {'#',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#','#',' ','#'},
        {'#',' ','#',' ','#','#',' ',' ',' ',' ',' ','#','#',' ','#'},
        {'#',' ','#',' ',' ',' ','#',' ','#',' ',' ','#','#',' ','#'},
        {'#',' ','#',' ','#','#','#',' ','#','#','#',' ',' ',' ','#'},
        {'#',' ','#',' ',' ',' ','#',' ',' ',' ','#',' ',' ','#','#'},
        {'#',' ','#',' ','#','#','#','#',' ','#',' ',' ',' ','#','#'},
        {'#',' ',' ',' ',' ',' ',' ','#',' ','#',' ','#',' ',' ','#'},
        {'#',' ','#',' ','#','#',' ','#',' ',' ',' ',' ',' ','#','#'},
        {'#',' ','#',' ','#',' ',' ',' ',' ','#',' ','#',' ',' ','#'},
        {'#',' ','#','#','#','#','#','#','#','#',' ','#',' ',' ','#'},
        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
            };
        }

        public bool IsWalkable(Position pos)
        {
            return pos.X >= 0 && pos.X < Width &&
                   pos.Y >= 0 && pos.Y < Height &&
                   maze[pos.Y, pos.X] != '#';
        }

        public bool CollectDot(Position pos)
        {
            if (dots[pos.Y, pos.X])
            {
                dots[pos.Y, pos.X] = false;
                totalDots--;
                return true;
            }
            return false;
        }

        public bool AllDotsCollected() => totalDots == 0;

        public void NextLevel()
        {
            if (currentLevel < MAX_LEVEL)
            {
                currentLevel++;
                Initialize();
            }
        }

        public bool IsLastLevel() => currentLevel >= MAX_LEVEL;

        public (int X, int Y)[] GetMobStartPositions()
        {
            return currentLevel switch
            {
                1 => new[] { (13, 1), (13, 12) },
                2 => new[] { (13, 1), (13, 12) },
                3 => new[] { (13, 1), (13, 12) },
                _ => new[] { (8, 1), (8, 8) }
            };
        }

        public (int X, int Y) GetStartPosition()
        {
            return currentLevel switch
            {
                1 => (1, 1),  // Linkerbovenhoek
                2 => (1, 1),
                3 => (1, 1),

            };
        }

    }
}