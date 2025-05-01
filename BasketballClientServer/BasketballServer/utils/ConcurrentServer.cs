using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BasketballServer.utils
{
    public abstract class ConcurrentServer: AbstractServer
    {
        public ConcurrentServer(string host, int port) : base(host, port)
        {
        }

        protected override void ProcessRequest(TcpClient client)
        {
            Thread worker = CreateWorker(client);
            worker.Start();
        }

        protected abstract Thread CreateWorker(TcpClient client);
    }
}
