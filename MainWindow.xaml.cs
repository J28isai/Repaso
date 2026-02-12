using System.Windows;
using Repaso.ViewModel;

namespace Repaso
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
