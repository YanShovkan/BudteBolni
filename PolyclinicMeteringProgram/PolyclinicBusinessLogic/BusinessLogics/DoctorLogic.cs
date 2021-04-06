using System;
using System.Collections.Generic;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;

namespace PolyclinicBusinessLogic.BusinessLogics
{
    public class DoctorLogic
    {
        private readonly IDoctor _doctorStorage;
        public DoctorLogic(IDoctor doctorStorage)
        {
            _doctorStorage = doctorStorage;
        }

        public List<DoctorViewModel> Read(DoctorBindingModel model)
        {
            if (model == null)
            {
                return _doctorStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<DoctorViewModel> { _doctorStorage.GetElement(model) };
            }
            return _doctorStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(DoctorBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _doctorStorage.Update(model);
            }
            else
            {
                _doctorStorage.Insert(model);
            }
        }
        public void Delete(DoctorBindingModel model)

        {
            var element = _doctorStorage.GetElement(new DoctorBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Доктор не найден");
            }
            _doctorStorage.Delete(model);
        }
    }
}
