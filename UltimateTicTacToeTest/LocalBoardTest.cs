using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UltimateTicTacToe;

namespace UltimateTicTacToeTest
{
    [TestClass]
    public class LocalBoardTest
    {
        [TestMethod]
        public void makeMove_ValidFirstMove()
        {
            LocalBoardState[,] expected1 = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.Blank }, 
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank }, 
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] expected2 = { { LocalBoardState.O, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] expected3 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.X },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] expected4 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.O, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] expected5 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.O, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] expected6 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.X } };

            LocalBoard board1 = new LocalBoard();
            LocalBoard board2 = new LocalBoard();
            LocalBoard board3 = new LocalBoard();
            LocalBoard board4 = new LocalBoard();
            LocalBoard board5 = new LocalBoard();
            LocalBoard board6 = new LocalBoard();

            board1.makeMove(0, 0, Player.X);
            board2.makeMove(0, 0, Player.O);
            board3.makeMove(0, 2, Player.X);
            board4.makeMove(1, 1, Player.O);
            board5.makeMove(2, 0, Player.O);
            board6.makeMove(2, 2, Player.X);

            CollectionAssert.AreEqual(expected1, board1.Board);
            CollectionAssert.AreEqual(expected2, board2.Board);
            CollectionAssert.AreEqual(expected3, board3.Board);
            CollectionAssert.AreEqual(expected4, board4.Board);
            CollectionAssert.AreEqual(expected5, board5.Board);
            CollectionAssert.AreEqual(expected6, board6.Board);
        }

        [TestMethod]
        public void makeMove_MultipleValidMoves()
        {
            LocalBoardState[,] expected1 = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] expected2 = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.O, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] expected3 = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.O, LocalBoardState.O },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] expected4 = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.O, LocalBoardState.O },
                                                { LocalBoardState.O, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] expected5 = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.O, LocalBoardState.O },
                                                { LocalBoardState.O, LocalBoardState.Blank, LocalBoardState.X } };
            LocalBoardState[,] expected6 = { { LocalBoardState.X, LocalBoardState.O, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.O, LocalBoardState.O },
                                                { LocalBoardState.O, LocalBoardState.Blank, LocalBoardState.X } };

            LocalBoard board = new LocalBoard();
            board.makeMove(0, 0, Player.X);
            CollectionAssert.AreEqual(expected1, board.Board);
            board.makeMove(1, 1, Player.O);
            CollectionAssert.AreEqual(expected2, board.Board);
            board.makeMove(1, 2, Player.O);
            CollectionAssert.AreEqual(expected3, board.Board);
            board.makeMove(2, 0, Player.O);
            CollectionAssert.AreEqual(expected4, board.Board);
            board.makeMove(2, 2, Player.X);
            CollectionAssert.AreEqual(expected5, board.Board);
            board.makeMove(0, 1, Player.O);
            CollectionAssert.AreEqual(expected6, board.Board);
        }

        [TestMethod]
        public void makeMove_InvalidCoordinates_ReturnsError()
        {

            var board = new LocalBoard();
            var result1 = board.makeMove(-1, 0, Player.X);
            Assert.AreEqual(MoveResult.SpaceOutOfRange, result1);

            var result2 = board.makeMove(3, 0, Player.X);
            Assert.AreEqual(MoveResult.SpaceOutOfRange, result2);

            var result3 = board.makeMove(0, -1, Player.X);
            Assert.AreEqual(MoveResult.SpaceOutOfRange, result3);

            var result4 = board.makeMove(0, 3, Player.X);
            Assert.AreEqual(MoveResult.SpaceOutOfRange, result4);
        }

        [TestMethod]
        public void makeMove_AttemptedMoveOnPreviouslyUsedSpace_ReturnsError()
        {
            var board = new LocalBoard();
            board.makeMove(0, 0, Player.X);
            var result = board.makeMove(0, 0, Player.O);
            Assert.AreEqual(MoveResult.SpaceAlreadyUsed, result);
        }

        [TestMethod]
        public void makeMove_AttemptedMoveOnCompletedBoard_ReturnsError()
        {
            var board = new LocalBoard();
            board.makeMove(0, 0, Player.X);
            board.makeMove(1, 1, Player.X);
            board.makeMove(2, 2, Player.X);
            var result = board.makeMove(0, 1, Player.O);
            Assert.AreEqual(MoveResult.BoardAlreadyCompleted, result);
        }

        [TestMethod]
        public void verifyGlobalState_Open()
        {
            var board = new LocalBoard();
            Assert.AreEqual(GlobalBoardState.Open, board.BoardState);
            board.makeMove(0, 0, Player.X);
            Assert.AreEqual(GlobalBoardState.Open, board.BoardState);
            board.makeMove(0, 1, Player.X);
            Assert.AreEqual(GlobalBoardState.Open, board.BoardState);
            board.makeMove(0, 2, Player.O);
            Assert.AreEqual(GlobalBoardState.Open, board.BoardState);
            board.makeMove(1, 1, Player.X);
            Assert.AreEqual(GlobalBoardState.Open, board.BoardState);
        }

        [TestMethod]
        public void verifyGlobalState_XandO_NoExtraMoves()
        {
            LocalBoardState[,] input1 = { { LocalBoardState.X, LocalBoardState.X, LocalBoardState.X },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            var board1 = new LocalBoard(input1);
            Assert.AreEqual(GlobalBoardState.X, board1.BoardState);

            LocalBoardState[,] input2 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.O, LocalBoardState.O, LocalBoardState.O },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            var board2 = new LocalBoard(input2);
            Assert.AreEqual(GlobalBoardState.O, board2.BoardState);

            LocalBoardState[,] input3 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.O, LocalBoardState.O, LocalBoardState.O } };
            var board3 = new LocalBoard(input3);
            Assert.AreEqual(GlobalBoardState.O, board3.BoardState);

            LocalBoardState[,] input4 = { { LocalBoardState.O, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.O, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.O, LocalBoardState.Blank, LocalBoardState.Blank } };
            var board4 = new LocalBoard(input4);
            Assert.AreEqual(GlobalBoardState.O, board4.BoardState);

            LocalBoardState[,] input5 = { { LocalBoardState.Blank, LocalBoardState.X, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.X, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.X, LocalBoardState.Blank } };
            var board5 = new LocalBoard(input5);
            Assert.AreEqual(GlobalBoardState.X, board5.BoardState);

            LocalBoardState[,] input6 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.O },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.O },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.O } };
            var board6 = new LocalBoard(input6);
            Assert.AreEqual(GlobalBoardState.O, board6.BoardState);

            LocalBoardState[,] input7 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.X },
                                                { LocalBoardState.Blank, LocalBoardState.X, LocalBoardState.Blank },
                                                { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.Blank } };
            var board7 = new LocalBoard(input7);
            Assert.AreEqual(GlobalBoardState.X, board7.BoardState);

            LocalBoardState[,] input8 = { { LocalBoardState.O, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.O, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.O } };
            var board8 = new LocalBoard(input8);
            Assert.AreEqual(GlobalBoardState.O, board8.BoardState);
        }

        [TestMethod]
        public void verifyGlobalState_XandO_ExtraMoves()
        {
            LocalBoardState[,] input1 = { { LocalBoardState.X, LocalBoardState.O, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.X, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.X } };

            var board1 = new LocalBoard(input1);
            Assert.AreEqual(GlobalBoardState.X, board1.BoardState);

            LocalBoardState[,] input2 = { { LocalBoardState.O, LocalBoardState.X, LocalBoardState.O },
                                                { LocalBoardState.X, LocalBoardState.O, LocalBoardState.O },
                                                { LocalBoardState.X, LocalBoardState.O, LocalBoardState.O } };
            var board2 = new LocalBoard(input2);
            Assert.AreEqual(GlobalBoardState.O, board2.BoardState);

            LocalBoardState[,] input3 = { { LocalBoardState.X, LocalBoardState.X, LocalBoardState.X },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.X } };
            var board3 = new LocalBoard(input3);
            Assert.AreEqual(GlobalBoardState.X, board3.BoardState);
        }

        [TestMethod]
        public void verifyGlobalState_XandO_MultipleLinesInOneMove()
        {
            LocalBoardState[,] input = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.X },
                                                { LocalBoardState.Blank, LocalBoardState.X, LocalBoardState.X },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.X } };
            var board = new LocalBoard(input);
            Assert.AreEqual(GlobalBoardState.X, board.BoardState);
        }

        public void verifyGlobalState_Tie()
        {
            LocalBoardState[,] input = { { LocalBoardState.X, LocalBoardState.O, LocalBoardState.X },
                                                { LocalBoardState.O, LocalBoardState.X, LocalBoardState.O },
                                                { LocalBoardState.O, LocalBoardState.X, LocalBoardState.O } };
            var board = new LocalBoard(input);
            Assert.AreEqual(GlobalBoardState.Tie, board.BoardState);
        }

        [TestMethod]
        public void outputBoard_EmptyBoard()
        {
            var board = new LocalBoard();

            string[] expected = new string[8];
            expected[0] = "             ";
            expected[1] = "    |   |    ";
            expected[2] = " ___|___|___ ";
            expected[3] = "    |   |    ";
            expected[4] = " ___|___|___ ";
            expected[5] = "    |   |    ";
            expected[6] = "    |   |    ";
            expected[7] = "             ";

            CollectionAssert.AreEqual(expected, board.outputBoard());
        }

        [TestMethod]
        public void outputBoard_IncompleteBoard()
        {
            LocalBoardState[,] input = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.X },
                                                { LocalBoardState.Blank, LocalBoardState.X, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.O } };
            var board = new LocalBoard(input);

            string[] expected = new string[8];
            expected[0] = "             ";
            expected[1] = "  X |   | X  ";
            expected[2] = " ___|___|___ ";
            expected[3] = "    | X |    ";
            expected[4] = " ___|___|___ ";
            expected[5] = "    |   | O  ";
            expected[6] = "    |   |    ";
            expected[7] = "             ";

            CollectionAssert.AreEqual(expected, board.outputBoard());
        }

        [TestMethod]
        public void outputBoard_X()
        {
            LocalBoardState[,] input = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.X },
                                                { LocalBoardState.Blank, LocalBoardState.X, LocalBoardState.Blank },
                                                { LocalBoardState.O, LocalBoardState.O, LocalBoardState.X } };
            var board = new LocalBoard(input);

            string[] expected = new string[8];
            expected[0] = "  __     __  ";
            expected[1] = "  \\ \\   / /  ";
            expected[2] = "   \\ \\ / /   ";
            expected[3] = "    \\ V /    ";
            expected[4] = "     > <     ";
            expected[5] = "    / . \\    ";
            expected[6] = "   / / \\ \\   ";
            expected[7] = "  /_/   \\_\\  ";
        }

        [TestMethod]
        public void outputBoard_O()
        {
            LocalBoardState[,] input = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.X },
                                                { LocalBoardState.Blank, LocalBoardState.X, LocalBoardState.Blank },
                                                { LocalBoardState.O, LocalBoardState.O, LocalBoardState.O } };
            var board = new LocalBoard(input);

            string[] expected = new string[8];
            expected[0] = "   _______   ";
            expected[1] = "  / _____ \\  ";
            expected[2] = " | |     | | ";
            expected[3] = " | |     | | ";
            expected[4] = " | |     | | ";
            expected[5] = " | |_____| | ";
            expected[6] = "  \\_______/  ";
            expected[7] = "             ";

            CollectionAssert.AreEqual(expected, board.outputBoard());
        }

        [TestMethod]
        public void outputBoard_Tie()
        {
            LocalBoardState[,] input = { { LocalBoardState.X, LocalBoardState.O, LocalBoardState.X },
                                                { LocalBoardState.X, LocalBoardState.X, LocalBoardState.O },
                                                { LocalBoardState.O, LocalBoardState.X, LocalBoardState.O } };
            var board = new LocalBoard(input);

            string[] expected = new string[8];
            expected[0] = "  _________  ";
            expected[1] = " |         | ";
            expected[2] = " |__     __| ";
            expected[3] = "    |   |    ";
            expected[4] = "    |   |    ";
            expected[5] = "    |   |    ";
            expected[6] = "    |___|    ";
            expected[7] = "             ";
        }
    }
}
