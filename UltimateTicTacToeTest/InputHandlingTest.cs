﻿using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UltimateTicTacToe;
using System.Text;

namespace UltimateTicTacToeTest
{
    [TestClass]
    public class InputHandlingTest
    {

        Mock<GlobalBoard> mockBoard;

        [TestInitialize]
        public void TestInitialize()
        {
            mockBoard = new Mock<GlobalBoard>();
            mockBoard.Setup(x => x.ToString()).Returns("Test");
        }

        [TestMethod]
        public void handleInput_makeMove_ValidInput()
        {
            mockBoard.SetupSequence(x => x.currentPlayer)
                .Returns(Player.X)
                .Returns(Player.O)
                .Returns(Player.X);

            string result1 = InputHandling.sendInput("1 1", mockBoard.Object);
            mockBoard.Verify(x => x.makeMove(0, 0, 0, 0), Times.Once);
            Assert.AreEqual("Test\r\nNext Board: Any Board\r\nX's Move: ", result1);

            string result2 = InputHandling.sendInput("1 4", mockBoard.Object);
            mockBoard.Verify(x => x.makeMove(0, 0, 1, 0), Times.Once);
            Assert.AreEqual("Test\r\nNext Board: Any Board\r\nO's Move: ", result2);

            string result3 = InputHandling.sendInput("4 6", mockBoard.Object);
            mockBoard.Verify(x => x.makeMove(1, 0, 1, 2), Times.Once);
            Assert.AreEqual("Test\r\nNext Board: Any Board\r\nX's Move: ", result3);
        }

        [TestMethod]
        public void handleInput_makeMove_InvalidCoordinates()
        {
            mockBoard.Setup(x => x.currentPlayer).Returns(Player.X);

            string expected = "Test\r\nMove is invalid! Please Enter valid location.\r\nNext Board: Any Board\r\nX's Move: ";
            Assert.AreEqual(expected, InputHandling.sendInput("0 1", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("1 0", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("10 1", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("1 10", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("0 0", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("40 11", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("-1 4", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("4 -1", mockBoard.Object));
        }

        [TestMethod]
        public void handleInput_makeMove_SpaceAlreadyPlayed()
        {
            mockBoard.Setup(x => x.currentPlayer).Returns(Player.X);
            mockBoard.Setup(x => x.makeMove(0, 0, 0, 0)).Returns(MoveResult.SpaceAlreadyUsed);

            string expected = "Test\r\nSpace already used, choose another location.\r\nNext Board: Any Board\r\nX's Move: ";
            Assert.AreEqual(expected, InputHandling.sendInput("1 1", mockBoard.Object));
        }

        [TestMethod]
        public void handleInput_makeMove_LocalBoardCompleted()
        {
            mockBoard.Setup(x => x.currentPlayer).Returns(Player.X);
            mockBoard.Setup(x => x.makeMove(0, 0, It.IsAny<int>(), It.IsAny<int>())).Returns(MoveResult.BoardAlreadyCompleted);

            string expected = "Test\r\nSelected board is completed. Select another location.\r\nNext Board: Any Board\r\nX's Move: ";
            Assert.AreEqual(expected, InputHandling.sendInput("1 1", mockBoard.Object));
        }

        [TestMethod]
        public void handleInput_makeMove_RequiredBoardNotSelected()
        {
            mockBoard.Setup(x => x.currentPlayer).Returns(Player.X);
            mockBoard.Setup(x => x.makeMove(0, 0, It.IsAny<int>(), It.IsAny<int>())).Returns(MoveResult.RequiredBoardNotSelected);

            string expected = "Test\r\nNot going to requried board. Select another location.\r\nNext Board: Any Board\r\nX's Move: ";
            Assert.AreEqual(expected, InputHandling.sendInput("1 1", mockBoard.Object));
        }

        [TestMethod]
        public void handleInput_invalidInput()
        {
            mockBoard.Setup(x => x.currentPlayer).Returns(Player.X);

            string expected = "Test\r\nInvalid Input. Enter valid command, or type ? for help.\r\nNext Board: Any Board\r\nX's Move: ";
            Assert.AreEqual(expected, InputHandling.sendInput("sdffd", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("one two", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("one", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput(" ", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("1 2 4", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("12", mockBoard.Object));
            Assert.AreEqual(expected, InputHandling.sendInput("", mockBoard.Object));
        }

        [TestMethod]
        public void handleInput_help()
        {
            mockBoard.Setup(x => x.currentPlayer).Returns(Player.X);

            var expected = new StringBuilder();
            expected.AppendLine("Test");
            expected.AppendLine("To make a move, type in '1 2', where the first number is the board you want to move to, and the second number is the specific square you want to move on");
            expected.AppendLine("To exit, type exit");
            expected.AppendLine("Next Board: Any Board");
            expected.Append("X's Move: ");

            Assert.AreEqual(expected.ToString(), InputHandling.sendInput("?", mockBoard.Object));
            Assert.AreEqual(expected.ToString(), InputHandling.sendInput("help", mockBoard.Object));
            Assert.AreEqual(expected.ToString(), InputHandling.sendInput("HELP", mockBoard.Object));
            Assert.AreEqual(expected.ToString(), InputHandling.sendInput("hElP", mockBoard.Object));
        }

        [TestMethod]
        public void handleInput_exit()
        {
            string expected = "Thank you for playing!";

            Assert.AreEqual(expected, InputHandling.sendInput("exit", mockBoard.Object));
            Assert.IsTrue(mockBoard.Object.Exiting);

            mockBoard.Object.Exiting = false;
            Assert.AreEqual(expected, InputHandling.sendInput("quit", mockBoard.Object));
            Assert.IsTrue(mockBoard.Object.Exiting);

            mockBoard.Object.Exiting = false;
            Assert.AreEqual(expected, InputHandling.sendInput("EXIT", mockBoard.Object));
            Assert.IsTrue(mockBoard.Object.Exiting);

            mockBoard.Object.Exiting = false;
            Assert.AreEqual(expected, InputHandling.sendInput("QUIT", mockBoard.Object));
            Assert.IsTrue(mockBoard.Object.Exiting);
        }

        [TestMethod]
        public void initialBoardState_CorrectOutput()
        {
            mockBoard.Setup(x => x.currentPlayer).Returns(Player.X);

            var expected = "Test\r\nNext Board: Any Board\r\nX's Move: ";
            Assert.AreEqual(expected, InputHandling.initialBoardState(mockBoard.Object));
        }

        [TestMethod]
        public void nextBoard_AnyBoard()
        {
            mockBoard.Setup(x => x.currentPlayer).Returns(Player.X);
            mockBoard.Setup(x => x.nextBoardNumber()).Returns(0);

            var expected = "Test\r\nNext Board: Any Board\r\nX's Move: ";
            Assert.AreEqual(expected, InputHandling.sendInput("1 1", mockBoard.Object));
        }

        [TestMethod]
        public void nextBoard_SpecificBoard()
        {
            mockBoard.Setup(x => x.currentPlayer).Returns(Player.X);
            mockBoard.Setup(x => x.nextBoardNumber()).Returns(1);

            var expected = "Test\r\nNext Board: 1\r\nX's Move: ";
            Assert.AreEqual(expected, InputHandling.sendInput("1 1", mockBoard.Object));
        }
    }
}
