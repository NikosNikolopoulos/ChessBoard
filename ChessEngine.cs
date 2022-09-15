﻿using System;

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
                Console.WriteLine("Please enter the coordinates of the piece\nyou would like to move (ex. of format A2):\n__________________________________________");

                originInput = Console.ReadLine();
                //perform a basic input validity scan
                if (originInput.Length != 2)
                {
                    Console.WriteLine("Input should be 2 characters long.\n__________________________________________");
                }
                else
                {
                    char xOrig = originInput[0];
                    //ranges from [1,..,8]
                    int yOrig = Convert.ToInt32(originInput[1] - 48);

                    if (Utilities.char2Int(xOrig) < 0 || Utilities.char2Int(xOrig) > 7 || yOrig < 1 || yOrig > 8)
                    {
                        Console.WriteLine("Input is invalid.\n__________________________________________");
                    }
                    else
                    {
                        if (chessboard.getPieceAt(xOrig, yOrig) == null)
                        {
                            Console.WriteLine("The cell you have selected is empty.\n__________________________________________");
                        }

                        else
                        {
                            if (chessboard.getPieceAt(xOrig, yOrig).Color != BlackOrWhite)
                            {
                                Console.WriteLine($"Select a {BlackOrWhite} piece.\n__________________________________________");
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
                Console.WriteLine("Please enter the coordinates of the cell\nyou would like to move your piece to:\n__________________________________________");

                destinationInput = Console.ReadLine();
                //perform a basic input validity scan
                if (destinationInput == "")
                {
                    Console.WriteLine("Undoing selection.\n__________________________________________");
                    checkOriginInput(chessboard, selectedPiece.getColor());
                }
                else if (destinationInput.Length != 2)
                {
                    Console.WriteLine("Input should be 2 characters long.\n__________________________________________");
                }
                else
                {
                    char xDest = destinationInput[0];
                    //ranges from [1,..,8]
                    int yDest = Convert.ToInt32(destinationInput[1] - 48);
                    if (Utilities.char2Int(xDest) < 0 || Utilities.char2Int(xDest) > 7 || yDest < 1 || yDest > 8)
                    {
                        Console.WriteLine("Input is invalid.\n__________________________________________");
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

            Console.WriteLine($"\n\n{BlackOrWhite}'s turn!\n__________________________________________");

            //variables for storing the inputs 
            string OrigInput = ChessEngine.checkOriginInput(chessboard, BlackOrWhite);

            xOrig = OrigInput[0];
            //ranges from [1,..,8]
            yOrig = Convert.ToInt32(OrigInput[1] - 48);

            Piece selectedPiece = chessboard.getPieceAt(xOrig, yOrig);

            string DestInput = ChessEngine.checkDestinationInput(chessboard, selectedPiece);

            xDest = DestInput[0];
            //ranges from [1,..,8]
            yDest = Convert.ToInt32(DestInput[1] - 48);

            chessboard.movePieceAt(xOrig, yOrig, xDest, yDest);

            //if a pawn reaches the finish-line spawn a queen at its place
            if (yDest == 1 || yDest == 8 && selectedPiece.Kind == 'P' || selectedPiece.Kind == 'p')
                if (isWhitesTurn)
                    chessboard.placePieceAt(new Queen(selectedPiece.getColor(),selectedPiece.getPosition(),'Q'),xDest,yDest);
                else
                    chessboard.placePieceAt(new Queen(selectedPiece.getColor(), selectedPiece.getPosition(),'q'), xDest, yDest);

            chessboard.printBoard();

            Console.WriteLine("\nNext player's turn!");
        }
    }
}
