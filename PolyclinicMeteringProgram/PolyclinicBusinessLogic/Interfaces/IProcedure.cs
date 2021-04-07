using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace PolyclinicBusinessLogic.Interfaces
{
    public interface IProcedure
    {
        List<ProcedureViewModel> GetFullList();
        List<ProcedureViewModel> GetFilteredList(ProcedureBindingModel model);
        ProcedureViewModel GetElement(ProcedureBindingModel model);
        void Insert(ProcedureBindingModel model);
        void Update(ProcedureBindingModel model);
        void Delete(ProcedureBindingModel model);
    }
}
