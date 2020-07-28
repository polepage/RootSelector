using System.Collections.Generic;

namespace Root
{
    public class GameSetup
    {
        internal GameSetup(IReadOnlyList<Faction> seats, int firstPlayer)
        {
            Seats = new List<Faction>(seats);
            FirstPlayer = firstPlayer;
        }

        public IReadOnlyList<Faction> Seats { get; }
        public int FirstPlayer { get; }

        public static GameSetup Create(IReadOnlyList<Faction> seats, int firstPlayer) => new GameSetup(seats, firstPlayer);
    }
}
