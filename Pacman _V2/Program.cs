using Pacman_V2;
using System;
using System.Runtime.InteropServices; // For OS detection

namespace Pacman__V2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Only set Windows-specific properties if running on Windows
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Console.Title = "PACMAN";
                    Console.WindowWidth = 60;
                    Console.WindowHeight = 30;
                    Console.CursorVisible = false;
                }
                else
                {
                    Console.WriteLine("Running on non-Windows system - some display features disabled");
                }

                // UTF-8 encoding works cross-platform
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
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Console.CursorVisible = true;
                }
            }
        }
    }
}