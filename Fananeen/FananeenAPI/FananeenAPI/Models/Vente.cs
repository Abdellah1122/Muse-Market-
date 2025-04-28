namespace FananeenAPI.Models
{
    public class Vente
    {
        public int Id { get; set; }
        public DateTime? VenteDate { get; set; }
        public Client Client { get; set; }
        public Oeuvre Oeuvre { get; set; }
    }
}
