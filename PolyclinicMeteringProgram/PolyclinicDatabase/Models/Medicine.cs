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
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ActiveSubstance { get; set; }
        [Required]
        public string Classification { get; set; }
        [Required]
        // Поступление id
        public int ReceiptId { get; set; }
    }
}
