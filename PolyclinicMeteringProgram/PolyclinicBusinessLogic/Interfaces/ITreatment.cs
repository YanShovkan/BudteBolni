using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace PolyclinicBusinessLogic.Interfaces
{
    public interface ITreatment
    {
        List<TreatmentViewModel> GetFullList();
        List<TreatmentViewModel> GetFilteredList(TreatmentBindingModel model);
        TreatmentViewModel GetElement(TreatmentBindingModel model);
        void Insert(TreatmentBindingModel model);
        void Update(TreatmentBindingModel model);
        void Delete(TreatmentBindingModel model);
    }
}
