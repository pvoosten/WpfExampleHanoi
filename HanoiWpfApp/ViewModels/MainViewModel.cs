using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerOfHanoi.Model;

namespace HanoiWpfApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private Hanoi model;

        public MainViewModel()
        {
            GameControl = new GameControl_ViewModel();
            FirstTower = new Tower_ViewModel();
            SecondTower = new Tower_ViewModel();
            ThirdTower = new Tower_ViewModel();
            if (IsInDesignMode)
            {
                void addDisc(int diam) => FirstTower.Discs.Add(new Disc_ViewModel { Diameter = diam });
                addDisc(10);
                addDisc(20);
                addDisc(21);
            }
            else
            {
                GameControl.GameStateChanged += (s, e) => ChangeGameState();
            }
        }

        public GameControl_ViewModel GameControl { get; }

        public Tower_ViewModel FirstTower { get; set; }
        public Tower_ViewModel SecondTower { get; set; }
        public Tower_ViewModel ThirdTower { get; set; }

        private void ChangeGameState()
        {
            FirstTower.Discs.Clear();
            SecondTower.Discs.Clear();
            ThirdTower.Discs.Clear();
            int discCount = GameControl.NumberOfDiscs;

            model = new Hanoi(discCount);
            for (int i = 0; i < discCount; i++)
            {
                FirstTower.Discs.Add(new Disc_ViewModel { Diameter = i + 1 });
            }
        }
    }
}
