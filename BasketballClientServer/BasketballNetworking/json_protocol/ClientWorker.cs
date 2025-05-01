using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BasketballModel.domain;
using BasketballNetworking.dtos;
using BasketballServices;
using log4net;

namespace BasketballNetworking.json_protocol
{
    public class ClientWorker: IObserver
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ClientWorker));
        private IService _server;
        private TcpClient _connection;
        private NetworkStream _stream;
        private volatile bool _connected;

        // an OKResponse object
        private readonly Response okResponse = new Response { ResponseType = ResponseType.OK };

        public ClientWorker(IService server, TcpClient connection)
        {
            _server = server;
            _connection = connection;
            try
            {
                _stream = _connection.GetStream();
                _connected = true;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        public void Run()
        {
            using StreamReader reader = new StreamReader(_stream, Encoding.UTF8);
            while (_connected) {
                try
                {
                    string requestJson = reader.ReadLine();
                    if (string.IsNullOrEmpty(requestJson))
                    {
                        log.Error("Request was not received correctly!");
                        continue;
                    }
                    Request request = JsonSerializer.Deserialize<Request>(requestJson);
                    log.DebugFormat("Request with type = {0} was received", request.RequestType);

                    Response response = HandleRequest(request);
                    if (response != null)
                    {
                        SendResponse(response);
                    }
                }
                catch (Exception e) {
                    log.Error(e);
                }

                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    log.Error(e);
                }
            }

            try
            {
                _stream.Close();
                _connection.Close();
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

        private Response HandleRequest(Request request) { 
            Response response = null;

            if (request.RequestType == RequestType.LOGIN) { 
                Cashier cashier = DTOUtils.GetFromDTO(request.CashierDTO);
                try
                {
                    lock (_server)
                    {
                        _server.Login(cashier, this);
                    }
                    return okResponse;
                }
                catch (ServiceException ex) { 
                    _connected = false;
                    return JsonUtils.CreateErrorResponse(ex.Message);
                }
            }

            if (request.RequestType == RequestType.LOGOUT)
            {
                Cashier cashier = DTOUtils.GetFromDTO(request.CashierDTO);
                try
                {
                    lock (_server)
                    {
                        _server.Logout(cashier, this);
                    }
                    _connected = false;
                    return okResponse;
                }
                catch (ServiceException ex) { 
                    return JsonUtils.CreateErrorResponse(ex.Message);
                }
            }

            if (request.RequestType == RequestType.GET_GAMES)
            {
                try
                {
                    Game[] games = null;

                    lock (_server)
                    {
                        games = _server.GetGames();
                    }

                    return JsonUtils.CreateGetGamesResponse(games);
                }
                catch (ServiceException ex) {
                    return JsonUtils.CreateErrorResponse(ex.Message);
                }
            }

            if (request.RequestType == RequestType.BUY_TICKET)
            {
                Purchase purchase = DTOUtils.GetFromDTO(request.PurchaseDTO);
                try
                {
                    _server.BuyTicket(purchase.Client, purchase.TicketCounter.ToString(), purchase.Game);
                    log.DebugFormat("Purchase was made in ServiceImpl for game = {0}", purchase.Game);

                    SendResponse(okResponse); // trimitem clientului care a cerut cumpararea biletului ca achizitia a fost cu succes
                }
                catch (ServiceException ex)
                {
                    return JsonUtils.CreateErrorResponse(ex.Message);
                }
            }

            if (request.RequestType == RequestType.GET_PURCHASES)
            {
                Client client = DTOUtils.GetFromDTO(request.ClientDTO);
                try
                {
                    log.DebugFormat("Purchases filtered for client = {0}", client);

                    Purchase[] purchases = _server.GetPurchasesByNameAndAddress(client);
                    SendResponse(JsonUtils.CreateGetPurchasesResponse(purchases));
                }

                catch (ServiceException ex) { 
                    SendResponse(JsonUtils.CreateErrorResponse(ex.Message));
                }
            }

            // it should not reach here!
            return response;
        }

        public void BoughtTicket(Game game)
        {
            Response response = JsonUtils.CreateBuyTicketResponse(game);
            SendResponse(response);
        }

        private void SendResponse(Response response) { 
            string responseJson = JsonSerializer.Serialize(response);
            lock (_stream)
            {
                byte[] data = Encoding.UTF8.GetBytes(responseJson + "\n");
                _stream.Write(data, 0, data.Length);
                _stream.Flush();
                log.DebugFormat("Response with type = {0} was sent", response.ResponseType);
            }
        }
    }
}
