using System;

namespace ChessBoard
{
    public enum Messages
    {
        Notation,
        ColumnsTop,
        ColumnsBot,
        RowsLeft,
        RowsRight,
        PieceInCell,
        EmptyCell,
        SelectPiece,
        InvalidLength,
        InvalidInput,
        EmptySelection,
        WrongColor,
        SelectDestination,
        UndoSelection,
        NextPlayerColor,
        NextPlayer,
        IllegalMove,
        WinnerMessage
    }

    public static class ApplicationMessage
    {
        public static void PrintMessage(Messages msgNumber, string blackOrWhite = "White", int number = 1, char kind = 'P')
        {
            string msg = "";

            switch (msgNumber)
            {
                case Messages.Notation:
                    msg = "<=========================================>" +
                          "\n<                 RULES                   >" +
                          "\n<                                         >" +
                          "\n< 1.1 CONVENTION                          >" +
                          "\n<  The white pieces are represented with  >" +
                          "\n< capital letters whereas the black ones  >" +
                          "\n< are represented with small letters.     >" +
                          "\n<                                         >" +
                          "\n<  To UNDO a piece selection enter \"\" in  >" +
                          "\n< the destination coordinates field for   >" +
                          "\n< your selected piece.                    >" +
                          "\n<                                         >" +
                          "\n< 1.2 NOTATION                            >" +
                          "\n<  P --> white pawn    p --> black pawn   >" +
                          "\n<  R --> white rook    r --> black rook   >" +
                          "\n<  H --> white knight  h --> black knight >" +
                          "\n<  B --> white bishop  b --> black bishop >" +
                          "\n<  Q --> white queen   q --> black queen  >" +
                          "\n<  K --> white king    k --> black king   >" +
                          "\n<                                         >" +
                          "\n<=========================================>";
                    break;
                case Messages.SelectPiece:
                    msg = "Please enter the coordinates of the piece" +
                          "\nyou would like to move (ex. of format A2):" +
                          "\n__________________________________________";
                    break;
                case Messages.InvalidLength:
                    msg = "Input should be 2 characters long." +
                          "\n__________________________________________";
                    break;
                case Messages.InvalidInput:
                    msg = "Input is invalid." +
                          "\n__________________________________________";
                    break;
                case Messages.EmptySelection:
                    msg = "The cell you have selected is empty." +
                          "\n__________________________________________";
                    break;
                case Messages.WrongColor:
                    msg = $"Select a {blackOrWhite} piece." +
                          "\n__________________________________________";
                    break;
                case Messages.SelectDestination:
                    msg = "Please enter the coordinates of the cell" +
                          "\nyou would like to move your piece to:" +
                          "\n__________________________________________";
                    break;
                case Messages.UndoSelection:
                    msg = "Undoing selection." +
                          "\n__________________________________________";
                    break;
                case Messages.NextPlayerColor:
                    msg = $"\n{blackOrWhite}'s turn!" +
                          "\n__________________________________________";
                    break;
                case Messages.NextPlayer:
                    msg = "\nNext player's turn!" +
                          "\n__________________________________________";
                    break;
                case Messages.IllegalMove:
                    msg = "Illegal operation! Please try again." +
                          "\n__________________________________________";
                    break;
                case Messages.ColumnsTop:
                    msg = "     A   B   C   D   E   F   G   H" +
                          "\n    ___ ___ ___ ___ ___ ___ ___ ___";
                    break;
                case Messages.ColumnsBot:
                    msg = "\n     A   B   C   D   E   F   G   H";
                    break;
                case Messages.RowsLeft:
                    msg = number + "  ";
                    break;
                case Messages.RowsRight:
                    msg = "|  " + number;
                    break;
                case Messages.PieceInCell:
                    msg = "|_" + kind + "_";
                    break;
                case Messages.EmptyCell:
                    msg = "|___";
                    break;
                case Messages.WinnerMessage:
                    msg = $"Congratulations!!! {blackOrWhite}'s win!" +
                          "\n__________________________________________";
                    break;
            }

            if (msgNumber == Messages.RowsLeft || msgNumber == Messages.EmptyCell ||
                msgNumber == Messages.PieceInCell)
                Console.Write(msg);
            else
                Console.WriteLine(msg);
        }
    }
}
