using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiWpfApp.ViewModels
{
    public class GameControl_ViewModel : ViewModelBase
    {
        private int _numberOfDiscs;

        public GameControl_ViewModel()
        {

        }

        public event EventHandler GameStateChanged;

        public bool IsPlayManuallyChecked { get; set; }
        public bool IsPlayClickThroughChecked { get; set; }

        public RelayCommand StartNewGameCommand => new RelayCommand(() => GameStateChanged?.Invoke(this, EventArgs.Empty));
        public int NumberOfDiscs
        {
            get => _numberOfDiscs;
            set
            {
                if (_numberOfDiscs == value) return;
                _numberOfDiscs = value;
                RaisePropertyChanged(nameof(NumberOfDiscs));
            }
        }
    }
}
