using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicDatabase.Models
{
    // Процедура
    public class Procedure
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Cost { get; set; }
    }
}
