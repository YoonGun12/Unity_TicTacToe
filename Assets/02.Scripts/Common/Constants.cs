public class Constants
{
    public const string ServerURL = "http://localhost:3001";
    public const string GameServerURL = "ws://localhost:3001";

    public enum MultiplayManagerState
    {
        CreateRoom, 
        JoinRoom, 
        StartGame, 
        EndGame
    }
}
