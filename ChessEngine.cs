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
                Console.WriteLine("Please enter the coordinates of the piece");
                Console.WriteLine("you would like to move (ex. of format A2):");
                Console.WriteLine("__________________________________________");

                originInput = Console.ReadLine();
                //perform a basic input validity scan
                if (originInput.Length != 2)
                {
                    Console.WriteLine("Input should be 2 characters long.");
                    Console.WriteLine("__________________________________________");
                }
                else
                {
                    char xOrig = originInput[0];
                    //ranges from [1,..,8]
                    int yOrig = Convert.ToInt32(originInput[1] - 48);

                    if (Utilities.char2Int(xOrig) < 0 || Utilities.char2Int(xOrig) > 7 || yOrig < 1 || yOrig > 8)
                    {
                        Console.WriteLine("Input is invalid.");
                        Console.WriteLine("__________________________________________");
                    }
                    else
                    {
                        if (chessboard.getPieceAt(xOrig, yOrig) == null)
                        {
                            Console.WriteLine("The cell you have selected is empty.");
                            Console.WriteLine("__________________________________________");
                        }

                        else
                        {
                            if (chessboard.getPieceAt(xOrig, yOrig).Color != BlackOrWhite)
                            {
                                Console.WriteLine($"Select a {BlackOrWhite} piece.");
                                Console.WriteLine("__________________________________________");
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
                Console.WriteLine("Please enter the coordinates of the cell");
                Console.WriteLine("you would like to move your piece to:");
                Console.WriteLine("__________________________________________");

                destinationInput = Console.ReadLine();
                //perform a basic input validity scan
                if (destinationInput.Length != 2)
                {
                    Console.WriteLine("Input should be 2 characters long.");
                    Console.WriteLine("__________________________________________");
                }
                else
                {
                    char xDest = destinationInput[0];
                    //ranges from [1,..,8]
                    int yDest = Convert.ToInt32(destinationInput[1] - 48);
                    if (Utilities.char2Int(xDest) < 0 || Utilities.char2Int(xDest) > 7 || yDest < 1 || yDest > 8)
                    {
                        Console.WriteLine("Input is invalid.");
                        Console.WriteLine("__________________________________________");
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

        public static void nextMove(ChessBoard b, bool turn)
        {
            //variables for storing a char & an int after parsing the string
            char xOrig;
            int yOrig;

            //variables for storing a char & an int after parsing the string
            char xDest;
            int yDest;

            //string responsible for storing who is playing next "Black" or "White"
            string name;
            if (turn == true)
                name = "White";
            else
                name = "Black";

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"{name}'s turn!");
            Console.WriteLine("__________________________________________");

            //variables for storing the inputs 
            string OrigInput = ChessEngine.checkOriginInput(b, name);

            xOrig = OrigInput[0];
            //ranges from [1,..,8]
            yOrig = Convert.ToInt32(OrigInput[1] - 48);

            Piece p_select = b.getPieceAt(xOrig, yOrig);

            string DestInput = ChessEngine.checkDestinationInput(b, p_select);

            xDest = DestInput[0];
            //ranges from [1,..,8]
            yDest = Convert.ToInt32(DestInput[1] - 48);

            b.movePieceAt(xOrig, yOrig, xDest, yDest);
            b.printBoard();

            Console.WriteLine("");
            Console.WriteLine("Next player's turn!");
        }
    }
}
