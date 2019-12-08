using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Waf.Foundation;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LfdArchive.Applications.Controllers;

namespace LfdArchive
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AggregateCatalog catalog;
        private CompositionContainer container;
        private ApplicationController controller;


        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {

                base.OnStartup(e);

                catalog = new AggregateCatalog();
                // Add the WpfApplicationFramework assembly to the catalog
                catalog.Catalogs.Add(new AssemblyCatalog(typeof(Model).Assembly));
                // Add the WafApplication assembly to the catalog
                catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

                container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
                CompositionBatch batch = new CompositionBatch();
                batch.AddExportedValue(container);
                container.Compose(batch);

                controller = container.GetExportedValue<ApplicationController>();
                controller.Initialize();
                controller.Run();
            }
            catch (System.Exception ex)
            {
            
                Console.WriteLine($"{ex.Source}: {ex.Message}; {ex.StackTrace}");
                throw ex;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Console.WriteLine("Shutting down gracefully");

            controller.Shutdown();
            container.Dispose();
            catalog.Dispose();

            base.OnExit(e);
        }
    }
}
