namespace Pacman_V2
{
    public class HighScore
    {
        public string PlayerName { get; }
        public int Score { get; }
        public DateTime Date { get; }

        public HighScore(string playerName, int score, DateTime date)
        {
            PlayerName = playerName;
            Score = score;
            Date = date;
        }
    }
}