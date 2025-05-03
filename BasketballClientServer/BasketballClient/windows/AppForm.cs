using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasketballClient.controllers;

namespace BasketballClient.windows
{
    public partial class AppForm : Form
    {
        private Controller _controller;

        public AppForm()
        {
            InitializeComponent();
        }

        public void SetController(Controller controller)
        {
            _controller = controller;
        }

        public DataGridView GetDataGridViewGames()
        {
            return dataGridViewGames;
        }

        public DataGridView GetDataGridViewTickets()
        {
            return dataGridViewTickets;
        }

        public TextBox GetCurrentPriceText()
        {
            return textBoxCurrentPrice;
        }

        public void SetCurrentPriceText(string text)
        {
            textBoxCurrentPrice.Text = text;
        }

        public string GetNameText()
        {
            return textBoxName.Text;
        }

        public string GetAddressText()
        {
            return textBoxAddress.Text;
        }

        public string GetSeatsText()
        {
            return textBoxSeats.Text;
        }

        public void SetNameText(string name)
        {
            textBoxName.Text = name;
        }

        public void SetAddressText(string address)
        {
            textBoxAddress.Text = address;
        }

        public void SetSeatsText(string seats)
        {
            textBoxSeats.Text = seats;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            _controller.Logout();
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {
            _controller.BuyTicket();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            _controller.GetPurchases();
        }

        private void dataGridViewGames_SelectionChanged(object sender, EventArgs e)
        {
            _controller.GamesSelectionChanged();
        }
    }
}
