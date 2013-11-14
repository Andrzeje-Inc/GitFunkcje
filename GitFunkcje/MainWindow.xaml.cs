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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GitFunkcje
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window Tab;
        //Otwieranie okna na tablicy 
        Window OknoNaTablice()
        {
            Window newWindow = new Tablica();
            newWindow.Show();
            return newWindow;
        }

        public MainWindow()
        {
            InitializeComponent();
            App.Tablica = OknoNaTablice();
            App.GlowneOkno = this;
            this.Content = new Window1();
        }
        //Zamekanie okien
        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Tablica.Close();
        }


    }
}
