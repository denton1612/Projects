using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;

namespace BasketballPersistance.repository
{
    public interface IRepository<ID, E> where E: Entity<ID>
    {
        E Add(E entity);

        E Delete(E entity);

        E Update(E entity);

        E GetE(ID id);

        IEnumerable<E> GetAll();
    }
}
