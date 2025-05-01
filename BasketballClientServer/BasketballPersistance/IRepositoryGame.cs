using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;

namespace BasketballPersistance.repository
{
    public interface IRepositoryGame: IRepository<long, Game>
    {

    }
}
