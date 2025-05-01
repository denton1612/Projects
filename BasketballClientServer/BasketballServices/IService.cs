using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;
using BasketballModel.dtos;

namespace BasketballServices
{
    public interface IService
    {
        void Login(Cashier cashier, IObserver observer);

        void Logout(Cashier cashier, IObserver observer);

        Game[] GetGames();

        void BuyTicket(Client client, string seats, Game game);

        Purchase[] GetPurchasesByNameAndAddress(Client client);
    }
}
