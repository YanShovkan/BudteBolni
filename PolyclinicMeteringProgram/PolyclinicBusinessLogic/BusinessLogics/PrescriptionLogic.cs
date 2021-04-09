using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyclinicBusinessLogic.BusinessLogics
{
    public class PrescriptionLogic
    {
        private readonly IPrescription _prescriptionStorage;
        public PrescriptionLogic(IPrescription prescriptionStorage)
        {
            _prescriptionStorage = prescriptionStorage;
        }

        public List<PrescriptionViewModel> Read(PrescriptionBindingModel model)
        {
            if (model == null)
            {
                return _prescriptionStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PrescriptionViewModel> { _prescriptionStorage.GetElement(model) };
            }
            return _prescriptionStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(PrescriptionBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _prescriptionStorage.Update(model);
            }
            else
            {
                _prescriptionStorage.Insert(model);
            }
        }
        public void Delete(PrescriptionBindingModel model)
        {
            var receipt = _prescriptionStorage.GetElement(new PrescriptionBindingModel
            {
                Id = model.Id
            });
            if (receipt == null)
            {
                throw new Exception("Рецепт не найден");
            }
            _prescriptionStorage.Delete(model);
        }
    }
}
