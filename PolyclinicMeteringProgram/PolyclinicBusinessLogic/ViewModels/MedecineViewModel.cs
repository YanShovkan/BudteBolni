using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class MedecineViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название лекарства")]
        public string Name { get; set; }
        [DisplayName("Активное вещество")]
        public string ActiveSubstance { get; set; }
        [DisplayName("Классификация")]
        public string Classification { get; set; }
    }
}
