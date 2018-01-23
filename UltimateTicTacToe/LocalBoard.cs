using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe
{
    public class LocalBoard
    {
        public GlobalBoardState BoardState { get; private set; }
        public LocalBoardState[,] Board { get; private set; }
        
        public LocalBoard()
        {
            BoardState = GlobalBoardState.Open;
            Board = new LocalBoardState[3, 3];
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
    }
}
