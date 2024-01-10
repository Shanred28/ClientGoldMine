using System;
using System.Collections.Generic;

namespace ServerGoldMine
{
    public class PlayerList
    {
        private List<Player> _players = new List<Player>();
        public int Count => _players.Count;

        public Player this[int index]
        { 
            get => _players[index];
        }

        public void AddPlayers(List<Player> players)
        {
            _players.AddRange(players);
        }

        public void AddNewPlayer(PlayerInfo info)
        {
            if (ExistPlayer(info) == false)
            {
                _players.Add(new Player(info));
                Console.WriteLine("Add Player");
            }
        }

        public bool ExistPlayer(PlayerInfo info)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].playerInfo.Name == info.Name && _players[i].playerInfo.PasswordHash == info.PasswordHash) return true;
            }

            return false;
        }

        public void UpdatePlayerStats()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].playerStats.Update();
            }
        }

        public PlayerStats GetPlayerStats(PlayerInfo info)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].playerInfo.Name == info.Name)
                    return _players[i].playerStats;
            }

            return null;
        }
    }
}
