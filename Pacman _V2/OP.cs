//using System;
//using System.Collections.Generic;
//using System.Threading;

//class Pacman
//{
//    static char[,] maze;
//    static int playerX, playerY;
//    static int score = 0;
//    static List<(int, int)> mobs = new List<(int, int)>();
//    static Random random = new Random();
//    static int mobMoveDelay = 0;
//    static int MOB_MOVE_INTERVAL = 2; // Hoe vaak de mobs bewegen (hogere waarde = langzamere mobs)
//    static readonly (int X, int Y) MOB1_START = (8, 2);
//    static readonly (int X, int Y) MOB2_START = (8, 5);


//    enum MenuOption
//    {
//        StartGame,
//        ShowHighScores,
//        Exit
//    }

//    static List<(string Name, int Score)> highScores = new List<(string Name, int Score)>();

//    static string AskUsername()
//    {
//        Console.Clear();
//        Console.SetCursorPosition(Console.WindowWidth / 2 - 15, Console.WindowHeight / 2);
//        Console.Write("GAME OVER!!" +
//            "\n\rVoer je naam in: ");
//        return Console.ReadLine();
//    }


//    static void Main()
//    {
//        Console.WindowHeight = 30;
//        Console.WindowWidth = 60;
//        Console.CursorVisible = false;

//        bool running = true;
//        while (running)
//        {
//            var choice = ShowMenu();
//            switch (choice)
//            {
//                case MenuOption.StartGame:
//                    PlayGame();
//                    break;
//                case MenuOption.ShowHighScores:
//                    DisplayHighScores();
//                    break;
//                case MenuOption.Exit:
//                    running = false;
//                    break;
//            }
//        }
//    }
//    static MenuOption ShowMenu()
//    {
//        int selectedOption = 0;
//        string[] options = { "Start Spel", "High Scores", "Afsluiten" };

//        while (true)
//        {
//            Console.Clear();
//            int centerY = Console.WindowHeight / 2 - options.Length;

//            // Teken titel
//            string title = "PACMAN";
//            Console.SetCursorPosition(Console.WindowWidth / 2 - title.Length / 2, centerY - 2);
//            Console.WriteLine(title);

//            // Teken menu opties
//            for (int i = 0; i < options.Length; i++)
//            {
//                Console.SetCursorPosition(Console.WindowWidth / 2 - options[i].Length / 2, centerY + i);
//                if (i == selectedOption)
//                {
//                    Console.BackgroundColor = ConsoleColor.White;
//                    Console.ForegroundColor = ConsoleColor.Black;
//                }
//                Console.WriteLine(options[i]);
//                Console.ResetColor();
//            }
//            // naast enter ook spatie als optie geven om mee te selecteren 
//            // met tekst ook aangeven hoe de speler alles moet selecteren
//            // menu moet mee resizen met scherm 
//            // met esc toets spel leaven 
//            // 
//            var key = Console.ReadKey(true).Key;
//            switch (key)
//            {
//                case ConsoleKey.UpArrow:
//                    selectedOption = (selectedOption - 1 + options.Length) % options.Length;
//                    break;
//                case ConsoleKey.DownArrow:
//                    selectedOption = (selectedOption + 1) % options.Length;
//                    break;
//                case ConsoleKey.Enter:
//                    return (MenuOption)selectedOption;
//            }
//        }
//    }

//    static void ResetGame()
//    {
//        // Reset maze
//        InitializeMaze();

//        // Reset speler en mobs
//        InitializeEntities();

//        // Reset andere spelvariabelen
//        score = 0;
//        mobMoveDelay = 0;
//    }
//    static void PlayGame()
//    {
//        ResetGame();

//        while (true)
//        {
//            Console.Clear();
//            DrawMaze();
//            MoveMobs();


