using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace PolyclinicBusinessLogic.HelperModels
{
    class PdfInfoForDoctor
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportPatientReceiptViewModel> Receipts { get; set; }
    }
}
