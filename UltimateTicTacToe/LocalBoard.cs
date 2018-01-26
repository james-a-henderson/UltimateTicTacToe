using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe
{
    public class LocalBoard
    {
        public virtual GlobalBoardState BoardState { get; private set; }
        public LocalBoardState[,] Board { get; private set; }
        
        public LocalBoard()
        {
            BoardState = GlobalBoardState.Open;
            Board = new LocalBoardState[3, 3];
        }

        public LocalBoard(LocalBoardState[,] Board)
        {
            this.Board = Board;
            verifyGlobalState();
        }

        public GlobalBoardState makeMove(int row, int column, Player player)
        {
            if (BoardState != GlobalBoardState.Open)
                throw new ArgumentException("Board is already completed");

            if (row < 0 || row > 2 || column < 0 || column > 2)
                throw new ArgumentOutOfRangeException("Position outside of the local board");

            if(Board[row,column] == LocalBoardState.Blank)
            {
                if (player == Player.X)
                    Board[row, column] = LocalBoardState.X;
                else
                    Board[row, column] = LocalBoardState.O;
            }
            else
            {
                throw new ArgumentException("Attempting to make move on space where move was previously made");
            }

            verifyGlobalState();
            return BoardState;
        }

        //this method assumes that there cannot be a valid X and O row
        private void verifyGlobalState()
        {
            GlobalBoardState result = GlobalBoardState.Open;
            //test horizontal
            for(int row = 0; row < 3; row++)
            {
                result = testLine(Board[row, 0], Board[row, 1], Board[row, 2]);
                if (result == GlobalBoardState.X || result == GlobalBoardState.O)
                {
                    BoardState = result;
                    return;
                }
            }

            //test vertical
            for(int column = 0; column < 3; column++)
            {
                result = testLine(Board[0, column], Board[1, column], Board[2, column]);
                if (result == GlobalBoardState.X || result == GlobalBoardState.O)
                {
                    BoardState = result;
                    return;
                }
            }

            //test diagonals
            result = testLine(Board[0, 0], Board[1, 1], Board[2, 2]);
            if (result == GlobalBoardState.X || result == GlobalBoardState.O)
            {
                BoardState = result;
                return;
            }
            result = testLine(Board[0, 2], Board[1, 1], Board[2, 0]);
            if (result == GlobalBoardState.X || result == GlobalBoardState.O)
            {
                BoardState = result;
                return;
            }

            //test to see if board is not full
            for(int row = 0; row < 3; row++)
            {
                for(int column = 0; column < 3; column++)
                {
                    if (Board[row, column] == LocalBoardState.Blank)
                    {
                        BoardState = GlobalBoardState.Open;
                        return;
                    }
                }
            }

            //if board is full and no valid lines are found, result must be a tie
            BoardState = GlobalBoardState.Tie;
        }

        /**
        Tests a given row of three LocalBoardStates to see if they form a line

        Returns:    GlobalBoardState.X if the row contains all Xs
                    GlobalBoardState.O if the row contains all Os
                    GlobalBoardState.Open otherwise
        */
        private GlobalBoardState testLine(LocalBoardState p1, LocalBoardState p2, LocalBoardState p3)
        {
            if (p1 == LocalBoardState.X && p2 == LocalBoardState.X && p3 == LocalBoardState.X)
                return GlobalBoardState.X;
            else if (p1 == LocalBoardState.O && p2 == LocalBoardState.O && p3 == LocalBoardState.O)
                return GlobalBoardState.O;
            else
                return GlobalBoardState.Open;
        }

        public virtual string[] outputBoard()
        {
            string[] output = new string[8];

            if(BoardState == GlobalBoardState.Open)
            {
                output[0] = "             ";
                output[1] = String.Format("  {0} | {1} | {2}  ", 
                    localBoardStateToString(Board[0,0]), localBoardStateToString(Board[0,1]), localBoardStateToString(Board[0,2]));
                output[2] = " ___|___|___ ";
                output[3] = String.Format("  {0} | {1} | {2}  ",
                    localBoardStateToString(Board[1, 0]), localBoardStateToString(Board[1, 1]), localBoardStateToString(Board[1, 2]));
                output[4] = " ___|___|___ ";
                output[5] = String.Format("  {0} | {1} | {2}  ",
                    localBoardStateToString(Board[2, 0]), localBoardStateToString(Board[2, 1]), localBoardStateToString(Board[2, 2]));
                output[6] = "    |   |    ";
                output[7] = "             ";
            }
            else if(BoardState == GlobalBoardState.X)
            {
                output[0] = "  __     __  ";
                output[1] = "  \\ \\   / /  ";
                output[2] = "   \\ \\ / /   ";
                output[3] = "    \\ V /    ";
                output[4] = "     > <     ";
                output[5] = "    / . \\    ";
                output[6] = "   / / \\ \\   ";
                output[7] = "  /_/   \\_\\  ";
            }
            else if(BoardState == GlobalBoardState.O)
            {
                output[0] = "   _______   ";
                output[1] = "  / _____ \\  ";
                output[2] = " | |     | | ";
                output[3] = " | |     | | ";
                output[4] = " | |     | | ";
                output[5] = " | |_____| | ";
                output[6] = "  \\_______/  ";
                output[7] = "             ";
            }
            else if(BoardState == GlobalBoardState.Tie)
            {
                output[0] = "  _________  ";
                output[1] = " |         | ";
                output[2] = " |__     __| ";
                output[3] = "    |   |    ";
                output[4] = "    |   |    ";
                output[5] = "    |   |    ";
                output[6] = "    |___|    ";
                output[7] = "             ";
            }

            return output;
        }

        private string localBoardStateToString(LocalBoardState lbs)
        {
            if (lbs == LocalBoardState.X)
                return "X";
            else if (lbs == LocalBoardState.O)
                return "O";
            else
                return " ";
        }
    }
}
