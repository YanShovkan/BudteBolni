using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class StatisticByReceiptViewModel
    {
        [DisplayName("Имя доставщика")]
        public string DeliverymanName { get; set; }
        [DisplayName("Количество лекарств")]
        public int MedicineCount { get; set; }
    }
}
