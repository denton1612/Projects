namespace BasketballModel.domain
{
    public class Cashier : Entity<long>
    {
        private string _username;
        private string _password;

        public Cashier(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public string Username { get { return _username; } set { _username = value; } }
        public string Password { get { return _password; } set { _password = value; } }

        public override string ToString()
        {
            return _username;
        }
    }
}
