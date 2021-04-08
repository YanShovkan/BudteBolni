using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.HelperModels
{
    public class ExcelWordInfoForPharmacist
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportPatientViewModel> Patients { get; set; }
    }
}
