using Catmash.Domain.Interfaces;
using Catmash.Domain;


namespace Catmash.Application.Services
{
    public class EloService : IEloService
    {
        private const int K = 32;

        public void ApplyElo(Cat winner, Cat loser)
        {
            double expectedWinner = 1.0 / (1 + Math.Pow(10, (loser.Score - winner.Score) / 400.0));
            double expectedLoser = 1.0 / (1 + Math.Pow(10, (winner.Score - loser.Score) / 400.0));

            winner.Score += Math.Floor(K * (1 - expectedWinner));
            loser.Score += Math.Floor(K * (0 - expectedLoser));

            winner.Wins++;
            loser.Losses++;
        }
    }
}
