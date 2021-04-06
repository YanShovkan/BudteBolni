using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace PolyclinicBusinessLogic.Interfaces
{
    public interface IPatient
    {
        List<PatientViewModel> GetFullList();
        List<PatientViewModel> GetFilteredList(PatientBindingModel model);
        PatientViewModel GetElement(PatientBindingModel model);
        void Insert(PatientBindingModel model);
        void Update(PatientBindingModel model);
        void Delete(PatientBindingModel model);
    }
}
