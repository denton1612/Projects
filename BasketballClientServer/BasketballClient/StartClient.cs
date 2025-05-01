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

                IService server = new ServerProxy(default_host, default_port);
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
