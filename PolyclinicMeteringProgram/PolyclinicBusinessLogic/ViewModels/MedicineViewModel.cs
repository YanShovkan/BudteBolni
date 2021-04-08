using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class MedicineViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название лекарства")]
        public string Name { get; set; }
        [DisplayName("Активное вещество")]
        public string ActiveSubstance { get; set; }
        [DisplayName("Классификация")]
        public string Classification { get; set; }
        public int PharmacistId { get; set; }
        [DisplayName("Имя аптекаря")]
        public string PharmacistName { get; set; }
    }
}
