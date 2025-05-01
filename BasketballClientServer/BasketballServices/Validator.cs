using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;

namespace BasketballServices
{
    public class Validator
    {
        public static string Validate(Client client)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(client.Name))
            {
                errors += "Client's name cannot be empty\n";
            }

            if (string.IsNullOrEmpty(client.Address))
            {
                errors += "Client's address cannot be empty\n";
            }

            return errors;
        }

        public static string Validate(Purchase purchase) 
        {
            string errors = string.Empty;

            errors += Validate(purchase.Client); // incepem prin validarea clientului

            if (purchase.Game == null)
            {
                errors += "Not selected game!\n";
            }

            if (purchase.Game != null && purchase.Game.Seats < purchase.TicketCounter)
            {
                errors += "Not enough free seats!";
            }

            return errors;
        }
    }
}
