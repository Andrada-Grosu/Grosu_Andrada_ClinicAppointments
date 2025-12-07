namespace Grosu_Andrada_ClinicAppointments.Models
{
    public class Payment
    {
        public int ID { get; set; }

        public int AppointmentID { get; set; }
        public Appointment? Appointment { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }   // Cash / Card
        public string PaymentStatus { get; set; }   // Plătit / Neplătit
    }

}
