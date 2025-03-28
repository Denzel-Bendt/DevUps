namespace Pacman_V2
{
    public class ScoreManager
    {
        private List<HighScore> highScores;
        private const int MAX_HIGH_SCORES = 5;

        public ScoreManager()
        {
            highScores = new List<HighScore>();
        }

        public void AddScore(string name, int score)
        {
            highScores.Add(new HighScore(name, score, DateTime.Now));
            highScores = highScores
                .OrderByDescending(s => s.Score)
                .Take(MAX_HIGH_SCORES)
                .ToList();
        }

        public void DisplayHighScores()
        {
            Console.Clear();
            string title = "HIGH SCORES";
            Console.SetCursorPosition(Console.WindowWidth / 2 - title.Length / 2, Console.WindowHeight / 4);
            Console.WriteLine(title);

            var sortedScores = highScores.OrderByDescending(s => s.Score).Take(MAX_HIGH_SCORES);
            int row = Console.WindowHeight / 4 + 2;

            foreach (var (score, index) in sortedScores.Select((score, index) => (score, index)))
            {
                string scoreText = $"{index + 1}. {score.PlayerName.PadRight(10)} {score.Score.ToString().PadLeft(6)} {score.Date:dd/MM/yy}";
                Console.SetCursorPosition(Console.WindowWidth / 2 - scoreText.Length / 2, row + index);
                Console.WriteLine(scoreText);
            }

            Console.SetCursorPosition(Console.WindowWidth / 2 - 15, row + MAX_HIGH_SCORES + 2);
            Console.WriteLine("Druk op een toets om terug te gaan");
            Console.ReadKey(true);
        }
    }
}