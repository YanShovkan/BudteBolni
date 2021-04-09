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
        public void Delete(PrescriptionBindingModel model)
        {
            throw new NotImplementedException();
        }

        public PrescriptionViewModel GetElement(PrescriptionBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<PrescriptionViewModel> GetFilteredList(PrescriptionBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<PrescriptionViewModel> GetFullList()
        {
            throw new NotImplementedException();
        }

        public void Insert(PrescriptionBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(PrescriptionBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
