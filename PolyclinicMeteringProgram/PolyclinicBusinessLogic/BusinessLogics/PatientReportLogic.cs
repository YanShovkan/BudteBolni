using PolyclinicBusinessLogic.HelperModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.BusinessLogics
{
    public class PatientReportLogic
    {
        // Medicine => Procedure => Patient
        private readonly IProcedure _procedureStorage;
        private readonly IMedicine _medicineStorage;
        private readonly IPatient _patientStorage;
        public PatientReportLogic(IProcedure procedureStorage, IMedicine
      medicineStorage, IPatient patientStorage)
        {
            _procedureStorage = procedureStorage;
            _medicineStorage = medicineStorage;
            _patientStorage = patientStorage;
        }

        public List<ReportPatientViewModel> GetMedicinePatients(List<MedicineViewModel> medicines)
        {
            var procedures = _procedureStorage.GetFullList();
            var patients = _patientStorage.GetFullList();

            var list = new List<ReportPatientViewModel>();

            foreach (var medicine in medicines)
            {
                foreach (var procedure in procedures)
                {
                    if (procedure.ProcedureMedicines.ContainsKey(medicine.Id))
                    {
                        foreach (var patient in patients)
                        {
                            if (patient.PatientProcedures.ContainsKey(procedure.Id))
                            { 
                                list.Add(new ReportPatientViewModel
                                {
                                    MedicineName = medicine.Name,
                                    PatientName = patient.FullName,
                                    PhoneNumber = patient.PhoneNumber,
                                    DateOfBirth = patient.DateOfBirth
                                });
                            }
                        }
                    }
                }
            }
            return list;
        }

        public void SaveToWordFile(string fileName, List<MedicineViewModel> medicines)
        {
            SaveToWord.CreateDoc(new ExcelWordInfoForPharmacist
            {
                FileName = fileName,
                Title = "Список пациентов по лекарствам",
                Patients = GetMedicinePatients(medicines)
            });
        }

        public void SaveToExcelFile(string fileName, List<MedicineViewModel> medicines)
        {
            SaveToExcel.CreateDoc(new ExcelWordInfoForPharmacist
            {
                FileName = fileName,
                Title = "Список пациентов по лекарствам",
                Patients = GetMedicinePatients(medicines)
            });
        }
    }
}
