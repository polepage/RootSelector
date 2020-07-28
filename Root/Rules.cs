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

        public static ValueRange PlayerCount => new ValueRange(2, 6, 4);
        public static int MinimumReach => 17;

        public static ValueRange GetTargetReach(int playerCount)
        {
            int min = GetMinReach(_factions, playerCount);
            int max = GetMaxReach(_factions, playerCount);
            return new ValueRange(min, max, Math.Min(max, _defaultReach[playerCount]));
        }

        public static List<AvailableFaction> GetAvailableFactions(int playerCount, int targetReach)
        {
            var selectable = GetSelectableFactions(_factions, playerCount, targetReach, false);
            bool needVagabond2 = GetSelectableFactions(
                _factions.Where(f => f.Id != 'D'),
                playerCount - 1,
                targetReach - _factionsById['D'].Reach,
                true)
                .Contains(_factionsById['E']);

            if (needVagabond2)
            {
                selectable = selectable.Append(_factionsById['E']);
            }

            return selectable.OrderBy(f => f.Id).Select(f => new AvailableFaction(f)).ToList();
        }

        public static int GetNormalizedFactionCount(IEnumerable<Faction> factions)
        {
            int count = factions.Count();
            if (!factions.Contains(_factionsById['D']) && factions.Contains(_factionsById['E']))
            {
                // If there is no Vagabond, then don't count 2nd Vagabond
                count--;
            }

            return count;
        }

        public static int GetMinReach(IEnumerable<Faction> factions, int playerCount)
        {
            return Math.Max(MinimumReach, factions.Select(f => f.Reach).OrderBy(r => r).Take(playerCount).Sum());
        }

        public static int GetMaxReach(IEnumerable<Faction> factions, int playerCount)
        {
            return factions.Select(f => f.Reach).OrderByDescending(r => r).Take(playerCount).Sum();
        }

        public static GameSetup CreateSetup(int playerCount, int targetReach, IEnumerable<Faction> factions)
        {
            int maxReach = GetMaxReach(factions, playerCount);
            if (maxReach < MinimumReach || GetNormalizedFactionCount(factions) < playerCount)
            {
                throw new ArgumentOutOfRangeException("Invalid Faction Pool");
            }

            var rng = new Random();
            int remainingReach = Math.Min(maxReach, targetReach);
            var resultList = new List<Faction>(playerCount);
            for (int i = 0; i < playerCount; i++)
            {
                var pool = GetSelectableFactions(factions.Where(f => !resultList.Contains(f)),
                                                 playerCount - i,
                                                 remainingReach,
                                                 resultList.Contains(_factionsById['D']));
                Faction newSeat = pool.Skip(rng.Next(0, pool.Count())).First();
                resultList.Add(newSeat);

                remainingReach -= newSeat.Reach;
            }

            return new GameSetup(resultList, rng.Next(0, playerCount));
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
