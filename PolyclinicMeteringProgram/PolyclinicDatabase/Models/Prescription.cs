using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    // Рецепт
    public class Prescription
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullNameDoctor { get; set; }
        [Required]
        public string PharmacyAddress { get; set; }
    }
}
