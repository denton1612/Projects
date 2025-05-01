using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;

namespace BasketballPersistance.repository
{
    public interface IRepositoryCashier: IRepository<long, Cashier>
    {
        Cashier findByUsernameAndPassowrd(string username, string password);
    }
}
