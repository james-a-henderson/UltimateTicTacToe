using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Text;
using System.Collections.Generic;
using UltimateTicTacToe;

namespace UltimateTicTacToeTest
{
    [TestClass]
    public class GlobalBoardTest
    {
        LocalBoardState[,] emptyBoard;
        Mock<LocalBoard> xMock;
        Mock<LocalBoard> oMock;
        Mock<LocalBoard> tieMock;
        Mock<LocalBoard> openMock;

        [TestInitialize]
        public void TestInitialize()
        {
            emptyBoard = new LocalBoardState[3, 3];

            var xBoardString = new string[8];
            xBoardString[0] = "  __     __  ";
            xBoardString[1] = "  \\ \\   / /  ";
            xBoardString[2] = "   \\ \\ / /   ";
            xBoardString[3] = "    \\ V /    ";
            xBoardString[4] = "     > <     ";
            xBoardString[5] = "    / . \\    ";
            xBoardString[6] = "   / / \\ \\   ";
            xBoardString[7] = "  /_/   \\_\\  ";

            xMock = new Mock<LocalBoard>();
            xMock.Setup(x => x.BoardState).Returns(GlobalBoardState.X);
            xMock.Setup(x => x.outputBoard()).Returns(xBoardString);

            var oBoardString = new string[8];
            oBoardString[0] = "   _______   ";
            oBoardString[1] = "  / _____ \\  ";
            oBoardString[2] = " | |     | | ";
            oBoardString[3] = " | |     | | ";
            oBoardString[4] = " | |     | | ";
            oBoardString[5] = " | |_____| | ";
            oBoardString[6] = "  \\_______/  ";
            oBoardString[7] = "             ";

            oMock = new Mock<LocalBoard>();
            oMock.Setup(x => x.BoardState).Returns(GlobalBoardState.O);
            oMock.Setup(x => x.outputBoard()).Returns(oBoardString);

            var tieBoardString = new string[8];
            tieBoardString[0] = "  _________  ";
            tieBoardString[1] = " |         | ";
            tieBoardString[2] = " |__     __| ";
            tieBoardString[3] = "    |   |    ";
            tieBoardString[4] = "    |   |    ";
            tieBoardString[5] = "    |   |    ";
            tieBoardString[6] = "    |___|    ";
            tieBoardString[7] = "             ";

            tieMock = new Mock<LocalBoard>();
            tieMock.Setup(x => x.BoardState).Returns(GlobalBoardState.Tie);
            tieMock.Setup(x => x.outputBoard()).Returns(tieBoardString);

            var openBoardString = new string[8];
            openBoardString[0] = "             ";
            openBoardString[1] = "  X |   | X  ";
            openBoardString[2] = " ___|___|___ ";
            openBoardString[3] = "    | X |    ";
            openBoardString[4] = " ___|___|___ ";
            openBoardString[5] = "    |   | O  ";
            openBoardString[6] = "    |   |    ";
            openBoardString[7] = "             ";

            openMock = new Mock<LocalBoard>();
            openMock.Setup(x => x.BoardState).Returns(GlobalBoardState.Open);
            openMock.Setup(x => x.outputBoard()).Returns(openBoardString);
        }

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
        public void makeMove_moveMadeAfterGameCompleted_ThrowsException()
        {
            try
            {
                LocalBoard[,] localBoards = { { xMock.Object, xMock.Object, xMock.Object},
                                            { openMock.Object, openMock.Object, openMock.Object },
                                            { openMock.Object, openMock.Object, openMock.Object }};
                var board = new GlobalBoard(Player.X, localBoards);
                board.makeMove(0, 1, 0, 0);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual("Cannot make move on completed board", ae.Message);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }

            try
            {
                LocalBoard[,] localBoards = { { openMock.Object, tieMock.Object, oMock.Object},
                                            { tieMock.Object, tieMock.Object, oMock.Object},
                                             {tieMock.Object, tieMock.Object, oMock.Object } };
                var board = new GlobalBoard(Player.X, localBoards);
                board.makeMove(0, 0, 0, 0);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual("Cannot make move on completed board", ae.Message);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }

            try
            {
                LocalBoard[,] localBoards = { { openMock.Object, tieMock.Object, oMock.Object},
                                            { tieMock.Object, tieMock.Object, oMock.Object},
                                             {tieMock.Object, tieMock.Object, tieMock.Object } };
                var board = new GlobalBoard(Player.X, localBoards);
                board.makeMove(0, 0, 0, 0);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentException ae)
            {
                Assert.AreEqual("Cannot make move on completed board", ae.Message);
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }
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

        [TestMethod]
        public void verifyStatus_XWins()
        {
            LocalBoard[,] localBoards1 = { { xMock.Object, xMock.Object, xMock.Object},
                                            { openMock.Object, openMock.Object, openMock.Object },
                                            { openMock.Object, openMock.Object, openMock.Object }};

            var board1 = new GlobalBoard(Player.X, localBoards1);
            Assert.AreEqual(GameStatus.X_Win, board1.Status);

            LocalBoard[,] localBoards2 = { { xMock.Object, oMock.Object, oMock.Object},
                                            {oMock.Object, xMock.Object, oMock.Object },
                                            {oMock.Object, oMock.Object, xMock.Object }};
            var board2 = new GlobalBoard(Player.X, localBoards2);
            Assert.AreEqual(GameStatus.X_Win, board2.Status);
        }

        [TestMethod]
        public void verifyStatus_OWins()
        {

            LocalBoard[,] localBoards1 = { { tieMock.Object, tieMock.Object, oMock.Object},
                                            { tieMock.Object, tieMock.Object, oMock.Object},
                                             {tieMock.Object, tieMock.Object, oMock.Object } };

            var board1 = new GlobalBoard(Player.X, localBoards1);
            Assert.AreEqual(GameStatus.O_Win, board1.Status);

            LocalBoard[,] localBoards2 = { { xMock.Object, openMock.Object, oMock.Object},
                                            { oMock.Object, oMock.Object, xMock.Object},
                                             {oMock.Object, tieMock.Object, openMock.Object } };

            var board2 = new GlobalBoard(Player.X, localBoards2);
            Assert.AreEqual(GameStatus.O_Win, board2.Status);
        }

        [TestMethod]
        public void verifyStatus_Tie()
        {
            LocalBoard[,] localBoards1 = { { tieMock.Object, tieMock.Object, oMock.Object},
                                            { tieMock.Object, tieMock.Object, oMock.Object},
                                             {tieMock.Object, tieMock.Object, tieMock.Object } };

            var board1 = new GlobalBoard(Player.X, localBoards1);
            Assert.AreEqual(GameStatus.Tie, board1.Status);

            LocalBoard[,] localBoards2 = { { xMock.Object, openMock.Object, oMock.Object},
                                            { oMock.Object, oMock.Object, xMock.Object},
                                             {xMock.Object, tieMock.Object, openMock.Object } };

            var board2 = new GlobalBoard(Player.X, localBoards2);
            Assert.AreEqual(GameStatus.Tie, board2.Status);
        }

        [TestMethod]
        public void verifyStatus_InProgress()
        {
            LocalBoard[,] localBoards1 = { { tieMock.Object, tieMock.Object, oMock.Object},
                                            { tieMock.Object, tieMock.Object, oMock.Object},
                                             {tieMock.Object, tieMock.Object, openMock.Object } };

            var board1 = new GlobalBoard(Player.X, localBoards1);
            Assert.AreEqual(GameStatus.InProgress, board1.Status);

            LocalBoard[,] localBoards2 = { { xMock.Object, openMock.Object, xMock.Object},
                                            { oMock.Object, oMock.Object, xMock.Object},
                                             {xMock.Object, tieMock.Object, openMock.Object } };

            var board2 = new GlobalBoard(Player.X, localBoards2);
            Assert.AreEqual(GameStatus.InProgress, board2.Status);
        }

        [TestMethod]
        public void verifyStatus_StartsInProgress()
        {
            var board = new GlobalBoard();
            Assert.AreEqual(GameStatus.InProgress, board.Status);
        }

        [TestMethod]
        public void toString_Test()
        {
            var expectedBuilder = new StringBuilder();
            expectedBuilder.AppendLine("             ||             ||  _________  ");
            expectedBuilder.AppendLine("  X |   | X  ||  X |   | X  || |         | ");
            expectedBuilder.AppendLine(" ___|___|___ || ___|___|___ || |__     __| ");
            expectedBuilder.AppendLine("    | X |    ||    | X |    ||    |   |    ");
            expectedBuilder.AppendLine(" ___|___|___ || ___|___|___ ||    |   |    ");
            expectedBuilder.AppendLine("    |   | O  ||    |   | O  ||    |   |    ");
            expectedBuilder.AppendLine("    |   |    ||    |   |    ||    |___|    ");
            expectedBuilder.AppendLine("             ||             ||             ");
            expectedBuilder.AppendLine("===========================================");
            expectedBuilder.AppendLine("             ||             ||  __     __  ");
            expectedBuilder.AppendLine("  X |   | X  ||  X |   | X  ||  \\ \\   / /  ");
            expectedBuilder.AppendLine(" ___|___|___ || ___|___|___ ||   \\ \\ / /   ");
            expectedBuilder.AppendLine("    | X |    ||    | X |    ||    \\ V /    ");
            expectedBuilder.AppendLine(" ___|___|___ || ___|___|___ ||     > <     ");
            expectedBuilder.AppendLine("    |   | O  ||    |   | O  ||    / . \\    ");
            expectedBuilder.AppendLine("    |   |    ||    |   |    ||   / / \\ \\   ");
            expectedBuilder.AppendLine("             ||             ||  /_/   \\_\\  ");
            expectedBuilder.AppendLine("===========================================");
            expectedBuilder.AppendLine("             ||             ||   _______   ");
            expectedBuilder.AppendLine("  X |   | X  ||  X |   | X  ||  / _____ \\  ");
            expectedBuilder.AppendLine(" ___|___|___ || ___|___|___ || | |     | | ");
            expectedBuilder.AppendLine("    | X |    ||    | X |    || | |     | | ");
            expectedBuilder.AppendLine(" ___|___|___ || ___|___|___ || | |     | | ");
            expectedBuilder.AppendLine("    |   | O  ||    |   | O  || | |_____| | ");
            expectedBuilder.AppendLine("    |   |    ||    |   |    ||  \\_______/  ");
            expectedBuilder.AppendLine("             ||             ||             ");

            LocalBoard[,] localBoards = { { openMock.Object, openMock.Object, tieMock.Object},
                                            {openMock.Object, openMock.Object, xMock.Object },
                                            {openMock.Object, openMock.Object, oMock.Object }};
            var board = new GlobalBoard(Player.X, localBoards);
            Assert.AreEqual(expectedBuilder.ToString(), board.ToString());
        }

        [TestMethod]
        public void nextBoardNumber_StartWithAnyBoard()
        {
            LocalBoard[,] localBoards = { { openMock.Object, openMock.Object, openMock.Object},
                                            { openMock.Object, openMock.Object, openMock.Object },
                                            { openMock.Object, openMock.Object, openMock.Object }};

            var board = new GlobalBoard(Player.X, localBoards);

            Assert.AreEqual(0, board.nextBoardNumber());
        }

        [TestMethod]
        public void nextBoardNumber_CorrectlyIdentifyNextBoard()
        {
            LocalBoard[,] localBoards = { { openMock.Object, openMock.Object, openMock.Object},
                                            { openMock.Object, openMock.Object, openMock.Object },
                                            { openMock.Object, openMock.Object, openMock.Object }};

            var board = new GlobalBoard(Player.X, localBoards);
            board.makeMove(0, 0, 0, 0);
            Assert.AreEqual(1, board.nextBoardNumber());

            board.makeMove(0, 0, 0, 1);
            Assert.AreEqual(2, board.nextBoardNumber());

            board.makeMove(0, 1, 0, 2);
            Assert.AreEqual(3, board.nextBoardNumber());

            board.makeMove(0, 2, 1, 1);
            Assert.AreEqual(5, board.nextBoardNumber());

            board.makeMove(1, 1, 1, 2);
            Assert.AreEqual(6, board.nextBoardNumber());

            board.makeMove(1, 2, 2, 0);
            Assert.AreEqual(7, board.nextBoardNumber());

            board.makeMove(2, 0, 2, 2);
            Assert.AreEqual(9, board.nextBoardNumber());
        }

        [TestMethod]
        public void nextBoardNumber_AnyBoard_OnCompletedBoard()
        {
            LocalBoard[,] localBoards = { { openMock.Object, openMock.Object, openMock.Object},
                                            { xMock.Object, oMock.Object, tieMock.Object },
                                            { openMock.Object, openMock.Object, openMock.Object }};

            var board = new GlobalBoard(Player.X, localBoards);

            board.makeMove(0, 0, 1, 0);
            Assert.AreEqual(0, board.nextBoardNumber());
            board.makeMove(0, 0, 1, 1);
            Assert.AreEqual(0, board.nextBoardNumber());
            board.makeMove(0, 0, 1, 2);
            Assert.AreEqual(0, board.nextBoardNumber());
        }
    }
}
