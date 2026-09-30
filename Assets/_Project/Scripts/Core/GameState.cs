namespace DungeonDefense.Core
{
    /// <summary>
    /// Top-level game states. Allowed transitions are defined in <see cref="GameStateMachine"/>.
    /// To add a state: add it here, then add its transitions to the table in GameStateMachine.
    /// </summary>
    public enum GameState
    {
        Boot,
        MainMenu,
        Town,
        Battle,
        Reward,
        Pause,
        GameOver
    }
}
