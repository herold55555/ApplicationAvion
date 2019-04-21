using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class SettingsViewModel : BaseNotify
    {
        private ApplicationSettingsModel myModel;

        private ICommand _okCommand;
        private ICommand _cancelCommand;
        public ICommand OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => this.TransferSettings()));
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return this._cancelCommand ?? (this._cancelCommand =
                new CommandHandler(() => this.Nothing()));
            }
        }
        public string FlightServerIP
        {
            get;
            set;
        }
        public int FlightCommandPort
        {
            get;
            set;
        }
        public int FlightInfoPort
        {
            get;
            set;
        }
        public SettingsViewModel()
        {
            this.myModel = new ApplicationSettingsModel();
        }
        public void TransferSettings()
        {
            this.myModel.FlightServerIP = this.FlightServerIP;
            this.myModel.FlightInfoPort = this.FlightInfoPort;
            this.myModel.FlightCommandPort = this.FlightCommandPort;
            this.myModel.SaveSettings();
            NotifyPropertyChanged("OkCommand");
        }
        public void Nothing()
        {
            NotifyPropertyChanged("CancelCommand");
        }
    }
}
