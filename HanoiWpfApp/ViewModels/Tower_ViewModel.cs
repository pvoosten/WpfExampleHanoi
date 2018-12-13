using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiWpfApp.ViewModels
{
    public class Tower_ViewModel : ViewModelBase
    {
        public Tower_ViewModel()
        {
            Discs = new ObservableCollection<Disc_ViewModel>();
            if (IsInDesignMode)
            {
                Discs.Add(new Disc_ViewModel { Diameter = 10 });
                Discs.Add(new Disc_ViewModel { Diameter = 5 });
            }
            else
            {

            }
        }

        public ObservableCollection<Disc_ViewModel> Discs { get; }
    }
}
