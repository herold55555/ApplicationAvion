using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel myModel;

        private ICommand _connectCommand;
        private ICommand _settingsOpenCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand =
                new CommandHandler(() => this.Connect()));
            }
        }

        public ICommand SettingsOpenCommand
        {
            get
            {
                return this._settingsOpenCommand ?? (this._settingsOpenCommand =
                new CommandHandler(() => this.OpenSettingsWindow()));
            }
        }

        public FlightBoardViewModel()
        {
            this.myModel = new FlightBoardModel();
        }
        public double Lon
        {
            get;
        }

        public double Lat
        {
            get;
        }
        public void Connect()
        {
            this.myModel.StartListening();
            this.myModel.StartClient();
        }
        public void OpenSettingsWindow()
        {
            Settings mySettingsWindow = new Settings();
            mySettingsWindow.ShowDialog();
        }
    }
}
