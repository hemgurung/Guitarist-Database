using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcGuitarists.Models;
using System.Data.Entity;
using System.Net;

namespace mvcGuitarists.Controllers
{
    public class HomeController : Controller
    {
        private GuitaristDBEntities db = new GuitaristDBEntities();
        // GET: Guitarists
        public ActionResult Index(string guitaristGenre, string searchString, string sort)
        {
            var GenreList = new List<string>();
            var GenreQuery = from b in db.GuitaristTables
                            orderby b.Genre
                            select b.Genre;
            GenreList.AddRange(GenreQuery.Distinct());
            ViewBag.guitaristGenre = new SelectList(GenreList);

            var guitarists = from g in db.GuitaristTables
                             select g;

            if (!String.IsNullOrEmpty(guitaristGenre))
            {
                guitarists = guitarists.Where(x => x.Genre == guitaristGenre);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                guitarists = guitarists.Where(s => s.Name.Contains(searchString));
            }

            //SORTING   
            //var sortList = new List<string>();
            //sortList.Add("Date Added");
            //sortList.Add("Name");
            //ViewBag.sort = new SelectList(sortList);
            //if (String.Equals(sort, "Name"))
            //{
            //    guitarists = guitarists.OrderBy(n => n.Name);
            //}
            guitarists = guitarists.OrderBy(n => n.Name);
            return View(guitarists);
        }

        // GET: Guitarists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuitaristTable guitarist = db.GuitaristTables.Find(id);
            if (guitarist == null)
            {
                return HttpNotFound();
            }
            return View(guitarist);
        }

        // GET: Guitarists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guitarists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, Image, Name, Roles, MemberOf, Genre")] GuitaristTable guitarist)
        {
            if (guitarist.Image == null)
            {
                guitarist.Image = "http://cliparts.co/cliparts/pc7/M9z/pc7M9zqc9.png";
            }

            if (ModelState.IsValid)
            {
                db.GuitaristTables.Add(guitarist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guitarist);
        }

        // GET: Guitarists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuitaristTable guitarist = db.GuitaristTables.Find(id);
            if (guitarist == null)
            {
                return HttpNotFound();
            }
            return View(guitarist);
        }

        // POST: Guitarists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Image, Name, Roles, MemberOf, Genre")] GuitaristTable guitarist)
        {
            if (guitarist.Image == null)
            {
                guitarist.Image = "http://cliparts.co/cliparts/pc7/M9z/pc7M9zqc9.png";
            }

            if (ModelState.IsValid)
            {
                db.Entry(guitarist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guitarist);
        }

        // GET: Guitarists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuitaristTable guitarist = db.GuitaristTables.Find(id);
            if (guitarist == null)
            {
                return HttpNotFound();
            }
            return View(guitarist);
        }

        // POST: Guitarists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GuitaristTable guitarist = db.GuitaristTables.Find(id);
            db.GuitaristTables.Remove(guitarist);
            db.SaveChanges();
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