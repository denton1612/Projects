using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballNetworking.dtos
{
    public class CashierDTO
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public CashierDTO() { }

        public CashierDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override string ToString()
        {
            return string.Format("CashierDTO[username = {0}, password = {1}]", Username, Password);
        }
    }
}
