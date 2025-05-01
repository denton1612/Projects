
namespace BasketballModel.domain
{
    public class Purchase: Entity<long>
    {
        private readonly Client _client;
        private readonly Game _game;
        private readonly int _ticketCounter;

        public Purchase(Client client, Game game, int ticketCounter)
        {
            _client = client;
            _game = game;
            _ticketCounter = ticketCounter;
        }

        public Client Client { get { return _client; } }
        public Game Game { get { return _game; } }
        public int TicketCounter { get { return _ticketCounter; } } 

    }
}
