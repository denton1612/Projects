using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballModel.domain;

namespace BasketballNetworking.dtos
{
    public class GameDTO
    {
        public long Id { get; set; }    
        public string HomeTeam {  get; set; }
        public string AwayTeam { get; set; }
        public GameType GameType { get; set; }
        public int Seats {  get; set; }

        public DateTime StartTime { get; set; }

        public GameDTO() { }

        public GameDTO(Game game)
        {
            Id = game.Id;
            HomeTeam = game.HomeTeam;
            AwayTeam = game.AwayTeam;
            Seats = game.Seats;
            StartTime = game.StartTime;
            GameType = game.GameType;
        }

        public override string ToString()
        {
            return string.Format("GameDTO[Id = {0}, HomeTeam = {1}, AwayTeam = {2}, GameType = {3}, Seats = {4}, StartTime = {5}]", Id, HomeTeam, AwayTeam, GameType, Seats, StartTime);
        }
    }
}
