using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballNetworking.dtos
{
    public class ClientDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public ClientDTO() { }

        public ClientDTO(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return string.Format("ClientDTO[name = {0}, address = {1}]", Name, Address);
        }
    }
}
