
namespace Library.Repository.Migrations
{
	using Library.Repository.Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Library.Repository.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Library.Repository.Models.ApplicationDbContext context)
        {
			//For debuggin seed method
			//if (System.Diagnostics.Debugger.IsAttached.Equals(false))
			// System.Diagnostics.Debugger.Launch();
	        SeedRoles(context);
	        SeedUsers(context);
	        SeedCategories(context);
	        SeedBooks(context);
        }

		private void SeedRoles(ApplicationDbContext context)
		{
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
			if (!roleManager.RoleExists("Admin"))
			{
				var role = new IdentityRole { Name = "Admin" };
				roleManager.Create(role);
			}
		}
		private void SeedUsers(ApplicationDbContext context)
		{
			var store = new UserStore<ApplicationUser>(context);
			var manager = new UserManager<ApplicationUser>(store);
			if (!context.Users.Any(x => x.UserName == "admin@admin.pl"))
			{
				var user = new ApplicationUser
				{
					UserName = "admin@admin.pl",
					Email = "admin@admin.pl"
				};
				var adminResult = manager.Create(user, "adminADMIN");
				if (adminResult.Succeeded)
				{
					manager.AddToRole(user.Id, "Admin");
				}
			}
		}
		private void SeedBooks(ApplicationDbContext context)
	    {
		    var book1 = new Book()
		    {
			    Title = "Cooking Book",
			    Author = "Pan Kanapka",
			    Available = true,
				Isbn = "1234567890127",
				CategoryId = 2,
			    CreationDate = DateTime.Now,
				UpdatenDate = DateTime.Now
		    };
			context.Set<Book>().AddOrUpdate(book1);
			var book2 = new Book()
			{
				Title = "The Slowest Journey",
				Author = "Sir Lancelot",
				Available = true,
				Isbn = "1234567890126",
				CategoryId = 2,
				CreationDate = DateTime.Now,
				UpdatenDate = DateTime.Now

			};
			context.Set<Book>().AddOrUpdate(book2);
			var book3 = new Book()
			{
				Title = "The Myst",
				Author = "Jan Cloud",
				Available = true,
				Isbn = "1234567890125",
				CategoryId = 1,
				CreationDate = DateTime.Now,
				UpdatenDate = DateTime.Now
			};
			context.Set<Book>().AddOrUpdate(book3);
			var book4 = new Book()
			{
				Title = "Orinoko Flow",
				Author = "Zeus",
				Available = true,
				Isbn = "1234567890124",
				CategoryId = 1,
				CreationDate = DateTime.Now,
				UpdatenDate = DateTime.Now
			};
			context.Set<Book>().AddOrUpdate(book4);
			var book5 = new Book()
			{
				Title = "Prosperity is everything",
				Author = "Pan Kanapka",
				Available = true,
				Isbn = "1234567890123",
				CategoryId = 2,
				CreationDate = DateTime.Now,
				UpdatenDate = DateTime.Now
			};
			context.Set<Book>().AddOrUpdate(book5);
		    context.SaveChanges();
	    }
	    private void SeedCategories(ApplicationDbContext context)
	    {
		    var category1 = new Category()
		    {
			    Name = "Documentary",
			    CreationDate = DateTime.Now,
				UpdatenDate = DateTime.Now
			};
			context.Set<Category>().AddOrUpdate(category1);
			var category2 = new Category()
			{
				Name = "Science Fiction",
				CreationDate = DateTime.Now,
				UpdatenDate = DateTime.Now
			};
			context.Set<Category>().AddOrUpdate(category2);
			var category3 = new Category()
			{
				Name = "Fantasy",
				CreationDate = DateTime.Now,
				UpdatenDate = DateTime.Now
			};
			context.Set<Category>().AddOrUpdate(category3);
			context.SaveChanges();
		}

    }
}
