using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UltimateTicTacToe;

namespace UltimateTicTacToeTest
{
    [TestClass]
    public class GlobalBoardTest
    {
        LocalBoardState[,] emptyBoard = new LocalBoardState[3, 3];

        [TestMethod]
        public void makeMove_ValidFirstMove()
        {
            LocalBoardState[,] testBoard = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };

            var board1 = new GlobalBoard();
            board1.makeMove(0, 0, 0, 0);
            CollectionAssert.AreEqual(testBoard, board1.Board[0, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board1.Board[0, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board1.Board[0, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board1.Board[1, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board1.Board[1, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board1.Board[1, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board1.Board[2, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board1.Board[2, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board1.Board[2, 2].Board);

            var board2 = new GlobalBoard();
            board2.makeMove(2, 2, 0, 0);
            CollectionAssert.AreEqual(emptyBoard, board2.Board[0, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board2.Board[0, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board2.Board[0, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board2.Board[1, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board2.Board[1, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board2.Board[1, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board2.Board[2, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board2.Board[2, 1].Board);
            CollectionAssert.AreEqual(testBoard, board2.Board[2, 2].Board);
        }

        [TestMethod]
        public void makeMove_multipleValidMoves()
        {
            LocalBoardState[,] testBoard1 = { { LocalBoardState.X, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] testBoard2 = { { LocalBoardState.X, LocalBoardState.O, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] testBoard3 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.X, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] testBoard4 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.O, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };
            LocalBoardState[,] testBoard5 = { { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank },
                                                { LocalBoardState.Blank, LocalBoardState.O, LocalBoardState.X },
                                                { LocalBoardState.Blank, LocalBoardState.Blank, LocalBoardState.Blank } };

            var board = new GlobalBoard();
            board.makeMove(0, 0, 0, 0);
            CollectionAssert.AreEqual(testBoard1, board.Board[0, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[0, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[0, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 2].Board);

            board.makeMove(0, 0, 0, 1);
            CollectionAssert.AreEqual(testBoard2, board.Board[0, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[0, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[0, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 2].Board);

            board.makeMove(0, 1, 1, 1);
            CollectionAssert.AreEqual(testBoard2, board.Board[0, 0].Board);
            CollectionAssert.AreEqual(testBoard3, board.Board[0, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[0, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 2].Board);

            board.makeMove(1, 1, 1, 1);
            CollectionAssert.AreEqual(testBoard2, board.Board[0, 0].Board);
            CollectionAssert.AreEqual(testBoard3, board.Board[0, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[0, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 0].Board);
            CollectionAssert.AreEqual(testBoard4, board.Board[1, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 2].Board);

            board.makeMove(1, 1, 1, 2);
            CollectionAssert.AreEqual(testBoard2, board.Board[0, 0].Board);
            CollectionAssert.AreEqual(testBoard3, board.Board[0, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[0, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 0].Board);
            CollectionAssert.AreEqual(testBoard5, board.Board[1, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[1, 2].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 0].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 1].Board);
            CollectionAssert.AreEqual(emptyBoard, board.Board[2, 2].Board);
        }

        [TestMethod]
        public void makeMove_VerifyPlayersAlternate()
        {
            var board1 = new GlobalBoard();
            Assert.AreEqual(Player.X, board1.currentPlayer);
            board1.makeMove(0, 0, 0, 0);
            Assert.AreEqual(Player.O, board1.currentPlayer);
            board1.makeMove(0, 0, 0, 1);
            Assert.AreEqual(Player.X, board1.currentPlayer);
            board1.makeMove(0, 1, 0, 1);
            Assert.AreEqual(Player.O, board1.currentPlayer);
            board1.makeMove(0, 1, 0, 2);
            Assert.AreEqual(Player.X, board1.currentPlayer);

            var board2 = new GlobalBoard(Player.O);
            Assert.AreEqual(Player.O, board2.currentPlayer);
            board2.makeMove(0, 0, 0, 0);
            Assert.AreEqual(Player.X, board2.currentPlayer);
            board2.makeMove(0, 0, 0, 1);
            Assert.AreEqual(Player.O, board2.currentPlayer);
            board2.makeMove(0, 1, 0, 1);
            Assert.AreEqual(Player.X, board2.currentPlayer);
            board2.makeMove(0, 1, 0, 2);
            Assert.AreEqual(Player.O, board2.currentPlayer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void makeMove_IncorrectBoardSelection_ThrowsException()
        {
            var board = new GlobalBoard();
            board.makeMove(0, 0, 0, 2);
            board.makeMove(0, 0, 0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void makeMove_AttemptedMoveOnPreviouslyUsedSpace_ThrowsException()
        {
            var board = new GlobalBoard();
            board.makeMove(0, 0, 0, 0);
            board.makeMove(0, 0, 0, 0);
        }

        [TestMethod]
        public void makeMove_MoveAnywhere_OnCompletedBoard_ThrowsNoExceptions()
        {
            var board = new GlobalBoard();
            board.makeMove(0, 0, 1, 1);
            board.makeMove(1, 1, 0, 0);
            board.makeMove(0, 0, 1, 0);
            board.makeMove(1, 0, 0, 0);
            board.makeMove(0, 0, 1, 2);
            board.makeMove(1, 2, 0, 0);
            board.makeMove(2, 2, 0, 0);
            //No exceptions thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void makeMove_MoveOnCompletedBoard_ThrowsException()
        {
            var board = new GlobalBoard();
            board.makeMove(0, 0, 1, 1);
            board.makeMove(1, 1, 0, 0);
            board.makeMove(0, 0, 1, 0);
            board.makeMove(1, 0, 0, 0);
            board.makeMove(0, 0, 1, 2);
            board.makeMove(1, 2, 0, 0);
            board.makeMove(0, 0, 2, 2);
        }

        [TestMethod]
        public void makeMove_GlobalCoordinatesOutOfBounds_ThrowsException()
        {
            try
            {
                var board = new GlobalBoard();
                board.makeMove(-1,0,0,0);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException ae)
            {
                Assert.AreEqual("Board selection is out of range", ae.ParamName);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }

            try
            {
                var board = new GlobalBoard();
                board.makeMove(3, 0, 0, 0);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException ae)
            {
                Assert.AreEqual("Board selection is out of range", ae.ParamName);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }

            try
            {
                var board = new GlobalBoard();
                board.makeMove(0, -1, 0, 0);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException ae)
            {
                Assert.AreEqual("Board selection is out of range", ae.ParamName);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }

            try
            {
                var board = new GlobalBoard();
                board.makeMove(0, 3, 0, 0);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException ae)
            {
                Assert.AreEqual("Board selection is out of range", ae.ParamName);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }
        }
    }
}
