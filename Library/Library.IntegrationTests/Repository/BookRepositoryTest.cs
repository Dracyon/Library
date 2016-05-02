using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Repository.Models;
using Library.Repository.Repository;
using NUnit.Framework;

namespace Library.IntegrationTests.Repository
{
	[TestFixture]
	public class BookRepositoryTest
	{
		private ApplicationDbContext dbContext;
		public BookRepositoryTest()
		{
			dbContext = new ApplicationDbContext();
		}
		[Test]
		public void ShouldGetAllBooks()
		{
			var sut = new BookRepository(dbContext);
			var expectedCount = 5;
			Assert.AreEqual(sut.GetBooks().Count, expectedCount);
		}

		[Test]
		public void ShouldGetVaildBookById()
		{
			var sut = new BookRepository(dbContext);
			var expectedTitle = "Cooking Book";
			Assert.AreEqual(sut.GetBookById(1).Title, expectedTitle);
		}
		[Test]
		public void ShouldCreateBookIsValid()
		{
			var sut = new BookRepository(dbContext);
			var book = new Book()
			{
				Title = "asd1",
				Author = "asd2",
				CategoryId = 2,
				Isbn = "1231231231231"
			};
			sut.CreateBook(book);
			var book2 = sut.GetBooks();
			//Assert.That(
			//	sut.GetBooks().Any(
			//		x =>
			//			x.Title == book.Title && x.Isbn == book.Isbn && x.Author == book.Author && x.Available == true));
			Assert.That(book2.Any(x => x.Title == book.Title), "Title");
			Assert.That(book2.Any(x => x.Author == book.Author),"Author");
			Assert.That(book2.Any(x => x.Isbn == book.Isbn), "isbn");
			Assert.That(book2.Any(x => x.Available == true), "available");
			Assert.That(book2.Any(x => x.Category.Name == "Science Fiction"), "category");

		}

	}
}
