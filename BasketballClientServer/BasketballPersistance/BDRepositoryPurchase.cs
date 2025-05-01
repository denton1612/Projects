using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballPersistance.utils;
using BasketballModel.domain;
using BasketballModel.dtos;
using log4net;
using log4net.Util;

namespace BasketballPersistance.repository
{
    public class BDRepositoryPurchase : IRepositoryPurchase
    {
        private DbUtils dbUtils;
        private static readonly ILog log = LogManager.GetLogger(typeof(BDRepositoryPurchase));

        public BDRepositoryPurchase(string database) {
            dbUtils = new DbUtils(database);
        }

        public Purchase Add(Purchase entity)
        {
            log.Info("Adding purchase: " + entity.ToString());
            int result = -1;

            using (IDbConnection connection = dbUtils.GetConenction())
            {
                connection.Open();
                string sql = "insert into purchases (idClient, idGame, ticketCounter) values (@idClient, @idGame, @ticketCounter)";

                using (IDbCommand command = connection.CreateCommand()) {
                    command.CommandText = sql;
                    IDataParameter parameterClient = command.CreateParameter();
                    IDataParameter parameterGame = command.CreateParameter();
                    IDataParameter parameterTicket = command.CreateParameter();

                    parameterClient.ParameterName = "@idClient";
                    parameterClient.Value = entity.Client.Id;
                    parameterClient.DbType = DbType.Int64;

                    parameterGame.ParameterName = "@idGame";
                    parameterGame.Value = entity.Game.Id;
                    parameterGame.DbType = DbType.Int64;

                    parameterTicket.ParameterName = "@ticketCounter";
                    parameterTicket.Value = entity.TicketCounter;
                    parameterTicket.DbType = DbType.Int32;
                    
                    command.Parameters.Add(parameterClient);
                    command.Parameters.Add(parameterGame); 
                    command.Parameters.Add(parameterTicket);

                    result = command.ExecuteNonQuery();
                }
            }
            if (result > 0)
            {
                log.Info("Purchase was added.");
                return entity;
            }
            log.Error("Purchase " + entity.ToString() + " cannot be added");
            return null;
        }

        public Client findClientById(long id)
        {
            log.Info("Searching for client with id: " + id);
            Client client = null;
            using (IDbConnection connection = dbUtils.GetConenction()) { 
                connection.Open();
                string sql = "select name, address from clients where id = @id";

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    IDataParameter paramater = command.CreateParameter();
                    paramater.ParameterName = "@id";
                    paramater.Value = id;
                    paramater.DbType = DbType.Int64;

                    command.Parameters.Add(paramater);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            string address = reader.GetString(reader.GetOrdinal("address"));
                            client = new Client(name, address);
                            client.Id = id;
                        }
                    }
                }
            }
            log.InfoExt("Client found: " + client);
            return client;
        }

        public Game findGameById(long id) {
            log.Info("Searching for client with id: " + id);
            Game game = null;
            using (IDbConnection connection = dbUtils.GetConenction())
            {
                connection.Open();
                string sql = "select homeTeam, awayTeam, gameType, seats, startTime from games where id = @id";

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    IDataParameter paramater = command.CreateParameter();
                    paramater.ParameterName = "@id";
                    paramater.Value = id;
                    paramater.DbType = DbType.Int64;

                    command.Parameters.Add(paramater);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string homeTeam = reader.GetString(reader.GetOrdinal("homeTeam"));
                            string awayTeam = reader.GetString(reader.GetOrdinal("awayTeam"));
                            GameType gameType = (GameType)Enum.Parse(typeof(GameType), reader.GetString(reader.GetOrdinal("gameType")));
                            int seats = reader.GetInt32(reader.GetOrdinal("seats"));
                            DateTime startTime = DateTime.Parse(reader.GetString(reader.GetOrdinal("startTime")));
                            game = new Game(homeTeam, awayTeam, gameType, seats, startTime);
                            game.Id = id;
                        }
                    }
                }
            }
            log.InfoExt("Game found: " + game);
            return game;
        }

        public IEnumerable<Purchase> filterByNameAndAddress(string name, string address)
        {
            log.Info("Filter by client name and address is starting...");
            List<Purchase> purchases = new List<Purchase>();
            using (IDbConnection connection = dbUtils.GetConenction())
            {
                connection.Open();
                string sql = "select P.idClient, P.idGame, P.ticketCounter from purchases P inner join clients C on P.idClient = C.id where C.name = @name and C.address = @address";

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    IDataParameter parameterName = command.CreateParameter();
                    IDataParameter parameterAddress = command.CreateParameter();
                    parameterName.ParameterName = "@name";
                    parameterName.Value = name;
                    parameterAddress.ParameterName = "@address";
                    parameterAddress.Value = address;
                    command.Parameters.Add(parameterName);
                    command.Parameters.Add(parameterAddress);

                    List<(long idClient, long idGame, int ticketCounter)> tempData = new List<(long, long, int)>();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        long idClient, idGame;
                        int ticketCounter;
                        while (reader.Read())
                        {
                            idClient = reader.GetInt64(reader.GetOrdinal("idClient"));
                            idGame = reader.GetInt64(reader.GetOrdinal("idGame"));
                            ticketCounter = reader.GetInt32(reader.GetOrdinal("ticketCounter"));
                            tempData.Add((idClient, idGame, ticketCounter));
                        }
                    }
                    foreach (var (idClient, idGame, ticketCounter) in tempData)
                    {
                        Client client = findClientById(idClient);
                        Game game = findGameById(idGame);
                        purchases.Add(new Purchase(client, game, ticketCounter));
                    }
                }
            }
            log.InfoExt("Purchases were filtered");
            return purchases;
        }

        public IEnumerable<Purchase> filterByName(string name)
        {
            log.Info("Filter by client name is starting...");
            List<Purchase> purchases = new List<Purchase>();
            using (IDbConnection connection = dbUtils.GetConenction())
            {
                connection.Open();
                string sql = "select P.idClient, P.idGame, P.ticketCounter from purchases P inner join clients C on P.idClient = C.id where C.name = @name";

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@name";
                    parameter.Value = name;
                    command.Parameters.Add(parameter);

                    List<(long idClient, long idGame, int ticketCounter)> tempData = new List<(long, long, int)>();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        long idClient, idGame;
                        int ticketCounter;
                        while (reader.Read())
                        {
                            idClient = reader.GetInt64(reader.GetOrdinal("idClient"));
                            idGame = reader.GetInt64(reader.GetOrdinal("idGame"));
                            ticketCounter = reader.GetInt32(reader.GetOrdinal("ticketCounter"));
                            tempData.Add((idClient, idGame, ticketCounter));
                        }
                    }
                    foreach (var (idClient, idGame, ticketCounter) in tempData)
                    {
                        Client client = findClientById(idClient);
                        Game game = findGameById(idGame);
                        purchases.Add(new Purchase(client, game, ticketCounter));
                    }
                }
                log.InfoExt("Purchases were filtered");
                return purchases;
            }
        }

        public Purchase Delete(Purchase entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetAll()
        {
            throw new NotImplementedException();
        }

        public Purchase GetE(long id)
        {
            throw new NotImplementedException();
        }

        public Purchase Update(Purchase entity)
        {
            throw new NotImplementedException();
        }
    }
}
