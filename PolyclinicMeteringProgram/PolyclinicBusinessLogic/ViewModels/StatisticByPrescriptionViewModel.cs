using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class StatisticByPrescriptionViewModel
    {
        [DisplayName("Стоимость рецепта")]
        public int Price { get; set; }
        [DisplayName("Адрес аптеки")]
        public string PharmacyAddress { get; set; }
    }
}
