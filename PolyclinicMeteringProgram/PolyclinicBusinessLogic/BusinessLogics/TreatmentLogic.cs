using System;
using System.Collections.Generic;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;

namespace PolyclinicBusinessLogic.BusinessLogics
{
    public class TreatmentLogic
    {
        private readonly ITreatment _treatmentStorage;
        public TreatmentLogic(ITreatment treatmentStorage)
        {
            _treatmentStorage = treatmentStorage;
        }

        public List<TreatmentViewModel> Read(TreatmentBindingModel model)
        {
            if (model == null)
            {
                return _treatmentStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TreatmentViewModel> { _treatmentStorage.GetElement(model) };
            }
            return _treatmentStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(TreatmentBindingModel model)
        {
            var treatment = _treatmentStorage.GetElement(new TreatmentBindingModel
            {
                Name = model.Name
            });
            if (treatment != null && treatment.Id != model.Id)
            {
                throw new Exception("Уже есть такое лечение");
            }
            if (model.Id.HasValue)
            {
                _treatmentStorage.Update(model);
            }
            else
            {
                _treatmentStorage.Insert(model);
            }
        }

        public void Delete(TreatmentBindingModel model)

        {
            var treatment = _treatmentStorage.GetElement(new TreatmentBindingModel
            {
                Id = model.Id
            });
            if (treatment == null)
            {
                throw new Exception("Лечение не найдено");
            }
            _treatmentStorage.Delete(model);
        }
    }
}
