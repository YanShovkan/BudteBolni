using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.Interfaces
{
    public interface IPrescription
    {
        List<PrescriptionViewModel> GetFullList();
        List<PrescriptionViewModel> GetFilteredList(PrescriptionBindingModel model);
        PrescriptionViewModel GetElement(PrescriptionBindingModel model);
        void Insert(PrescriptionBindingModel model);
        void Update(PrescriptionBindingModel model);
        void Delete(PrescriptionBindingModel model);
    }
}
