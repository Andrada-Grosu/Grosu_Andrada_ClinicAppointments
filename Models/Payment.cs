using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grosu_Andrada_ClinicAppointments.Models
{
    public class Payment
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Programare")]
        public int AppointmentID { get; set; }
        public Appointment? Appointment { get; set; }

        [Display(Name = "Sumă")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }   

        [Required]
        [Display(Name = "Metodă de plată")]
        public string PaymentMethod { get; set; } = string.Empty;   

        [Display(Name = "Data plății")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Status plată")]
        public string? PaymentStatus { get; set; }  
    }
}
