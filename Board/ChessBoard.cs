namespace Chess
{
    public class ChessBoard
    {
        public List<string> Logs { get; set; }
        public Dictionary<Coordinate, Piece> PieceByCoordinate { get; set; }
        public bool IsWhitesTurn { get; set; }

        public void NextMove(string originInput, string destinationInput)
        {
            Logs = new List<string>();
            var origin = new Coordinate(originInput[0].ToString(), Mappings.ToNum(originInput[1].ToString()));
            if (!PieceByCoordinate.TryGetValue(origin, out var piece))
                Logs.Add(Utils.GetMessage(Errors.EmptySelection));
            else if ((IsWhitesTurn && piece.Color == Colors.Black) ||
                     (!IsWhitesTurn && piece.Color == Colors.White))
                Logs.Add(Utils.GetMessage(Errors.WrongColor, IsWhitesTurn ? Colors.White : Colors.Black));

            var destination = new Coordinate(destinationInput[0].ToString(), Mappings.ToNum(destinationInput[1].ToString()));

            if (piece != null && !piece.IsLegalMovePiece(origin, destination)) Logs.Add(Utils.GetMessage(Errors.IllegalMove));
            if (piece != null && !Checks.IsLegalMove(PieceByCoordinate, piece, origin, destination)) Logs.Add(Utils.GetMessage(Errors.IllegalMove));

            var errors = Logs;
            Logs = errors.ToHashSet().ToList();
            if (errors.Any()) return;

            PieceByCoordinate.Remove(origin);
            PieceByCoordinate.Remove(destination);
            PieceByCoordinate.Add(destination, piece);
            PieceByCoordinate = PieceByCoordinate.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            IsWhitesTurn = !IsWhitesTurn;
        }

        public void InitializeBoard()
        {
            PieceByCoordinate = new Dictionary<Coordinate, Piece>();
            IsWhitesTurn = true;
            Enumerable.Range(1, 8).ToList().ForEach(column =>
            {
                new[] { 1, 2, 7, 8 }.ToList().ForEach(row =>
                {
                    var color = row < 5 ? Colors.White : Colors.Black;
                    var piece = (row, column) switch
                    {
                        (2 or 7, _) => new Piece(color, Pieces.Pawn),
                        (_, 1 or 8) => new Piece(color, Pieces.Rook),
                        (_, 2 or 7) => new Piece(color, Pieces.Knight),
                        (_, 3 or 6) => new Piece(color, Pieces.Bishop),
                        (_, 4) => new Piece(color, Pieces.Queen),
                        (_, 5) => new Piece(color, Pieces.King),
                        (_, _) => null
                    };
                    PieceByCoordinate[new Coordinate(column, row)] = piece;
                });
            });
        }
    }
}