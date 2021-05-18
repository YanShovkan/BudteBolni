using System.ComponentModel;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class ProcedureMedicineViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название лекарства")]
        public string MedicineName { get; set; }
        [DisplayName("Количество")]
        public int MedicineCount { get; set; }
    }
}
