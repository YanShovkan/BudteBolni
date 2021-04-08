using System;
using System.ComponentModel;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class ReportPatientReceiptViewModel
    {
        [DisplayName("Имя пациента")]
        public string PatientName { get; set; }
        [DisplayName("Название процедуры")]
        public string ProcedureName { get; set; }
        [DisplayName("Название лекарства")]
        public string MedecineName { get; set; }
        [DisplayName("Дата поступления")]
        public DateTime Date { get; set; }
        [DisplayName("Имя доставщика")]
        public string DeliverymanName { get; set; }
    }
}
