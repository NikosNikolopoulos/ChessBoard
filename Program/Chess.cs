namespace Chess
{
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
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var chessBoard = new ChessBoard();
            chessBoard.Logs = new List<string>();
            chessBoard.InitializeBoard();
            PrintBoard(chessBoard.PieceByCoordinate);
            chessBoard.IsWhitesTurn = true;
            while (chessBoard.PieceByCoordinate.ContainsValue(new Piece(Colors.White, Pieces.King)) && chessBoard.PieceByCoordinate.ContainsValue(new Piece(Colors.Black, Pieces.King)))
            {
                string firstInput = Console.ReadLine();
                string secondInput = Console.ReadLine();

                if (firstInput.Length < 2 || secondInput.Length < 2)
                {
                    Console.WriteLine(Utils.GetMessage(Errors.InvalidLength));
                }
                else
                {
                    chessBoard.NextMove(firstInput, secondInput);
                }

                chessBoard.Logs.ForEach(Console.WriteLine);
                PrintBoard(chessBoard.PieceByCoordinate);
            }
            Console.WriteLine(Utils.GetMessage(Messages.WinnerMessage, chessBoard.IsWhitesTurn == true ? Colors.Black : Colors.White));
        }
    }
}