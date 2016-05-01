using System.Data.Entity;
using Library.Repository.Models;
using Library.Repository.Repository.IRepository;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Library.Repository.Repository
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
	{
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

		public DbSet<Book> Books { get; set; }
		public DbSet<RentHistory> RentHistories { get; set; }
		public DbSet<Category> Categories { get; set; }
    }
}