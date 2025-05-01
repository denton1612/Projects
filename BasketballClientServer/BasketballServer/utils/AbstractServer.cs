using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace BasketballServer.utils
{
    public abstract class AbstractServer
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AbstractServer));
        private TcpListener _server;
        private string _host;
        private int _port;

        public AbstractServer(string host, int port)
        {
            _host = host; _port = port;
        }

        public void Start() { 
            IPAddress ipaddr = IPAddress.Parse(_host);
            IPEndPoint endpoint = new IPEndPoint(ipaddr, _port);
            _server = new TcpListener(endpoint);    
            _server.Start();
            while (true) {
                log.Debug("Waiting for clients...");
                TcpClient client = _server.AcceptTcpClient();
                log.Debug("Client connected");
                ProcessRequest(client);
            }
        }

        protected abstract void ProcessRequest(TcpClient client);
    }
}
