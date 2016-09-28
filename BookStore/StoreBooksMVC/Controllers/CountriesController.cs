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
using PagedList;

namespace StoreBooksMVC.Controllers
{
    public class CountriesController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();

        // GET: Countries
        public ActionResult Index(string sortedQuery, string currentFilter, string searchQuery, int page = 1)
        {
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

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortedQuery) ? "Country1_desc" : "";
            ViewBag.NameSortParm = sortedQuery == "ISO" ? "ISO_desc" : "ISO";

            var countries = from c in db.Countries select c;

            if (!String.IsNullOrEmpty(searchQuery))
            {
                countries = countries.Where(s => s.Country1.ToUpper().Contains(searchQuery.ToUpper())
                                || s.ISO.ToUpper().Contains(searchQuery.ToUpper()));
            }

            switch (sortedQuery)
            {
                case "Country_desc":
                    countries = countries.OrderByDescending(c => c.Country1);
                    break;
                case "ISO":
                    countries = countries.OrderBy(c => c.ISO);
                    break;
                default:
                    countries = countries.OrderBy(c => c.Country1);
                    break;
            }

            int pageSize = 50;

            return View(countries.ToPagedList(page, pageSize));
        }

        // GET: Countries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = await db.Countries.FindAsync(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Country1,ISO,PhoneCode")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Countries.Add(country);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = await db.Countries.FindAsync(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Country1,ISO,PhoneCode")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = await db.Countries.FindAsync(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Country country = await db.Countries.FindAsync(id);
            db.Countries.Remove(country);
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
