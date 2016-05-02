using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Library.Repository.Models;
using Library.Repository.Repository;
using Library.Repository.Repository.IRepository;
using PagedList;
using static System.String;

namespace Library.Controllers
{
	[Authorize(Roles = "Admin")]
	public class BooksController : Controller
	{
		private IBookRepository _bookRepository;

		public BooksController(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}
        // GET: Books
        public ActionResult Index(bool? isEdit, bool? isDeleted, bool? isRent, bool? isReturn, bool? isError, bool? isCreated, int? page, string sortOrder)
        {
	        try
	        {
				int currentPage = page ?? 1;
		        int pageSize = 20;
				var books = _bookRepository.GetBooks();
		        ViewBag.CurrentSort = sortOrder;
		        ViewBag.Title = sortOrder == "Title" ? "TitleAsc" : "Title";
		        ViewBag.Author = sortOrder == "Author" ? "AuthorAsc" : "Author";
		        ViewBag.Isbn = sortOrder == "Isbn" ? "IsbnAsc" : "Isbn";
				ViewBag.CreationDate = sortOrder == "CreationDate" ? "CreationDateAsc" : "CreationDate";
				ViewBag.UpdateDate = sortOrder == "UpdateDate" ? "UpdateDateAsc" : "UpdateDate";

		        switch (sortOrder)
		        {
					case "Title":
				        books = books.OrderByDescending(b => b.Title).ToList();
				        break;
					case "TitleAsc":
						books = books.OrderBy(b => b.Title).ToList();
						break;
					case "Author":
						books = books.OrderByDescending(b => b.Author).ToList();
						break;
					case "AuthorAsc":
						books = books.OrderBy(b => b.Author).ToList();
						break;
					case "Isbn":
						books = books.OrderByDescending(b => b.Isbn).ToList();
						break;
					case "IsbnAsc":
						books = books.OrderBy(b => b.Isbn).ToList();
						break;
					case "CreationDate":
						books = books.OrderByDescending(b => b.CreationDate).ToList();
						break;
					case "CreationDateAsc":
						books = books.OrderBy(b => b.CreationDate).ToList();
						break;
					case "UpdateDate":
						books = books.OrderByDescending(b => b.UpdatenDate).ToList();
						break;
					case "UpdateDateAsc":
						books = books.OrderBy(b => b.UpdatenDate).ToList();
						break;
					default:
				        books = books?.OrderBy(b => b.Id).ToList();
				        break;
		        }

		        if (isDeleted != null && isDeleted.Value)
			        ViewBag.IsDeleted = true;
		        if (isEdit != null && isEdit.Value)
			        ViewBag.IsEdited = true;
				if (isError != null && isError.Value)
					ViewBag.IsError = true;
		        if (isRent != null && isRent.Value)
			        ViewBag.IsRent = true;
		        if (isReturn != null && isReturn.Value)
			        ViewBag.IsReturn = true;
				if (isCreated != null && isCreated.Value)
					ViewBag.IsCreated = true;
				
				var booksList = new List<Book>();

		        if (books != null)
			        booksList = books;

		        return View(booksList.ToPagedList<Book>(currentPage, pageSize));
	        }
	        catch (Exception e)
	        {
				ViewBag.Error = true;
	        }
			return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
	        try
	        {
		        if (id == null)
		        {
			        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		        }
		        List<RentHistory> bookHistory = _bookRepository.GetBookRentHistory(id.Value);
		        if (bookHistory == null || bookHistory.Count == 0)
		        {
			        return RedirectToAction("DetailsWithoutHistory", _bookRepository.GetBookById(id.Value));
		        }
		        return View(bookHistory);
	        }
	        catch (Exception e)
	        {
		        return RedirectToAction("Index", new {isError = true});
	        }
        }

