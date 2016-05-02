using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Repository.Models;
using Library.Repository.Repository;
using NUnit.Framework;

namespace Library.IntegrationTests
{
	[SetUpFixture]
	public class TestFixtureLifecycle : IDisposable
	{
		public TestFixtureLifecycle()
		{
			EnsureDataDirectoryConnectionStringPlaceholderIsSet();
			EnsureNoExistingDatabaseFiles();
			var dbContext = new ApplicationDbContext();
			SeedCategories(dbContext);
			SeedBooks(dbContext);
		}

		private static void EnsureDataDirectoryConnectionStringPlaceholderIsSet()
		{
			AppDomain.CurrentDomain.SetData("DataDirectory", NUnit.Framework.TestContext.CurrentContext.TestDirectory);
		}

		private static void EnsureNoExistingDatabaseFiles()
		{
			const string connetionString = "name=DefaultConnection";
			if (Database.Exists(connetionString))
			{
				Database.Delete(connetionString);
			}
		}
		public void Dispose()
		{
			EnsureNoExistingDatabaseFiles();
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
