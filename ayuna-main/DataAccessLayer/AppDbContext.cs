﻿using ayuna_main.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ayuna_main.DataAccessLayer
{
	public class AppDbContext : IdentityDbContext<AppUser>
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
		public DbSet<HeaderTop> headerTop { get; set; }
		public DbSet<HeaderNav> headerNav { get; set; }
		public DbSet<FooterContent> footerContent { get; set; }
		public DbSet<FooterLink> footerLink { get; set; }
		public DbSet<FooterCategory> footerCategory { get; set; } 
		public DbSet<FooterContact> footerContacts { get; set; }
	}
}
