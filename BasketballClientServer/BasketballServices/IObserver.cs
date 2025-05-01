using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;

namespace BasketballServices
{
    public interface IObserver
    {
        public void BoughtTicket(Game game);  
    }
}
