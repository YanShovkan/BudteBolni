using PolyclinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace PolyclinicBusinessLogic.HelperModels
{
    public class InfoForDoctor
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportReceiptViewModel> Receipts { get; set; }
    }
}
