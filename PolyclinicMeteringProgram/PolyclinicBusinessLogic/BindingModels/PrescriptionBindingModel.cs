using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.BindingModels
{
    public class PrescriptionBindingModel
    {
        public int? Id { get; set; }
        public int Price { get; set; }
        public string PharmacyAddress { get; set; }
        public Dictionary<int, (string, int)> PrescriptionMedicines { get; set; }
        public Dictionary<int, (string, int)> PrescriptionTreatment { get; set; }
    }
}
