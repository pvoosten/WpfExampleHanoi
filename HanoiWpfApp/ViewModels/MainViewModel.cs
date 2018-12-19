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
        private Disc_ViewModel takenDisc;

        public MainViewModel()
        {
            GameControl = new GameControl_ViewModel();
            FirstTower = CreateTower();
            SecondTower = CreateTower();
            ThirdTower = CreateTower();
            if (IsInDesignMode)
            {
                GameControl.NumberOfDiscs = 5;
                FirstTower.SetTower(new Hanoi(5).StartTower);
            }
            else
            {
                GameControl.GameStateChanged += (s, e) => ChangeGameState();
            }
        }

        private Tower_ViewModel CreateTower()
        {
            var tower = new Tower_ViewModel();
            tower.LayDownDiscRequested += Tower_LayDownDiskRequested;
            tower.DiscTaken += Tower_DiscTaken;
            return tower;
        }

        private void Tower_DiscTaken(object sender, DiscMovedEventArgs e)
        {
            SetAllowTakeDisc(false);
            takenDisc = e.Disc;
        }

        private void SetAllowTakeDisc(bool setAllowTake)
        {
            FirstTower.IsTakeDiscAllowed = setAllowTake;
            SecondTower.IsTakeDiscAllowed = setAllowTake;
            ThirdTower.IsTakeDiscAllowed = setAllowTake;
        }

        private void Tower_LayDownDiskRequested(object sender, EventArgs e)
        {
            if (takenDisc != null && sender is Tower_ViewModel tower)
            {
                if (tower.TryLayDownDisc(takenDisc))
                {
                    SetAllowTakeDisc(true);
                }
            }
        }

        public GameControl_ViewModel GameControl { get; }

        public Tower_ViewModel FirstTower { get; }
        public Tower_ViewModel SecondTower { get; }
        public Tower_ViewModel ThirdTower { get; }

        private void ChangeGameState()
        {
            FirstTower.Discs.Clear();
            SecondTower.Discs.Clear();
            ThirdTower.Discs.Clear();
            int discCount = GameControl.NumberOfDiscs;

            model = new Hanoi(discCount);
            FirstTower.SetTower(model.StartTower);
            SecondTower.SetTower(model.MiddleTower);
            ThirdTower.SetTower(model.EndTower);

            FirstTower.IsPlayedManually = GameControl.IsPlayManuallyChecked;
            SecondTower.IsPlayedManually = GameControl.IsPlayManuallyChecked;
            ThirdTower.IsPlayedManually = GameControl.IsPlayManuallyChecked;
            SetAllowTakeDisc(GameControl.IsPlayManuallyChecked);
        }
    }
}
