using System.ComponentModel;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class StatisticByProcedureViewModel
    {
        [DisplayName("Название")]
        public string ProcedureName { get; set; }

        [DisplayName("Стоимость")]
        public int ProcedureCost { get; set; }
    }
}
