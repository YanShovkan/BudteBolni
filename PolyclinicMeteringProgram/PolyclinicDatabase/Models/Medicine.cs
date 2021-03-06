using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    // Лекарство
    public class Medicine
    {  
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ActiveSubstance { get; set; }
        [Required]
        public string Classification { get; set; }
        [Required]
        // Поступление id
        public int PharmacistId { get; set; }
        public virtual Pharmacist Pharmacist { get; set; }

        [ForeignKey("MedicineId")]
        public virtual List<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        [ForeignKey("MedicineId")]
        public virtual List<ProcedureMedicine> ProcedureMedicines { get; set; }
        [ForeignKey("MedicineId")]
        public virtual List<ReceiptMedicine> ReceiptMedicines { get; set; }
    }
}
