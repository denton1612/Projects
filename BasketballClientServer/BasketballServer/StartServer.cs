using System.Configuration;
using System.Reflection;
using BasketballPersistance.repository;
using BasketballServer.utils;
using BasketballServices;
using log4net;
using log4net.Config;

namespace BasketballServer
{
    public class StartServer
    {
        private static int default_port = 55555;
        private static string default_ip = "127.0.0.1";
        private static readonly ILog log = LogManager.GetLogger(typeof(StartServer));

        public static void Main(string[] args)
        {
            // configurare jurnalizare folosind log4net
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            log.Info("Starting basketball server");
            log.Info("Reading properties from app.config...");

            int port = default_port;
            String ip = default_ip;

            String portS = ConfigurationManager.AppSettings["port"];
            if (portS == null)
            {
                log.Debug("Port property not set. Using default value " + default_port);
            }
            else
            {
                bool result = Int32.TryParse(portS, out port);
                if (!result)
                {
                    log.Debug("Port property not a number. Using default value " + default_port);
                    port = default_port;
                    log.Debug("Portul " + port);
                }
            }
            String ipS = ConfigurationManager.AppSettings["ip"];

            if (ipS == null)
            {
                log.Info("Port property not set. Using default value " + default_ip);
            }
            else
            {
                ip = ipS;
            }

            string databaseName = "basketballDB";
            IRepositoryCashier cashierRepository = new BDRepositoryCashier(databaseName);
            IRepositoryGame gameRepository = new BDRepositoryGame(databaseName);
            IRepositoryClient clientRepository = new BDRepositoryClient(databaseName);
            IRepositoryPurchase purchaseRepository = new BDRepositoryPurchase(databaseName);

            IService serviceImpl = new ServiceImpl(cashierRepository, gameRepository, clientRepository, purchaseRepository); 
            JsonConcurrentServer server = new JsonConcurrentServer(ip, port, serviceImpl);

            server.Start();
        }
    }
}
