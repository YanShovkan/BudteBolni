using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using PolyclinicDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolyclinicDatabase.Implements
{
    public class MedicineStorage : IMedicine
    {
        public List<MedicineViewModel> GetFullList()
        {
            using (var context = new PolyclinicDatabase())
            {
                return context.Medicines
                    .Include(rec => rec.Pharmacist)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public List<MedicineViewModel> GetFilteredList(MedicineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PolyclinicDatabase())
            {
                return context.Medicines
                    .Include(rec => rec.Pharmacist)
                    .Where(rec => rec.PharmacistId == model.PharmacistId)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public MedicineViewModel GetElement(MedicineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PolyclinicDatabase())
            {
                var medicine = context.Medicines
                    .Include(rec => rec.Pharmacist)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return medicine != null ?
                    CreateModel(medicine) :
                    null;
            }
        }
        public void Insert(MedicineBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                context.Medicines.Add(CreateModel(model, new Medicine()));
                context.SaveChanges();
            }
        }
        public void Update(MedicineBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                var medicine = context.Medicines.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (medicine == null)
                {
                    throw new Exception("Лекарство не найдено");
                }
                CreateModel(model, medicine);
                context.SaveChanges();
            }
        }
        public void Delete(MedicineBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                var medicine = context.Medicines.FirstOrDefault(rec => rec.Id == model.Id);

                if (medicine == null)
                {
                    throw new Exception("Лекарство не найдено");
                }

                context.Medicines.Remove(medicine);
                context.SaveChanges();
            }
        }
        private MedicineViewModel CreateModel(Medicine medicine)
        {
            return new MedicineViewModel
            {
                Id = medicine.Id,
                Name = medicine.Name,
                ActiveSubstance = medicine.ActiveSubstance,
                Classification = medicine.Classification,
                PharmacistId = medicine.PharmacistId,
                PharmacistName = medicine.Pharmacist?.FullName
            };

        }

        private Medicine CreateModel(MedicineBindingModel model, Medicine medicine)
        {
            medicine.Name = model.Name;
            medicine.ActiveSubstance = model.ActiveSubstance;
            medicine.Classification = model.Classification;
            medicine.PharmacistId = model.PharmacistId;
            return medicine;
        }
    }
}
