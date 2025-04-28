namespace MuseMarket.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public Ville ville { get; set; }
        public string Addres { get; set; }
        public int Postcode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int NbrCommisionListed { get; set; }
        public int ReviewsMade { get; set; }
        public int ArtistsBooked { get; set; }
        public int WorksBought { get; set; }
    }
}
