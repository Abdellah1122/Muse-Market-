namespace MuseMarket.Models
{
    public class Commentaire
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public Client client { get; set; }
        public DateTime DateCommentaire { get; set; }
        public int IdArtist { get; set; }
    }
}
