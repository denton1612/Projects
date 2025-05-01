using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;

namespace BasketballPersistance.repository
{
    public interface IRepositoryClient: IRepository<long, Client>
    {
        Client findByNameAndAddress(string name, string address);
        IEnumerable<Client> findByName(string name);
    }
}
