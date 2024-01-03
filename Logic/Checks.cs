namespace Chess
{
    public static class Checks
    {
        public static bool IsPieceInPath(this Dictionary<Coordinate, Piece> pieceByCoordinate, Coordinate origin, Coordinate destination)
        {
            var x1 = Mappings.ToNum(origin.Letter);
            var x2 = Mappings.ToNum(destination.Letter);
            var y1 = origin.Number;
            var y2 = destination.Number;

            var deltax = x2 - x1;
            var deltay = y2 - y1;

            for (var step = 1; step < Math.Max(Math.Abs(deltax), Math.Abs(deltay)); step++)
            {
                var x = x1 + step * Math.Sign(deltax);
                var y = y1 + step * Math.Sign(deltay);
                var coordinate = new Coordinate(Mappings.ToLet(x), y);
                if (pieceByCoordinate.ContainsKey(coordinate)) return true;
            }
            return false;
        }
        public static bool IsLegalMovePiece(this Piece piece, Coordinate origin, Coordinate destination)
        {
            int x1 = Mappings.ToNum(origin.Letter);
            int y1 = origin.Number;
            int x2 = Mappings.ToNum(destination.Letter);
            int y2 = destination.Number;

            switch (piece.Kind)
            {
                case Pieces.Pawn:
                    return ((piece.Color == Colors.White && (y2 - y1 == 1 || (y2 == 4 && y1 == 2 && x1 == x2))) ||
                                             (piece.Color == Colors.Black && (y1 - y2 == 1 || (y2 == 5 && y1 == 7 && x1 == x2))));
                case Pieces.Rook: return (x2 == x1 || y2 == y1);
                case Pieces.Knight:
                    return ((Math.Abs(x1 - x2) == 1 && Math.Abs(y1 - y2) == 2) ||
                                             (Math.Abs(x1 - x2) == 2 && Math.Abs(y1 - y2) == 1));
                case Pieces.Bishop: return (Math.Abs(x1 - x2) == Math.Abs(y1 - y2));
                case Pieces.Queen: return ((x2 == x1 || y2 == y1) || (Math.Abs(x1 - x2) == Math.Abs(y1 - y2)));
                case Pieces.King: return (Math.Abs(y2 - y1) == 1 || Math.Abs(x2 - x1) == 1);
                default: return false;
            }
        }
        public static bool IsLegalMove(this Dictionary<Coordinate, Piece> pieceByCoordinate, Piece piece, Coordinate origin, Coordinate destination)
        {
            bool isPieceInPath = IsPieceInPath(pieceByCoordinate, origin, destination);
            bool isPieceAtDestination = pieceByCoordinate.TryGetValue(destination, out var pieceAtDestination);

            if (isPieceAtDestination && pieceAtDestination.Color == piece.Color) return false;

            int x1 = Mappings.ToNum(origin.Letter);
            int x2 = Mappings.ToNum(destination.Letter);
            var y1 = origin.Number;
            var y2 = destination.Number;

            switch (piece.Kind)
            {
                case Pieces.Pawn:
                    return (isPieceAtDestination && Math.Abs(x2 - x1) < 2 && Math.Abs(y2 - y1) < 2) ||
                                          (!isPieceAtDestination && Math.Abs(x2 - x1) < 1) &&
                                           !isPieceInPath;
                case Pieces.Rook: return !isPieceInPath;
                case Pieces.Bishop: return !isPieceInPath;
                case Pieces.Queen: return !isPieceInPath;
                default: return true;
            }
        }
    }
}