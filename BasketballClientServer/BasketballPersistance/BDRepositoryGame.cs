using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using BasketballPersistance.utils;
using BasketballModel.domain;
using Microsoft.Data.Sqlite;


namespace BasketballPersistance.repository
{
    public class BDRepositoryGame : IRepositoryGame
    {
        private readonly DbUtils dbUtils;
        private static readonly ILog log = LogManager.GetLogger(typeof(BDRepositoryGame));

        public BDRepositoryGame(string database) { 
            dbUtils = new DbUtils(database);
        }

        public Game Add(Game entity)
        {
            throw new NotImplementedException();
        }

        public Game Delete(Game entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> GetAll()
        {
            List<Game> games = new List<Game> ();
            using (IDbConnection connection = dbUtils.GetConenction()) { 
                connection.Open ();
                string sql = "select * from games";

                using (IDbCommand command = connection.CreateCommand()) { 
                    command.CommandText = sql;

                    using (IDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            string homeTeam = reader.GetString(reader.GetOrdinal("homeTeam"));
                            string awayTeam = reader.GetString(reader.GetOrdinal("awayTeam"));
                            GameType gameType = (GameType) Enum.Parse(typeof(GameType), reader.GetString(reader.GetOrdinal("gameType")));
                            int seats = reader.GetInt32(reader.GetOrdinal("seats"));
                            DateTime startTime = DateTime.Parse(reader.GetString(reader.GetOrdinal("startTime")));
                            Game game = new Game(homeTeam, awayTeam, gameType, seats, startTime);
                            game.Id = reader.GetInt64 (reader.GetOrdinal("id"));
                            games.Add(game);
                        }
                    }
                }
            }
            return games;
        }

        public Game GetE(long id)
        {
            throw new NotImplementedException();
        }

        public Game Update(Game entity)
        {   
            int result = -1;

            using (IDbConnection connection = dbUtils.GetConenction()) { 
                connection.Open();
                string sql = "update games set homeTeam = @hteam, awayTeam = @ateam, gameType = @gtype, seats = @seats, startTime = @stime where id = @idGame";

                using (IDbCommand command = connection.CreateCommand()) { 
                    command.CommandText = sql;
                    IDataParameter[] parameter = new IDataParameter[6];
                    for (int i = 0; i < 6; i++)
                    {
                        parameter[i] = new SqliteParameter();
                    }
                    parameter[0].ParameterName = "@hteam";
                    parameter[0].Value = entity.HomeTeam;
                    parameter[1].ParameterName = "@ateam";
                    parameter[1].Value = entity.AwayTeam;
                    parameter[2].ParameterName = "@gtype";
                    parameter[2].Value = entity.GameType.ToString();
                    parameter[3].ParameterName = "@seats";
                    parameter[3].Value = entity.Seats;
                    parameter[3].DbType = DbType.Int32;
                    parameter[4].ParameterName = "@stime";
                    parameter[4].Value = entity.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
                    parameter[5].ParameterName = "@idGame";
                    parameter[5].DbType = DbType.Int64;
                    parameter[5].Value = entity.Id;

                    for (int i = 0; i < 6; i++)
                    {
                        command.Parameters.Add(parameter[i]);
                    }

                    result = command.ExecuteNonQuery();
                }
            }
            if (result > 0) {
                return entity;
            }
            return null;
        }
    }
}
