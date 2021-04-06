using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{ 
    // Процедура Пациент
    public class ProcedurePatient
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ProcedureId { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Procedure Procedure { get; set; }
    }
}
