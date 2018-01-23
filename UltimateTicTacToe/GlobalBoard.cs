using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToe
{

    public class GlobalBoard
    {
        public LocalBoard[,] Board { get; private set; }
        public GameStatus status { get; private set; }
        public Player currentPlayer { get; private set; }

        private int nextRow = -1;
        private int nextColumn = -1;

        public GlobalBoard()
        {
            initialize(Player.X);
        }

        public GlobalBoard(Player startingPlayer)
        {
            initialize(startingPlayer);
        }

        private void initialize(Player startingPlayer)
        {
            Board = new LocalBoard[3, 3];

            for(int row = 0; row < 3; row++)
            {
                for(int col = 0; col < 3; col++)
                {
                    Board[row, col] = new LocalBoard();
                }
            }

            status = GameStatus.InProgress;
            currentPlayer = startingPlayer;
        }

        public void makeMove(int globalRow, int globalColumn, int localRow, int localColumn)
        {
            verifyBoardSelection(globalRow, globalColumn);

            Board[globalRow, globalColumn].makeMove(localRow, localColumn, currentPlayer);

            nextRow = localRow;
            nextColumn = localColumn;

            if (currentPlayer == Player.O)
                currentPlayer = Player.X;
            else
                currentPlayer = Player.O;
        }

        //throws exceptions if board selection is not valid
        private void verifyBoardSelection(int globalRow, int globalColumn)
        {
            if (globalRow < 0 || globalRow > 2 || globalColumn < 0 || globalColumn > 2)
                throw new ArgumentOutOfRangeException("Board selection is out of range");

            //is first move
            if (nextRow < 0 || nextColumn < 0)
                return;

            //check to see if the player is required to go to the nextRow & nextColumn
            if(Board[nextRow,nextColumn].BoardState == GlobalBoardState.Open)
            {
                //ensure player is going to that board
                if (globalRow != nextRow || globalColumn != nextColumn)
                    throw new ArgumentException("Not going to required board");
                else
                    return;
            }

            //ensure selected board is open
            if(Board[globalRow, globalColumn].BoardState != GlobalBoardState.Open)
            {
                throw new ArgumentException("Selected board is not valid");
            }
        }
    }
}
