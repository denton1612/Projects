using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;
using BasketballNetworking.dtos;

namespace BasketballNetworking.json_protocol
{
    public class JsonUtils
    {
        public static Request CreateLoginRequest(Cashier cashier)
        {
            return new Request { RequestType = RequestType.LOGIN, CashierDTO = DTOUtils.GetDTO(cashier) };
        }

        public static Request CreateLogoutRequest(Cashier cashier) {
            return new Request { RequestType = RequestType.LOGOUT, CashierDTO = DTOUtils.GetDTO(cashier) };
        }

        public static Request CreateGetGamesRequest()
        {
            return new Request { RequestType = RequestType.GET_GAMES };
        }

        public static Request CreateBuyTicketRequest(Purchase purchase)
        {
            return new Request { RequestType = RequestType.BUY_TICKET, PurchaseDTO = DTOUtils.GetDTO(purchase) };
        }

        public static Request CreateGetPurchasesRequest(Client client)
        {
            return new Request { RequestType = RequestType.GET_PURCHASES, ClientDTO = DTOUtils.GetDTO(client) };
        }

        public static Response CreateOKResponse()
        {
            return new Response { ResponseType = ResponseType.OK };
        }

        public static Response CreateErrorResponse(string errorMessage)
        {
            return new Response { ResponseType = ResponseType.ERROR, ErrorMessage = errorMessage };
        }

        public static Response CreateGetGamesResponse(Game[] games)
        {
            return new Response { ResponseType = ResponseType.GET_GAMES, Games = DTOUtils.GetDTO(games) };
        }

        public static Response CreateBuyTicketResponse(Game game)
        {
            Game[] games = new Game[1];
            games[0] = game;
            return new Response { ResponseType = ResponseType.BUY_TICKET, Games = DTOUtils.GetDTO(games) };
        }

        public static Response CreateGetPurchasesResponse(Purchase[] purchases)
        {
            return new Response { ResponseType = ResponseType.GET_PURCHASES, Purchases = purchases.Select(purchase => DTOUtils.GetDTO(purchase)).ToArray() };
        }
    }
}
