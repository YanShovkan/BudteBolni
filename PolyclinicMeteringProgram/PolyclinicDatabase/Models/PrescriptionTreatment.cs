using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    // Рецепт лечение 
    public class PrescriptionTreatment
    {
        public int Id { get; set; }
        public int TreatmentId { get; set; }
        public int PrescriptionId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
