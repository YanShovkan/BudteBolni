using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using PolyclinicDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyclinicDatabase.Implements
{
    public class PharmacistStorage : IPharmacist
    {
        public List<PharmacistViewModel> GetFullList()
        {
            using (var context = new PolyclinicDatabase())
            {
                return context.Pharmacists
                .Select(CreateModel).ToList();
            }
        }

        public List<PharmacistViewModel> GetFilteredList(PharmacistBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new PolyclinicDatabase())
            {
                return context.Pharmacists
                    .Where(rec => rec.FullName.Contains(model.FullName) || (rec.PhoneNumber.Equals(model.PhoneNumber)))
                    .Select(CreateModel).ToList();
            }
        }

        public PharmacistViewModel GetElement(PharmacistBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new PolyclinicDatabase())
            {
                var pharmacist = context.Pharmacists
                .FirstOrDefault(rec => rec.Id == model.Id || rec.FullName == model.FullName);
                return pharmacist != null ?
                CreateModel(pharmacist) : null;
            }
        }

        public void Insert(PharmacistBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                context.Pharmacists.Add(CreateModel(model, new Pharmacist()));
                context.SaveChanges();
            }
        }

        public void Update(PharmacistBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                var element = context.Pharmacists.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Аптекарь не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(PharmacistBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                Pharmacist element = context.Pharmacists.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Pharmacists.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Аптекарь не найден");
                }
            }
        }

        private Pharmacist CreateModel(PharmacistBindingModel model, Pharmacist pharmacist)
        {
            pharmacist.FullName = model.FullName;
            pharmacist.PhoneNumber = model.PhoneNumber;
            pharmacist.Password = model.Password;
            return pharmacist;
        }

        private PharmacistViewModel CreateModel(Pharmacist pharmacist)
        {
            return new PharmacistViewModel
            {
                Id = pharmacist.Id,
                FullName = pharmacist.FullName,
                PhoneNumber = pharmacist.PhoneNumber,
                Password = pharmacist.Password
            };
        }

    }
}