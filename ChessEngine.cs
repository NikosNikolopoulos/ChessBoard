using System;

namespace ChessBoard
{
    public class ChessEngine
    {
        public static void playChess()
        {
            //this field flags if its the black or white players turn
            var turn = true;

            ChessBoard chessboard = new ChessBoard();

            ApplicationMessage.printNotation();
            chessboard.initialiseBoard();
            chessboard.printBoard();

            while (true)
            {
                ChessEngine.nextMove(chessboard, turn);
                turn = !turn;
            }

        }

        public static string checkOriginInput(ChessBoard chessboard, string BlackOrWhite)
        {
            string originInput = "";
            bool isOriginInputValid = false;

            while (isOriginInputValid == false)
            {
                ApplicationMessage.printDialog(Dialogs.SelectPiece, BlackOrWhite);

                originInput = Console.ReadLine();
                //perform a basic input validity scan
                if (originInput.Length != 2)
                {
                    ApplicationMessage.printDialog(Dialogs.InvalidLength, BlackOrWhite);
                }
                else
                {
                    char xOrig = originInput[0];
                    //ranges from [1,..,8]
                    int yOrig = Convert.ToInt32(originInput[1] - 48);

                    if (Utilities.char2Int(xOrig) < 0 || Utilities.char2Int(xOrig) > 7 || yOrig < 1 || yOrig > 8)
                    {
                        ApplicationMessage.printDialog(Dialogs.InvalidInput, BlackOrWhite);
                    }
                    else
                    {
                        if (chessboard.getPieceAt(xOrig, yOrig) == null)
                        {
                            ApplicationMessage.printDialog(Dialogs.EmptySelection, BlackOrWhite);
                        }

                        else
                        {
                            if (chessboard.getPieceAt(xOrig, yOrig).Color != BlackOrWhite)
                            {
                                ApplicationMessage.printDialog(Dialogs.WrongColor, BlackOrWhite);
                            }
                            else
                            {
                                isOriginInputValid = true;
                            }
                        }
                    }
                }
            }
            return originInput;
        }

        public static string checkDestinationInput(ChessBoard chessboard, Piece selectedPiece)
        {
            string destinationInput = "";
            bool isDestinationInputValid = false;

            while (isDestinationInputValid == false)
            {
                ApplicationMessage.printDialog(Dialogs.SelectDestination);

                destinationInput = Console.ReadLine();

                //perform a basic input validity scan
                if (destinationInput == "")
                {
                    ApplicationMessage.printDialog(Dialogs.UndoSelection);
                    break;
                }
                else if (destinationInput.Length != 2)
                {
                    ApplicationMessage.printDialog(Dialogs.InvalidLength);
                }
                else
                {
                    char xDest = destinationInput[0];
                    //ranges from [1,..,8]
                    int yDest = Convert.ToInt32(destinationInput[1] - 48);
                    if (Utilities.char2Int(xDest) < 0 || Utilities.char2Int(xDest) > 7 || yDest < 1 || yDest > 8)
                    {
                        ApplicationMessage.printDialog(Dialogs.InvalidInput);
                    }
                    else
                    {
                        if (selectedPiece.isLegalMove(xDest, yDest, chessboard))
                        {
                            isDestinationInputValid = true;
                        }
                    }
                }
            }

            if (destinationInput == "")
                return null;
            else
                return destinationInput;
        }

        public static void nextMove(ChessBoard chessboard, bool isWhitesTurn)
        {
            //variables for storing a char & an int after parsing the string
            char xOrig, xDest;
            int yOrig, yDest;

            //string responsible for storing who is playing next "Black" or "White"
            string BlackOrWhite;
            if (isWhitesTurn == true)
                BlackOrWhite = "White";
            else
                BlackOrWhite = "Black";

            ApplicationMessage.printDialog(Dialogs.NextPlayerColor,BlackOrWhite);

            //variables for storing the inputs 
            string OrigInput = checkOriginInput(chessboard, BlackOrWhite);

            xOrig = OrigInput[0];
            //ranges from [1,..,8]
            yOrig = Convert.ToInt32(OrigInput[1] - 48);

            Piece selectedPiece = chessboard.getPieceAt(xOrig, yOrig);

            string DestInput = checkDestinationInput(chessboard, selectedPiece);

            //UNDO input detected
            while (DestInput == null)
            {
                //variables for storing the inputs 
                OrigInput = checkOriginInput(chessboard, BlackOrWhite);
                
                xOrig = OrigInput[0];
                //ranges from [1,..,8]
                yOrig = Convert.ToInt32(OrigInput[1] - 48); 
                
                selectedPiece = chessboard.getPieceAt(xOrig, yOrig); 
                DestInput = checkDestinationInput(chessboard, selectedPiece);
            }
            
            xDest = DestInput[0];
            //ranges from [1,..,8]
            yDest = Convert.ToInt32(DestInput[1] - 48);

            chessboard.movePieceAt(xOrig, yOrig, xDest, yDest);

            //if a pawn reaches the finish-line spawn a queen at its place
            if ((yDest == 1 || yDest == 8) && (selectedPiece.Kind == 'P' || selectedPiece.Kind == 'p'))
            {
                if (isWhitesTurn)
                    chessboard.placePieceAt(new Queen(selectedPiece.getColor(), selectedPiece.getPosition(), 'Q'),
                        xDest, yDest);
                else
                    chessboard.placePieceAt(new Queen(selectedPiece.getColor(), selectedPiece.getPosition(), 'q'),
                        xDest, yDest);
            }

            chessboard.printBoard();

            ApplicationMessage.printDialog(Dialogs.NextPlayer, BlackOrWhite);
        }
    }
}
