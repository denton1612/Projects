using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasketballClient.controllers;
using BasketballClient.windows;
using BasketballNetworking.json_protocol;
using BasketballServices;
using log4net;
using log4net.Config;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "config/log4net.config", Watch = true)]
namespace BasketballClient
{
    public class StartClient
    {
        private static int default_port = 55555;
        private static string default_host = "127.0.0.1";
        private static readonly ILog log = LogManager.GetLogger(typeof(StartClient));

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            
                // configurare jurnalizare folosind log4net
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            
                int port = default_port;
                string ip = default_ip;
            
                string portConfig = ConfigurationManager.AppSettings["port"];
                if (portConfig == null) { 
                    log.Debug("Port property not set. Using default value: " +  port);
                }
                else
                {
                    bool result = Int32.TryParse(portConfig, out port);
                    if (!result)
                    {
                        log.Debug("Port property not a number. Using default value: " + port);
                        port = default_port;
                    }
                }
            
                string ipConfig = ConfigurationManager.AppSettings["ip"];
                if (ipConfig == null) { 
                    log.Debug("Ip property not set. Using default ip: " + ip);
                }
                else
                {
                    ip = ipConfig;
                }
            
                IService server = new ServerProxyProtobuf(ip, port);
                LoginForm loginForm = new LoginForm();
                Controller loginController = new Controller(loginForm, server);
                loginForm.SetController(loginController);
            
                Application.Run(loginForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Startup error: " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }
    }
}
