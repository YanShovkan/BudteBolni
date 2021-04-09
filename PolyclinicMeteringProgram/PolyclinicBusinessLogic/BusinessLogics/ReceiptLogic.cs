using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.BusinessLogics
{
    public class ReceiptLogic
    {
        private readonly IReceipt _receiptStorage;
        public ReceiptLogic(IReceipt receiptStorage)
        {
            _receiptStorage = receiptStorage;
        }

        public List<ReceiptViewModel> Read(ReceiptBindingModel model)
        {
            if (model == null)
            {
                return _receiptStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ReceiptViewModel> { _receiptStorage.GetElement(model) };
            }
            return _receiptStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ReceiptBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _receiptStorage.Update(model);
            }
            else
            {
                _receiptStorage.Insert(model);
            }
        }
        public void Delete(ReceiptBindingModel model)
        {
            var receipt = _receiptStorage.GetElement(new ReceiptBindingModel
            {
                Id = model.Id
            });
            if (receipt == null)
            {
                throw new Exception("Поступление не найдено");
            }
            _receiptStorage.Delete(model);
        }
    }
}
