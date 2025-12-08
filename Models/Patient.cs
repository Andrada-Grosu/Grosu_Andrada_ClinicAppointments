using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Grosu_Andrada_ClinicAppointments.Models
{
    public class Patient
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Prenume")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nume")]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Display(Name = "Gen")]
        public string Gender { get; set; }

        [Range(0, 120)]
        [Display(Name = "Vârstă")]
        public int Age { get; set; }

    
        public ICollection<Appointment>? Appointments { get; set; }
    }

}
