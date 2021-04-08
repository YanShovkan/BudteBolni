using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    // Рецепт
    public class Prescription
    {
        public int Id { get; set; }
        [Required]
        public string FullNameDoctor { get; set; }
        [Required]
        public string PharmacyAddress { get; set; }
        [ForeignKey("PrescriptionId")]
        public virtual List<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        [ForeignKey("PrescriptionId")]
        public virtual List<PrescriptionTreatment> PrescriptionTreatments { get; set; }
    }
}
