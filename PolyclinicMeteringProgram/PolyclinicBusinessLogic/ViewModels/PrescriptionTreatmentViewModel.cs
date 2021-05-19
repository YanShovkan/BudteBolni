using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class PrescriptionTreatmentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название лечения")]
        public string TreatmentName { get; set; }
        [DisplayName("Количество")]
        public int TreatmentCount { get; set; }
    }
}
