using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.BindingModels
{
    public class MedicineBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActiveSubstance { get; set; }
        public string Classification { get; set; }
    }
}
