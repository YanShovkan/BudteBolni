using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using PolyclinicDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyclinicDatabase.Implements
{
    public class TreatmentStorage : ITreatment
    {
        public List<TreatmentViewModel> GetFullList()
        {
            using (var context = new PolyclinicDatabase())
            {
                return context.Treatments
                .Select(rec => new TreatmentViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Urgency = rec.Urgency,
                    AreaOfAction = rec.AreaOfAction
                })
               .ToList();
            }
        }
        public List<TreatmentViewModel> GetFilteredList(TreatmentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new PolyclinicDatabase())
            {
                return context.Treatments
                .Where(rec => rec.Name.Contains(model.Name))
               .Select(rec => new TreatmentViewModel
               {
                   Id = rec.Id,
                   Name = rec.Name,
                   Urgency = rec.Urgency,
                   AreaOfAction = rec.AreaOfAction
               })
                .ToList();
            }
        }
        public TreatmentViewModel GetElement(TreatmentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new PolyclinicDatabase())
            {
                var treatment = context.Treatments
                .FirstOrDefault(rec => rec.Name == model.Name ||
               rec.Id == model.Id);
                return treatment != null ?
               new TreatmentViewModel
               {
                   Id = treatment.Id,
                   Name = treatment.Name,
                   Urgency = treatment.Urgency,
                   AreaOfAction = treatment.AreaOfAction
               } :
               null;
            }
        }
        public void Insert(TreatmentBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                context.Treatments.Add(CreateModel(model, new Treatment()));
                context.SaveChanges();
            }
        }
        public void Update(TreatmentBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                var treatment = context.Treatments.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (treatment == null)
                {
                    throw new Exception("Лечение не найдено");
                }
                CreateModel(model, treatment);
                context.SaveChanges();
            }
        }
        public void Delete(TreatmentBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                var treatment = context.Treatments.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (treatment != null)
                {
                    context.Treatments.Remove(treatment);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Лечение не найдено");
                }
            }
        }
        private Treatment CreateModel(TreatmentBindingModel model, Treatment treatment)
        {
            treatment.Name = model.Name;
            treatment.Urgency = model.Urgency;
            treatment.AreaOfAction = model.AreaOfAction;
            return treatment;
        }
    }
}
