using System.Numerics;

namespace Grosu_Andrada_ClinicAppointments.Models
{
    public class Specialty
    {
        public int ID { get; set; }
        public string Name { get; set; }           
        public string? Description { get; set; }

        public ICollection<Doctor>? Doctors { get; set; }
    }
}
