using System;
using System.Collections.Generic;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;

namespace PolyclinicBusinessLogic.BusinessLogics
{
    public class PatientLogic
    {
        private readonly IPatient _patientStorage;
        public PatientLogic(IPatient patientStorage)
        {
            _patientStorage = patientStorage;
        }

        public List<PatientViewModel> Read(PatientBindingModel model)
        {
            if (model == null)
            {
                return _patientStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PatientViewModel> { _patientStorage.GetElement(model) };
            }
            return _patientStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(PatientBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _patientStorage.Update(model);
            }
            else
            {
                _patientStorage.Insert(model);
            }
        }
        public void Delete(PatientBindingModel model)
        {
            var patient = _patientStorage.GetElement(new PatientBindingModel
            {
                Id = model.Id
            });
            if (patient == null)
            {
                throw new Exception("Пациент не найден");
            }
            _patientStorage.Delete(model);
        }
    }
}

