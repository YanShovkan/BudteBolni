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
        public void Delete(ProcedureBindingModel model)
        {
            throw new NotImplementedException();
        }

        public ProcedureViewModel GetElement(ProcedureBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<ProcedureViewModel> GetFilteredList(ProcedureBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<ProcedureViewModel> GetFullList()
        {
            throw new NotImplementedException();
        }

        public void Insert(ProcedureBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(ProcedureBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
