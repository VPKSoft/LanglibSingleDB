using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VPKSoft.LangLib;

namespace LangLibTestWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Utils.ShouldLocalize() != null) // Localize and exit.
            {
                new MainWindow();
                new AboutWindow();
                System.Diagnostics.Process.GetCurrentProcess().Kill(); // self kill after localization
                return;
            }
        }
    }
}
