using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    // Лекарство лечение 
    public class PrescriptionTreatment
    {
        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual List<Prescription> PrescriptionId { get; set; }
        [ForeignKey("Id")]
        public virtual List<Treatment> TreatmentId { get; set; }
    }
}
