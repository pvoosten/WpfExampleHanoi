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
        public ObservableCollection<Disc_ViewModel> Discs { get; set; }
    }
}
