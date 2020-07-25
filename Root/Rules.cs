using System;
using System.Collections.Generic;
using System.Linq;

namespace Root
{
    public static class Rules
    {
        private static readonly List<Faction> _factions = new List<Faction>
        {
            new Faction('A', "Marquise de Cat", 10),
            new Faction('B', "Eyrie Dynasties", 7),
            new Faction('C', "Woodland Alliance", 3),
            new Faction('D', "Vagabond", 5),
            new Faction('E', "Vagabond (2nd)", 2),
            new Faction('F', "Lizard Cult", 2),
            new Faction('G', "Riverfolk Company", 5),
            new Faction('H', "Underground Duchy", 8),
            new Faction('I', "Corvid Conspiracy", 3)
        };
        private static readonly Dictionary<char, Faction> _factionsById = _factions.ToDictionary(f => f.Id);

        private static readonly Dictionary<int, int> _defaultReach = new Dictionary<int, int>
        {
            { 2, 17 },
            { 3, 18 },
            { 4, 21 },
            { 5, 25 },
            { 6, 28 }
        };

        private static readonly IComparer<Faction> _factionComparer = Comparer<Faction>.Create((f1, f2) => f1.Id - f2.Id);

        public static ValueRange PlayerCount => new ValueRange(2, 6, 4);

        public static ValueRange GetTargetReach(int playerCount)
        {
            int min = Math.Max(17, _factions.Select(f => f.Reach).OrderBy(r => r).Take(playerCount).Sum());
            int max = _factions.Select(f => f.Reach).OrderByDescending(r => r).Take(playerCount).Sum();
            return new ValueRange(min, max, Math.Min(max, _defaultReach[playerCount]));
        }

        public static List<Faction> GetAvailableFactions(int playerCount, int targetReach)
        {
            var selectable = GetSelectableFactions(_factions, playerCount, targetReach, false);
            bool needVagabond2 = GetSelectableFactions(
                _factions.Where(f => f.Id != 'D'),
                playerCount - 1,
                targetReach - _factionsById['D'].Reach,
                true)
                .Contains(_factionsById['E']);

            var result = new List<Faction>(selectable);
            if (needVagabond2)
            {
                result.Add(_factionsById['E']);
            }

            result.Sort(_factionComparer);
            return result;
        }

        private static IEnumerable<Faction> GetSelectableFactions(IEnumerable<Faction> factions, int selections, int reach, bool allowSecondVagabond)
        {
            var highestReach = factions.OrderByDescending(f => f.Reach).Take(selections - 1);
            int minReach = reach - highestReach.Sum(f => f.Reach);

            return factions
                .Where(f => f.Reach >= minReach)
                .Where(f => allowSecondVagabond || f.Id != 'E');
        }
    }
}
