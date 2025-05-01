namespace BasketballModel.dtos
{
    public class PurchaseDTO
    {
        public string Name {  get; set; }
        public string Address { get; set; }
        public string Game {  get; set; }
        public int TicketCounter { get; set; }
        
        public PurchaseDTO(string name, string address, string game, int ticketCounter)
        {
            Name = name;
            Address = address;
            Game = game;
            TicketCounter = ticketCounter;
        }

        public override string ToString() { 
            return string.Format("PurchaseDTOModel[Name = {0}, Address = {1}, Game = {2}, TicketCounter = {3}]", Name, Address, Game, TicketCounter);
        }
    }
}
