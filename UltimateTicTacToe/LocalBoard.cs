using System;

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

        //this method assumes that there cannot be a valid X row and valid O row at the same time
        private void verifyGlobalState()
        {
            GlobalBoardState horizontalResult = testHorizontalRows();
            GlobalBoardState verticalResult = testVerticalRows();
            GlobalBoardState diagonalResult = testDiagonalRows();
            GlobalBoardState finalResult;
            
            if(horizontalResult != GlobalBoardState.Open)
            {
                finalResult = horizontalResult;
            }
            else if (verticalResult != GlobalBoardState.Open)
            {
                finalResult = verticalResult;
            }
            else if (diagonalResult != GlobalBoardState.Open)
            {
                finalResult = diagonalResult;
            }
            else if (boardIsFull())
            {
                //if board is full with no lines, result is a tie
                finalResult = GlobalBoardState.Tie;
            }
            else
            {
                finalResult = GlobalBoardState.Open;
            }

            BoardState = finalResult;
        }

        private GlobalBoardState testHorizontalRows()
        {
            GlobalBoardState result = GlobalBoardState.Open;
            for (int row = 0; row < 3; row++)
            {
                result = testLine(Board[row, 0], Board[row, 1], Board[row, 2]);
                if (result == GlobalBoardState.X || result == GlobalBoardState.O)
                {
                    return result;
                }
            }

            return GlobalBoardState.Open;
        }

        private GlobalBoardState testVerticalRows()
        {
            GlobalBoardState result = GlobalBoardState.Open;
            for (int column = 0; column < 3; column++)
            {
                result = testLine(Board[0, column], Board[1, column], Board[2, column]);
                if (result == GlobalBoardState.X || result == GlobalBoardState.O)
                {
                    return result;
                }
            }

            return GlobalBoardState.Open;
        }

        private GlobalBoardState testDiagonalRows()
        {
            GlobalBoardState result = GlobalBoardState.Open;

            result = testLine(Board[0, 0], Board[1, 1], Board[2, 2]);
            if (result == GlobalBoardState.X || result == GlobalBoardState.O)
            {
                return result;
            }
            result = testLine(Board[0, 2], Board[1, 1], Board[2, 0]);
            if (result == GlobalBoardState.X || result == GlobalBoardState.O)
            {
                return result;
            }

            return GlobalBoardState.Open;
        }

        private bool boardIsFull()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (Board[row, column] == LocalBoardState.Blank)
                    {
                        BoardState = GlobalBoardState.Open;
                        return false;
                    }
                }
            }

            return true;
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
