namespace UltimateTicTacToe
{
    public enum Player
    {
        X,
        O
    }

    public enum LocalBoardState
    {
        Blank,
        X,
        O
    }

    public enum GlobalBoardState
    {
        Open,
        X,
        O,
        Tie
    }

    public enum GameStatus
    {
        InProgress,
        X_Win,
        O_Win,
        Tie
    }

    public enum MoveResult
    {
        Success,
        BoardOutOfRange,
        SpaceOutOfRange,
        BoardAlreadyCompleted,
        RequiredBoardNotSelected,
        SpaceAlreadyUsed,
        OtherError
    }
}
