using Microsoft.AspNetCore.Mvc;
using Grosu_Andrada_ClinicAppointments.Services;
using Grosu_Andrada_ClinicAppointments.Models;
using System.Threading.Tasks;
using System.Linq; // Necesar pentru FirstOrDefault
using Grosu_Andrada_ClinicAppointments.Data; // Asigură-te că folosești spațiul de nume corect pentru Context

namespace Grosu_Andrada_ClinicAppointments.Controllers
{
    public class PredictionApiController : Controller
    {
        private readonly INoShowPredictionService _noShowService;
        // Injectăm Contextul bazei de date (presupunând numele standard)
        private readonly Grosu_Andrada_ClinicAppointmentsContext _context;

        public PredictionApiController(INoShowPredictionService noShowService, Grosu_Andrada_ClinicAppointmentsContext context)
        {
            _noShowService = noShowService;
            _context = context;
        }

        // GET: /PredictionApi/Index
        [HttpGet]
        public IActionResult Index(int? patientId) // Primim ID-ul pacientului din URL
        {
            var model = new NoShowPredictionViewModel();

            if (patientId.HasValue)
            {

                var patient = _context.Patient
                    .FirstOrDefault(p => p.ID == patientId.Value);

                if (patient != null)
                {
                    // Populează ViewModel-ul cu datele pacientului
                    model.PatientId = patient.ID;
                    model.Age = patient.Age; // Folosim Age din Patient.cs
                    model.Gender = patient.Gender; // Folosim Gender din Patient.cs

                    // Setează data programării la ziua de azi ca valoare implicită
                    model.AppointmentDay = DateTime.Today;
                }
            }

            return View(model);
        }

        // POST: /PredictionApi/Index (Rămâne neschimbat în logica de predicție)
        [HttpPost]
        public async Task<IActionResult> Index(NoShowPredictionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var input = new ModelInput
            {
                Age = model.Age,
                Gender = model.Gender,
                AppointmentDay = model.AppointmentDay
            };
            // apelăm Web API-ul
            var prediction = await _noShowService.PredictNoShowAsync(input);
            model.PredictedNoShow = prediction;
            return View(model);
        }
    }
}