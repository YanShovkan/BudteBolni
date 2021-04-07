using System.ComponentModel;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class TreatmentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Срочность")]
        public string Urgency { get; set; }
        [DisplayName("Область действия")]
        public string AreaOfAction { get; set; }
    }
}
