using System;
using System.Collections.Generic;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;

namespace PolyclinicBusinessLogic.BusinessLogics
{
    public class PharmacistLogic
    {
        private readonly IPharmacist _pharmacistStorage;
        public PharmacistLogic(IPharmacist pharmacistStorage)
        {
            _pharmacistStorage = pharmacistStorage;
        }

        public List<PharmacistViewModel> Read(PharmacistBindingModel model)
        {
            if (model == null)
            {
                return _pharmacistStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PharmacistViewModel> { _pharmacistStorage.GetElement(model) };
            }
            return _pharmacistStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(PharmacistBindingModel model)
        {
            var pharmacist = _pharmacistStorage.GetElement(new PharmacistBindingModel
            {
                FullName = model.FullName
            });
            if (pharmacist != null && pharmacist.Id != pharmacist.Id)
            {
                throw new Exception("Уже есть такой пользователь");
            }
            if (model.Id.HasValue)
            {
                _pharmacistStorage.Update(model);
            }
            else
            {
                _pharmacistStorage.Insert(model);
            }
        }

        public void Delete(PharmacistBindingModel model)

        {
            var pharmacist = _pharmacistStorage.GetElement(new PharmacistBindingModel
            {
                Id = model.Id
            });
            if (pharmacist == null)
            {
                throw new Exception("Аптекарь не найден");
            }
            _pharmacistStorage.Delete(model);
        }

        public int CheckPassword(string userName, string password)
        {
            var pharmacist = _pharmacistStorage.GetElement(new PharmacistBindingModel
            {
                FullName = userName
            });
            if (pharmacist == null)
            {
                throw new Exception("Нет такого пользователя");
            }
            if (pharmacist.Password != password)
            {
                throw new Exception("Неверный пароль");
            }
            return pharmacist.Id;
        }
    }
}