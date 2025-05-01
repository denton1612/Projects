using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballNetworking.dtos;

namespace BasketballNetworking.json_protocol
{
    public class Response
    {
        public ResponseType ResponseType { get; set; }

        public GameDTO[] Games { get; set; }

        public BasketballNetworking.dtos.PurchaseDTO[] Purchases { get; set; }

        public string ErrorMessage { get; set; }

        public Response() { }

        public override string ToString()
        {
            return string.Format("Response[type = {0}, error_message = {1}]", ResponseType, ErrorMessage);
        }
    }
}
