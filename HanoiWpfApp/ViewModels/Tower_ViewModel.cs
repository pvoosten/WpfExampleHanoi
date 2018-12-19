using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerOfHanoi.Model;

namespace HanoiWpfApp.ViewModels
{
    public class Tower_ViewModel : ViewModelBase
    {

        private Tower _tower;

        public Tower_ViewModel()
        {
            Discs = new ObservableCollection<Disc_ViewModel>();
            if (IsInDesignMode)
            {
                AddDisc(new Disc(10));
                AddDisc(new Disc(5));
            }
        }

        public void SetTower(Tower tower)
        {
            _tower = tower;
            Discs.Clear();
            foreach (var disc in tower.DiscsFromBottomToTop())
            {
                AddDisc(disc);
            }
        }

        /// <summary>
        /// Adds a disc (from the model!!!) to the tower viewmodel
        /// </summary>
        /// <param name="disc">The disc from the model to add to the tower viewmodel.</param>
        private void AddDisc(Disc disc)
        {
            Discs.Add(new Disc_ViewModel(disc));
        }

        public ObservableCollection<Disc_ViewModel> Discs { get; }

        public event EventHandler<DiscMovedEventArgs> DiscTaken;

        public event EventHandler LayDownDiscRequested;

        internal bool TryLayDownDisc(Disc_ViewModel discVm)
        {
            try
            {
                _tower.LayDiscOnTop(discVm.Disc);
                // if no exception occurred in the model, everything is ok and the disk can be added on top in the view
                Discs.Insert(0, discVm);
                return true;
            }
            catch { }
            return false;
        }

        #region IsPlayedManually property
        private bool _isPlayedManually;

        public bool IsPlayedManually
        {
            get => _isPlayedManually;
            set
            {
                if (_isPlayedManually == value) return;
                _isPlayedManually = value;
                RaisePropertyChanged(nameof(IsPlayedManually));
            }
        }
        #endregion IsPlayedManually property

        #region IsTakeDiscAllowed property

        private bool _isTakeDiscAllowed;

        public bool IsTakeDiscAllowed
        {
            get => _isTakeDiscAllowed;
            set
            {
                if (_isTakeDiscAllowed == value) return;
                _isTakeDiscAllowed = value;
                RaisePropertyChanged(nameof(IsTakeDiscAllowed));
            }
        }
        #endregion IsTakeDiscAllowed property

        #region Lay down disc command
        public RelayCommand LayDownDiscCommand => new RelayCommand(LayDownDisc, CanLayDownDisc);

        private bool CanLayDownDisc()
        {
            return true;
        }

        private void LayDownDisc()
        {
            LayDownDiscRequested?.Invoke(this, EventArgs.Empty);
        }

        #endregion Lay down disc command

        #region Take disc command
        public RelayCommand TakeDiscCommand => new RelayCommand(TakeDisc, CanTakeDisc);

        private void TakeDisc()
        {
            if (!CanTakeDisc()) return;
            var disc = Discs[0]; // Extension method, needs "using System.Linq;" op top of the file.
            Discs.RemoveAt(0);
            _tower.TakeDisc();
            DiscTaken?.Invoke(this, new DiscMovedEventArgs(disc));
        }

        private bool CanTakeDisc()
        {
            return Discs.Count != 0 && IsTakeDiscAllowed;
        }
        #endregion
    }
}
