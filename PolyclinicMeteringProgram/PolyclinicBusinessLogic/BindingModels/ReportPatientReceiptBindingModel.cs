﻿using System;

namespace PolyclinicBusinessLogic.BindingModels
{
    public class ReportPatientReceiptBindingModel
    {
        public string FileName { get; set; }
        public int DoctorId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
