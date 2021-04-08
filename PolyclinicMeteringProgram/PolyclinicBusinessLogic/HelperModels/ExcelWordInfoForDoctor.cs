using PolyclinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace PolyclinicBusinessLogic.HelperModels
{
    public class ExcelWordInfoForDoctor
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportReceiptViewModel> Receipts { get; set; }
    }
}
