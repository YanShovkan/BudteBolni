using PolyclinicBusinessLogic.BindingModels;
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
    public class ReportProcedureReceiptLogic
    {
        //Receipt => Medecine => Procedure
        private readonly IReceipt _receiptStorage;
        private readonly IMedicine _medecineStorage;
        private readonly IProcedure _procedureStorage;

        public ReportProcedureReceiptLogic(IReceipt receiptStorage, IMedicine medecineStorage,
     IProcedure procedureStorage)
        {
            _receiptStorage = receiptStorage;
            _medecineStorage = medecineStorage;
            _procedureStorage = procedureStorage;
        }

        public List<ReportProcedureReceiptViewModel> GetProcedureReceipt(ReportProcedureReceiptBindingModel model)
        {
            var receipts = _receiptStorage.GetFilteredList(new ReceiptBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var medicines = _medecineStorage.GetFilteredList(new MedicineBindingModel
            {
                PharmacistId = model.PharmacistId
            }); 
            var procedures = _procedureStorage.GetFullList();

            var list = new List<ReportProcedureReceiptViewModel>();

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
                                list.Add(new ReportProcedureReceiptViewModel
                                {
                                    DeliverymanName = receipt.DeliverymanName,
                                    Date = receipt.Date,
                                    MedecineName = medicine.Name,
                                    ProcedureName = procedure.Name
                                });
                            }
                        }
                    }
                }
            }
            return list;
        }

        public void SaveToPdfFile(ReportProcedureReceiptBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfoForPharmacist
            {
                FileName = model.FileName,
                Title = "Список пациентов и поступлений",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Receipts = GetProcedureReceipt(model)
            });
        }
    }
}
