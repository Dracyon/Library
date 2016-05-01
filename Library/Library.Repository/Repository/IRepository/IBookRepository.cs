using System.Collections.Generic;
using Library.Repository.Models;

namespace Library.Repository.Repository.IRepository
{
	public interface IBookRepository
	{
		List<Book> GetBooks();
		Book GetBookById(int id);
		void CreateBook(Book book);
		void DeleteBook(int id);
		void UpdateBook(Book book);
		IEnumerable<Category> GetCategories();
		void SaveChanges();
		void RentBookToFriend(RentHistory rentHistory);
		void ReturnBookFromFriend(Book book);
	}
}