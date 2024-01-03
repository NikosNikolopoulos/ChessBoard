namespace Chess
{
    public record Coordinate
    {
        public string Letter { get; init; }
        public int Number { get; init; }

        public static List<string> log;

        public Coordinate(string letter, int number)
        {
            log = new List<string>();
            Letter = letter;
            Number = number;
            if (!this.IsValid())
                log.Add(Utils.GetMessage(Errors.InvalidInput));
        }

        public Coordinate(int letter, int number)
        {
            log = new List<string>();
            Letter = Mappings.ToLet(letter);
            Number = number;
            if (!this.IsValid())
                log.Add(Utils.GetMessage(Errors.InvalidInput));
        }

        public bool IsValid() => (Mappings.ToNum(Letter) >= 1 &&
                                   Mappings.ToNum(Letter) <= 8 &&
                                   Number >= 1 && Number <= 8);
    }
}
