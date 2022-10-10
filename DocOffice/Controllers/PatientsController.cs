using DocOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DocOffice.Controllers
{
  public class PatientsController : Controller
  {
    private readonly DocOfficeContext _db;

    public PatientsController(DocOfficeContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Patients.ToList());
    }

    public ActionResult Create()
    {
			ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patient patient, int DoctorId)
    {
      _db.Patients.Add(patient);
      _db.SaveChanges();
			if (DoctorId != 0)
			{
				_db.DoctorPatient.Add(new DoctorPatient() {DoctorId = DoctorId, PatientId = patient.PatientId });
				_db.SaveChanges();
			}
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisPatient = _db.Patients
        .Include(patient => patient.JoinEntities)
        .ThenInclude(join => join.Patient)
        .FirstOrDefault(patient => patient.PatientId == id);
      return View(thisPatient);
    }
  }
}