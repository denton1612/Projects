namespace BasketballClient.windows
{
    partial class AppForm
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
            gamesLabel = new Label();
            dataGridViewGames = new DataGridView();
            labelName = new Label();
            labelAddress = new Label();
            labelSeats = new Label();
            textBoxName = new TextBox();
            textBoxAddress = new TextBox();
            textBoxSeats = new TextBox();
            buttonBuy = new Button();
            buttonSearch = new Button();
            label1 = new Label();
            dataGridViewTickets = new DataGridView();
            ClientName = new DataGridViewTextBoxColumn();
            Address = new DataGridViewTextBoxColumn();
            Game = new DataGridViewTextBoxColumn();
            TicketCounter = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            buttonLogout = new Button();
            labelCurrentPrice = new Label();
            textBoxCurrentPrice = new TextBox();
            HomeTeam = new DataGridViewTextBoxColumn();
            AwayTeam = new DataGridViewTextBoxColumn();
            GameType = new DataGridViewTextBoxColumn();
            Seats = new DataGridViewTextBoxColumn();
            PricePerSeat = new DataGridViewTextBoxColumn();
            StartTime = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGames).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTickets).BeginInit();
            SuspendLayout();
            // 
            // gamesLabel
            // 
            gamesLabel.AutoSize = true;
            gamesLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gamesLabel.Location = new Point(651, 44);
            gamesLabel.Name = "gamesLabel";
            gamesLabel.Size = new Size(78, 30);
            gamesLabel.TabIndex = 0;
            gamesLabel.Text = "Games";
            // 
            // dataGridViewGames
            // 
            dataGridViewGames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewGames.Columns.AddRange(new DataGridViewColumn[] { HomeTeam, AwayTeam, GameType, Seats, PricePerSeat, StartTime });
            dataGridViewGames.Location = new Point(258, 94);
            dataGridViewGames.Name = "dataGridViewGames";
            dataGridViewGames.Size = new Size(868, 150);
            dataGridViewGames.TabIndex = 1;
            dataGridViewGames.SelectionChanged += dataGridViewGames_SelectionChanged;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelName.Location = new Point(355, 282);
            labelName.Name = "labelName";
            labelName.Size = new Size(55, 21);
            labelName.TabIndex = 2;
            labelName.Text = "Name:";
            // 
            // labelAddress
            // 
            labelAddress.AutoSize = true;
            labelAddress.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelAddress.Location = new Point(639, 282);
            labelAddress.Name = "labelAddress";
            labelAddress.Size = new Size(69, 21);
            labelAddress.TabIndex = 3;
            labelAddress.Text = "Address:";
            // 
            // labelSeats
            // 
            labelSeats.AutoSize = true;
            labelSeats.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelSeats.Location = new Point(926, 284);
            labelSeats.Name = "labelSeats";
            labelSeats.Size = new Size(50, 21);
            labelSeats.TabIndex = 4;
            labelSeats.Text = "Seats:";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(416, 282);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(163, 23);
            textBoxName.TabIndex = 5;
            // 
            // textBoxAddress
            // 
            textBoxAddress.Location = new Point(714, 282);
            textBoxAddress.Name = "textBoxAddress";
            textBoxAddress.Size = new Size(149, 23);
            textBoxAddress.TabIndex = 6;
            // 
            // textBoxSeats
            // 
            textBoxSeats.Location = new Point(982, 284);
            textBoxSeats.Name = "textBoxSeats";
            textBoxSeats.Size = new Size(44, 23);
            textBoxSeats.TabIndex = 7;
            // 
            // buttonBuy
            // 
            buttonBuy.Location = new Point(551, 361);
            buttonBuy.Name = "buttonBuy";
            buttonBuy.Size = new Size(75, 23);
            buttonBuy.TabIndex = 8;
            buttonBuy.Text = "Buy";
            buttonBuy.UseVisualStyleBackColor = true;
            buttonBuy.Click += buttonBuy_Click;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(736, 361);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(75, 23);
            buttonSearch.TabIndex = 9;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(651, 398);
            label1.Name = "label1";
            label1.Size = new Size(81, 30);
            label1.TabIndex = 10;
            label1.Text = "Tickets";
            // 
            // dataGridViewTickets
            // 
            dataGridViewTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTickets.Columns.AddRange(new DataGridViewColumn[] { ClientName, Address, Game, TicketCounter, Price });
            dataGridViewTickets.Location = new Point(332, 443);
            dataGridViewTickets.Name = "dataGridViewTickets";
            dataGridViewTickets.Size = new Size(713, 116);
            dataGridViewTickets.TabIndex = 11;
            // 
            // ClientName
            // 
            ClientName.DataPropertyName = "Name";
            ClientName.HeaderText = "Name";
            ClientName.Name = "ClientName";
            ClientName.Width = 150;
            // 
            // Address
            // 
            Address.DataPropertyName = "Address";
            Address.HeaderText = "Address";
            Address.Name = "Address";
            Address.Width = 200;
            // 
            // Game
            // 
            Game.DataPropertyName = "Game";
            Game.HeaderText = "Game";
            Game.Name = "Game";
            Game.Width = 200;
            // 
            // TicketCounter
            // 
            TicketCounter.DataPropertyName = "TicketCounter";
            TicketCounter.HeaderText = "Ticket Counter";
            TicketCounter.Name = "TicketCounter";
            TicketCounter.Width = 60;
            // 
            // Price
            // 
            Price.DataPropertyName = "Price";
            Price.HeaderText = "Price";
            Price.Name = "Price";
            Price.Width = 60;
            // 
            // buttonLogout
            // 
            buttonLogout.Location = new Point(648, 592);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new Size(75, 23);
            buttonLogout.TabIndex = 12;
            buttonLogout.Text = "Logout";
            buttonLogout.UseVisualStyleBackColor = true;
            buttonLogout.Click += buttonLogout_Click;
            // 
            // labelCurrentPrice
            // 
            labelCurrentPrice.AutoSize = true;
            labelCurrentPrice.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCurrentPrice.Location = new Point(573, 324);
            labelCurrentPrice.Name = "labelCurrentPrice";
            labelCurrentPrice.Size = new Size(104, 21);
            labelCurrentPrice.TabIndex = 13;
            labelCurrentPrice.Text = "Current price:";
            // 
            // textBoxCurrentPrice
            // 
            textBoxCurrentPrice.ReadOnly = true;
            textBoxCurrentPrice.Location = new Point(706, 324);
            textBoxCurrentPrice.Name = "textBoxCurrentPrice";
            textBoxCurrentPrice.Size = new Size(100, 23);
            textBoxCurrentPrice.TabIndex = 14;
            // 
            // HomeTeam
            // 
            HomeTeam.DataPropertyName = "HomeTeam";
            HomeTeam.HeaderText = "Home Team";
            HomeTeam.Name = "HomeTeam";
            HomeTeam.Width = 200;
            // 
            // AwayTeam
            // 
            AwayTeam.DataPropertyName = "AwayTeam";
            AwayTeam.HeaderText = "Away Team";
            AwayTeam.Name = "AwayTeam";
            AwayTeam.Width = 200;
            // 
            // GameType
            // 
            GameType.DataPropertyName = "GameType";
            GameType.HeaderText = "Game Type";
            GameType.Name = "GameType";
            // 
            // Seats
            // 
            Seats.DataPropertyName = "Seats";
            Seats.HeaderText = "Seats";
            Seats.Name = "Seats";
            Seats.Width = 50;
            // 
            // PricePerSeat
            // 
            PricePerSeat.DataPropertyName = "Price";
            PricePerSeat.HeaderText = "Price per seat";
            PricePerSeat.Name = "PricePerSeat";
            PricePerSeat.Width = 75;
            // 
            // StartTime
            // 
            StartTime.DataPropertyName = "StartTime";
            StartTime.HeaderText = "Start Time";
            StartTime.Name = "StartTime";
            StartTime.Width = 200;
            // 
            // AppForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1414, 642);
            Controls.Add(textBoxCurrentPrice);
            Controls.Add(labelCurrentPrice);
            Controls.Add(buttonLogout);
            Controls.Add(dataGridViewTickets);
            Controls.Add(label1);
            Controls.Add(buttonSearch);
            Controls.Add(buttonBuy);
            Controls.Add(textBoxSeats);
            Controls.Add(textBoxAddress);
            Controls.Add(textBoxName);
            Controls.Add(labelSeats);
            Controls.Add(labelAddress);
            Controls.Add(labelName);
            Controls.Add(dataGridViewGames);
            Controls.Add(gamesLabel);
            Name = "AppForm";
            Text = "AppForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewGames).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTickets).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label gamesLabel;
        private DataGridView dataGridViewGames;
        private Label labelName;
        private Label labelAddress;
        private Label labelSeats;
        private TextBox textBoxName;
        private TextBox textBoxAddress;
        private TextBox textBoxSeats;
        private Button buttonBuy;
        private Button buttonSearch;
        private Label label1;
        private DataGridView dataGridViewTickets;
        private Button buttonLogout;
        private DataGridViewTextBoxColumn ClientName;
        private DataGridViewTextBoxColumn Address;
        private DataGridViewTextBoxColumn Game;
        private DataGridViewTextBoxColumn TicketCounter;
        private DataGridViewTextBoxColumn Price;
        private Label labelCurrentPrice;
        private TextBox textBoxCurrentPrice;
        private DataGridViewTextBoxColumn HomeTeam;
        private DataGridViewTextBoxColumn AwayTeam;
        private DataGridViewTextBoxColumn GameType;
        private DataGridViewTextBoxColumn Seats;
        private DataGridViewTextBoxColumn PricePerSeat;
        private DataGridViewTextBoxColumn StartTime;
    }
}