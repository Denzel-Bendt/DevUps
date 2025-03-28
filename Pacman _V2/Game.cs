using Pacman__V2;

namespace Pacman_V2
{
    public class Game
    {
        private readonly Board board;
        private readonly Player player;
        private readonly MobManager mobManager;
        private readonly ScoreManager scoreManager;
        private bool isRunning;

        public Game()
        {
            board = new Board();
            player = new Player();
            mobManager = new MobManager();
            scoreManager = new ScoreManager();
        }

        public void Start()
        {
            Console.WindowHeight = 30;
            Console.WindowWidth = 60;
            Console.CursorVisible = false;

            isRunning = true;
            while (isRunning)
            {
                ShowMainMenu();
            }
        }

        private void ShowMainMenu()
        {
            var choice = Menu.Show();
            switch (choice)
            {
                case MenuOption.StartGame:
                    PlayGame();
                    break;
                case MenuOption.ShowHighScores:
                    scoreManager.DisplayHighScores();
                    break;
                case MenuOption.Exit:
                    isRunning = false;
                    break;
            }
        }

        private void PlayGame()
        {
            ResetGame();

            while (true)
            {
                Console.Clear();
                board.Draw(player.Position, mobManager.GetMobPositions(), player.Score);
                mobManager.MoveMobs(player.Position, board);

                for (int i = 0; i < 3; i++) // Speler krijgt 3 bewegingen per mob beweging
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.Escape) return;
                        HandlePlayerInput(key);
                    }
                }

                if (CheckGameOver())
                {
                    HandleGameOver();
                    break;
                }

                if (board.AllDotsCollected())
                {
                    //HandleVictory();
                    HandleLevelComplete();
                    return;
                }

                Thread.Sleep(50);
            }
        }

        private void HandlePlayerInput(ConsoleKey key)
        {
            Direction? direction = key switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                _ => null
            };

            if (direction.HasValue)
            {
                Position newPosition = player.GetNextPosition(direction.Value);
                if (board.IsWalkable(newPosition))
                {
                    player.Move(direction.Value);
                    if (board.CollectDot(player.Position))
                    {
                        player.AddScore(10);
                    }
                }
            }
        }

        private bool CheckGameOver()
        {
            return mobManager.GetMobPositions().Any(m => m == player.Position);
        }

        private void HandleGameOver()
        {
            Console.Clear();
            string gameOver = $"Game Over! Level {board.CurrentLevel} - Score: {player.Score}";
            try
            {
                int centerX = Math.Max(0, Console.WindowWidth / 2 - gameOver.Length / 2);
                int centerY = Math.Max(0, Console.WindowHeight / 2);
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine(gameOver);
            }
            catch
            {
                // Fallback als de cursor positie niet werkt
                Console.WriteLine(gameOver);
            }


            string playerName = PromptForUsername();
            scoreManager.AddScore(playerName, player.Score);

            Thread.Sleep(2000);
        }

        private void HandleLevelComplete()
        {
            Console.Clear();

            if (board.IsLastLevel() && board.AllDotsCollected())
            {
                // Spel uitgespeeld
                Console.WriteLine("Gefeliciteerd! Je hebt alle levels uitgespeeld!");
                Console.WriteLine($"Eindscore: {player.Score}");

                // Vraag de gebruiker om hun naam in te vullen
                string playerName = PromptForUsername();
                scoreManager.AddScore(playerName, player.Score);

                // Wacht op een toetsdruk om verder te gaan
                Console.WriteLine("Druk op een toets om terug te gaan naar het menu...");
                Console.ReadKey(true);

                // Terug naar het hoofdmenu
                return; // Of je kunt hier een methode aanroepen om het menu te tonen
            }

            // Volgend level
            string message = $"Level {board.CurrentLevel} Behaald! Score: {player.Score}";
            Console.SetCursorPosition(Console.WindowWidth / 2 - message.Length / 2, Console.WindowHeight / 2);
            Console.WriteLine(message);
            Thread.Sleep(2000);

            board.NextLevel();
            var (x, y) = board.GetStartPosition();
            player.SetPosition(x, y);
            var mobPositions = board.GetMobStartPositions();
            mobManager.ResetWithPositions(mobPositions);

            PlayGame();
        }

        private string PromptForUsername()
        {
            Console.Write("Voer je naam in (max 10 karakters): ");
            string playerName = string.Empty;

            while (playerName.Length < 10)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    if (playerName.Length > 0)
                        break; // Stop als er een naam is ingevoerd
                }
                else if (key.Key == ConsoleKey.Backspace && playerName.Length > 0)
                {
                    playerName = playerName[..^1]; // Verwijder het laatste karakter
                    Console.Write("\b \b"); // Verwijder het karakter van de console
                }
                else if (char.IsLetterOrDigit(key.KeyChar) && playerName.Length < 10)
                {
                    playerName += key.KeyChar; // Voeg het karakter toe aan de naam
                    Console.Write(key.KeyChar); // Toon het karakter in de console
                }
            }

            Console.WriteLine(); // Nieuwe regel na het invoeren van de naam
            return playerName;
        }

        private void ResetGame()
        {
            board.Initialize();
            (int x, int y) startPos = board.GetStartPosition();  // Expliciete tuple declaratie
            player.Reset(startPos.x, startPos.y);                // Gebruik x en y van de tuple
            var mobPositions = board.GetMobStartPositions();
            mobManager.ResetWithPositions(mobPositions);
        }

    }
}