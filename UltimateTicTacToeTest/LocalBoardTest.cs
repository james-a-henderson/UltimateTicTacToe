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
        public void makeMove_InvalidCoordinates_ThrowsException()
        {
            try
            {
                var board = new LocalBoard();
                board.makeMove(-1, 0, Player.X);
                Assert.Fail("Exception not thrown");
            }
            catch(ArgumentOutOfRangeException ae)
            {
                Assert.AreEqual("Position outside of the local board", ae.ParamName);
            }
            catch(Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }

            try
            {
                var board = new LocalBoard();
                board.makeMove(0, 3, Player.X);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException ae)
            {
                Assert.AreEqual("Position outside of the local board", ae.ParamName);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }

            try
            {
                var board = new LocalBoard();
                board.makeMove(3, -1, Player.X);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException ae)
            {
                Assert.AreEqual("Position outside of the local board", ae.ParamName);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void makeMove_AttemptedMoveOnPreviouslyUsedSpace_ThrowsException()
        {
            var board = new LocalBoard();
            board.makeMove(0, 0, Player.X);
            board.makeMove(0, 0, Player.O);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void makeMove_AttemptedMoveOnCompletedBoard_ThrowsException()
        {
            var board = new LocalBoard();
            board.makeMove(0, 0, Player.X);
            board.makeMove(1, 1, Player.X);
            board.makeMove(2, 2, Player.X);
            board.makeMove(0, 1, Player.O);
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
            var board1 = new LocalBoard();
            board1.makeMove(0, 0, Player.X);
            board1.makeMove(0, 1, Player.X);
            board1.makeMove(0, 2, Player.X);
            Assert.AreEqual(GlobalBoardState.X, board1.BoardState);

            var board2 = new LocalBoard();
            board2.makeMove(1, 0, Player.O);
            board2.makeMove(1, 1, Player.O);
            board2.makeMove(1, 2, Player.O);
            Assert.AreEqual(GlobalBoardState.O, board2.BoardState);

            var board3 = new LocalBoard();
            board3.makeMove(2, 0, Player.O);
            board3.makeMove(2, 1, Player.O);
            board3.makeMove(2, 2, Player.O);
            Assert.AreEqual(GlobalBoardState.O, board3.BoardState);

            var board4 = new LocalBoard();
            board4.makeMove(0, 0, Player.O);
            board4.makeMove(1, 0, Player.O);
            board4.makeMove(2, 0, Player.O);
            Assert.AreEqual(GlobalBoardState.O, board4.BoardState);

            var board5 = new LocalBoard();
            board5.makeMove(0, 1, Player.O);
            board5.makeMove(1, 1, Player.O);
            board5.makeMove(2, 1, Player.O);
            Assert.AreEqual(GlobalBoardState.O, board5.BoardState);

            var board6 = new LocalBoard();
            board6.makeMove(0, 2, Player.O);
            board6.makeMove(1, 2, Player.O);
            board6.makeMove(2, 2, Player.O);
            Assert.AreEqual(GlobalBoardState.O, board6.BoardState);

            var board7 = new LocalBoard();
            board7.makeMove(0, 2, Player.O);
            board7.makeMove(1, 1, Player.O);
            board7.makeMove(2, 0, Player.O);
            Assert.AreEqual(GlobalBoardState.O, board7.BoardState);

            var board8 = new LocalBoard();
            board8.makeMove(0, 0, Player.O);
            board8.makeMove(1, 1, Player.O);
            board8.makeMove(2, 2, Player.O);
            Assert.AreEqual(GlobalBoardState.O, board8.BoardState);
        }

        [TestMethod]
        public void verifyGlobalState_XandO_ExtraMoves()
        {
            var board1 = new LocalBoard();
            board1.makeMove(0, 0, Player.X);
            board1.makeMove(0, 1, Player.O);
            board1.makeMove(1, 1, Player.X);
            board1.makeMove(2, 2, Player.X);
            Assert.AreEqual(GlobalBoardState.X, board1.BoardState);

            var board2 = new LocalBoard();
            board2.makeMove(0, 0, Player.O);
            board2.makeMove(0, 1, Player.X);
            board2.makeMove(0, 2, Player.O);
            board2.makeMove(1, 0, Player.X);
            board2.makeMove(1, 1, Player.O);
            board2.makeMove(1, 2, Player.O);
            board2.makeMove(2, 0, Player.X);
            board2.makeMove(2, 1, Player.O);
            board2.makeMove(2, 2, Player.O);
            Assert.AreEqual(GlobalBoardState.O, board2.BoardState);

            var board3 = new LocalBoard();
            board3.makeMove(0, 0, Player.X);
            board3.makeMove(0, 1, Player.X);
            board3.makeMove(1, 1, Player.X);
            board3.makeMove(2, 2, Player.X);
            Assert.AreEqual(GlobalBoardState.X, board3.BoardState);
        }

        [TestMethod]
        public void verifyGlobalState_XandO_MultipleLinesInOneMove()
        {
            var board = new LocalBoard();
            board.makeMove(0, 0, Player.X);
            board.makeMove(0, 2, Player.X);
            board.makeMove(1, 1, Player.X);
            board.makeMove(1, 2, Player.X);
            board.makeMove(2, 2, Player.X);
            Assert.AreEqual(GlobalBoardState.X, board.BoardState);
        }

        public void verifyGlobalState_Tie()
        {
            var board = new LocalBoard();
            board.makeMove(0, 0, Player.X);
            board.makeMove(0, 1, Player.O);
            board.makeMove(0, 2, Player.X);
            board.makeMove(1, 0, Player.O);
            board.makeMove(1, 1, Player.X);
            board.makeMove(1, 2, Player.O);
            board.makeMove(2, 0, Player.O);
            board.makeMove(2, 1, Player.X);
            board.makeMove(2, 2, Player.O);
            Assert.AreEqual(GlobalBoardState.Tie, board.BoardState);
        }
    }
}
