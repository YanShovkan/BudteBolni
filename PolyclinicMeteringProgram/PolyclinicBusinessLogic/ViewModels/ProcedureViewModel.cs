using System.Collections.Generic;
using System.ComponentModel;


namespace PolyclinicBusinessLogic.ViewModels
{
    public class ProcedureViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Стоимость")]
        public int Cost { get; set; }
        public Dictionary<int, (string, int)> ProcedureMedicines { get; set; }
        public Dictionary<int, string> ProcedureTreatments { get; set; }
    }
}
