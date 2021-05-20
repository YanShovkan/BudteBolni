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
    public class ProcedureStorage : IProcedure
    {
        public List<ProcedureViewModel> GetFullList()
        {
            using (var context = new PolyclinicDatabase())
            {
                return context.Procedures
                    .Include(rec => rec.ProcedureMedicines)
                    .ThenInclude(rec => rec.Medicine)
                    .Include(rec => rec.ProcedureTreatments)
                    .ThenInclude(rec => rec.Treatment)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public List<ProcedureViewModel> GetFilteredList(ProcedureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PolyclinicDatabase())
            {
                return context.Procedures
                    .Include(rec => rec.ProcedureMedicines)
                    .ThenInclude(rec => rec.Medicine)
                    .Include(rec => rec.ProcedureTreatments)
                    .ThenInclude(rec => rec.Treatment)
                    .Where(rec => rec.Name == model.Name)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public ProcedureViewModel GetElement(ProcedureBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PolyclinicDatabase())
            {
                var procedure = context.Procedures
                    .Include(rec => rec.ProcedureMedicines)
                    .ThenInclude(rec => rec.Medicine)
                    .Include(rec => rec.ProcedureTreatments)
                    .ThenInclude(rec => rec.Treatment)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return procedure != null ?
                    CreateModel(procedure) :
                    null;
            }
        }
        public void Insert(ProcedureBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Procedure(), context);
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
        public void Update(ProcedureBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var procedure = context.Procedures.FirstOrDefault(rec => rec.Id == model.Id);

                        if (procedure == null)
                        {
                            throw new Exception("Процедура не найдена");
                        }

                        CreateModel(model, procedure, context);
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
        public void Delete(ProcedureBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                var patient = context.Patients.FirstOrDefault(rec => rec.Id == model.Id);

                if (patient == null)
                {
                    throw new Exception("Пациент не найден");
                }

                context.Patients.Remove(patient);
                context.SaveChanges();
            }
        }
        private ProcedureViewModel CreateModel(Procedure procedure)
        {
            return new ProcedureViewModel
            {
                Id = procedure.Id,
                Name = procedure.Name,
                Cost = procedure.Cost,

                ProcedureTreatments = procedure.ProcedureTreatments
                            .ToDictionary(recProcedureTreatments => recProcedureTreatments.TreatmentId,
                            recProcedureTreatments => (recProcedureTreatments.Treatment?.Name, recProcedureTreatments.Count)),
                ProcedureMedicines = procedure.ProcedureMedicines
                            .ToDictionary(recMedicineProcedures => recMedicineProcedures.MedicineId,
                            recMedicineProcedures => (recMedicineProcedures.Medicine?.Name, recMedicineProcedures.Count))
            };
        }

        private Procedure CreateModel(ProcedureBindingModel model, Procedure procedure, PolyclinicDatabase context)
        {
            procedure.Name = model.Name;
            procedure.Cost = model.Cost;

            if (procedure.Id == 0)
            {
                context.Procedures.Add(procedure);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var procedureMedicines = context.ProcedureMedicines
                    .Where(rec => rec.ProcedureId == model.Id.Value)
                    .ToList();

                context.ProcedureMedicines.RemoveRange(procedureMedicines.ToList());

                var procedureTreatments = context.ProcedureTreatments
                   .Where(rec => rec.ProcedureId == model.Id.Value)
                   .ToList();

                context.ProcedureTreatments.RemoveRange(procedureTreatments.ToList());

                context.SaveChanges();
            }

            foreach (var procrdureMedicine in model.ProcedureMedicines)
            {
                context.ProcedureMedicines.Add(new ProcedureMedicine
                {
                    ProcedureId = procedure.Id,
                    MedicineId = procrdureMedicine.Key,
                    Count = procrdureMedicine.Value.Item2
                });
                context.SaveChanges();
            }
            
            foreach (var procedureTreatments in model.ProcedureTreatments)
            {
                context.ProcedureTreatments.Add(new ProcedureTreatment
                {
                    ProcedureId = procedure.Id,
                    TreatmentId = procedureTreatments.Key,
                    Count = procedureTreatments.Value.Item2
                });
                context.SaveChanges();
            }
            return procedure;
        }
    }
}
