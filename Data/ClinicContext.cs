
    using Grosu_Andrada_ClinicAppointments.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    namespace Grosu_Andrada_ClinicAppointments.Data
    {
        public class ClinicContext : DbContext
        {
            public ClinicContext(DbContextOptions<ClinicContext> options)
                : base(options)
            {
            }

            public DbSet<Patient> Patients { get; set; } = default!;
            public DbSet<Doctor> Doctors { get; set; } = default!;
            public DbSet<Specialty> Specialties { get; set; } = default!;
            public DbSet<Appointment> Appointments { get; set; } = default!;
            public DbSet<Payment> Payments { get; set; } = default!;
        }
    }


