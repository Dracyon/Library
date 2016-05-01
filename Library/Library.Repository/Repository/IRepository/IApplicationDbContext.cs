using System.Data.Entity;
using Library.Repository.Models;

namespace Library.Repository.Repository.IRepository
{
	public interface IApplicationDbContext
	{
		DbSet<Book> Books { get; set; }
		DbSet<RentHistory> RentHistories { get; set; }
		DbSet<Category> Categories { get; set; }
		Database Database { get; }
		int SaveChanges();
	}
}