namespace MuseMarket.Models
{
    public class Commision
    { 
        public int Id { get; set; }
        public string Subject { get; set; }
        public string imageCommision { get; set; }
        public string body { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime WantedReturnDate { get; set; }
        public Client Client { get; set; }
        public SousCategorieOeuvre SousCategorieOeuvre { get; set; }
        public bool IsDone { get; set; }

    }
}
