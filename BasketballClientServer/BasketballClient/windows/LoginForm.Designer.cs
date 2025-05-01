namespace BasketballClient.windows
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loginLabel = new Label();
            usernameLabel = new Label();
            passwordLabel = new Label();
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            loginButton = new Button();
            SuspendLayout();
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            loginLabel.Location = new Point(360, 31);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new Size(69, 30);
            loginLabel.TabIndex = 0;
            loginLabel.Text = "Login";
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            usernameLabel.Location = new Point(263, 162);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(84, 21);
            usernameLabel.TabIndex = 1;
            usernameLabel.Text = "Username:";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            passwordLabel.Location = new Point(263, 210);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(79, 21);
            passwordLabel.TabIndex = 2;
            passwordLabel.Text = "Password:";
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(374, 160);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(136, 23);
            usernameTextBox.TabIndex = 3;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(374, 212);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(136, 23);
            passwordTextBox.TabIndex = 4;
            // 
            // loginButton
            // 
            loginButton.Location = new Point(354, 322);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(75, 23);
            loginButton.TabIndex = 5;
            loginButton.Text = "Login";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(loginButton);
            Controls.Add(passwordTextBox);
            Controls.Add(usernameTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(usernameLabel);
            Controls.Add(loginLabel);
            Name = "LoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label loginLabel;
        private Label usernameLabel;
        private Label passwordLabel;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
    }
}