namespace Root
{
    public class Faction
    {
        internal Faction(char id, string name, int reach)
        {
            Id = id;
            Name = name;
            Reach = reach;
        }

        public char Id { get; }
        public string Name { get; }
        public int Reach { get; }

        public static Faction Create(char id, string name, int reach) => new Faction(id, name, reach);

        public override int GetHashCode() => Id.GetHashCode();

        public override bool Equals(object obj)
        {
            return obj is Faction f &&
                   f.Id == Id;
        }
    }
}
