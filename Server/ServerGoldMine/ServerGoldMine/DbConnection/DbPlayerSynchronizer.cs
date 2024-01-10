using System;
using System.Threading;

namespace ServerGoldMine
{
    public class DbPlayerSynchronizer
    {
        private DbPlayerRequestCollection _requestCollection;
        private PlayerList _playerList;

        private int _timeOut;

        public DbPlayerSynchronizer(DbPlayerRequestCollection requestCollection, PlayerList playerList, int timeOut)
        {
            _requestCollection = requestCollection;
            _playerList = playerList;
            _timeOut = timeOut;
        }

        public void StartSynchronize()
        {
            while (true)
            {
                for (int i = 0; i < _playerList.Count; i++)
                {
                    _requestCollection.SetPlayerStats(_playerList[i]);
                }

                Console.WriteLine("Player stats synchronized with database");

                Thread.Sleep(_timeOut);
            }
        }
    }
}
