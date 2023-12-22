using System.ComponentModel;

public enum Colors
{
    Black, White, NoColor
}
public enum Messages
{
    NextPlayerColor, WinnerMessage
}
public enum Errors
{
    InvalidLength, InvalidInput, EmptySelection, WrongColor, IllegalMove, InvalidForm
}
public enum Pieces
{
    Pawn, Rook, Knight, Bishop, Queen, King, NoPiece
}
public static class Mappings
{
    public static int ToNum(string x)
    {
        switch (x)
        {
            case "A": case "1": return 1;
            case "B": case "2": return 2;
            case "C": case "3": return 3;
            case "D": case "4": return 4;
            case "E": case "5": return 5;
            case "F": case "6": return 6;
            case "G": case "7": return 7;
            case "H": case "8": return 8;

            default: return 0;
        }
    }

    public static string ToLet(int x)
    {
        switch (x)
        {
            case 1: return "A";
            case 2: return "B";
            case 3: return "C";
            case 4: return "D";
            case 5: return "E";
            case 6: return "F";
            case 7: return "G";
            case 8: return "H";

            default: return "Z";
        }
    }
}

namespace Chess
{
    public class Utils
    {
        public static string GetMessage(Messages message, Colors blackOrWhite = Colors.NoColor)
        {
            switch (message)
            {
                case Messages.NextPlayerColor: return $"\n{blackOrWhite}'s turn!";
                case Messages.WinnerMessage: return $"Congratulations!!! {blackOrWhite}'s win!";

                default: return null;
            }
        }
        public static string GetMessage(Errors error, Colors blackOrWhite = Colors.NoColor)
        {
            switch (error)
            {
                case Errors.InvalidLength: return "Input should be 2 characters long.";
                case Errors.InvalidForm: return "Input has invalid form.";
                case Errors.InvalidInput: return "Input is invalid.";
                case Errors.WrongColor: return $"Select a {blackOrWhite} piece.";
                case Errors.EmptySelection: return "The cell you have selected is empty.";
                case Errors.IllegalMove: return "Illegal operation! Please try again.";

                default: return "Error not found.";
            }
        }
        public static string GetSymbol(Pieces kind, Colors color)
        {
            switch (kind, color)
            {
                case (Pieces.Pawn, Colors.White): return "♙";
                case (Pieces.Pawn, Colors.Black): return "♟";
                case (Pieces.Knight, Colors.White): return "♘";
                case (Pieces.Knight, Colors.Black): return "♞";
                case (Pieces.Rook, Colors.White): return "♖";
                case (Pieces.Rook, Colors.Black): return "♜";
                case (Pieces.Bishop, Colors.White): return "♗";
                case (Pieces.Bishop, Colors.Black): return "♝";
                case (Pieces.Queen, Colors.White): return "♕";
                case (Pieces.Queen, Colors.Black): return "♛";
                case (Pieces.King, Colors.White): return "♔";
                case (Pieces.King, Colors.Black): return "♚";

                default: return null;
            }
        }
    }
    public record Coordinate
    {
        public string Letter { get; init; }
        public int Number { get; init; }

        public static List<string> log;

        public Coordinate(string letter, int number)
        {
            Letter = letter;
            Number = number;
            if (!this.IsValid())
                log.Add(Utils.GetMessage(Errors.InvalidInput));
        }

        public Coordinate(int letter, int number)
        {
            Letter = Mappings.ToLet(letter);
            Number = number;
            if (!this.IsValid())
                log.Add(Utils.GetMessage(Errors.InvalidInput));
        }

        public bool IsValid() => (Mappings.ToNum(Letter) >= 1 &&
                                   Mappings.ToNum(Letter) <= 8 &&
                                   Number >= 1 && Number <= 8);
    }

    public record ImportExport
    {
        public string Letter { get; init; }
        public int Number { get; init; }
        public Pieces Kind { get; init; }
        public Colors Color { get; init; }

        public string Symbol => Utils.GetSymbol(Kind, Color);
    }
    public record Piece(Colors Color, Pieces Kind);

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

    public class Scopes
    {
        public interface ChessBoard
        {
            List<string> Logs { get; set; }
            Dictionary<Coordinate, Piece> PieceByCoordinate { get; set; }
            [DefaultValue(true)] bool IsWhitesTurn { get; set; }

            void NextMove(string originInput, string destinationInput)
            {
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

            void InitializeBoard()
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

        public interface IO
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

        public class Chessboard : Scopes.ChessBoard
        {
            public List<string> Logs { get; set; }
            public Dictionary<Coordinate, Piece> PieceByCoordinate { get; set; }
            [DefaultValue(true)] public bool IsWhitesTurn { get; set; }

            public Chessboard()
            {
                Logs = new List<string>();
                PieceByCoordinate = new Dictionary<Coordinate, Piece>();
                IsWhitesTurn = true;
            }

            public void NextMove(string originInput, string destinationInput)
            {
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

    public class Chess
    {
        public static void PrintBoard(Dictionary<Coordinate, Piece> pieceByCoordinate)
        {
            Console.WriteLine("    A   B   C   D   E   F   G   H");
            Console.Write("   ___ ___ ___ ___ ___ ___ ___ ___");
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("");
                Console.Write(i + " ");
                for (int j = 1; j < 9; j++)
                {
                    var coordinate = new Coordinate(j, i);
                    var piece = pieceByCoordinate.TryGetValue(coordinate, out var currentPiece) ? currentPiece : null;
                    var symbol = piece != null ? Utils.GetSymbol(piece.Kind, piece.Color) : "_";
                    Console.Write($"|_{symbol}_");
                }
                Console.Write("|");
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var chessBoard = new Scopes.Chessboard();
            chessBoard.InitializeBoard();
            PrintBoard(chessBoard.PieceByCoordinate);
        }
    }
}