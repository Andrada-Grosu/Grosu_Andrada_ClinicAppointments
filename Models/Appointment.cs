namespace Grosu_Andrada_ClinicAppointments.Models
{
    public class Appointment
    {
        public int ID { get; set; }                     // PK intern
        public int AppointmentKaggleId { get; set; }    // AppointmentID din CSV

        public int PatientID { get; set; }
        public Patient? Patient { get; set; }

        public int DoctorID { get; set; }
        public Doctor? Doctor { get; set; }

        public DateTime ScheduledDate { get; set; }     // ScheduledDay
        public DateTime AppointmentDate { get; set; }   // AppointmentDay

        public bool SmsReceived { get; set; }           // SMS_received
        public bool NoShow { get; set; }                // No-show (Yes/No)

       
        public float? NoShowRiskScore { get; set; }

        public Payment? Payment { get; set; }           // 1–1
    }

}
