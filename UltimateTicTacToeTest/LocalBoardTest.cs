using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UltimateTicTacToe;

namespace UltimateTicTacToeTest
{
    [TestClass]
    public class LocalBoardTest
    {
        [TestMethod]
        public void makeMove_ValidFirstMoveTest()
        {
            LocalBoardState[,] expected1 = new LocalBoardState[,] { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.Blank }, 
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

            board1.makeMove(0, 0, LocalBoardState.X);
            board2.makeMove(0, 0, LocalBoardState.O);
            board3.makeMove(0, 2, LocalBoardState.X);
            board4.makeMove(1, 1, LocalBoardState.O);
            board5.makeMove(2, 0, LocalBoardState.O);
            board6.makeMove(2, 2, LocalBoardState.X);

            CollectionAssert.AreEqual(expected1, board1.Board);
            CollectionAssert.AreEqual(expected2, board2.Board);
            CollectionAssert.AreEqual(expected3, board3.Board);
            CollectionAssert.AreEqual(expected4, board4.Board);
            CollectionAssert.AreEqual(expected5, board5.Board);
            CollectionAssert.AreEqual(expected6, board6.Board);
        }

        [TestMethod]
        public void makeMove_MultipleValidMoves_Test()
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
            board.makeMove(0, 0, LocalBoardState.X);
            CollectionAssert.AreEqual(expected1, board.Board);
            board.makeMove(1, 1, LocalBoardState.O);
            CollectionAssert.AreEqual(expected2, board.Board);
            board.makeMove(1, 2, LocalBoardState.O);
            CollectionAssert.AreEqual(expected3, board.Board);
            board.makeMove(2, 0, LocalBoardState.O);
            CollectionAssert.AreEqual(expected4, board.Board);
            board.makeMove(2, 2, LocalBoardState.X);
            CollectionAssert.AreEqual(expected5, board.Board);
            board.makeMove(0, 1, LocalBoardState.O);
            CollectionAssert.AreEqual(expected6, board.Board);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void makeMove_BlankMove_ThrowsException()
        {
            LocalBoard board = new LocalBoard();
            board.makeMove(0, 0, LocalBoardState.Blank);
        }

