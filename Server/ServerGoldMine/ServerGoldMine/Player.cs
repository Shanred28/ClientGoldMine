namespace ServerGoldMine
{
    public class Player
    {
        public PlayerInfo playerInfo;
        public PlayerStats playerStats;

        public Player(PlayerInfo info)
        { 
            playerInfo = info;
            playerStats = new PlayerStats(10, 1);
        }

        public Player(PlayerInfo info, PlayerStats stats) 
        {
            playerInfo =info;
            playerStats = stats;
        }
    }
}
