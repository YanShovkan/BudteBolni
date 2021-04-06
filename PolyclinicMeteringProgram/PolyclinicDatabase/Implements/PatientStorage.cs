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
                    .Include(rec => rec.ProcedurePatients)
                    .ThenInclude(rec => rec.Patient)
                    .ToList()
                    .Select(rec => new PatientViewModel
                    {
                        Id = rec.Id,
                        FullName = rec.FullName,
                        PhoneNumber = rec.PhoneNumber,
                        DateOfBirth = rec.DateOfBirth,
                        DoctorName = rec.Doctor.FullName,
                        ProcedurePatients = rec.ProcedurePatients
                            .ToDictionary(recProcedurePatients => recProcedurePatients.ProcedureId,
                            recProcedurePatients => recProcedurePatients.Procedure?.Name)
                    })
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
                    .Include(rec => rec.ProcedurePatients)
                    .ThenInclude(rec => rec.Procedure)
                    .Where(rec => rec.FullName.Contains(model.FullName))
                    .ToList()
                    .Select(rec => new PatientViewModel
                    {
                        Id = rec.Id,
                        FullName = rec.FullName,
                        PhoneNumber = rec.PhoneNumber,
                        DateOfBirth = rec.DateOfBirth,
                        DoctorName = rec.Doctor.FullName,
                        ProcedurePatients = rec.ProcedurePatients
                            .ToDictionary(recProcedurePatients => recProcedurePatients.ProcedureId,
                            recProcedurePatients => recProcedurePatients.Procedure?.Name)
                    })
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
                    .Include(rec => rec.ProcedurePatients)
                    .ThenInclude(rec => rec.Procedure)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return patient != null ?
                    new PatientViewModel
                    {
                        Id = patient.Id,
                        FullName = patient.FullName,
                        PhoneNumber = patient.PhoneNumber,
                        DateOfBirth = patient.DateOfBirth,
                        DoctorName = patient.Doctor.FullName,
                        ProcedurePatients = patient.ProcedurePatients
                            .ToDictionary(recProcedurePatients => recProcedurePatients.ProcedureId,
                            recProcedurePatients => recProcedurePatients.Procedure?.Name)
                    } :
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

        private Patient CreateModel(PatientBindingModel model, Patient patient, PolyclinicDatabase context)
        {
            patient.FullName = model.FullName;
            patient.PhoneNumber = model.PhoneNumber.Value;
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

                context.ProcedurePatients.RemoveRange(patientProcedures
                    .Where(rec => !model.ProcedurePatients.ContainsKey(rec.PatientId))
                    .ToList());
                context.SaveChanges();

                foreach (var procedure in patientProcedures)
                {
                    model.ProcedurePatients.Remove(procedure.PatientId);
                }
                context.SaveChanges();
            }
            foreach (var patientProcedure in model.ProcedurePatients)
            {
                context.ProcedurePatients.Add(new ProcedurePatient
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
