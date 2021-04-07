using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using System.Collections.Generic;


namespace PolyclinicBusinessLogic.Interfaces
{
    public interface IPharmacist
    {
        List<PharmacistViewModel> GetFullList();
        List<PharmacistViewModel> GetFilteredList(PharmacistBindingModel model);
        PharmacistViewModel GetElement(PharmacistBindingModel model);
        void Insert(PharmacistBindingModel model);
        void Update(PharmacistBindingModel model);
        void Delete(PharmacistBindingModel model);
    }
}
