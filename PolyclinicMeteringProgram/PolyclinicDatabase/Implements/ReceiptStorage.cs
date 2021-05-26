using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using PolyclinicDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolyclinicDatabase.Implements
{
    public class ReceiptStorage : IReceipt
    {
        public List<ReceiptViewModel> GetFullList()
        {
            using (var context = new PolyclinicDatabase())
            {
                return context.Receipts
                    .Include(rec => rec.ReceiptMedicines)
                    .ThenInclude(rec => rec.Medicine)
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public List<ReceiptViewModel> GetFilteredList(ReceiptBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new PolyclinicDatabase())
            {
                return context.Receipts
                    .Include(rec => rec.ReceiptMedicines)
                    .ThenInclude(rec => rec.Medicine)
                    .Where(rec => rec.Date >= model.DateFrom && rec.Date <= model.DateTo)
                    .Select(CreateModel).ToList();
            }
        }

        public ReceiptViewModel GetElement(ReceiptBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new PolyclinicDatabase())
            {
                var receipt = context.Receipts
                    .Include(rec => rec.ReceiptMedicines)
                    .ThenInclude(rec => rec.Medicine)
                    .FirstOrDefault(rec => rec.Id == model.Id);
                return receipt != null ?
                CreateModel(receipt) : null;
            }
        }

        public void Insert(ReceiptBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Receipt(), context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update(ReceiptBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var receipt = context.Receipts.FirstOrDefault(rec => rec.Id == model.Id);

                        if (receipt == null)
                        {
                            throw new Exception("Поставка не найдена");
                        }

                        CreateModel(model, receipt, context);
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(ReceiptBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                Receipt element = context.Receipts.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Receipts.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Поступление не найдено");
                }
            }
        }

        private Receipt CreateModel(ReceiptBindingModel model, Receipt receipt, PolyclinicDatabase context)
        {
            receipt.Date = model.Date;
            receipt.DeliverymanName = model.DeliverymanName;

            if (receipt.Id == 0)
            {
                context.Receipts.Add(receipt);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var receiptMedicines = context.ReceiptMedicines
                     .Where(rec => rec.ReceiptId == model.Id.Value)
                     .ToList();

                context.ReceiptMedicines.RemoveRange(receiptMedicines.ToList());

                context.SaveChanges();
            }

            foreach (var receiptMedicine in model.ReceiptMedecines)
            {
                context.ReceiptMedicines.Add(new ReceiptMedicine
                {
                    ReceiptId = receipt.Id,
                    MedicineId = receiptMedicine.Key,
                    Count = receiptMedicine.Value.Item2
                });
                context.SaveChanges();
            }
            return receipt;
        }

        private ReceiptViewModel CreateModel(Receipt receipt)
        {
            return new ReceiptViewModel
            {
                Id = receipt.Id,
                Date = receipt.Date,
                DeliverymanName = receipt.DeliverymanName,
                ReceiptMedicines =  receipt.ReceiptMedicines
                            .ToDictionary(recReceiptMedicines => recReceiptMedicines.MedicineId,
                            recReceiptMedicines => (recReceiptMedicines.Medicine?.Name, recReceiptMedicines.Count)),
            };
        }
    }
}
