using System;

namespace ChessBoard
{
    public enum Dialogs
    {
        SelectPiece,
        InvalidLength,
        InvalidInput,
        EmptySelection,
        WrongColor,
        SelectDestination,
        UndoSelection,
        NextPlayerColor,
        NextPlayer
    }

    public static class ApplicationMessage
    {
        public static void printNotation()
        {
            Console.WriteLine("<=========================================>" +
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
                              "\n<=========================================>");
        }

        public static void printDialog(this Dialogs DialogNumber,string BlackOrWhite = "White")
        {
            string MessageToPrint = "";

            switch (DialogNumber)
            {
                case Dialogs.SelectPiece:
                    MessageToPrint = "Please enter the coordinates of the piece\nyou would like to move (ex. of format A2):\n__________________________________________";
                    break;
                case Dialogs.InvalidLength:
                    MessageToPrint = "Input should be 2 characters long.\n__________________________________________";
                    break;
                case Dialogs.InvalidInput:
                    MessageToPrint = "Input is invalid.\n__________________________________________";
                    break;
                case Dialogs.EmptySelection:
                    MessageToPrint = "The cell you have selected is empty.\n__________________________________________";
                    break;
                case Dialogs.WrongColor:
                    MessageToPrint = $"Select a {BlackOrWhite} piece.\n__________________________________________";
                    break;
                case Dialogs.SelectDestination:
                    MessageToPrint = "Please enter the coordinates of the cell\nyou would like to move your piece to:\n__________________________________________";
                    break;
                case Dialogs.UndoSelection:
                    MessageToPrint = "Undoing selection.\n__________________________________________";
                    break;
                case Dialogs.NextPlayerColor:
                    MessageToPrint = $"\n\n{BlackOrWhite}'s turn!\n__________________________________________";
                    break;
                case Dialogs.NextPlayer:
                    MessageToPrint = "\nNext player's turn!";
                    break;
            }
            
            Console.WriteLine(MessageToPrint);
        }
    }
}
