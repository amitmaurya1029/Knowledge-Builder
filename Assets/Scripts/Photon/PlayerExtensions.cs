using Photon.Realtime;

public static class PlayerExtensions
{
    public static bool IsQuizMaster(this Player player)
    {
        return player.CustomProperties.ContainsKey("IsQuizMaster") && 
               (bool)player.CustomProperties["IsQuizMaster"];
    }
}
