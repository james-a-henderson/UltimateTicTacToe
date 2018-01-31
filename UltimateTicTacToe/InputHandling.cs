using System;
using System.Text;

/**
    Static wrapper class that handles the input to the globalGameBoard
    */
namespace UltimateTicTacToe
{
    public static class InputHandling
    {
        public static string initialBoardState(GlobalBoard board)
        {
            return buildOutput("", board);
        }

        public static string sendInput(string input, GlobalBoard board)
        {
            string message = "";
            string[] splitInput = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int globalBoardNum, localBoardNum;

            if(splitInput.Length == 2 && int.TryParse(splitInput[0], out globalBoardNum)
                                        && int.TryParse(splitInput[1], out localBoardNum)) //player is attempting to make a move
            {
                message = makeMove(board, globalBoardNum, localBoardNum);
            }
            else if (input.Trim().ToUpper() == "HELP" || input.Trim() == "?")
            {
                message = help();
            }
            else if (input.Trim().ToUpper() == "EXIT" || input.Trim().ToUpper() == "QUIT")
            {
                message = exit(board);
            }
            else //invalid input
            {
                message = "Invalid Input. Enter valid command, or type ? for help.";
            }

            return buildOutput(message, board);
        }
        
        private static string buildOutput(string message, GlobalBoard board)
        {
            //We don't want to generate output when the player is exiting
            if (board.Exiting)
                return message;

            var builder = new StringBuilder();
            builder.AppendLine(board.ToString());
            if (message.Length > 0)
            {
                builder.AppendLine(message);
            }
                
            builder.AppendLine(nextBoard(board));
            if(board.currentPlayer == Player.X)
            {
                builder.Append("X's Move: ");
            }
            else
            {
                builder.Append("O's Move: ");
            }

            return builder.ToString();
        }

        private static Tuple<int, int> boardNumberToCoordinates(int boardNum)
        {
            return new Tuple<int, int>((boardNum - 1) / 3, (boardNum - 1) % 3);
        }

        private static string makeMove(GlobalBoard board, int globalBoardNum, int localBoardNum)
        {
            try
            {
                if (globalBoardNum < 1 || globalBoardNum > 9 || localBoardNum < 1 || localBoardNum > 9)
                {
                    return "Move is invalid! Please Enter valid location.";
                }

                var globalBoard = boardNumberToCoordinates(globalBoardNum);
                var localBoard = boardNumberToCoordinates(localBoardNum);

                board.makeMove(globalBoard.Item1, globalBoard.Item2, localBoard.Item1, localBoard.Item2);

                return "";
            }
            catch (ArgumentException ae)
            {
                if (ae.Message == "Selected board is not valid")
                    return "Selected board is completed. Select another location.";
                else if (ae.Message == "Not going to required board")
                    return "Not going to requried board. Select another location.";
                else if (ae.Message == "Attempting to make move on space where move was previously made")
                    return "Space already used, choose another location.";
                else
                    return "Unknown input error. Choose another location.";
            }
        }

        private static string help()
        {
            var helpMessage = new StringBuilder();
            helpMessage.AppendLine("To make a move, type in '1 2', where the first number is the board you want to move to, and the second number is the specific square you want to move on");
            helpMessage.Append("To exit, type exit");

            return helpMessage.ToString();
        }

        private static string exit(GlobalBoard board)
        {
            board.Exiting = true;
            return "Thank you for playing!";
        }

        private static string nextBoard(GlobalBoard board)
        {
            int nextNumber = board.nextBoardNumber();
            if (nextNumber == 0)
                return "Next Board: Any Board";
            else
                return "Next Board: " + nextNumber;
        }
    }
}
