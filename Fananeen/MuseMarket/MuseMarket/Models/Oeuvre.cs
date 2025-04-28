namespace MuseMarket.Models
{
	public class Oeuvre
	{
		public int Id { get; set; }
		public string Titre { get; set; }
		public string Description { get; set; }
		public double Prix { get; set; }
		public DateTime DateDeCreation { get; set; }
		public bool IsLicensed { get; set; }
		public string ImageOeuvre1 { get; set; }
		public string ImageOeuvre2 { get; set; }
		public string ImageOeuvre3 { get; set; }
		public Artist Artist { get; set; }
        public SousCategorieOeuvre Scategorie { get; set; }
        public StatutOeuvre Statut { get; set; }


    }
}
