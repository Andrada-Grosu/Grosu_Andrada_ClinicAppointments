namespace Grosu_Andrada_ClinicAppointments.Models
{
    public class Patient
    {
        public int ID { get; set; }                // PK intern
        public long PatientKaggleId { get; set; }  // PatientId din CSV

        public string Gender { get; set; }         // "F"/"M"
        public int Age { get; set; }
        public string Neighbourhood { get; set; }  // din CSV

     
        public bool Scholarship { get; set; }
        public bool Hipertension { get; set; }
        public bool Diabetes { get; set; }
        public bool Alcoholism { get; set; }
        public int Handicap { get; set; }

    
        public ICollection<Appointment>? Appointments { get; set; }
    }

}
