using System.ComponentModel.DataAnnotations;

namespace PolyclinicDatabase.Models
{
    public class MedicinePrescription
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int PrescriptionId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Medicine Medicine { get; set; }
        public virtual Prescription Prescription { get; set; }

    }
}
