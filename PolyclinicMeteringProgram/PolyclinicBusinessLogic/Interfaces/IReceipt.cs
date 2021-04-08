using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.Interfaces
{
    public interface IReceipt
    {
        List<ReceiptViewModel> GetFullList();
        List<ReceiptViewModel> GetFilteredList(ReceiptBindingModel model);
        ReceiptViewModel GetElement(ReceiptBindingModel model);
        void Insert(ReceiptBindingModel model);
        void Update(ReceiptBindingModel model);
        void Delete(ReceiptBindingModel model);
    }
}
