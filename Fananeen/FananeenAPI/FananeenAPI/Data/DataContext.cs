using FananeenAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FananeenAPI.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<Artist> Artists { get; set; }
		public DbSet<Oeuvre> Oeuvres { get; set; }
		public DbSet<CategorieOeuvre> CategoriesOeuvre { get; set; }
		public DbSet<SousCategorieOeuvre> SousCategoriesOeuvre { get; set; }
		public DbSet<Client> Clients { get; set; }
        public DbSet<ArtForm> ArtForms { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Vente> Vente { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Commision> Commisions { get; set; }

    }
}
