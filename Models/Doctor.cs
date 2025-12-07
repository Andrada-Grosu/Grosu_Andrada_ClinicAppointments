namespace Grosu_Andrada_ClinicAppointments.Models
{
    public class Doctor
    {
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int SpecialtyID { get; set; }
        public Specialty? Specialty { get; set; }

        public string? Phone { get; set; }
        public string? Email { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
