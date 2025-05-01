using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;

namespace BasketballNetworking.dtos
{
    public class PurchaseDTO
    {
        public ClientDTO Client { get; set; }
        public int Seats {  get; set; }

        public GameDTO Game { get; set; }

        public PurchaseDTO() { }

        public PurchaseDTO(ClientDTO client, int seats, GameDTO game)
        {
            Client = client;
            Seats = seats;
            Game = game;
        }

        public override string ToString()
        {
            return string.Format("PurchaseDTO[client = {0}, seats = {1}, game = {2}", Client, Seats, Game);
        }
    }
}
