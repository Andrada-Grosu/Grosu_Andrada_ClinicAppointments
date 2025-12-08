using Grosu_Andrada_ClinicAppointments.Data;
using Grosu_Andrada_ClinicAppointments.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grosu_Andrada_ClinicAppointments.Controllers
{
    public class PatientsController : Controller
    {
        private readonly Grosu_Andrada_ClinicAppointmentsContext _context;

        public PatientsController(Grosu_Andrada_ClinicAppointmentsContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index(string sortOrder, string? searchName, int? age, string? gender)
        {
            // pentru view (sortare curentă + parametrii de sortare pentru linkuri)
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AgeSortParm"] = sortOrder == "Age" ? "age_desc" : "Age";

            // filtre curente (le refolosim în view)
            ViewData["CurrentName"] = searchName;
            ViewData["CurrentAge"] = age;
            ViewData["CurrentGender"] = gender;

            var patients = from p in _context.Patient
                           select p;

            // 🔎 filtrare după nume (prenume + nume)
            if (!string.IsNullOrEmpty(searchName))
            {
                var lowered = searchName.ToLower();
                patients = patients.Where(p =>
                    (p.FirstName + " " + p.LastName).ToLower().Contains(lowered));
            }

           

            // filtrare după gen
            if (!string.IsNullOrEmpty(gender))
            {
                patients = patients.Where(p => p.Gender == gender);
            }

            // sortare
            switch (sortOrder)
            {
                case "name_desc":
                    patients = patients
                        .OrderByDescending(p => p.LastName)
                        .ThenByDescending(p => p.FirstName);
                    break;

                case "Age":
                    patients = patients.OrderBy(p => p.Age);
                    break;

                case "age_desc":
                    patients = patients.OrderByDescending(p => p.Age);
                    break;

                default:
                    // sortare implicită: Nume A-Z
                    patients = patients
                        .OrderBy(p => p.LastName)
                        .ThenBy(p => p.FirstName);
                    break;
            }

            return View(await patients.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.ID == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            ViewData["GenderList"] = new SelectList(new[]
     {
        new { Value = "Male", Text = "Masculin" },
        new { Value = "Female", Text = "Feminin" }
    }, "Value", "Text");

            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email,Phone,Gender,Age")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
                return NotFound();

            ViewData["GenderList"] = new SelectList(new[]
            {
        new { Value = "Male", Text = "Masculin" },
        new { Value = "Female", Text = "Feminin" }
    }, "Value", "Text", patient.Gender);

            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Email,Phone,Gender,Age")] Patient patient)
        {
            if (id != patient.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.ID == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient != null)
            {
                _context.Patient.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.ID == id);
        }
    }
}
