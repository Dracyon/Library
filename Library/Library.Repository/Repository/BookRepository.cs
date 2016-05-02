using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Library.Repository.Models;
using Library.Repository.Repository.IRepository;

namespace Library.Repository.Repository
{
	public class BookRepository : IBookRepository
	{
		private readonly ApplicationDbContext _db;
		public BookRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public List<Book> GetBooks()
		{
			var books = _db.Books.Include(b => b.Category).Include(b => b.RentHistories).ToList();
			return books;
		}

		public Book GetBookById(int id)
		{
			var book = _db.Books.Include(b => b.Category).Include(b => b.RentHistories).First(b => b.Id == id);
			return book;
		}

		public void CreateBook(Book book)
		{
			book.CreationDate = DateTime.Now;
			book.UpdatenDate = DateTime.Now;
			book.Available = true;
			book.RentHistories = new List<RentHistory>();
			_db.Books.Add(book);
		}

		public void DeleteBook(int id)
		{
			Book book = GetBookById(id);
			_db.Books.Remove(book);
		}

		public void UpdateBook(Book book)
		{
			book.UpdatenDate = DateTime.Now;
			_db.Entry(book).State = EntityState.Modified;
		}

		public IEnumerable<Category> GetCategories()
		{
			return _db.Categories;
		}
		public void SaveChanges()
		{
			_db.SaveChanges();
		}

		public void RentBookToFriend(RentHistory rentHistory, Book book)
		{
			book.Available = false;
			book.RentHistories.Add(new RentHistory()
			{
			   RenterName =  rentHistory.RenterName,
			   UpdatenDate = DateTime.Now,
			   CreationDate = DateTime.Now,
			   Comment = rentHistory.Comment
			});
			book.UpdatenDate = DateTime.Now;
			_db.Entry(book).State = EntityState.Modified;
		}

		public void ReturnBookFromFriend(Book book)
		{
			book.Available = true;
			book.UpdatenDate = DateTime.Now;
			_db.Entry(book).State = EntityState.Modified;
		}

		public List<RentHistory> GetBookRentHistory(int id)
		{
			return _db.RentHistories.Where(x => x.BookId == id).Include(x => x.Book).ToList();
		}
	}
}