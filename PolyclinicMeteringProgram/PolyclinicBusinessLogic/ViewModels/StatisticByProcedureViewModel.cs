using System.ComponentModel;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class StatisticByPatientViewModel
    {
        [DisplayName("Имя пациента")]
        public string PatientName { get; set; }

        [DisplayName("Количество процедур")]
        public int ProcedureCount { get; set; }
    }
}
