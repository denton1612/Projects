using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;
using BasketballPersistance.utils;
using log4net;

namespace BasketballPersistance.repository
{
    public class BDRepositoryClient : IRepositoryClient
    {
        private DbUtils dbUtils;
        private static readonly ILog log = LogManager.GetLogger(typeof(BDRepositoryClient));

        public BDRepositoryClient(string database)
        {
            dbUtils = new DbUtils(database);
        }

        public Client Add(Client entity)
        {
            log.Info("Adding client: " + entity.ToString());
                using (IDbConnection connection = dbUtils.GetConenction())
                {
                    connection.Open();
                    string sql = "insert into clients (name, address) values (@name, @address)";

                    int result = -1;

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;

                        IDbDataParameter parameterName = command.CreateParameter();
                        IDbDataParameter parameterAddress = command.CreateParameter();

                        parameterName.ParameterName = "@name";
                        parameterAddress.ParameterName = "@address";
                        parameterName.Value = entity.Name;
                        parameterAddress.Value = entity.Address;

                        command.Parameters.Add(parameterName);
                        command.Parameters.Add(parameterAddress);

                        result = command.ExecuteNonQuery();
                    }

                    if (result > 0)
                    {
                        log.Info("Client " + entity.ToString() + " was added.");
                        return entity;
                    }
                    log.Error("Adding client impossible.");
                    return null;
                }
        }

        public Client Delete(Client entity)
        {
            throw new NotImplementedException();
        }

        public Client findByNameAndAddress(string name, string address)
        {
            Client client = null;
            using (IDbConnection connection = dbUtils.GetConenction()) {
                connection.Open();
                string sql = "select * from clients where name = @name and address = @address";

                using (IDbCommand command = connection.CreateCommand()) { 
                    command.CommandText= sql;

                    IDataParameter parameterName = command.CreateParameter();
                    IDataParameter parameterAddress = command.CreateParameter();
                    parameterName.ParameterName = "@name";
                    parameterAddress.ParameterName = "@address";
                    parameterName.Value= name;
                    parameterAddress.Value = address;

                    command.Parameters.Add(parameterName);
                    command.Parameters.Add(parameterAddress);

                    using (IDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            client = new Client(reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("address")));
                            client.Id = reader.GetInt64(reader.GetOrdinal("id"));
                        }
                    }
                }
            }
            return client;
        }

        public IEnumerable<Client> findByName(string name)
        {
            List<Client> clients = new List<Client>();
            using (IDbConnection connection = dbUtils.GetConenction())
            {
                connection.Open();
                string sql = "select * from clients where name = @name";

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;

                    IDataParameter parameterName = command.CreateParameter();
                    parameterName.ParameterName = "@name";
                    parameterName.Value = name;
 

                    command.Parameters.Add(parameterName);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Client client = new Client(reader.GetString(reader.GetOrdinal("name")), reader.GetString(reader.GetOrdinal("address")));
                            client.Id = reader.GetInt64(reader.GetOrdinal("id"));
                            clients.Add(client);
                        }
                    }
                }
            }
            return clients;
        }

        public IEnumerable<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client GetE(long id)
        {
            throw new NotImplementedException();
        }

        public Client Update(Client entity)
        {
            throw new NotImplementedException();
        }
    }
}