//            // Meerdere bewegingen per loop voor de speler
//            for (int i = 0; i < 10; i++) // Speler kan 3 keer bewegen voordat mobs bewegen
//            {
//                if (Console.KeyAvailable)
//                {
//                    var key = Console.ReadKey(true).Key;
//                    if (key == ConsoleKey.Escape) return;
//                    MovePlayer(key);
//                }
//            }

//            if (CheckCollision() | Level1())
//            {
//                Console.Clear();
//                string gameOver = "Game Over! Je score: " + score;
//                Console.SetCursorPosition(Console.WindowWidth / 2 - gameOver.Length / 2, Console.WindowHeight / 2);
//                Console.WriteLine(gameOver);

//                string playerName = AskUsername();
//                highScores.Add((playerName, score));

//                Thread.Sleep(2000);
//                break;
//            }




//            Thread.Sleep(50);
//        }
//    }

//    static void DisplayHighScores()
//    {
//        Console.Clear();
//        string title = "HIGH SCORES";
//        Console.SetCursorPosition(Console.WindowWidth / 2 - title.Length / 2, Console.WindowHeight / 4);
//        Console.WriteLine(title);

//        var sortedScores = highScores.OrderByDescending(s => s).Take(5).ToList();

//        for (int i = 0; i < sortedScores.Count; i++)
//        {
//            string scoreText = $"{i + 1}. {sortedScores[i]}";
//            Console.SetCursorPosition(Console.WindowWidth / 2 - scoreText.Length / 2, Console.WindowHeight / 4 + 2 + i);
//            Console.WriteLine(scoreText);
//        }

//        Console.SetCursorPosition(Console.WindowWidth / 2 - 15, Console.WindowHeight / 4 + 8);
//        Console.WriteLine("Druk op een toets om terug te gaan");
//        Console.ReadKey(true);
//    }

//    static void InitializeMaze()
//    {
//        maze = new char[,]
//        {
//        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
//        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
//        {'#','#',' ',' ',' ',' ','#',' ',' ','#',' ','#',' ','#','#'},
//        {'#','#',' ','#','#',' ','#',' ','#','#',' ','#',' ','#','#'},
//        {'#','#',' ','#',' ',' ',' ',' ',' ',' ',' ','#',' ','#','#'},
//        {'#','#',' ','#',' ','#','#','#','#',' ','#','#',' ','#','#'},
//        {'#','#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ','#','#'},
//        {'#','#',' ','#','#','#',' ','#','#',' ','#','#',' ','#','#'},
//        {'#','#',' ',' ',' ','#',' ',' ',' ',' ',' ','#',' ','#','#'},
//        {'#','#','#','#',' ','#','#','#','#',' ','#','#',' ','#','#'},
//        {'#','#',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ','#','#'},
//        {'#','#',' ','#','#','#','#','#','#',' ','#','#','#','#','#'},
//        {'#','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#'},
//        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
//        {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}

//        };

//        // Replace empty spaces with points
//        for (int y = 0; y < maze.GetLength(0); y++)
//        {
//            for (int x = 0; x < maze.GetLength(1); x++)
//            {
//                if (maze[y, x] == ' ')
//                {
//                    maze[y, x] = '.'; // Set points where the player can collect
//                }
//            }
//        }
//    }

//    static void InitializeEntities()
//    {
//        playerX = 2;
//        playerY = 3;
//        maze[playerY, playerX] = 'P';

//        // Reset mobs naar originele posities
//        mobs.Clear();
//        mobs.Add(MOB1_START);
//        mobs.Add(MOB2_START);

//        foreach (var mob in mobs)
//        {
//            maze[mob.Item2, mob.Item1] = 'M';
//        }
//    }

//    static void DrawMaze()
//    {
//        // Bereken het midden van de console
//        int consoleWidth = Console.WindowWidth;
//        int consoleHeight = Console.WindowHeight;
//        int mazeWidth = maze.GetLength(1);
//        int mazeHeight = maze.GetLength(0);

