using System.ComponentModel;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class PatientProcedureViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название процедуры")]
        public string ProcedureName { get; set; }
    }
}
