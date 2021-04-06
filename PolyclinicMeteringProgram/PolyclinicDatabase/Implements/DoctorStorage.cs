using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicBusinessLogic.ViewModels;
using PolyclinicDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyclinicDatabase.Implements
{
    public class DoctorStorage : IDoctor
    {
        public List<DoctorViewModel> GetFullList()
        {
            using (var context = new PolyclinicDatabase())
            {
                return context.Doctors
                .Select(CreateModel).ToList();
            }
        }

        public List<DoctorViewModel> GetFilteredList(DoctorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new PolyclinicDatabase())
            {
                return context.Doctors
                    .Where(rec => rec.FullName.Contains(model.FullName) || (rec.Position.Equals(model.Position)))
                    .Select(CreateModel).ToList();
            }
        }

        public DoctorViewModel GetElement(DoctorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new PolyclinicDatabase())
            {
                var doctor = context.Doctors
                .FirstOrDefault(rec => rec.Id == model.Id);
                return doctor != null ?
                CreateModel(doctor) : null;
            }
        }

        public void Insert(DoctorBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                context.Doctors.Add(CreateModel(model, new Doctor()));
                context.SaveChanges();
            }
        }

        public void Update(DoctorBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                var element = context.Doctors.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Доктор не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(DoctorBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                Doctor element = context.Doctors.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Doctors.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Доктор не найден");
                }
            }
        }

        private Doctor CreateModel(DoctorBindingModel model, Doctor doctor)
        {
            doctor.FullName = model.FullName;
            doctor.Position = model.Position;
            doctor.Password = model.Password;
            return doctor;
        }

        private DoctorViewModel CreateModel(Doctor doctor)
        {
            return new DoctorViewModel
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Position = doctor.Position,
                Password = doctor.Password
            };
        }
    }
}