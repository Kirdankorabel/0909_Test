public static class Main
{
    private static WinPanel ResultPanel;
    private static PlayerController PlayerController;
    private static ObstacleController ObstacleController;

    public static void SetResultPanel(WinPanel winPanel)
    {
        ResultPanel = winPanel;
        ResultPanel.OnGameRestarted += RestartGame;
    }

    public static void SetPlayerController(PlayerController playerController)
    {
        PlayerController = playerController;
        PlayerController.OnWin += ShowResult;
    }

    public static void SetObstacleController(ObstacleController obstacleController)
    {
        ObstacleController = obstacleController;
    }

    public static void ShowResult(float result)
    {
        ResultPanel.ShowResult(result);
    }

    private static void RestartGame()
    {
        ObstacleController.ShufflePositions();
        ObstacleController.ActivateAll();
        PlayerController.Reset();
    }
}