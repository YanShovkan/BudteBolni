using System;
using System.Collections.Generic;

namespace PolyclinicBusinessLogic.BindingModels
{
    public class PatientBindingModel
    {
        public int? Id { get; set; }
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Dictionary<int, string> PatientProcedures { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
