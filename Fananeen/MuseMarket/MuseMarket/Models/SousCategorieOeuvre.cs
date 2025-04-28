namespace MuseMarket.Models
{
	public class SousCategorieOeuvre
	{
		public int Id { get; set; }
		public string Nom { get; set; }
		public string Description { get; set; }
		public CategorieOeuvre Categorie { get; set; }

	}
}
