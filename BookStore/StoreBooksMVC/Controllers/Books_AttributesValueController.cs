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

namespace StoreBooksMVC.Controllers
{
    public class Books_AttributesValueController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();

        // GET: Books_AttributesValue
        public async Task<ActionResult> Index()
        {
            var books_AttributesValue = db.Books_AttributesValue.Include(b => b.Attribute).Include(b => b.Book);
            return View(await books_AttributesValue.ToListAsync());
        }

        // GET: Books_AttributesValue/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books_AttributesValue books_AttributesValue = await db.Books_AttributesValue.FindAsync(id);
            if (books_AttributesValue == null)
            {
                return HttpNotFound();
            }
            return View(books_AttributesValue);
        }

        // GET: Books_AttributesValue/Create\[Authorize]
        [Authorize]
        
        public ActionResult Create()
        {
            ViewBag.AttributesID = new SelectList(db.Attributes, "ID", "AttributeName");
            ViewBag.BooksID = new SelectList(db.Books, "ID", "Title");
            return View();
        }

        // POST: Books_AttributesValue/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
       
        public async Task<ActionResult> Create([Bind(Include = "ID,BooksID,AttributesID,Value")] Books_AttributesValue books_AttributesValue)
        {
            if (ModelState.IsValid)
            {
                db.Books_AttributesValue.Add(books_AttributesValue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AttributesID = new SelectList(db.Attributes, "ID", "AttributeName", books_AttributesValue.AttributesID);
            ViewBag.BooksID = new SelectList(db.Books, "ID", "Title", books_AttributesValue.BooksID);
            return View(books_AttributesValue);
        }

        // GET: Books_AttributesValue/Edit/5
        [Authorize]
      
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books_AttributesValue books_AttributesValue = await db.Books_AttributesValue.FindAsync(id);
            if (books_AttributesValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttributesID = new SelectList(db.Attributes, "ID", "AttributeName", books_AttributesValue.AttributesID);
            ViewBag.BooksID = new SelectList(db.Books, "ID", "Title", books_AttributesValue.BooksID);
            return View(books_AttributesValue);
        }

        // POST: Books_AttributesValue/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        
        public async Task<ActionResult> Edit([Bind(Include = "ID,BooksID,AttributesID,Value")] Books_AttributesValue books_AttributesValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(books_AttributesValue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AttributesID = new SelectList(db.Attributes, "ID", "AttributeName", books_AttributesValue.AttributesID);
            ViewBag.BooksID = new SelectList(db.Books, "ID", "Title", books_AttributesValue.BooksID);
            return View(books_AttributesValue);
        }

        // GET: Books_AttributesValue/Delete/5
        [Authorize]
      
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books_AttributesValue books_AttributesValue = await db.Books_AttributesValue.FindAsync(id);
            if (books_AttributesValue == null)
            {
                return HttpNotFound();
            }
            return View(books_AttributesValue);
        }

        // POST: Books_AttributesValue/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
      
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Books_AttributesValue books_AttributesValue = await db.Books_AttributesValue.FindAsync(id);
            db.Books_AttributesValue.Remove(books_AttributesValue);
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
