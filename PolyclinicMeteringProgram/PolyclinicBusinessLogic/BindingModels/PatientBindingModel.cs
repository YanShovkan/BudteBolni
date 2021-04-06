using System;

namespace PolyclinicBusinessLogic.BindingModels
{
    public class PatientBindingModel
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public int? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DoctorId { get; set; }
    }
}
