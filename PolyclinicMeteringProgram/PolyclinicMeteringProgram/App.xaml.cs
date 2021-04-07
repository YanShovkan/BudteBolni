
using System.Windows;
using Unity;
using Unity.Lifetime;
using PolyclinicBusinessLogic.BusinessLogics;
using PolyclinicBusinessLogic.BindingModels;
using PolyclinicBusinessLogic.Interfaces;
using PolyclinicDatabase.Implements;

namespace PolyclinicMeteringProgram
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
