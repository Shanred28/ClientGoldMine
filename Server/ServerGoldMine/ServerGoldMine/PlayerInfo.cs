namespace ServerGoldMine
{
    public class PlayerInfo
    {
        private string _name;
        private string _passwordHash;

        public string Name => _name;
        public string PasswordHash => _passwordHash;

        public PlayerInfo(string name, string passwordHash)
        {
            _name = name;
            _passwordHash = passwordHash;
        }
    }
}
