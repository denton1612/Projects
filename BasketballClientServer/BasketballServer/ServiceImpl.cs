using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BasketballModel.domain;
using BasketballPersistance.repository;
using BasketballServices;

namespace BasketballServer
{
    public class ServiceImpl: IService
    {
        private IRepositoryCashier _cashierRepository;
        private IRepositoryGame _gameRepository;
        private IRepositoryClient _clientRepository;
        private IRepositoryPurchase _purchaseRepository;
        private readonly IDictionary<long, IObserver> _loggedCashiers;


        public ServiceImpl(IRepositoryCashier cashierRepository, IRepositoryGame gameRepository, IRepositoryClient clientRepository, IRepositoryPurchase purchaseRepository)
        {
            _cashierRepository = cashierRepository;
            _gameRepository = gameRepository;
            _clientRepository = clientRepository;
            _purchaseRepository = purchaseRepository;
            _loggedCashiers = new Dictionary<long, IObserver>();
        }

        public void Login(Cashier cashier, IObserver observer)
        {
            Cashier cashierFound = _cashierRepository.findByUsernameAndPassowrd(cashier.Username, cashier.Password);
            if (cashierFound != null) {
                if (_loggedCashiers.ContainsKey(cashierFound.Id)) {
                    throw new ServiceException("Cashier already logged in!");
                }
                _loggedCashiers[cashierFound.Id] = observer;
            }
            else
            {
                throw new ServiceException("Cashier has not an account!");
            }
        }

        public void Logout(Cashier cashier, IObserver observer)
        {
            Cashier cashierFound = _cashierRepository.findByUsernameAndPassowrd(cashier.Username, cashier.Password);
            _loggedCashiers?.Remove(cashierFound.Id);
        }

        public Game[] GetGames()
        {
            IEnumerable<Game> games = _gameRepository.GetAll();
            return games.ToArray();
        }

        private void updateSeats(Game game, int boughtSeats)
        {
            Game gameUpdated = new Game(game.HomeTeam, game.AwayTeam, game.GameType, game.Seats - boughtSeats, game.StartTime);
            gameUpdated.Id = game.Id;
            _gameRepository.Update(gameUpdated);
        }

        private void NotifyBoughtTicket(Game game)
        {
            foreach (var observer in _loggedCashiers.Values)
            {
                Task.Run(() => { observer.BoughtTicket(game); }); // folosim Task pentru a paraleliza operatia de notificare
            }
        }

        public void BuyTicket(Client client, string seats, Game game)
        {
            Client clientFound = _clientRepository.findByNameAndAddress(client.Name, client.Address);
            if (clientFound == null)
            {
                _clientRepository.Add(client);
            }
            client = _clientRepository.findByNameAndAddress(client.Name, client.Address); // astfel putem sa obtinem si id-ul clientului pentru a putea crea purchase-ul

            Purchase purchase = new Purchase(client, game, Int32.Parse(seats)); // aici suntem siguri ca seats poate fi parsat ca integer
            _purchaseRepository.Add(purchase);
            updateSeats(game, Int32.Parse(seats));

            game.Seats -= Int32.Parse(seats);
            NotifyBoughtTicket(game); // notificam toti clientii despre purchase-ul facut
        }

        public Purchase[] GetPurchasesByNameAndAddress(Client client)
        {
            IEnumerable<Purchase> purchases = null;

            if (client.Address.Length == 0)
                purchases = _purchaseRepository.filterByName(client.Name);
            else
                purchases = _purchaseRepository.filterByNameAndAddress(client.Name, client.Address);

            return purchases.ToArray();
        }
    }
}
