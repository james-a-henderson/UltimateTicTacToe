using System;

namespace UltimateTicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new GlobalBoard();

            Console.WindowHeight = 35;

            Console.Write(InputHandling.initialBoardState(board));
            while(board.Status == GameStatus.InProgress && !board.Exiting)
            {
                var input = Console.ReadLine();
                var output = InputHandling.sendInput(input, board);
                Console.Write(output);
            }

            Console.WriteLine("Game Over. Press Any Key to Exit");
            Console.ReadKey();
        }
    }
}
