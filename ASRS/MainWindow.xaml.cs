using System.Windows;

namespace ASRS
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

        private void bt_Researcher_Interface_Click(object sender, RoutedEventArgs e)
        {
            Researcher newWindow = new Researcher();

            newWindow.Show();
        }

        private void bt_Specialist_Interface_Click(object sender, RoutedEventArgs e)
        {
            Specialist newWindow = new Specialist();

            newWindow.Show();
        }
    }
}