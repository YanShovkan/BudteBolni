using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace PolyclinicBusinessLogic.BusinessLogics
{
    public class MedicineLogic
    {
        private readonly IMedicine _medicineStorage;
        public MedicineLogic(IMedicine medicineStorage)
        {
            _medicineStorage = medicineStorage;
        }

        public List<MedicineViewModel> Read(MedicineBindingModel model)
        {
            if (model == null)
            {
                return _medicineStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<MedicineViewModel> { _medicineStorage.GetElement(model) };
            }
            return _medicineStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(MedicineBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _medicineStorage.Update(model);
            }
            else
            {
                _medicineStorage.Insert(model);
            }
        }
        public void Delete(MedicineBindingModel model)
        {
            var medicine = _medicineStorage.GetElement(new MedicineBindingModel
            {
                Id = model.Id
            });
            if (medicine == null)
            {
                throw new Exception("Лекарство не найдено");
            }
            _medicineStorage.Delete(model);
        }
    }
}