        [TestMethod]
        public void makeMove_InvalidCoordinates_ThrowsException()
        {
            try
            {
                var board = new LocalBoard();
                board.makeMove(-1, 0, LocalBoardState.X);
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
                board.makeMove(0, 3, LocalBoardState.X);
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
                board.makeMove(3, -1, LocalBoardState.X);
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
            board.makeMove(0, 0, LocalBoardState.X);
            board.makeMove(0, 0, LocalBoardState.O);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void makeMove_AttemptedMoveOnCompletedBoard_ThrowsException()
        {
            var board = new LocalBoard();
            board.makeMove(0, 0, LocalBoardState.X);
            board.makeMove(1, 1, LocalBoardState.X);
            board.makeMove(2, 2, LocalBoardState.X);
            board.makeMove(0, 1, LocalBoardState.O);
        }

        [TestMethod]
        public void verifyGlobalState_Open()
        {
            var board = new LocalBoard();
            Assert.AreEqual(GlobalBoardState.Open, board.BoardState);
            board.makeMove(0, 0, LocalBoardState.X);
            Assert.AreEqual(GlobalBoardState.Open, board.BoardState);
            board.makeMove(0, 1, LocalBoardState.X);
            Assert.AreEqual(GlobalBoardState.Open, board.BoardState);
            board.makeMove(0, 2, LocalBoardState.O);
            Assert.AreEqual(GlobalBoardState.Open, board.BoardState);
            board.makeMove(1, 1, LocalBoardState.X);
            Assert.AreEqual(GlobalBoardState.Open, board.BoardState);
        }

        [TestMethod]
        public void verifyGlobalState_XandO_NoExtraMoves()
        {
            var board1 = new LocalBoard();
            board1.makeMove(0, 0, LocalBoardState.X);
            board1.makeMove(0, 1, LocalBoardState.X);
            board1.makeMove(0, 2, LocalBoardState.X);
            Assert.AreEqual(GlobalBoardState.X, board1.BoardState);

            var board2 = new LocalBoard();
            board2.makeMove(1, 0, LocalBoardState.O);
            board2.makeMove(1, 1, LocalBoardState.O);
            board2.makeMove(1, 2, LocalBoardState.O);
            Assert.AreEqual(GlobalBoardState.O, board2.BoardState);

            var board3 = new LocalBoard();
            board3.makeMove(2, 0, LocalBoardState.O);
            board3.makeMove(2, 1, LocalBoardState.O);
            board3.makeMove(2, 2, LocalBoardState.O);
            Assert.AreEqual(GlobalBoardState.O, board3.BoardState);

            var board4 = new LocalBoard();
            board4.makeMove(0, 0, LocalBoardState.O);
            board4.makeMove(1, 0, LocalBoardState.O);
            board4.makeMove(2, 0, LocalBoardState.O);
            Assert.AreEqual(GlobalBoardState.O, board4.BoardState);

            var board5 = new LocalBoard();
            board5.makeMove(0, 1, LocalBoardState.O);
            board5.makeMove(1, 1, LocalBoardState.O);
            board5.makeMove(2, 1, LocalBoardState.O);
            Assert.AreEqual(GlobalBoardState.O, board5.BoardState);

            var board6 = new LocalBoard();
            board6.makeMove(0, 2, LocalBoardState.O);
            board6.makeMove(1, 2, LocalBoardState.O);
            board6.makeMove(2, 2, LocalBoardState.O);
            Assert.AreEqual(GlobalBoardState.O, board6.BoardState);

            var board7 = new LocalBoard();
            board7.makeMove(0, 2, LocalBoardState.O);
            board7.makeMove(1, 1, LocalBoardState.O);
            board7.makeMove(2, 0, LocalBoardState.O);
            Assert.AreEqual(GlobalBoardState.O, board7.BoardState);

            var board8 = new LocalBoard();
            board8.makeMove(0, 0, LocalBoardState.O);
            board8.makeMove(1, 1, LocalBoardState.O);
            board8.makeMove(2, 2, LocalBoardState.O);
            Assert.AreEqual(GlobalBoardState.O, board8.BoardState);
        }

        [TestMethod]
        public void verifyGlobalState_XandO_ExtraMoves()
        {
            var board1 = new LocalBoard();
            board1.makeMove(0, 0, LocalBoardState.X);
            board1.makeMove(0, 1, LocalBoardState.O);
            board1.makeMove(1, 1, LocalBoardState.X);
            board1.makeMove(2, 2, LocalBoardState.X);
            Assert.AreEqual(GlobalBoardState.X, board1.BoardState);

            var board2 = new LocalBoard();
            board2.makeMove(0, 0, LocalBoardState.O);
            board2.makeMove(0, 1, LocalBoardState.X);
            board2.makeMove(0, 2, LocalBoardState.O);
            board2.makeMove(1, 0, LocalBoardState.X);
            board2.makeMove(1, 1, LocalBoardState.O);
            board2.makeMove(1, 2, LocalBoardState.O);
            board2.makeMove(2, 0, LocalBoardState.X);
            board2.makeMove(2, 1, LocalBoardState.O);
            board2.makeMove(2, 2, LocalBoardState.O);
            Assert.AreEqual(GlobalBoardState.O, board2.BoardState);

            var board3 = new LocalBoard();
            board3.makeMove(0, 0, LocalBoardState.X);
            board3.makeMove(0, 1, LocalBoardState.X);
            board3.makeMove(1, 1, LocalBoardState.X);
            board3.makeMove(2, 2, LocalBoardState.X);
            Assert.AreEqual(GlobalBoardState.X, board3.BoardState);
        }

        [TestMethod]
        public void verifyGlobalState_XandO_MultipleLinesInOneMove()
        {
            var board = new LocalBoard();
            board.makeMove(0, 0, LocalBoardState.X);
            board.makeMove(0, 2, LocalBoardState.X);
            board.makeMove(1, 1, LocalBoardState.X);
            board.makeMove(1, 2, LocalBoardState.X);
            board.makeMove(2, 2, LocalBoardState.X);
            Assert.AreEqual(GlobalBoardState.X, board.BoardState);
        }

        public void verifyGlobalState_Tie()
        {
            var board = new LocalBoard();
            board.makeMove(0, 0, LocalBoardState.X);
            board.makeMove(0, 1, LocalBoardState.O);
            board.makeMove(0, 2, LocalBoardState.X);
            board.makeMove(1, 0, LocalBoardState.O);
            board.makeMove(1, 1, LocalBoardState.X);
            board.makeMove(1, 2, LocalBoardState.O);
            board.makeMove(2, 0, LocalBoardState.O);
            board.makeMove(2, 1, LocalBoardState.X);
            board.makeMove(2, 2, LocalBoardState.O);
            Assert.AreEqual(GlobalBoardState.Tie, board.BoardState);
        }
    }
}
