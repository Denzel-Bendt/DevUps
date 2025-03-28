namespace Pacman_V2
{
    public static class Menu
    {
        public static MenuOption Show()
        {
            int selectedOption = 0;
            string[] options = { "Start Spel", "High Scores", "Afsluiten" };

            string[] titleArt = {
            @"  ____    _    ____ __  __    _    _   _ ",
            @" |  _ \  / \  / ___|  \/  |  / \  | \ | |",
            @" | |_) |/ _ \| |   | |\/| | / _ \ |  \| |",
            @" |  __// ___ \ |___| |  | |/ ___ \| |\  |",
            @" |_|  /_/   \_\____|_|  |_/_/   \_\_| \_|"
        };

            while (true)
            {
                Console.Clear();

                // Teken titel
                DrawTitle(titleArt);

                // Teken menu opties
                int centerY = Console.WindowHeight / 2 - options.Length;
                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - options[i].Length / 2, centerY + i + 8);

                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(" " + options[i] + " ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" " + options[i] + " ");
                    }

                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption - 1 + options.Length) % options.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption + 1) % options.Length;
                        break;
                    case ConsoleKey.Enter:
                        return (MenuOption)selectedOption;
                }
            }
        }

        private static void DrawTitle(string[] titleArt)
        {
            int startY = Console.WindowHeight / 4;
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < titleArt.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - titleArt[i].Length / 2, startY + i);
                Console.Write(titleArt[i]);
            }

            Console.ResetColor();
        }

        public static string AskUsername()
        {
            Console.Clear();
            string prompt = "Voer je naam in (max 10 karakters): ";
            string username = "";

            Console.SetCursorPosition(Console.WindowWidth / 2 - prompt.Length / 2, Console.WindowHeight / 2);
            Console.Write(prompt);
            Console.SetCursorPosition(Console.WindowWidth / 2 - username.Length / 2, Console.WindowHeight / 2);
            Console.WriteLine(username);


            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter && username.Length > 0)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace && username.Length > 0)
                {
                    username = username[..^1];
                    Console.SetCursorPosition(Console.WindowWidth / 2 - prompt.Length / 2 + prompt.Length, Console.WindowHeight / 2);
                    Console.Write(new string(' ', 10));
                    Console.SetCursorPosition(Console.WindowWidth / 2 - prompt.Length / 2 + prompt.Length, Console.WindowHeight / 2);
                    Console.Write(username);
                }
                else if (char.IsLetterOrDigit(key.KeyChar) && username.Length < 10)
                {
                    username += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }

            return username;
        }
    }
}