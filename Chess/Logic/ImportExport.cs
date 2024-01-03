namespace Chess
{
    public record ImportExport
    {
        public string Letter { get; init; }
        public int Number { get; init; }
        public Pieces Kind { get; init; }
        public Colors Color { get; init; }

        public string Symbol => Utils.GetSymbol(Kind, Color);
    }
}
