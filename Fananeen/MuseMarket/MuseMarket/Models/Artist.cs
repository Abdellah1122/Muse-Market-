namespace MuseMarket.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Bio { get; set; }
        public DateTime DatedeNaissance { get; set; }
        public string ImageArtist { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public bool IsAvailable { get; set; }
        public int NbrFoisVisite { get; set; }
        public int NbrOeuvreVendu { get; set; }
        public int Rating { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public ArtForm artForm { get; set; }
        public Ville ville { get; set; }
    }
}
