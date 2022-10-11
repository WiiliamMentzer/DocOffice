using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocOffice.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DocOffice.Controllers
{
	public class SpecialtiesController : Controller
	{
		private readonly DocOfficeContext _db;

		public SpecialtiesController(DocOfficeContext db)
		{
			_db = db;
		}

		public ActionResult Index()
		{
			return View(_db.Specialties.ToList());
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Specialty specialty)
		{
			_db.Specialties.Add(specialty);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Details(int id)
		{
			Specialty thisSpec = _db.Specialties
				.Include(specialty => specialty.JoinEntSpec)
				.ThenInclude(join => join.Specialty)
				.FirstOrDefault(specialty => specialty.SpecialtyId == id);
			return View(thisSpec);
		}

		public ActionResult Edit(int id)
		{
			Specialty thisSpec = _db.Specialties.FirstOrDefault(specialty => specialty.SpecialtyId == id);
			return View(thisSpec);
		}

		[HttpPost]
		public ActionResult Edit(Specialty specialty)
		{
			_db.Entry(specialty).State = EntityState.Modified;
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			Specialty thisSpec = _db.Specialties.FirstOrDefault(specialty => specialty.SpecialtyId == id);
			return View(thisSpec);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Specialty thisSpec = _db.Specialties.FirstOrDefault(specialty => specialty.SpecialtyId == id);
			_db.Specialties.Remove(thisSpec);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}