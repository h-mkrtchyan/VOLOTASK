using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using System.IO;
using System.ComponentModel.DataAnnotations;
using PagedList;


namespace StoreBooksMVC.Controllers
{
    public class BooksController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();
        private BookStoreEntities DB = new BookStoreEntities();
        private const string imageUnavailable = "noBook.jpg";

        // GET: Books
        public ActionResult Index(string sortedQuery, string currentFilter, string searchQuery, int page = 1)
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.Country).Include(b => b.Genre);

            ViewBag.CurrentSort = sortedQuery;

            if (searchQuery != null)
            {
                page = 1;
            }
            else
            {
                searchQuery = currentFilter;
            }

            ViewBag.CurrentFilter = searchQuery;

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortedQuery) ? "Title_desc" : "";
            ViewBag.NameSortParam = sortedQuery == "Author" ? "Author_desc" : "Author";

            books = from s in db.Books select s;

            if (!String.IsNullOrEmpty(searchQuery))
            {
                books = books.Where(s => s.Title.ToUpper().Contains(searchQuery.ToUpper())
                                || s.Author.FullName.ToUpper().Contains(searchQuery.ToUpper())
                                || s.Genre.GenreName.ToUpper().Contains(searchQuery.ToUpper()));
            }

            switch (sortedQuery)
            {
                case "Title_desc":
                    books = books.OrderByDescending(s => s.Title);
                    break;
                case "Author":                                                          //sorting functionality
                    books = books.OrderByDescending(s => s.AuthorID);
                    break;
                default:
                    books = books.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 5;

            return View(books.ToPagedList(page, pageSize));
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FullName");
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Country1");
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "GenreName");

            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,AuthorID,GenreID,ImagePath,PageCount,Description,CountryID,Price")] Book book, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(path);
                    book.ImagePath = fileName;

                }
                else
                {
                    book.ImagePath = imageUnavailable;
                }
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FullName", book.AuthorID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Country1", book.CountryID);
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "GenreName", book.GenreID);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FullName", book.AuthorID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Country1", book.CountryID);
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "GenreName", book.GenreID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,AuthorID,GenreID,ImagePath,PageCount,Description,CountryID,Price")] Book book, HttpPostedFileBase file)
        {
            string imagePt = "";
            using (DB)
            {
                imagePt = DB.Books.Find(book.ID).ImagePath;
            }

            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    if (imagePt != imageUnavailable)
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Images"), imagePt));
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(path);
                    book.ImagePath = fileName;
                }
                else
                {
                    book.ImagePath = imagePt;
                }

                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FullName", book.AuthorID);
            ViewBag.CountryID = new SelectList(db.Countries, "ID", "Country1", book.CountryID);
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "GenreName", book.GenreID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);

            if (book.ImagePath != null && book.ImagePath != imageUnavailable)
            {
                var fileName = Path.GetFileName(book.ImagePath);
                var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
