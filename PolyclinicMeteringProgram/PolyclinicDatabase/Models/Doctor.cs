using System.ComponentModel.DataAnnotations;

namespace PolyclinicDatabase.Models
{
    // Доктор
    public class Doctor
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
