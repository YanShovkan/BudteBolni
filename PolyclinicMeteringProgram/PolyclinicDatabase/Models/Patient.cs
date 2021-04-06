using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    // Пациент
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }        
        public int? PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        [ForeignKey("PatientId")]
        public virtual List<ProcedurePatient> ProcedurePatients { get; set; }
    }
}
