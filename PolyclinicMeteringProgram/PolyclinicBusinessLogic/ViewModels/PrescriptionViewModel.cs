using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class PrescriptionViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО доктора")]
        public string FullNameDoctor { get; set; }
        [DisplayName("Адрес аптеки")]
        public string PharmacyAddress { get; set; }
    }
}
