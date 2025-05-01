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
    public partial class LoginForm : Form
    {
        private Controller _controller;

        public LoginForm()
        {
            InitializeComponent();
            
        }

        public void SetController(Controller controller)
        {
            _controller = controller;
        }

        public string GetUsernameText()
        {
            return usernameTextBox.Text;
        }

        public void SetUsernameText(string username)
        {
            usernameTextBox.Text = username;
        }

        public string GetPasswordText()
        {
            return passwordTextBox.Text;
        }

        public void SetPasswordText(string password)
        {
            passwordTextBox.Text = password;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            _controller.Login();
        }
    }
}
