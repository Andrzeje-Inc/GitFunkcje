using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GitFunkcje
{
    /// <summary>
    /// Interaction logic for Tablica.xaml
    /// </summary>
    public partial class Tablica : Window
    {
        public Tablica()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.Left = SystemParameters.PrimaryScreenWidth + 100;
            this.WindowState = System.Windows.WindowState.Maximized;
            this.Content = new Wygaszacz();
        }


    }
}