        // GET: Books/Create
        public ActionResult Create()
        {
	        try
	        {
		        ViewBag.CategoryId = new SelectList(_bookRepository.GetCategories(), "Id", "Name");
		        return View();
	        }
			catch (Exception e)
			{
				return RedirectToAction("Index", new { isError = true });
			}
		}

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Author,CategoryId,Isbn,Available")] Book book)
        {
	        try
	        {
		        if (ModelState.IsValid)
		        {
			        _bookRepository.CreateBook(book);
			        _bookRepository.SaveChanges();
					return RedirectToAction("Index", new { isCreated = true });
				}

		        ViewBag.CategoryId = new SelectList(_bookRepository.GetCategories(), "Id", "Name", book.CategoryId);
		        return View(book);
	        }
			catch (Exception e)
			{
				return RedirectToAction("Index", new { isError = true });
			}
		}

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
	        try
	        {
		        if (id == null)
		        {
			        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		        }
		        Book book = _bookRepository.GetBookById(id.Value);
		        if (book == null)
		        {
			        return HttpNotFound();
		        }
		        ViewBag.CategoryId = new SelectList(_bookRepository.GetCategories(), "Id", "Name", book.CategoryId);
		        return View(book);
	        }
			catch (Exception e)
			{
				return RedirectToAction("Index", new { isError = true });
			}
		}

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,CategoryId,Isbn,Available,CreationDate")] Book book)
        {
	        try
	        {
		        if (ModelState.IsValid)
		        {
			        _bookRepository.UpdateBook(book);
			        _bookRepository.SaveChanges();
			        return RedirectToAction("Index", new {isEdit = true});
		        }
		        ViewBag.CategoryId = new SelectList(_bookRepository.GetCategories(), "Id", "Name", book.CategoryId);
		        return View(book);
	        }
			catch (Exception e)
			{
				return RedirectToAction("Index", new { isError = true });
			}
		}

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
	        try
	        {
		        if (id == null)
		        {
			        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		        }
		        Book book = _bookRepository.GetBookById(id.Value);
		        if (book == null)
		        {
			        return HttpNotFound();
		        }
		        return View(book);
	        }
			catch (Exception e)
			{
				return RedirectToAction("Index", new { isError = true });
			}
		}

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
	        try
	        {
		        Book book = _bookRepository.GetBookById(id);
		        _bookRepository.DeleteBook(id);
		        _bookRepository.SaveChanges();
		        return RedirectToAction("Index", new {isDeleted = true});
	        }
			catch (Exception e)
			{
				return RedirectToAction("Index", new { isError = true });
			}
		}

		// POST: Books/RentToFriend/5
		[HttpPost, ActionName("RentToFriend")]
		[ValidateAntiForgeryToken]
		public ActionResult RentToFriend(RentHistory rentHistory, int id)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var book = _bookRepository.GetBookById(id);
					_bookRepository.RentBookToFriend(rentHistory, book);
					_bookRepository.SaveChanges();
					return RedirectToAction("Index", new {isRent = true});
				}

				return View(rentHistory);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", new { isError = true });
			}

		}

		// GET: Books/ReturnBook/5
		public ActionResult ReturnBook(int id)
		{
			try
			{
				var book = _bookRepository.GetBookById(id);
				_bookRepository.ReturnBookFromFriend(book);
				_bookRepository.SaveChanges();
				return RedirectToAction("Index", new {isReturn = true});
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", new { isError = true });
			}
		}

		// GET: Books/RentToFriend/5
		public ActionResult RentToFriend(int? id)
		{
			try
			{
				if (id == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				Book book = _bookRepository.GetBookById(id.Value);
				if (book == null)
				{
					return HttpNotFound();
				}
				ViewBag.CategoryId = new SelectList(_bookRepository.GetCategories(), "Id", "Name", book.CategoryId);
				return View(new RentHistory() {BookId = book.Id});
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", new { isError = true });
			}
		}

		// GET: Books/Search/test
		public ActionResult Search(string searchString)
		{
			if (!IsNullOrEmpty(searchString))
			{
				if (_bookRepository.GetBooks().Any(x => x.Title.ToLower().Contains(searchString.ToLower())))
					ViewBag.SearchTitle = _bookRepository.GetBooks().Where(x => x.Title.Contains(searchString));
				if (_bookRepository.GetBooks().Any(x => x.Author.ToLower().Contains(searchString.ToLower())))
					ViewBag.SearchAuthor = _bookRepository.GetBooks().Where(x => x.Author.ToLower().Contains(searchString.ToLower()));
				if (_bookRepository.GetBooks().Any(x => x.Isbn.Contains(searchString)))
					ViewBag.SearchIsbn = _bookRepository.GetBooks().Where(x => x.Isbn.ToLower().Contains(searchString.ToLower()));
				if (_bookRepository.GetBooks().Any(x => x.Category.Name.Contains(searchString)))
					ViewBag.SearchCategory = _bookRepository.GetBooks().Where(x => x.Category.Name.ToLower().Contains(searchString.ToLower()));
				return View();
			}
			return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
		}

		public ActionResult DetailsWithoutHistory(Book book)
		{
			try
			{
				book = _bookRepository.GetBookById(book.Id);
				if (book == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
				return View(book);
			}
			catch (Exception e)
			{
				return RedirectToAction("Index", new { isError = true });
			}
		}
	}
}
