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
    public class PrescriptionStorage : IPrescription
    {
        public List<PrescriptionViewModel> GetFullList()
        {
            using (var context = new PolyclinicDatabase())
            {
                return context.Prescriptions
                    .Include(rec => rec.PrescriptionMedicines)
                    .Include(rec => rec.PrescriptionTreatments)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public List<PrescriptionViewModel> GetFilteredList(PrescriptionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PolyclinicDatabase())
            {
                return context.Prescriptions
                     .Include(rec => rec.PrescriptionMedicines)
                    .Include(rec => rec.PrescriptionTreatments)
                    .Where(rec => rec.Id == model.Id)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public PrescriptionViewModel GetElement(PrescriptionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PolyclinicDatabase())
            {
                var prescription = context.Prescriptions
                    .Include(rec => rec.PrescriptionMedicines)
                    .ThenInclude(rec => rec.Medicine)
                    .Include(rec => rec.PrescriptionTreatments)
                    .ThenInclude(rec => rec.Treatment)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return prescription != null ?
                    CreateModel(prescription) :
                    null;
            }
        }
        public void Insert(PrescriptionBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Prescription(), context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(PrescriptionBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var prescription = context.Prescriptions.FirstOrDefault(rec => rec.Id == model.Id);

                        if (prescription == null)
                        {
                            throw new Exception("Рецепт не найден");
                        }

                        CreateModel(model, prescription, context);
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(PrescriptionBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                var prescription = context.Prescriptions.FirstOrDefault(rec => rec.Id == model.Id);

                if (prescription == null)
                {
                    throw new Exception("Рецепт не найден");
                }

                context.Prescriptions.Remove(prescription);
                context.SaveChanges();
            }
        }
        private PrescriptionViewModel CreateModel(Prescription prescription)
        {
            return new PrescriptionViewModel
            {
                Id = prescription.Id,
                Price = prescription.Price,
                PharmacyAddress = prescription.PharmacyAddress,

                PrescriptionTreatment = prescription.PrescriptionTreatments
                            .ToDictionary(recPrescriptionTreatments => recPrescriptionTreatments.TreatmentId,
                            recPrescriptionTreatments => (recPrescriptionTreatments.Treatment?.Name, recPrescriptionTreatments.Count)),
                PrescriptionMedicines = prescription.PrescriptionMedicines
                            .ToDictionary(recPrescriptionMedicines => recPrescriptionMedicines.MedicineId,
                            recPrescriptionMedicines => (recPrescriptionMedicines.Medicine?.Name, recPrescriptionMedicines.Count))

            };

        }

        private Prescription CreateModel(PrescriptionBindingModel model, Prescription prescription, PolyclinicDatabase context)
        {
            prescription.Price = model.Price;
            prescription.PharmacyAddress = model.PharmacyAddress;

            if (prescription.Id == 0)
            {
                context.Prescriptions.Add(prescription);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var prescriptionTreatments = context.PrescriptionTreatments
                    .Where(rec => rec.PrescriptionId == model.Id.Value)
                    .ToList();

                context.PrescriptionTreatments.RemoveRange(prescriptionTreatments.ToList());

                var prescriptionMedicines = context.PrescriptionMedicines
                    .Where(rec => rec.PrescriptionId == model.Id.Value)
                    .ToList();

                context.PrescriptionMedicines.RemoveRange(prescriptionMedicines.ToList());

                context.SaveChanges();
            }

            foreach (var prescriptionTreatment in model.PrescriptionTreatment)
            {
                context.PrescriptionTreatments.Add(new PrescriptionTreatment
                {
                    PrescriptionId = prescription.Id,
                    TreatmentId = prescriptionTreatment.Key,
                    Count = prescriptionTreatment.Value.Item2
                });
                context.SaveChanges();
            }

            foreach (var prescriptionMedicines in model.PrescriptionMedicines)
            {
                context.PrescriptionMedicines.Add(new PrescriptionMedicine
                {
                    PrescriptionId = prescription.Id,
                    MedicineId = prescriptionMedicines.Key,
                    Count = prescriptionMedicines.Value.Item2
                });
                context.SaveChanges();
            }
            return prescription;
        }
    }
}
