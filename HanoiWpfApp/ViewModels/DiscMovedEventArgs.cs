namespace HanoiWpfApp.ViewModels
{
    public class DiscMovedEventArgs
    {
        public DiscMovedEventArgs(Disc_ViewModel disc)
        {
            Disc = disc;
        }

        public  Disc_ViewModel Disc { get; }
    }
}