using Grpc.Net.Client;
using GrpcPatientService;
using LibraryModel.Models;

using Microsoft.AspNetCore.Mvc;

namespace Grosu_Andrada_ClinicAppointments.Controllers
{
    public class PatientsGrpcController : Controller
    {
        private readonly GrpcChannel channel;
        public IActionResult Index()
        {
            return View();
        }
    }
}
