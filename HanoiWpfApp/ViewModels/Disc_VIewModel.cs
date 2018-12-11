using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiWpfApp.ViewModels
{
    public class Disc_ViewModel : ViewModelBase
    {
        public Disc_ViewModel()
        {
            if (IsInDesignMode)
            {
                Diameter = 20;
            }
        }

        private int _diameter;        
        public int Diameter
        {
            get => _diameter;
            set
            {
                if (_diameter == value) return;
                _diameter = value;
                RaisePropertyChanged(nameof(Diameter));
            }
        }
    }
}
