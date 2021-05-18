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
                .FirstOrDefault(rec => rec.Id == model.Id);
                return receipt != null ?
                CreateModel(receipt) : null;
            }
        }

        public void Insert(ReceiptBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                context.Receipts.Add(CreateModel(model, new Receipt()));
                context.SaveChanges();
            }
        }

        public void Update(ReceiptBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                var element = context.Receipts.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Поступление не найдено");
                }
                CreateModel(model, element);
                context.SaveChanges();
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

        private Receipt CreateModel(ReceiptBindingModel model, Receipt receipt)
        {
            receipt.Date = model.Date;
            receipt.DeliverymanName = model.DeliverymanName;
            return receipt;
        }

        private ReceiptViewModel CreateModel(Receipt receipt)
        {
            return new ReceiptViewModel
            {
                Id = receipt.Id,
                Date = receipt.Date,
                DeliverymanName = receipt.DeliverymanName
            };
        }


    }
}
