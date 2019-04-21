using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private SettingsViewModel mySettingViewModel;

        public Settings()
        {
            InitializeComponent();
            this.mySettingViewModel = new SettingsViewModel();
            this.DataContext = this.mySettingViewModel;
            mySettingViewModel.PropertyChanged += (s,e) => this.Close();
        }

    }
}
