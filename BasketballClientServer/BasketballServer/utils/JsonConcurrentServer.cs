using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BasketballNetworking.json_protocol;
using BasketballServices;
using log4net;

namespace BasketballServer.utils
{
    public class JsonConcurrentServer: ConcurrentServer
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(JsonConcurrentServer));
        private readonly IService _service;
        private ClientWorker _worker;

        public JsonConcurrentServer(string host, int port, IService service) : base(host, port) { 
            _service = service;
            log.Debug("Creating JsonConcurrentServer...");
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            _worker = new ClientWorker(_service, client);
            return new Thread(_worker.Run);
        }
    }
}
