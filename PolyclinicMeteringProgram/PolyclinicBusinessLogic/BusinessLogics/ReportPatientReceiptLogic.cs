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

                foreach (var medicine in medicines)
                {
                    if (receipt.ReceiptMedicines.ContainsKey(medicine.Id))
                    {
                        foreach (var procedure in procedures)
                        {
                            if (procedure.ProcedureMedicines.ContainsKey(medicine.Id))
                            {
                                foreach (var patient in patients)
                                {
                                    if (procedure.ProcedureMedicines.ContainsKey(medicine.Id))
                                    {
                                        list.Add(new ReportPatientReceiptViewModel
                                        {
                                            DeliverymanName = receipt.DeliverymanName,
                                            Date = receipt.Date,
                                            PatientName = patient.FullName,
                                            MedecineName = medicine.Name,
                                            ProcedureName = procedure.Name
                                        });
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
                Title = "Отчет по пациентам и поступлениям",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Receipts = GetPatientReceipt(model)
            });
        }
    }
}
