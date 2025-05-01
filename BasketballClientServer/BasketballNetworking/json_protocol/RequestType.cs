using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballNetworking.json_protocol
{
    public enum RequestType
    {
        LOGIN,
        LOGOUT,
        GET_GAMES,
        BUY_TICKET,
        GET_PURCHASES
    }
}