//        // Bereken padding voor horizontale centrering
//        int leftPadding = (consoleWidth - mazeWidth) / 2;
//        // Bereken padding voor verticale centrering
//        int topPadding = (consoleHeight - mazeHeight) / 2;

//        // Clear console en zet cursor op de juiste startpositie
//        Console.Clear();

//        // Print score gecentreerd
//        Console.SetCursorPosition(leftPadding, topPadding - 2);
//        Console.WriteLine($"Score: {score}");

//        // Print maze
//        for (int y = 0; y < mazeHeight; y++)
//        {
//            Console.SetCursorPosition(leftPadding, topPadding + y);
//            for (int x = 0; x < mazeWidth; x++)
//            {
//                Console.Write(maze[y, x]);
//            }
//        }
//    }

//    static void MovePlayer(ConsoleKey key)
//    {
//        int newX = playerX;
//        int newY = playerY;

//        switch (key)
//        {
//            case ConsoleKey.UpArrow: newY--; break;
//            case ConsoleKey.DownArrow: newY++; break;
//            case ConsoleKey.LeftArrow: newX--; break;
//            case ConsoleKey.RightArrow: newX++; break;
//        }

//        if (maze[newY, newX] != '#')
//        {
//            maze[playerY, playerX] = ' ';
//            if (maze[newY, newX] == '.')
//            {
//                score++;
//            }
//            playerX = newX;
//            playerY = newY;
//            maze[playerY, playerX] = 'P';
//        }
//    }

//    static void MoveMobs()
//    {
//        if (mobs.Count > 2)
//        {
//            mobs = mobs.Take(2).ToList();
//        }
//        mobMoveDelay++;
//        if (mobMoveDelay < MOB_MOVE_INTERVAL) // Mobs bewegen alleen als de delay voorbij is
//        {
//            return;
//        }
//        mobMoveDelay = 0;

//        List<(int, int)> newMobPositions = new List<(int, int)>();

//        foreach (var mob in mobs)
//        {
//            int mobX = mob.Item1;
//            int mobY = mob.Item2;

//            maze[mobY, mobX] = '.';

//            int deltaX = playerX - mobX;
//            int deltaY = playerY - mobY;

//            int newX = mobX;
//            int newY = mobY;

//            if (Math.Abs(deltaX) > Math.Abs(deltaY))
//            {
//                newX += Math.Sign(deltaX);
//                if (maze[mobY, newX] == '#' || maze[mobY, newX] == 'M' || newMobPositions.Contains((newX, mobY)))
//                {
//                    newX = mobX;
//                    newY += Math.Sign(deltaY);
//                }
//            }
//            else
//            {
//                newY += Math.Sign(deltaY);
//                if (maze[newY, mobX] == '#' || maze[newY, mobX] == 'M' || newMobPositions.Contains((mobX, newY)))
//                {
//                    newY = mobY;
//                    newX += Math.Sign(deltaX);
//                }
//            }

//            if (maze[newY, newX] == '#' || maze[newY, newX] == 'M' || newMobPositions.Contains((newX, newY)))
//            {
//                newX = mobX;
//                newY = mobY;
//            }

//            newMobPositions.Add((newX, newY));
//            maze[newY, newX] = 'M';
//        }

//        mobs = newMobPositions;
//    }

//    static bool CheckCollision()
//    {
//        foreach (var mob in mobs)
//        {
//            if (mob.Item1 == playerX && mob.Item2 == playerY)
//            {
//                return true;
//            }
//        }
//        return false;
//    }

//    static bool Level1()
//    {
//        // Check if there are any points left in the maze
//        for (int y = 0; y < maze.GetLength(0); y++)
//        {
//            for (int x = 0; x < maze.GetLength(1); x++)
//            {
//                if (maze[y, x] == '.') // If there's still a point
//                {
//                    return false; // Level is not complete
//                }
//            }
//        }
//        return true; // All points collected, level is complete
//    }
//}
