using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.Interfaces
{
    public interface IMedicine
    {
        List<MedecineViewModel> GetFullList();
        List<MedecineViewModel> GetFilteredList(MedicineBindingModel model);
        MedecineViewModel GetElement(MedicineBindingModel model);
        void Insert(MedicineBindingModel model);
        void Update(MedicineBindingModel model);
        void Delete(MedicineBindingModel model);
    }
}
