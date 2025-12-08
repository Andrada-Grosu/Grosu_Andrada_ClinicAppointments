using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grosu_Andrada_ClinicAppointments.Data;
using Grosu_Andrada_ClinicAppointments.Models;

namespace Grosu_Andrada_ClinicAppointments.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly Grosu_Andrada_ClinicAppointmentsContext _context;

        public DoctorsController(Grosu_Andrada_ClinicAppointmentsContext context)
        {
            _context = context;
        }

        // GET: Doctors
       
            public async Task<IActionResult> Index(string sortOrder, string? searchName, string? searchSpecialty)
        {
            // info pentru sortare
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            // info pentru filtre (le refolosim în view)
            ViewData["CurrentName"] = searchName;
            ViewData["CurrentSpecialty"] = searchSpecialty;

            var doctors = _context.Doctor
                .Include(d => d.Specialty)
                .AsQueryable();

            // 🔎 filtru după nume (prenume + nume)
            if (!string.IsNullOrEmpty(searchName))
            {
                var lowered = searchName.ToLower();
                doctors = doctors.Where(d =>
                    (d.FirstName + " " + d.LastName).ToLower().Contains(lowered));
            }

            // 🔎 filtru după specialitate (numele specializării)
            if (!string.IsNullOrEmpty(searchSpecialty))
            {
                var loweredSpec = searchSpecialty.ToLower();
                doctors = doctors.Where(d => d.Specialty.Name.ToLower().Contains(loweredSpec));
            }

            // 🔀 sortare după nume
            switch (sortOrder)
            {
                case "name_desc":
                    doctors = doctors
                        .OrderByDescending(d => d.LastName)
                        .ThenByDescending(d => d.FirstName);
                    break;

                default:
                    doctors = doctors
                        .OrderBy(d => d.LastName)
                        .ThenBy(d => d.FirstName);
                    break;
            }

            return View(await doctors.ToListAsync());
        }
        

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .Include(d => d.Specialty)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            ViewData["SpecialtyID"] = new SelectList(_context.Specialty, "ID", "Name");
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,SpecialtyID,Phone,Email")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecialtyID"] = new SelectList(_context.Specialty, "ID", "Name", doctor.SpecialtyID);
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["SpecialtyID"] = new SelectList(_context.Specialty, "ID", "Name", doctor.SpecialtyID);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,SpecialtyID,Phone,Email")] Doctor doctor)
        {
            if (id != doctor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.ID))
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
            ViewData["SpecialtyID"] = new SelectList(_context.Specialty, "ID", "Name", doctor.SpecialtyID);
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .Include(d => d.Specialty)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctor.Remove(doctor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.ID == id);
        }
    }
}
