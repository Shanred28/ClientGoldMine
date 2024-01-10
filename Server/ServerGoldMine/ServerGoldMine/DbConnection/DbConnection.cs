using System.Data.SQLite;

namespace ServerGoldMine
{
    public class DbConnection
    {
        private const string DATASOURCE = "database.db";
        private SQLiteConnection connection;


        public void Open()
        {
            connection = new SQLiteConnection($"Data Source = {DATASOURCE}");
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public void ExecuteCommand(string commandText)
        {
            SQLiteCommand cmd = new SQLiteCommand(commandText, connection);
            cmd.ExecuteNonQuery();
        }

        public SQLiteDataReader ExecuteCommandWithResult(string commandText)
        {
            SQLiteCommand cmd = new SQLiteCommand(commandText, connection);

            return cmd.ExecuteReader();
        }
    }
}
