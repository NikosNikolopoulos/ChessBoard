namespace Chess
{
    public class IO
    {
        ChessBoard Board { get; set; }

        void Resume(List<ImportExport> items)
        {
            Board.PieceByCoordinate.Clear();
            items.ForEach(item => Board.PieceByCoordinate.Add(new Coordinate(item.Letter, item.Number), new Piece(item.Color, item.Kind)));
        }

        List<ImportExport> PiecesList() => Board.PieceByCoordinate.Select(x =>
                                            new ImportExport
                                            {
                                                Letter = x.Key.Letter,
                                                Number = x.Key.Number,
                                                Kind = x.Value.Kind,
                                                Color = x.Value.Color
                                            }).ToList();

        List<ImportExport> PiecesListWithEmptyCells()
        {
            var items = PiecesList();
            Enumerable.Range(1, 8).ToList().ForEach(i =>
            {
                Enumerable.Range(1, 8).ToList().ForEach(j =>
                {
                    var coordinate = new Coordinate(i, j);
                    if (!items.Any(x => x.Letter == coordinate.Letter && x.Number == coordinate.Number))
                        items.Add(new ImportExport
                        {
                            Letter = coordinate.Letter,
                            Number = coordinate.Number,
                            Kind = Pieces.NoPiece,
                            Color = Colors.NoColor
                        });
                });
            });
            return items.ToList();
        }
    }
}
