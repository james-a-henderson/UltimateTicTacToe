using System;
using System.Text;

namespace UltimateTicTacToe
{

    public class GlobalBoard
    {
        public LocalBoard[,] Board { get; private set; }
        public GameStatus Status { get; private set; }
        public virtual Player currentPlayer { get; private set; }

        public bool Exiting { get; set; }

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

        //for test purposes
        public GlobalBoard(Player startingPlayer, LocalBoard[,] Board)
        {
            this.Board = Board;
            Status = GameStatus.InProgress;
            currentPlayer = startingPlayer;
            Exiting = false;
            verifyBoardState();
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

            Status = GameStatus.InProgress;
            currentPlayer = startingPlayer;
            Exiting = false;
        }

        public virtual void makeMove(int globalRow, int globalColumn, int localRow, int localColumn)
        {
            if (Status != GameStatus.InProgress)
                throw new ArgumentException("Cannot make move on completed board");

            verifyBoardSelection(globalRow, globalColumn);

            Board[globalRow, globalColumn].makeMove(localRow, localColumn, currentPlayer);

            nextRow = localRow;
            nextColumn = localColumn;

            if (currentPlayer == Player.O)
                currentPlayer = Player.X;
            else
                currentPlayer = Player.O;

            verifyBoardState();
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

        private void verifyBoardState()
        {
            GameStatus result = GameStatus.InProgress;
            int tieCount = 0;
            //test horizontal
            for (int row = 0; row < 3; row++)
            {
                result = testLine(Board[row, 0].BoardState, Board[row, 1].BoardState, Board[row, 2].BoardState);
                if (result == GameStatus.X_Win || result == GameStatus.O_Win)
                {
                    Status = result;
                    return;
                }
                else if (result == GameStatus.Tie)
                {
                    tieCount++;
                }
            }

            //test vertical
            for (int column = 0; column < 3; column++)
            {
                result = testLine(Board[0, column].BoardState, Board[1, column].BoardState, Board[2, column].BoardState);
                if (result == GameStatus.X_Win || result == GameStatus.O_Win)
                {
                    Status = result;
                    return;
                }
                else if (result == GameStatus.Tie)
                {
                    tieCount++;
                }
            }

            //test diagonals
            result = testLine(Board[0, 0].BoardState, Board[1, 1].BoardState, Board[2, 2].BoardState);
            if (result == GameStatus.X_Win || result == GameStatus.O_Win)
            {
                Status = result;
                return;
            }
            else if (result == GameStatus.Tie)
            {
                tieCount++;
            }

            result = testLine(Board[0, 2].BoardState, Board[1, 1].BoardState, Board[2, 0].BoardState);
            if (result == GameStatus.X_Win || result == GameStatus.O_Win)
            {
                Status = result;
                return;
            }
            else if (result == GameStatus.Tie)
            {
                tieCount++;
            }

            //if all 8 possible lines are ties, then the game is tied
            if (tieCount == 8)
                Status = GameStatus.Tie;
            else
                Status = GameStatus.InProgress;
        }

        /**
        Tests a given row of three LocalBoardStates to see if they form a line

        Returns:    GlobalBoardState.X if the row contains all Xs
                    GlobalBoardState.O if the row contains all Os
                    GlobalBoardState.Tie if neither X or O can win the row
                    GloblaBoardState.Open otherwise
        */
        private GameStatus testLine(GlobalBoardState p1, GlobalBoardState p2, GlobalBoardState p3)
        {
            GlobalBoardState[] testArray = { p1, p2, p3 };
            int openCount = 0;
            int xCount = 0;
            int oCount = 0;
            bool containsTie = false;

            foreach(var b in testArray)
            {
                if (b == GlobalBoardState.Open)
                    openCount++;
                else if (b == GlobalBoardState.X)
                    xCount++;
                else if (b == GlobalBoardState.O)
                    oCount++;
                else
                    containsTie = true;
            }

            if (containsTie)
                return GameStatus.Tie;

            if (xCount == 3)
                return GameStatus.X_Win;

            if (oCount == 3)
                return GameStatus.O_Win;

            if ((openCount + xCount == 3) || (openCount + oCount == 3))
                return GameStatus.InProgress;

            return GameStatus.Tie;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            string [,][] outputArray = new string[3,3][];

            for(int row = 0; row < 3; row++)
            {
                for(int column = 0; column < 3; column++)
                {
                    outputArray[row, column] = Board[row, column].outputBoard();
                }
            }

            for(int i = 0; i < 8; i++)
            {
                builder.AppendLine(outputArray[0, 0][i] + "||" + outputArray[0, 1][i] + "||" + outputArray[0,2][i]);
            }
            builder.AppendLine("===========================================");

            for (int i = 0; i < 8; i++)
            {
                builder.AppendLine(outputArray[1, 0][i] + "||" + outputArray[1, 1][i] + "||" + outputArray[1,2][i]);
            }
            builder.AppendLine("===========================================");
            for (int i = 0; i < 8; i++)
            {
                builder.AppendLine(outputArray[2, 0][i] + "||" + outputArray[2, 1][i] + "||" + outputArray[2, 2][i]);
            }

            return builder.ToString();
        }

        //gets the next board as a single number
        //returns 0 if any board is possible
        public virtual int nextBoardNumber()
        {

            if (nextRow < 0 || nextColumn < 0)
            {
                return 0;
            }
            else if (Board[nextRow, nextColumn].BoardState == GlobalBoardState.Open)
            {
                return (nextRow * 3) + nextColumn + 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
