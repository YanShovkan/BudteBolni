using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    // Поступление
    public class Receipt
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int NumberOfPackages { get; set; }
        [ForeignKey("ReceiptId")]
        public virtual List<MedicinePrescription> MedicinePrescriptions { get; set; }
    }
}
