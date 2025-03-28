using Pacman_V2;

namespace Pacman__V2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "PACMAN";
                Console.WindowWidth = 60;
                Console.WindowHeight = 30;
                Console.CursorVisible = false;
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                var game = new Game();
                game.Start();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Er is een fout opgetreden:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine("\nDruk op een toets om af te sluiten...");
                Console.ReadKey(true);
            }
            finally
            {
                Console.CursorVisible = true;
            }
        }
    }
}