using System;
using System.Collections.Generic;

namespace PolyclinicBusinessLogic.BindingModels
{
    public class ProcedureBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public Dictionary<int, (string, int)> ProcedureMedicines { get; set; }
        public Dictionary<int, string> ProcedureTreatments { get; set; }
        public Dictionary<int, string> PatientProcedure { get; set; }
    }
}
