using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolyclinicDatabase.Models
{
    // Доктор
    public class Doctor
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Position { get; set; }
        [ForeignKey("DoctorId")]
        public virtual List<Patient> Patients { get; set; }
    }
}
