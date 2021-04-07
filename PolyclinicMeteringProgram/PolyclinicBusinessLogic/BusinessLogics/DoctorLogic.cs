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
            var doctor = _doctorStorage.GetElement(new DoctorBindingModel
            {
                FullName = model.FullName
            });
            if (doctor != null && doctor.Id != model.Id)
            {
                throw new Exception("Уже есть такой пользователь");
            }
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
            var doctor = _doctorStorage.GetElement(new DoctorBindingModel
            {
                Id = model.Id
            });
            if (doctor == null)
            {
                throw new Exception("Доктор не найден");
            }
            _doctorStorage.Delete(model);
        }

        public int CheckPassword(string userName, string password)
        {
            var doctor = _doctorStorage.GetElement(new DoctorBindingModel
            {
                FullName = userName
            }); 
            if (doctor == null)
            {
                throw new Exception("Нет такого пользователя");
            }
            if (doctor.Password != password)
            {
                throw new Exception("Неверный пароль");
            }
            return doctor.Id;
        }
    }
}
