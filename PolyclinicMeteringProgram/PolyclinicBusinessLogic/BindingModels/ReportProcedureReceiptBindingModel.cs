using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.BindingModels
{
    public class ReportProcedureReceiptBindingModel
    {
        public string FileName { get; set; }
        public int PharmacistId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
