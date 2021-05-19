using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.HelperModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Linq;

//ВЛАД НЕ ТРОЖЬ!!!!!!!
namespace PolyclinicBusinessLogic.BusinessLogics
{
    //
    public class ReportPatientReceiptLogic
    {
        //Receipt => Medecine => Procedure => Patient
        private readonly IReceipt _receiptStorage;
        private readonly IMedicine _medecineStorage;
        private readonly IProcedure _procedureStorage;
        private readonly IPatient _pacientStorage;

        public ReportPatientReceiptLogic(IReceipt receiptStorage, IMedicine medecineStorage,
     IProcedure procedureStorage, IPatient pacientStorage)
        {
            _receiptStorage = receiptStorage;
            _medecineStorage = medecineStorage;
            _procedureStorage = procedureStorage;
            _pacientStorage = pacientStorage;
        }

        public List<ReportPatientReceiptViewModel> GetPatientReceipt(ReportPatientReceiptBindingModel model)
        {
            var receipts = _receiptStorage.GetFilteredList(new ReceiptBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var medicines = _medecineStorage.GetFullList();
            var procedures = _procedureStorage.GetFullList();
            var patients = _pacientStorage.GetFilteredList(new PatientBindingModel
            {
                DoctorId = model.DoctorId
            });

            var list = new List<ReportPatientReceiptViewModel>();

            foreach (var receipt in receipts)
            {
                var record = new ReportPatientReceiptViewModel
                {
                    DeliverymanName = receipt.DeliverymanName,
                    Date = receipt.Date
                };
                foreach (var medicine in medicines)
                {
                    if (receipt.ReceiptMedicines.ContainsKey(medicine.Id))
                    {
                        record.MedecineName = medicine.Name;
                        foreach (var procedure in procedures)
                        {
                            if (procedure.ProcedureMedicines.ContainsKey(medicine.Id))
                            {
                                record.ProcedureName = procedure.Name;
                                foreach (var patient in patients)
                                {
                                    if (procedure.ProcedureMedicines.ContainsKey(medicine.Id))
                                    {
                                        record.PatientName = patient.FullName;
                                        list.Add(record);
                                    }
                                }   
                            }
                        }
                    }
                }
            }
            return list;
        }

        public void SaveToPdfFile(ReportPatientReceiptBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfoForDoctor
            {
                FileName = model.FileName,
                Title = "Список пациентов и поступлений",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Receipts = GetPatientReceipt(model)
            });
        }
    }
}
