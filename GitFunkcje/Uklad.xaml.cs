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
    /// Interaction logic for Uklad.xaml
    /// </summary>
    public partial class Uklad : Page
    {
        int skala = 100;
        int ziarnistosc = 2;
        bool siatka = false;
        public Uklad()
        {
            InitializeComponent();
            RysujUklad(Canva.Height,Canva.Width);
        }
                public void RysujUklad(double wysokosc, double szerokosc)
        {
            Canva.Children.Clear();
            if (siatka) RysujSiatke();
            Line iksy = new Line();
            iksy.StrokeThickness = 2;
            iksy.Stroke = Brushes.Black;
            Line ygreki = new Line();
            ygreki.StrokeThickness =2;
            ygreki.Stroke = Brushes.Black;

            iksy.X1 = 0.0;
            iksy.Y1 = wysokosc / 2.0 ;
            iksy.X2 = szerokosc;
            iksy.Y2 = wysokosc / 2.0 ;
            ygreki.X1 = szerokosc / 2.0;
            ygreki.Y1 = 0.0;
            ygreki.X2 = szerokosc / 2.0 ;
            ygreki.Y2 = wysokosc;

            Canva.Children.Add(iksy);
            Canva.Children.Add(ygreki);
            // Rysowanie skali na prostych X i Y
            //X
            for (double i = 0.0; i < szerokosc; i += skala / ziarnistosc)
            {
                Line skalaWykresuX = new Line();
                skalaWykresuX.Stroke = Brushes.Black;
                skalaWykresuX.StrokeThickness = 2;

                Label text = new Label();
                text.Margin = new Thickness(i, wysokosc / 2 , i, wysokosc / 2.0);

                skalaWykresuX.X1 = i;
                skalaWykresuX.Y1 = wysokosc / 2.0 + 3 ;
                skalaWykresuX.X2 = i;
                skalaWykresuX.Y2 = wysokosc / 2.0 - 3 ;
                
                text.Content =string.Format("{0:N2}",(szerokosc / 2 - szerokosc) / skala + i / skala );
                Canva.Children.Add(text);
                
                Canva.Children.Add(skalaWykresuX);
            }
            //Y
            for (double i = 0.0; i <= wysokosc; i += skala / ziarnistosc)
            {
                Line skalaWykresuY = new Line();
                skalaWykresuY.Stroke = Brushes.Black;
                skalaWykresuY.StrokeThickness = 2;

                Label text = new Label();
                text.Margin = new Thickness(szerokosc / 2.0 , i, szerokosc / 2.0 , i);

                skalaWykresuY.X1 = szerokosc / 2.0 + 3 ;
                skalaWykresuY.Y1 = i;
                skalaWykresuY.X2 = szerokosc / 2.0 - 3 ;
                skalaWykresuY.Y2 = i;

                text.Content = string.Format("{0:N2}", -((wysokosc / 2 - wysokosc) / skala + i / skala));
                Canva.Children.Add(text);
                
                Canva.Children.Add(skalaWykresuY);
            }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Rysowanie grotow wykresu
            Line grot = new Line();
            grot.Stroke = Brushes.Black;
            grot.StrokeThickness = 1;

            grot.X1 = szerokosc / 2.0 ;
            grot.X2 = szerokosc / 2.0 - 5.0;
            grot.Y1 = 0;
            grot.Y2 = 10.0;
            Canva.Children.Add(grot);

            grot = new Line();
            grot.Stroke = Brushes.Black;
            grot.StrokeThickness = 1;
            
            grot.X1 = szerokosc / 2.0 ;
            grot.X2 = szerokosc / 2.0 + 5.0;
            grot.Y1 = 0;
            grot.Y2 = 10.0;
            Canva.Children.Add(grot);

            grot = new Line();
            grot.Stroke = Brushes.Black;
            grot.StrokeThickness = 1;

            grot.X1 = szerokosc;
            grot.X2 = szerokosc - 10.0;
            grot.Y1 = wysokosc / 2.0 ;
            grot.Y2 = wysokosc / 2.0 + 5.0 ;
            Canva.Children.Add(grot);

            grot = new Line();
            grot.Stroke = Brushes.Black;
            grot.StrokeThickness = 1;

            grot.X1 = szerokosc;
            grot.X2 = szerokosc - 10.0;
            grot.Y1 = wysokosc / 2.0 ;
            grot.Y2 = wysokosc / 2.0 - 5.0 ;
            Canva.Children.Add(grot);

        }
        public void RysujSiatke()
        {
            for (double i = 0.0; i <= Canva.Width; i += skala / ziarnistosc/2)
            {

                Line skalaSiatkiX = new Line();
                skalaSiatkiX.Stroke = Brushes.Red;
                skalaSiatkiX.StrokeThickness = 1;

                skalaSiatkiX.X1 = i;
                skalaSiatkiX.Y1 = 0;
                skalaSiatkiX.X2 = i;
                skalaSiatkiX.Y2 = Canva.Height;

                Canva.Children.Add(skalaSiatkiX);
            }

            for (double i = 0.0; i <= Canva.Height; i += skala / ziarnistosc/2)
            {
                Line skalaSiatkiY = new Line();
                skalaSiatkiY.Stroke = Brushes.Red;
                skalaSiatkiY.StrokeThickness = 1;

                skalaSiatkiY.X1 = 0;
                skalaSiatkiY.Y1 = i;
                skalaSiatkiY.X2 = Canva.Width;
                skalaSiatkiY.Y2 = i;

                Canva.Children.Add(skalaSiatkiY);
            }
        }

        //Czyszczenie wykresu
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Canva.Strokes.Clear();
        }
        //Rysowanie siatki
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (siatka)
            {
                siatka = false;

            }
            else
            {
                siatka = true;
            }
            RysujUklad(Canva.Height, Canva.Width);
        }
        private void koniec_Click(object sender, RoutedEventArgs e)
        {
            App.Tablica.Content = new Wygaszacz();
        }
    }
}
