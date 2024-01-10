using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace ServerGoldMine
{
    public class DbPlayerRequestCollection
    {
        private DbConnection connection;

        public DbPlayerRequestCollection(DbConnection connection)
        {
            this.connection = connection;
        }

        public List<Player> GetAllPlayers()
        { 
            List<Player> allPlayers = new List<Player>();

            PlayerInfo[] playersInfo = GetAllPlayerInfo();
            PlayerStats[] playerStats = GetAllPlayerStats();

            for (int i = 0; i < playersInfo.Length; i++)
            {
                allPlayers.Add(new Player(playersInfo[i], playerStats[i]));
                Console.WriteLine(playersInfo[i].Name + playersInfo[i].PasswordHash);
            }

            return allPlayers;
        }

        public PlayerInfo[] GetAllPlayerInfo()
        { 
            List<PlayerInfo> playerInfos = new List<PlayerInfo>();
            string commandText = $"SELECT * FROM player_info";

            SQLiteDataReader reader = connection.ExecuteCommandWithResult(commandText);

            if (reader.HasRows)
            { 
                while (reader.Read()) 
                { 
                    string name = reader.GetValue(1).ToString();
                    string passwordHash = reader.GetValue(2).ToString();

                    playerInfos.Add(new PlayerInfo(name, passwordHash));
                }
            }

            return playerInfos.ToArray();
        }

        public PlayerStats[] GetAllPlayerStats()
        {
            List<PlayerStats> playerStats = new List<PlayerStats>();
            string commandText = $"SELECT * FROM player_stats";

            SQLiteDataReader reader = connection.ExecuteCommandWithResult(commandText);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int gold = reader.GetInt32(1);
                    int level = reader.GetInt32(2);

                    playerStats.Add(new PlayerStats(gold, level));
                }
            }

            return playerStats.ToArray();
        }

        public void SetPlayerStats(Player player)
        {
            int playerId = GetPlayerId(player.playerInfo);

            string commandText = $"UPDATE player_stats SET gold={player.playerStats.Gold}, level={player.playerStats.Level} WHERE player_id=\"{playerId}\"";

            connection.ExecuteCommand(commandText);
        }

        private int GetPlayerId(PlayerInfo playerInfo)
        {
            string commandText = $"SELECT id FROM player_info WHERE name=\"{playerInfo.Name}\"";

            SQLiteDataReader reader = connection.ExecuteCommandWithResult(commandText);

            if (reader.HasRows)
            { 
                reader.Read();
                return reader.GetInt32(0);
            }

            return -1;
        }
    }
}
