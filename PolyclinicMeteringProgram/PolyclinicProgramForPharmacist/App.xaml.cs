using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicDatabase.Implements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace PolyclinicProgramForPharmacist
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            //doctor
            currentContainer.RegisterType<IDoctor, DoctorStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<DoctorLogic>(new
            HierarchicalLifetimeManager());
            //аптекарь
            currentContainer.RegisterType<IPharmacist, PharmacistStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<PharmacistLogic>(new
            HierarchicalLifetimeManager());
            //рецепт
            currentContainer.RegisterType<IPrescription, PrescriptionStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<PrescriptionLogic>(new
            HierarchicalLifetimeManager());
            //лечение
            currentContainer.RegisterType<ITreatment, TreatmentStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<TreatmentLogic>(new
            HierarchicalLifetimeManager());
            //процедура
            currentContainer.RegisterType<IProcedure, ProcedureStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ProcedureLogic>(new
            HierarchicalLifetimeManager());
            //пациент
            currentContainer.RegisterType<IPatient, PatientStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<PatientLogic>(new
            HierarchicalLifetimeManager());
            //tabletki
            currentContainer.RegisterType<IMedicine, MedicineStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<MedicineLogic>(new
            HierarchicalLifetimeManager());
            //receipt
            currentContainer.RegisterType<IReceipt, ReceiptStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReceiptLogic>(new
            HierarchicalLifetimeManager());
            //Репорт
            currentContainer.RegisterType<ReceiptReportLogic>(new
            HierarchicalLifetimeManager());

            return currentContainer;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = BuildUnityContainer();
            var welcomeWindow = container.Resolve<WelcomeWindow>();
            welcomeWindow.Show();
        }
    }
}
