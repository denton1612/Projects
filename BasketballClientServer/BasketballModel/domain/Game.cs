
namespace BasketballModel.domain
{
    public class Game : Entity<long>
    {
        private readonly string _homeTeam;
        private readonly string _awayTeam;
        private readonly GameType _gameType;
        private int _seats;
        private readonly DateTime _startTime;

        public Game(string homeTeam, string awayTeam, GameType gameType, int seats, DateTime startTime)
        {
            _homeTeam = homeTeam;
            _awayTeam = awayTeam;
            _gameType = gameType;
            _seats = seats;
            _startTime = startTime;
        }

        public string HomeTeam { get { return _homeTeam; } }
        public string AwayTeam { get { return _awayTeam; } }
        public GameType GameType { get {return _gameType; } }
        public int Seats { get { return _seats; } set { _seats = value; } }
        public DateTime StartTime { get { return _startTime; } }

        public double GetPrice()
        {
            if (_gameType == GameType.GROUP)
                return 25;
            if (_gameType == GameType.QUARTERFINAL)
                return 40;
            if (_gameType == GameType.SEMIFINAL)
                return 75;
            return 125;
        }

        public override string ToString()
        {
            return HomeTeam + " - " + AwayTeam;
        }


    }
}
