using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;
using BasketballPersistance.utils;
using log4net;

namespace BasketballPersistance.repository
{
    public class BDRepositoryCashier : IRepositoryCashier
    {
        private readonly DbUtils dbUtils;
        private static readonly ILog log = LogManager.GetLogger(typeof(BDRepositoryCashier));

        public BDRepositoryCashier(string database)
        {
            dbUtils = new DbUtils(database);
        }

        public Cashier Add(Cashier entity)
        {
            throw new NotImplementedException();
        }

        public Cashier Delete(Cashier entity)
        {
            throw new NotImplementedException();
        }

        public Cashier findByUsernameAndPassowrd(string username, string password)
        {
            log.DebugFormat("Find cashier in basketball.db by username = {0} and password = {1}", username, password);
            Cashier entity = null;
            try
            {
                using (IDbConnection connection = dbUtils.GetConenction())
                {
                    connection.Open();
                    string sql = "select * from cashiers where username = @username and password = @password";

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;

                        IDbDataParameter parameterUsername = command.CreateParameter();
                        IDbDataParameter parameterPassword = command.CreateParameter();

                        parameterUsername.ParameterName = "@username";
                        parameterPassword.ParameterName = "@password";
                        parameterUsername.Value = username;
                        parameterPassword.Value = password;

                        command.Parameters.Add(parameterUsername);
                        command.Parameters.Add(parameterPassword);

                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                entity = new Cashier(reader.GetString(reader.GetOrdinal("username")), reader.GetString(reader.GetOrdinal("password")));
                                entity.Id = reader.GetInt64(reader.GetOrdinal("id"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { 
                log.Error(ex);
            }
            return entity;
        }

        public IEnumerable<Cashier> GetAll()
        {
            throw new NotImplementedException();
        }

        public Cashier GetE(long id)
        {
            throw new NotImplementedException();
        }

        public Cashier Update(Cashier entity)
        {
            throw new NotImplementedException();
        }
    }
}
