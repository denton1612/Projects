using System;
using System.Collections.Concurrent;
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
    public class ServerProxy: IService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ServerProxy));
        private string _host;
        private int _port;

        private IObserver _observer;
        private NetworkStream _stream;
        private TcpClient _connection;
        private BlockingCollection<Response> _responses;
        private volatile bool _finished;


        public ServerProxy(string host, int port)
        {
            _host = host;
            _port = port;
            _responses = new BlockingCollection<Response>();
        }

        public void Login(Cashier cashier, IObserver observer)
        {
            InitConnection();
            SendRequest(JsonUtils.CreateLoginRequest(cashier));
            Response response = ReadResponse();
            if (response.ResponseType == ResponseType.OK) { 
                _observer = observer;
            }
            if (response.ResponseType == ResponseType.ERROR)
            {
                string errorMessage = response.ErrorMessage;
                CloseConnection();
                throw new ServiceException(errorMessage);
            }
        }

        public void Logout(Cashier cashier, IObserver observer)
        {
            SendRequest(JsonUtils.CreateLogoutRequest(cashier));
            Response response = ReadResponse();
            if (response.ResponseType == ResponseType.ERROR)
            {
                throw new ServiceException(response.ErrorMessage);
            }
            CloseConnection();
            _observer = null;
        }

        public Game[] GetGames()
        {
            SendRequest(JsonUtils.CreateGetGamesRequest());
            Response response = ReadResponse();

            if (response.ResponseType == ResponseType.GET_GAMES)
            {
                Game[] games = DTOUtils.GetFromDTO(response.Games);
                return games;
            }
            if (response.ResponseType == ResponseType.ERROR)
            {
                throw new ServiceException(response.ErrorMessage);
            }

            return null;
        }

        public void BuyTicket(Client client, string seats, Game game)
        {
            string errors = "";

            int seats_int = 0;
            
            try
            {
                seats_int = int.Parse(seats);
                if (seats_int <= 0)
                {
                    errors += "Seats must have positive integer value!'\n'";
                }
            }
            catch (Exception ex)
            {
                errors += "Seats must have positive integer value!'\n'";
            }
           
            Purchase purchase = new Purchase(client, game, seats_int);
            errors += Validator.Validate(purchase); // verificam validitatea achizitiei

            if (errors.Length > 0)
            {
                throw new ServiceException(errors);
            }
            log.Error(errors);

            SendRequest(JsonUtils.CreateBuyTicketRequest(purchase));
            Response response = ReadResponse();

            if (response.ResponseType == ResponseType.ERROR)
            {
                throw new ServiceException(response.ErrorMessage);
            }
        }

        public Purchase[] GetPurchasesByNameAndAddress(Client client)
        {
            SendRequest(JsonUtils.CreateGetPurchasesRequest
                (client));
            Response response = ReadResponse();
            if (response.ResponseType == ResponseType.ERROR)
            {
                throw new ServiceException(response.ErrorMessage);
            }

            PurchaseDTO[] purchasesDTO = response.Purchases;

            return purchasesDTO.Select(purchaseDTO => DTOUtils.GetFromDTO(purchaseDTO) ).ToArray();
        }

        private void InitConnection() {
            try
            {
                _connection = new TcpClient(_host, _port);
                _stream = _connection.GetStream();
                _finished = false;
                StartReader();
            }
            catch (Exception ex) { 
                log.Error(ex);
            }
        }

        private void CloseConnection()
        {
            _finished = true;
            try
            {
                _stream.Close();
                _connection.Close();
                _observer = null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        private void SendRequest(Request request)
        {
            try
            {
                lock (_stream)
                {
                    string requestJson = JsonSerializer.Serialize(request);
                    byte[] data = Encoding.UTF8.GetBytes(requestJson + "\n");
                    _stream.Write(data, 0, data.Length);
                    _stream.Flush();
                    log.DebugFormat("Request with type = {0} was sent successfully", request.RequestType);
                }
            }
            catch (Exception e) { 
                throw new ServiceException("Error sending object " + e);
            }
        }


        private Response ReadResponse()
        {
            Response response = null;

            try
            {
                response = _responses.Take();
                log.DebugFormat("Response with type = {0} was received", response.ResponseType);
            }
            catch (Exception e)
            {
                log.Error(e);
            }

            return response;
        }

        private void StartReader() {
            Thread reader = new Thread(Run);
            reader.Start();
        }

        private bool IsUpdate(Response response)
        {
            return response.ResponseType == ResponseType.BUY_TICKET;
        }

        private void HandleUpdate(Response response)
        {
            if (response.ResponseType == ResponseType.BUY_TICKET)
            {
                Game game = DTOUtils.GetFromDTO(response.Games)[0];
                _observer.BoughtTicket(game);
            }
        }

        private void Run() { 
            using StreamReader reader = new StreamReader(_stream, Encoding.UTF8);
            while (!_finished) {
                try
                {
                    string responseJson = reader.ReadLine();
                    if (string.IsNullOrEmpty(responseJson))
                    {
                        continue;
                    }

                    Response response = JsonSerializer.Deserialize<Response>(responseJson);
                    if (IsUpdate(response))
                    {
                        HandleUpdate(response);
                    }
                    else
                    {
                        _responses.Add(response);
                    }
                }
                catch (Exception e) {
                    log.Error(e);
                }
            }
        }
    }
}
