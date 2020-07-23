using System;
using System.Collections.Generic;
using System.Linq;

namespace Root
{
    public static class Rules
    {
        private static Dictionary<char, Faction> _factions = new Dictionary<char, Faction>
        {
            { 'A', new Faction('A', "Marquise de Cat", 10) },
            { 'B', new Faction('B', "Eyrie Dynasties", 7) },
            { 'C', new Faction('C', "Woodland Alliance", 3) },
            { 'D', new Faction('D', "Vagabond", 5) },
            { 'E', new Faction('E', "Vagabond (2nd)", 2) },
            { 'F', new Faction('F', "Lizard Cult", 2) },
            { 'G', new Faction('G', "Riverfolk Company", 5) },
            { 'H', new Faction('H', "Underground Duchy", 8) },
            { 'I', new Faction('I', "Corvid Conspiracy", 3) },
        };

        private static Dictionary<int, int> _defaultReach = new Dictionary<int, int>
        {
            { 2, 17 },
            { 3, 18 },
            { 4, 21 },
            { 5, 25 },
            { 6, 28 }
        };

        public static ValueRange PlayerCount => new ValueRange(2, 6, 4);

        public static ValueRange TargetReach(int playerCount)
        {
            int min = Math.Max(17, _factions.Values.Select(f => f.Reach).OrderBy(r => r).Take(playerCount).Sum());
            int max = _factions.Values.Select(f => f.Reach).OrderByDescending(r => r).Take(playerCount).Sum();
            return new ValueRange(min, max, Math.Min(max, _defaultReach[playerCount]));
        }
    }
}
