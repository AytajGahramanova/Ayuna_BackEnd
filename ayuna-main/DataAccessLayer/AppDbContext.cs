using ayuna_main.Models;
using Microsoft.EntityFrameworkCore;

namespace ayuna_main.DataAccessLayer
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Slider> sliders { get; set; }
		public DbSet<About> abouts { get; set; }
		public DbSet<Portfolio> portfolios { get; set; }
		public DbSet<GiftCard> giftCards { get; set; }
		public DbSet<Blog> blogs { get; set; }
		public DbSet<AboutBreadcrumb> aboutBreadcrumbs { get; set; }
		public DbSet<AboutSigniture> aboutSignitures { get; set; }
		public DbSet<AboutContent> aboutContents { get; set; }
		public DbSet<AboutFaq> aboutFaqs { get; set; }
		public DbSet<GiftCardPage> giftCardsPage { get; set; }
	}
}
