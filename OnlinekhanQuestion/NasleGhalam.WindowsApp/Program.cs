using NasleGhalam.ServiceLayer.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NasleGhalam.WindowsApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SiteConfig.RegisterAutoMapper();
            Application.Run(StructureMapConfig.Container.GetInstance<Main>());
        }
    }
}
