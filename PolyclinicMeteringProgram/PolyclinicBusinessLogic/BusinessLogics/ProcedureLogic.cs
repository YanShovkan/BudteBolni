using System;
using System.Collections.Generic;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;


namespace PolyclinicBusinessLogic.BusinessLogics
{
    public class ProcedureLogic
    {
        private readonly IProcedure _procedureStorage;
        public ProcedureLogic(IProcedure procedureStorage)
        {
            _procedureStorage = procedureStorage;
        }

        public List<ProcedureViewModel> Read(ProcedureBindingModel model)
        {
            if (model == null)
            {
                return _procedureStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ProcedureViewModel> { _procedureStorage.GetElement(model) };
            }
            return _procedureStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ProcedureBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _procedureStorage.Update(model);
            }
            else
            {
                _procedureStorage.Insert(model);
            }
        }
        public void Delete(ProcedureBindingModel model)
        {
            var patient = _procedureStorage.GetElement(new ProcedureBindingModel
            {
                Id = model.Id
            });
            if (patient == null)
            {
                throw new Exception("Пациент не найден");
            }
            _procedureStorage.Delete(model);
        }
    }
}
