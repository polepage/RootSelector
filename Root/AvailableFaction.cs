namespace Root
{
    public class AvailableFaction
    {
        public AvailableFaction(Faction faction)
        {
            Faction = faction;
            Available = true;
        }

        public Faction Faction { get; }
        public bool Available { get; set; }

        public override int GetHashCode()
        {
            return Faction.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Faction.Equals(obj);
        }

        public static implicit operator Faction(AvailableFaction af) => af.Faction;
        public static explicit operator AvailableFaction(Faction f) => new AvailableFaction(f);
    }
}
