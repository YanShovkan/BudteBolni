using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    public class ProcedureTreatment
    {
        public int Id { get; set; }
        public int TreatmentId { get; set; }
        public int ProcedureId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Procedure Procedure { get; set; }
    }
}
