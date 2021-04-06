using System.ComponentModel;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя доктора")]
        public string FullName { get; set; }
        [DisplayName("Должность")]
        public string Position { get; set; }
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
