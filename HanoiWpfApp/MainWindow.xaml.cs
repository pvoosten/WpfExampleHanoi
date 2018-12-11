using System.Windows;

namespace HanoiWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Op knop geklikt");
            
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            checkBox.IsChecked=true;
        }
    }
}
