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
}
