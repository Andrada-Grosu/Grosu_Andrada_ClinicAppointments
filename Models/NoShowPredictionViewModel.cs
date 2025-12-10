using System;

namespace Grosu_Andrada_ClinicAppointments.Models
{
    public class NoShowPredictionViewModel
    {
        public int? PatientId { get; set; }
        public string Gender { get; set; }
        public DateTime AppointmentDay { get; set; }
        public int Age { get; set; }

       
        public string? PredictedNoShow { get; set; }
    }
}
