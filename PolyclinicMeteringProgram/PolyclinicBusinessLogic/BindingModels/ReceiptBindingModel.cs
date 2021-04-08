using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.BindingModels
{
    public class ReceiptBindingModel
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public string DeliverymanName { get; set; }
        public Dictionary<int, (string, int)> ReceiptMedecines { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
