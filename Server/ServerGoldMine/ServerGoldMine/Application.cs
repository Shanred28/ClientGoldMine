using System;
using System.Threading;

namespace ServerGoldMine
{
    public class Application
    {
        static void Main(string[] args)
        {
            //Подключение к базе
            DbConnection dbConnection = new DbConnection();
            DbPlayerRequestCollection dbPlayerRequestCollection = new DbPlayerRequestCollection(dbConnection);
            dbConnection.Open();

            // Получения игроков
            PlayerList playerList = new PlayerList();
            playerList.AddPlayers(dbPlayerRequestCollection.GetAllPlayers());
            Console.WriteLine($"List of players loaded from database");
            //playerList.AddNewPlayer(new PlayerInfo("Zolder", "65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5"));

            // Обработка запросов
            ResponseCollection responseCollection = new ResponseCollection(playerList);
            RequestListner requestListner = new RequestListner(playerList, responseCollection, "http://192.168.1.254:88/playerStats/");
            Thread requestThreed = new Thread(requestListner.StartRequestListen);
            requestThreed.Start();

            //Синхронизация с бд.
            DbPlayerSynchronizer dbPlayerSynchronizer = new DbPlayerSynchronizer(dbPlayerRequestCollection, playerList,5000);
            Thread dbPlayerSynchronizerThread = new Thread(dbPlayerSynchronizer.StartSynchronize);
            dbPlayerSynchronizerThread.Start();


            //Игровая логика
            Game game = new Game(playerList);

            while (true)
            {
                game.UpdateGame();
            }
        }

    }
}
