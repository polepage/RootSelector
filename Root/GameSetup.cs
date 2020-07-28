using System.Collections.Generic;
using System.Linq;

namespace Root
{
    public class GameSetup
    {
        internal GameSetup(IList<Faction> seats, int firstPlayer)
        {
            Seats = new List<Faction>(seats);
            FirstPlayer = firstPlayer;
        }

        public List<Faction> Seats { get; }
        public int FirstPlayer { get; }
        public int Reach => Seats.Sum(f => f.Reach);

        public static GameSetup Create(IList<Faction> seats, int firstPlayer) => new GameSetup(seats, firstPlayer);
    }
}
