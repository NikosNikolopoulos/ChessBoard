﻿using System;

namespace ChessBoard
{
    public class ChessEngine
    {
        public static void PlayChess()
        {
            var turn = true;                                                                                                                        //this field flags if its the black or white players turn

            ChessBoard chessboard = new ChessBoard();

            ApplicationMessage.printMessage(Messages.Notation);
            chessboard.initialiseBoard();
            chessboard.printBoard();

            while (true)
            {
                ChessEngine.NextMove(chessboard, turn);
                turn = !turn;
            }

        }

        public static string CheckOriginInput(ChessBoard chessboard, string blackOrWhite)
        {
            string originInput = "";
            bool isOriginInputValid = false;

            while (isOriginInputValid == false)
            {
                ApplicationMessage.printMessage(Messages.SelectPiece, blackOrWhite);

                originInput = Console.ReadLine();
                
                if (originInput.Length != 2)                                                                                                        //perform a basic input validity scan
                {
                    ApplicationMessage.printMessage(Messages.InvalidLength, blackOrWhite);
                }
                else
                {
                    char xOrig = originInput[0];
                    
                    int yOrig = Convert.ToInt32(originInput[1] - 48);                                                                               //ranges from [1,..,8]

                    if (Utilities.Char2Int(xOrig) < 1 || Utilities.Char2Int(xOrig) > 8 || yOrig < 1 || yOrig > 8)
                    {
                        ApplicationMessage.printMessage(Messages.InvalidInput, blackOrWhite);
                    }
                    else
                    {
                        if (chessboard.getPieceAt(xOrig, yOrig) == null)
                        {
                            ApplicationMessage.printMessage(Messages.EmptySelection, blackOrWhite);
                        }

                        else
                        {
                            if (chessboard.getPieceAt(xOrig, yOrig).Color != blackOrWhite)
                            {
                                ApplicationMessage.printMessage(Messages.WrongColor, blackOrWhite);
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

        public static string CheckDestinationInput(ChessBoard chessboard, Piece selectedPiece)
        {
            string destinationInput = "";
            bool isDestinationInputValid = false;

            while (isDestinationInputValid == false)
            {
                ApplicationMessage.printMessage(Messages.SelectDestination);

                destinationInput = Console.ReadLine();

                if (destinationInput == "")                                                                                                         //perform a basic input validity scan
                {
                    ApplicationMessage.printMessage(Messages.UndoSelection);
                    break;
                }
                if (destinationInput.Length != 2)
                {
                    ApplicationMessage.printMessage(Messages.InvalidLength);
                }
                else
                {
                    char xDest = destinationInput[0];
                    
                    int yDest = Convert.ToInt32(destinationInput[1] - 48);                                                                          //ranges from [1,..,8]
                    if (Utilities.Char2Int(xDest) < 1 || Utilities.Char2Int(xDest) > 8 || yDest < 1 || yDest > 8)
                    {
                        ApplicationMessage.printMessage(Messages.InvalidInput);
                    }
                    else
                    {
                        if (selectedPiece.IsLegalMove(xDest, yDest, chessboard))
                        {
                            isDestinationInputValid = true;
                        }
                    }
                }
            }

            return destinationInput == "" ? null : destinationInput;
        }

        public static void NextMove(ChessBoard chessboard, bool isWhitesTurn)
        {
            string blackOrWhite = isWhitesTurn ? "White" : "Black";

            ApplicationMessage.printMessage(Messages.NextPlayerColor,blackOrWhite);

            string origInput = CheckOriginInput(chessboard, blackOrWhite);                                                                          //variables for storing the inputs 

            var xOrig = origInput[0];                                                                                                          //variables for storing a char & an int after parsing the string

            var yOrig = Convert.ToInt32(origInput[1] - 48);                                                                                         //ranges from [1,..,8]

            var selectedPiece = chessboard.getPieceAt(xOrig, yOrig);

            string destInput = CheckDestinationInput(chessboard, selectedPiece);

            while (destInput == null)                                                                                                               //UNDO input detected
            {
                origInput = CheckOriginInput(chessboard, blackOrWhite);                                                                             //variables for storing the inputs

                xOrig = origInput[0];
                yOrig = Convert.ToInt32(origInput[1] - 48);                                                                                         //ranges from [1,..,8]

                selectedPiece = chessboard.getPieceAt(xOrig, yOrig); 
                destInput = CheckDestinationInput(chessboard, selectedPiece);
            }
            
            var xDest = destInput[0];
            var yDest = Convert.ToInt32(destInput[1] - 48);                                                                                         //ranges from [1,..,8]

            chessboard.movePieceAt(xOrig, yOrig, xDest, yDest);

            if ((yDest == 1 || yDest == 8) && (selectedPiece.Kind == 'P' || selectedPiece.Kind == 'p'))                                             //if a pawn reaches the finish-line spawn a queen at its place
            {
                chessboard.placePieceAt(
                    isWhitesTurn
                        ? new Queen(selectedPiece.getColor(), selectedPiece.getPosition(), 'Q')
                        : new Queen(selectedPiece.getColor(), selectedPiece.getPosition(), 'q'),
                    xDest, yDest);
            }

            chessboard.printBoard();

            ApplicationMessage.printMessage(Messages.NextPlayer, blackOrWhite);
        }
    }
}
