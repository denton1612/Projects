namespace BasketballModel.domain
{
    public class Client : Entity<long>
    {
        private string _name;
        private string _address;

        public Client(string name, string address)
        {
            _name = name;
            _address = address;
        }

        public string Name { get { return _name; } set { _name = value; } }
        public string Address { get { return _address; } set { _address = value; } }

        public override string ToString()
        {
            return "Name: " + _name + ", " + "Address: " + _address;
        }
    }
}
