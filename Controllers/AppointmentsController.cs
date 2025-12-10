using Grosu_Andrada_ClinicAppointments.Data;
using Grosu_Andrada_ClinicAppointments.Models;
using Grosu_Andrada_ClinicAppointments.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Grosu_Andrada_ClinicAppointments.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly INoShowPredictionService _predictionService;
        private readonly Grosu_Andrada_ClinicAppointmentsContext _context;

        public AppointmentsController(
      INoShowPredictionService predictionService,
      Grosu_Andrada_ClinicAppointmentsContext context)
        {
            _predictionService = predictionService;
            _context = context;
        }

        

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = _context.Appointment
                .Include(a => a.Patient)
                .Include(a => a.Doctor);

            return View(await appointments.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointment
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (appointment == null) return NotFound();

            return View(appointment);
        }

        // GET: Appointments/Create
        [HttpGet]
        public IActionResult Create()
        {
            LoadDropdowns();

            ViewBag.PaymentMethods = new SelectList(new[]
            {
                "Cash la clinică",
                "Card la clinică",
                "Online acum"
            });

            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PatientID,DoctorID,AppointmentDate")] Appointment appointment,
            string paymentMethod)
        {
            bool alreadyBooked = await _context.Appointment.AnyAsync(a =>
                a.DoctorID == appointment.DoctorID &&
                a.AppointmentDate == appointment.AppointmentDate);

            if (alreadyBooked)
            {
                ModelState.AddModelError(string.Empty,
                    "Doctorul are deja o programare la această dată și oră.");
            }

            if (string.IsNullOrEmpty(paymentMethod))
            {
                ModelState.AddModelError("paymentMethod", "Selectează metoda de plată.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();

                var payment = new Payment
                {
                    AppointmentID = appointment.ID,
                    PaymentMethod = paymentMethod,
                    PaymentDate = paymentMethod == "Online acum"
                        ? DateTime.Now
                        : appointment.AppointmentDate,
                    Amount = 0m,
                    PaymentStatus = paymentMethod == "Online acum"
                        ? "Paid"
                        : "Pending"
                };

                _context.Payment.Add(payment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns(appointment.PatientID, appointment.DoctorID);
            ViewBag.PaymentMethods = new SelectList(new[]
            {
                "Cash la clinică",
                "Card la clinică",
                "Online acum"
            }, paymentMethod);

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null) return NotFound();

            LoadDropdowns(appointment.PatientID, appointment.DoctorID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PatientID,DoctorID,AppointmentDate")] Appointment appointment)
        {
            if (id != appointment.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.ID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns(appointment.PatientID, appointment.DoctorID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointment
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (appointment == null) return NotFound();

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.ID == id);
        }

        private void LoadDropdowns(int? selectedPatient = null, int? selectedDoctor = null)
        {
            var patients = _context.Patient
                .Select(p => new
                {
                    p.ID,
                    FullName = p.FirstName + " " + p.LastName
                })
                .ToList();

            ViewBag.PatientID = new SelectList(patients, "ID", "FullName", selectedPatient);

            var doctors = _context.Doctor
                .Include(d => d.Specialty)
                .Select(d => new
                {
                    d.ID,
                    FullName = d.FirstName + " " + d.LastName + " (" + d.Specialty.Name + ")"
                })
                .ToList();

            ViewBag.DoctorID = new SelectList(doctors, "ID", "FullName", selectedDoctor);
        }
    }
}
