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
    public class PatientStorage : IPatient
    {
        public List<PatientViewModel> GetFullList()
        {
            using (var context = new PolyclinicDatabase())
            {
                return context.Patients
                    .Include(rec => rec.PatientProcedures)
                    .Include(rec => rec.Doctor)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public List<PatientViewModel> GetFilteredList(PatientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PolyclinicDatabase())
            {
                return context.Patients
                    .Include(rec => rec.PatientProcedures)
                    .Include(rec => rec.Doctor)
                    .Where(rec => rec.DoctorId == model.DoctorId)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public PatientViewModel GetElement(PatientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new PolyclinicDatabase())
            {
                var patient = context.Patients
                    .Include(rec => rec.PatientProcedures)
                    .Include(rec => rec.Doctor)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return patient != null ?
                    CreateModel(patient) :
                    null;
            }
        }
        public void Insert(PatientBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Patient(), context);
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
        public void Update(PatientBindingModel model)
        {
            using (var context = new PolyclinicDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var patient = context.Patients.FirstOrDefault(rec => rec.Id == model.Id);

                        if (patient == null)
                        {
                            throw new Exception("Пациент не найден");
                        }

                        CreateModel(model, patient, context);
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
        public void Delete(PatientBindingModel model)
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
        private PatientViewModel CreateModel(Patient patient)
        {
            return new PatientViewModel
            {
                Id = patient.Id,
                DoctorId = patient.DoctorId,
                FullName = patient.FullName,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth,
                DoctorName = patient.Doctor.FullName,
                PatientProcedures = patient.PatientProcedures
                            .ToDictionary(recProcedurePatient => recProcedurePatient.ProcedureId,
                            recProcedurePatient => recProcedurePatient.Procedure?.Name)
            };
           
        }

        private Patient CreateModel(PatientBindingModel model, Patient patient, PolyclinicDatabase context)
        {
            patient.FullName = model.FullName;
            patient.PhoneNumber = model.PhoneNumber;
            patient.DateOfBirth = model.DateOfBirth;
            patient.DoctorId = model.DoctorId;
            if (patient.Id == 0)
            {
                context.Patients.Add(patient);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var patientProcedures = context.ProcedurePatients
                    .Where(rec => rec.PatientId == model.Id.Value)
                    .ToList();
                if (patientProcedures.Count > 0 && model.PatientProcedures.Count != 0)
                {
                    context.ProcedurePatients.RemoveRange(patientProcedures
                    .Where(rec => !model.PatientProcedures.ContainsKey(rec.ProcedureId))
                    .ToList());

                    context.SaveChanges();

                    foreach (var procedure in patientProcedures)
                    {
                        model.PatientProcedures.Remove(procedure.ProcedureId);
                    }

                    context.SaveChanges();
                }
            }
            foreach (var patientProcedure in model.PatientProcedures)
            {
                context.ProcedurePatients.Add(new PatientProcedure
                {
                    PatientId = patient.Id,
                    ProcedureId = patientProcedure.Key
                });
                context.SaveChanges();
            }
            return patient;
        }
    }
}
