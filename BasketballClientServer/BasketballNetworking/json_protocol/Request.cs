using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballNetworking.dtos;

namespace BasketballNetworking.json_protocol
{
    public class Request
    {
        public CashierDTO CashierDTO { get; set; }
        public PurchaseDTO PurchaseDTO { get; set; }
        public ClientDTO ClientDTO { get; set; }
        public RequestType RequestType { get; set; }

        public Request() { }

        public override string ToString()
        {
            return string.Format("Request[type = {0}, cashier = {1}, purchase = {2}, client = {3}]", RequestType.ToString(), CashierDTO, PurchaseDTO, ClientDTO);
        }
    }
}
