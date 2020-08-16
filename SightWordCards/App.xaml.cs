using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Specialized;
using Crews.Education.Presentation.SightWordCards.Properties;

namespace Crews.Education.Presentation.SightWordCards
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly static string DataFile = "swc_master.ini";
        private MainWindow LaunchWindow { get; set; }
        private IniFile DecksFile { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DecksFile = new IniFile("swc_master.ini");
            if (Settings.Default.EnabledDecks == null)
            {
                Settings.Default.EnabledDecks = new StringCollection();
                Settings.Default.Save();
            }
            LaunchWindow = new MainWindow(DecksFile);
            LaunchWindow.Show();
        }
    }
}
