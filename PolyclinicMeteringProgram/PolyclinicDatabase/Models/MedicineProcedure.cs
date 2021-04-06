using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    public class MedicineProcedure
    {
        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual List<Medicine> MedicineId { get; set; }
        [ForeignKey("Id")]
        public virtual List<Procedure> ProcedureId { get; set; }
    }
}
