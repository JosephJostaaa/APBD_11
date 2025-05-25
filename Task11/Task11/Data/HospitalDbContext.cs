using Microsoft.EntityFrameworkCore;
using Task11.Model;

namespace Task11.Data;

public class HospitalDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Patient> Patients { get; set; }
    

    public HospitalDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected HospitalDbContext()
    {
    }
}