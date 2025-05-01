using System.ComponentModel;
using BasketballClient.windows;
using BasketballModel.domain;
using BasketballModel.dtos;
using BasketballNetworking.dtos;
using BasketballServices;
using log4net;

namespace BasketballClient.controllers
{
    public class Controller : IObserver
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Controller));
        private readonly LoginForm _loginForm;
        private Cashier _cashier;
        private AppForm _appForm;
        private readonly IService _server;

        private readonly BindingList<Game> _games;

        public Controller(LoginForm loginForm, IService server)
        {
            _loginForm = loginForm;
            _server = server;
            _games = new BindingList<Game>();
        }

        private void InitGames()
        {
            _appForm.GetDataGridViewGames().DataSource = _games;
            _appForm.GetDataGridViewGames().AutoGenerateColumns = false;
            Game[] games = _server.GetGames();
            foreach (Game game in games)
            {
                _games.Add(game);
            }
        }

        private void InitAppForm()
        {
            _appForm = new AppForm();
            _appForm.SetController(this);
            InitGames();
        }

        public void Login()
        {
            log.Debug("Butonul login a fost apasat");
            string username = _loginForm.GetUsernameText();
            string password = _loginForm.GetPasswordText();

            Cashier cashier = new Cashier(username, password);
            try
            {
                _server.Login(cashier, this);
                _cashier = cashier;

                // cream si afisam app form-ul
                InitAppForm();
                _appForm.Show();

                // ascundem login form-ul (pentru un eventual logout il pastram in memorie)
                _loginForm.Hide();
                _loginForm.SetUsernameText("");
                _loginForm.SetPasswordText("");
            }
            catch (ServiceException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Logout()
        {
            log.Debug("Butonul logout a fost apasat");
            _server.Logout(_cashier, this);
            _appForm.Close();
            _loginForm.Show();
        }

        public void BuyTicket()
        {
            string name = _appForm.GetNameText();
            string address = _appForm.GetAddressText();
            Client client = new Client(name, address);
            string seats = _appForm.GetSeatsText();
            Game game = null;
            if (_appForm.GetDataGridViewGames().SelectedRows.Count > 0)
            {
                game = (Game)_appForm.GetDataGridViewGames().SelectedRows[0].DataBoundItem;
            }

            try
            {
                _server.BuyTicket(client, seats, game);
                MessageBox.Show(name + " bought " + seats + " seats at " + game);
                _appForm.SetNameText("");
                _appForm.SetAddressText("");
                _appForm.SetSeatsText("");
            }
            catch (ServiceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BoughtTicket(Game game)
        {
            for (int i = 0; i < _games.Count; i++)
            {
                Game currentGame = _games[i];
                if (currentGame.Id == game.Id)
                {
                    _games[i] = game;
                    break;
                }
            }
        }

        private BasketballModel.dtos.PurchaseDTO[] GetPurchasesDTO(Purchase[] purchases)
        {
            List<BasketballModel.dtos.PurchaseDTO> purchasesDTO = new List<BasketballModel.dtos.PurchaseDTO>();

            foreach (Purchase purchase in purchases) {
                purchasesDTO.Add(new BasketballModel.dtos.PurchaseDTO(purchase.Client.Name, purchase.Client.Address, purchase.Game.ToString(), purchase.TicketCounter));
            }

            return purchasesDTO.ToArray();
        }

        public void GetPurchases()
        {
            string name = _appForm.GetNameText();
            string address = _appForm.GetAddressText();

            Purchase[] purchases = _server.GetPurchasesByNameAndAddress(new Client(name, address));
            BasketballModel.dtos.PurchaseDTO[] purchasesDTOModel = GetPurchasesDTO(purchases);

            _appForm.GetDataGridViewTickets().DataSource = purchasesDTOModel;
            _appForm.SetNameText(""); _appForm.SetAddressText("");
        }
    }
}
