using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.ViewModels
{
    class ReceiptViewModel
    {
        public int Id { get; set; }
        [DisplayName("Дата поступления")]
        public DateTime Date { get; set; }
        [DisplayName("Количество упаковок")]
        public int NumberOfPackages { get; set; }
        [DisplayName("Список лекарств")]
        public Dictionary<int, string> MedicinePrescriptions { get; set; }
    }
}
