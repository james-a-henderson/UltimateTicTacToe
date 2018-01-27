using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
    Static wrapper class that handles the input to the globalGameBoard
    */
namespace UltimateTicTacToe
{
    public static class InputHandling
    {
        public static string sendInput(string input, GlobalBoard board)
        {
            string[] splitInput = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int globalBoardNum, localBoardNum;

            if(splitInput.Length == 2 && int.TryParse(splitInput[0], out globalBoardNum)
                                        && int.TryParse(splitInput[1], out localBoardNum)) //player is attempting to make a move
            {

                try
                {
                    if(globalBoardNum < 1 || globalBoardNum > 9 || localBoardNum < 1 || localBoardNum > 9)
                    {
                        return buildOutput("Move is invalid! Please Enter valid location. ", board);
                    }
                        
                    var globalBoard = boardNumberToCoordinates(globalBoardNum);
                    var localBoard = boardNumberToCoordinates(localBoardNum);

                    board.makeMove(globalBoard.Item1, globalBoard.Item2, localBoard.Item1, localBoard.Item2);

                    return buildOutput("", board);
                } catch (ArgumentException ae)
                {
                    if (ae.Message == "Selected board is not valid")
                        return buildOutput("Selected board is completed. Select another location. ", board);
                    else if (ae.Message == "Not going to required board")
                        return buildOutput("Not going to requried board. Select another location. ", board);
                    else if (ae.Message == "Attempting to make move on space where move was previously made")
                        return buildOutput("Space already used, choose another location. ", board);
                    else
                        return buildOutput("Unknown input error. Choose another location. ", board);
                }

            }
            else //invalid input
            {
                return buildOutput("Invalid Input. Enter valid command, or type ? for help. ", board);
            }
        }
        
        private static string buildOutput(string message, GlobalBoard board)
        {
            var builder = new StringBuilder();
            builder.AppendLine(board.ToString());
            builder.Append(message);
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
    }
}
