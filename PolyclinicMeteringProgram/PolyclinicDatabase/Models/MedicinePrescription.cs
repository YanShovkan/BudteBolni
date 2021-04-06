using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    public class MedicinePrescription
    {
        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual List<Medicine> MedicineId { get; set; }
        [ForeignKey("Id")]
        public virtual List<Prescription> PrescriptionId { get; set; }

    }
}
