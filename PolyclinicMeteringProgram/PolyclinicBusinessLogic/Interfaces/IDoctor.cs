using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace PolyclinicBusinessLogic.Interfaces
{
    public interface IDoctor
    {
        List<DoctorViewModel> GetFullList();
        List<DoctorViewModel> GetFilteredList(DoctorBindingModel model);
        DoctorViewModel GetElement(DoctorBindingModel model);
        void Insert(DoctorBindingModel model);
        void Update(DoctorBindingModel model);
        void Delete(DoctorBindingModel model);
    }
}
