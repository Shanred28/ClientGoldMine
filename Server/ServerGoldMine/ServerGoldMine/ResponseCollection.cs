using Newtonsoft.Json;

namespace ServerGoldMine
{
    public class ResponseCollection
    {
        private PlayerList playerList;

        public ResponseCollection(PlayerList playerList)
        {
            this.playerList = playerList;
        }

        public string GetResponseForGET(string resource, PlayerInfo info)
        {
            if (resource == "/playerStats") return GetSerializedPlayerStats(info);

            return "";
        }

        public string ResponseForPOST(string resource, string content, PlayerInfo info)
        {
            if (resource == "/playerStats" && content == "UpgrateLevel") return UpgrateLevel(info);

            return "";
        }

        private string GetSerializedPlayerStats(PlayerInfo info)
        {
            PlayerStats stats = playerList.GetPlayerStats(info);
            return JsonConvert.SerializeObject(stats);
        }

        private string UpgrateLevel(PlayerInfo info)
        {
            PlayerStats playerStats = playerList.GetPlayerStats(info);
            playerStats.NextLevel();
            return JsonConvert.SerializeObject(playerStats);
        }
    }
}
