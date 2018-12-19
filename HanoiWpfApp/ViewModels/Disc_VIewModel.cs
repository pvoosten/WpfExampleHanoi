using GalaSoft.MvvmLight;
using TowerOfHanoi.Model;

namespace HanoiWpfApp.ViewModels
{
    public class Disc_ViewModel : ViewModelBase
    {

        public Disc_ViewModel()
        {
            if (IsInDesignMode)
            {
                Disc = new Disc(5);
            }
        }

        public Disc_ViewModel(Disc disc)
        {
            Disc = disc;
        }

        public Disc Disc { get; }

        public int Diameter => Disc.Diameter;

        public int DiameterToDraw => Disc.Diameter * 10;
    }
}
