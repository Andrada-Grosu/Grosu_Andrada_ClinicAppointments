using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grosu_Andrada_ClinicAppointments.Models;

namespace Grosu_Andrada_ClinicAppointments.Data
{
    public class Grosu_Andrada_ClinicAppointmentsContext : DbContext
    {
        public Grosu_Andrada_ClinicAppointmentsContext (DbContextOptions<Grosu_Andrada_ClinicAppointmentsContext> options)
            : base(options)
        {
        }

        public DbSet<Grosu_Andrada_ClinicAppointments.Models.Patient> Patient { get; set; } = default!;
        public DbSet<Grosu_Andrada_ClinicAppointments.Models.Specialty> Specialty { get; set; } = default!;
        public DbSet<Grosu_Andrada_ClinicAppointments.Models.Doctor> Doctor { get; set; } = default!;
        public DbSet<Grosu_Andrada_ClinicAppointments.Models.Appointment> Appointment { get; set; } = default!;
        public DbSet<Grosu_Andrada_ClinicAppointments.Models.Payment> Payment { get; set; } = default!;
    }
}
