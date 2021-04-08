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
            //Pharmacist
            currentContainer.RegisterType<IPharmacist, PharmacistStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<PharmacistLogic>(new
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
