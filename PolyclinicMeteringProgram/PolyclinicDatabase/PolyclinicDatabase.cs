using PolyclinicDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase
{
    public class PolyclinicDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-NSNKSRN;Initial Catalog=PolyclinicDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Doctor> Doctors { set; get; }
        public virtual DbSet<Medicine> Medicines { set; get; }
        public virtual DbSet<PrescriptionMedicine> PrescriptionMedicines { set; get; }
        public virtual DbSet<ProcedureMedicine> ProcedureMedicines { set; get; }
        public virtual DbSet<ReceiptMedicine> ReceiptMedicines { set; get; }
        public virtual DbSet<Patient> Patients { set; get; }
        public virtual DbSet<Pharmacist> Pharmacists { set; get; }
        public virtual DbSet<Prescription> Prescriptions { set; get; }
        public virtual DbSet<PrescriptionTreatment> PrescriptionTreatments { set; get; }
        public virtual DbSet<PatientProcedure> PatientProcedures { set; get; }
        public virtual DbSet<ProcedureTreatment> ProcedureTreatments { set; get; }
        public virtual DbSet<Procedure> Procedures { set; get; }
        public virtual DbSet<Receipt> Receipts { set; get; }
        public virtual DbSet<Treatment> Treatments { set; get; }
    }
}

