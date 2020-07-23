namespace Root
{
    public struct ValueRange
    {
        internal ValueRange(int min, int max, int def)
        {
            Min = min;
            Max = max;
            Default = def;
        }

        public int Min { get; }
        public int Max { get; }
        public int Default { get; }
    }
}
