using System.ComponentModel.DataAnnotations;

namespace PolyclinicDatabase.Models
{
    public class MedicineProcedure
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int ProcedureId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Medicine Medicine { get; set; }
        public virtual Procedure Procedure { get; set; }
    }
}
