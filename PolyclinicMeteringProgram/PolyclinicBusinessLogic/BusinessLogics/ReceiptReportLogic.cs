﻿using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.HelperModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Linq;


//ВЛАД НЕ ТРОЖЬ!!!!!!!
namespace PolyclinicBusinessLogic.BusinessLogics
{
    class ReceiptReportLogic
    {
        // Procedure => Medicine => Receipt
        private readonly IProcedure _procedureStorage;
        private readonly IMedicine _medicineStorage;
        private readonly IReceipt _receiptStorage;
        public ReceiptReportLogic(IProcedure procedureStorage, IMedicine
      medicineStorage, IReceipt receiptStorage)
        {
            _procedureStorage = procedureStorage;
            _medicineStorage = medicineStorage;
            _receiptStorage = receiptStorage;
        }
       
        public List<ReportReceiptViewModel> GetProcedureRecepts(List<ProcedureViewModel> procedures)
        {
            var medicines = _medicineStorage.GetFullList();
            var receipts = _receiptStorage.GetFullList();

            var list = new List<ReportReceiptViewModel>();

            foreach (var procedure in procedures)
            {
                var record = new ReportReceiptViewModel
                {
                    ProcedureName = procedure.Name
                };
                foreach (var medicine in medicines)
                {
                    if (procedure.ProcedureMedicines.ContainsKey(medicine.Id))
                    {
                        foreach (var receipt in receipts)
                        {
                            if (receipt.ReceptMedecines.ContainsKey(medicine.Id))
                            {
                                record.Date = receipt.Date;
                                record.DeliverymanName = receipt.DeliverymanName;
                            }
                        }
                    }
                }
                list.Add(record);
            }
            return list;
        }
       
        public void SaveMaterialsToWordFile(string fileName, List<ProcedureViewModel> procedures)
        {
            SaveToWord.CreateDoc(new InfoForDoctor
            {
                FileName = fileName,
                Title = "Список поступлений по процедурам",
                Receipts = GetProcedureRecepts(procedures)
            });
        }
       
        public void SaveGiftsMaterialsToExcelFile(string fileName, List<ProcedureViewModel> procedures)
        {
            SaveToExcel.CreateDoc(new InfoForDoctor
            {
                FileName = fileName,
                Title = "Список поступлений по процедурам",
                Receipts = GetProcedureRecepts(procedures)
            });
        }

    }
}
