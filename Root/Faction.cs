namespace Root
{
    class Faction
    {
        public Faction(char id, string name, int reach)
        {
            Id = id;
            Name = name;
            Reach = reach;
        }

        public char Id { get; }
        public string Name { get; }
        public int Reach { get; }
    }
}
