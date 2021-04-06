using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PolyclinicBusinessLogic.ViewModels
{
    public class PatientViewModel
    {
      
        public int Id { get; set; }
        [DisplayName("Имя пациента")]
        public string FullName { get; set; }
        [DisplayName("Номер телефона")]
        public int? PhoneNumber { get; set; }
        [DisplayName("Дата рождения")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Имя доктора")]
        public string DoctorName { get; set; }
        public Dictionary<int, string> ProcedurePatients { get; set; }

    }
}
