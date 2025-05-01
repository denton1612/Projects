using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;

namespace BasketballNetworking.dtos
{
    public class DTOUtils
    {
        public static CashierDTO GetDTO(Cashier cashier)
        {
            return new CashierDTO(cashier.Username, cashier.Password);
        }

        public static Cashier GetFromDTO(CashierDTO cashierDTO)
        {
            return new Cashier(cashierDTO.Username, cashierDTO.Password);
        }

        public static GameDTO GetDTO(Game game)
        {
            return new GameDTO(game);
        }

        public static Game GetFromDTO(GameDTO gameDTO)
        {
            Game game = new Game(gameDTO.HomeTeam, gameDTO.AwayTeam, gameDTO.GameType, gameDTO.Seats, gameDTO.StartTime);
            game.Id = gameDTO.Id;
            return game;
        }

        public static GameDTO[] GetDTO(Game[] games)
        {
            List<GameDTO> gamesDTO = new List<GameDTO>();
            foreach (Game game in games)
            {
                gamesDTO.Add(new GameDTO(game));
            }
            return gamesDTO.ToArray();
        }

        public static Game[] GetFromDTO(GameDTO[] gamesDTO)
        {
            List<Game> games = new List<Game>();
            foreach (GameDTO gameDTO in gamesDTO)
            {
                Game game = new Game(gameDTO.HomeTeam, gameDTO.AwayTeam, gameDTO.GameType, gameDTO.Seats, gameDTO.StartTime);
                game.Id = gameDTO.Id;
                games.Add(game);
            }
            return games.ToArray();
        }

        public static ClientDTO GetDTO(Client client)
        {
            return new ClientDTO(client.Name, client.Address);
        }

        public static Client GetFromDTO(ClientDTO clientDTO)
        {
            return new Client(clientDTO.Name, clientDTO.Address);
        }

        public static BasketballNetworking.dtos.PurchaseDTO GetDTO(Purchase purchase)
        {
            return new PurchaseDTO(DTOUtils.GetDTO(purchase.Client), purchase.TicketCounter, DTOUtils.GetDTO(purchase.Game));
        }

        public static Purchase GetFromDTO(BasketballNetworking.dtos.PurchaseDTO purchaseDTO)
        {
            return new Purchase(DTOUtils.GetFromDTO(purchaseDTO.Client), DTOUtils.GetFromDTO(purchaseDTO.Game), purchaseDTO.Seats);
        }
    }
}
