using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;
using BasketballModel.dtos;
namespace BasketballPersistance.repository
{
    public interface IRepositoryPurchase: IRepository<long, Purchase>
    {
        IEnumerable<Purchase> filterByName(string name);
        IEnumerable<Purchase> filterByNameAndAddress(string name, string address);

        Client findClientById(long id);
        Game findGameById(long id);
    }
}
