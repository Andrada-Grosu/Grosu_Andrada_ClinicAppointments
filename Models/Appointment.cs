using System;
using System.ComponentModel.DataAnnotations;

namespace Grosu_Andrada_ClinicAppointments.Models
{
    public class Appointment
    {
        public int ID { get; set; }   

        
        [Required]
        [Display(Name = "Pacient")]
        public int PatientID { get; set; }
        public Patient? Patient { get; set; }

        
        [Required]
        [Display(Name = "Doctor")]
        public int DoctorID { get; set; }
        public Doctor? Doctor { get; set; }

    
        [Required]
        [Display(Name = "Data și ora")]
        public DateTime AppointmentDate { get; set; }

        
        public Payment? Payment { get; set; }
    }
}
