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
        public int NumberOfPackages { get; set; }
        public Dictionary<int, string> ReceiptMedecines { get; set; }
    }
}
