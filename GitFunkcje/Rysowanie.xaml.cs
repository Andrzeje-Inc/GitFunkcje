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
    /// Interaction logic for Rysowanie.xaml
    /// </summary>
    public partial class Rysowanie : Page
    {
        int skala = 100;
        int przesunieciePoX = 0;
        int przesunieciePoY = 0;
        bool siatka = false;
        bool dragging = false;

        PointCollection myPointCollection = new PointCollection();
        PointCollection punktyNaWykres100 = new PointCollection();
        PointCollection punktyNaWykres200 = new PointCollection();
        PointCollection punktyNaWykres500 = new PointCollection();
        PointCollection punktyDoFunkcji = new PointCollection();

        void RysujUklad(double wysokosc, double szerokosc, int skala, int przesunieciePoX, int przesunieciePoY)
        {
            canvas1.Children.Clear();
            if (siatka) RysujSiatke();
            Line iksy = new Line();
            iksy.StrokeThickness = 1;
            iksy.Stroke = Brushes.Black;
            Line ygreki = new Line();
            ygreki.StrokeThickness = 1;
            ygreki.Stroke = Brushes.Black;

            iksy.X1 = 0.0;
            iksy.Y1 = wysokosc / 2.0 + przesunieciePoY;
            iksy.X2 = szerokosc;
            iksy.Y2 = wysokosc / 2.0 + przesunieciePoY;
            ygreki.X1 = szerokosc / 2.0 + przesunieciePoX;
            ygreki.Y1 = 0.0;
            ygreki.X2 = szerokosc / 2.0 + przesunieciePoX;
            ygreki.Y2 = wysokosc;

            canvas1.Children.Add(iksy);
            canvas1.Children.Add(ygreki);

            // Rysowanie skali na prostych X i Y
            //X
            for (double i = 0.0; i <= szerokosc; i += skala / 2)
            {
                Line skalaWykresuX = new Line();
                skalaWykresuX.Stroke = Brushes.Black;
                skalaWykresuX.StrokeThickness = 0.5;

                Label text = new Label();
                text.Margin = new Thickness(i, wysokosc / 2 + przesunieciePoY, i, wysokosc / 2.0 + przesunieciePoY);

                if (i % skala == 0)
                {
                    skalaWykresuX.X1 = i;
                    skalaWykresuX.Y1 = wysokosc / 2.0 + 3 + przesunieciePoY;
                    skalaWykresuX.X2 = i;
                    skalaWykresuX.Y2 = wysokosc / 2.0 - 3 + przesunieciePoY;

                    text.Content = (szerokosc / 2 - szerokosc) / skala + i / skala - przesunieciePoX / skala;
                    canvas1.Children.Add(text);
                }
                else
                {
                    skalaWykresuX.X1 = i;
                    skalaWykresuX.Y1 = wysokosc / 2.0 + 5 + przesunieciePoY;
                    skalaWykresuX.X2 = i;
                    skalaWykresuX.Y2 = wysokosc / 2.0 - 5 + przesunieciePoY;

                    text.Content = (szerokosc / 2 - szerokosc) / skala + i / skala - przesunieciePoX / skala;
                    canvas1.Children.Add(text);
                }

                canvas1.Children.Add(skalaWykresuX);
            }
            //Y
            for (double i = 0.0; i <= wysokosc; i += skala / 2)
            {
                Line skalaWykresuY = new Line();
                skalaWykresuY.Stroke = Brushes.Black;
                skalaWykresuY.StrokeThickness = 0.5;

                Label text = new Label();
                text.Margin = new Thickness(szerokosc / 2.0 + przesunieciePoX, i, szerokosc / 2.0 + przesunieciePoX, i);

                if (i % skala == 0)
                {
                    skalaWykresuY.X1 = szerokosc / 2.0 + 3 + przesunieciePoX;
                    skalaWykresuY.Y1 = i;
                    skalaWykresuY.X2 = szerokosc / 2.0 - 3 + przesunieciePoX;
                    skalaWykresuY.Y2 = i;

                    text.Content = -((wysokosc / 2 - wysokosc) / skala + i / skala) + przesunieciePoY / skala;
                    canvas1.Children.Add(text);


                }
                else
                {
                    skalaWykresuY.X1 = szerokosc / 2.0 + 5 + przesunieciePoX;
                    skalaWykresuY.Y1 = i;
                    skalaWykresuY.X2 = szerokosc / 2.0 - 5 + przesunieciePoX;
                    skalaWykresuY.Y2 = i;

                    text.Content = -((wysokosc / 2 - wysokosc) / skala + i / skala) + przesunieciePoY / skala;
                    canvas1.Children.Add(text);
                }


                canvas1.Children.Add(skalaWykresuY);

            }

            //Rysowanie grotow wykresu
            Line grot = new Line();
            grot.Stroke = Brushes.Black;
            grot.StrokeThickness = 1;

            grot.X1 = szerokosc / 2.0 + przesunieciePoX;
            grot.X2 = szerokosc / 2.0 + przesunieciePoX - 5.0;
            grot.Y1 = 0;
            grot.Y2 = 10.0;
            canvas1.Children.Add(grot);

            grot = new Line();
            grot.Stroke = Brushes.Black;
            grot.StrokeThickness = 1;
            grot.X1 = szerokosc / 2.0 + przesunieciePoX;
            grot.X2 = szerokosc / 2.0 + przesunieciePoX + 5.0;
            grot.Y1 = 0;
            grot.Y2 = 10.0;
            canvas1.Children.Add(grot);

            grot = new Line();
            grot.Stroke = Brushes.Black;
            grot.StrokeThickness = 1;

            grot.X1 = szerokosc;
            grot.X2 = szerokosc - 10.0;
            grot.Y1 = wysokosc / 2.0 + przesunieciePoY;
            grot.Y2 = wysokosc / 2.0 + 5.0 + przesunieciePoY;
            canvas1.Children.Add(grot);

            grot = new Line();
            grot.Stroke = Brushes.Black;
            grot.StrokeThickness = 1;

            grot.X1 = szerokosc;
            grot.X2 = szerokosc - 10.0;
            grot.Y1 = wysokosc / 2.0 + przesunieciePoY;
            grot.Y2 = wysokosc / 2.0 - 5.0 + przesunieciePoY;
            canvas1.Children.Add(grot);

            RysujWykresFunkcji();

        }

        void RysujSiatke()
        {
            for (double i = 0.0; i <= canvas1.Width; i += skala / 4)
            {

                Line skalaSiatkiX = new Line();
                skalaSiatkiX.Stroke = Brushes.Red;
                skalaSiatkiX.StrokeThickness = 0.5;

                skalaSiatkiX.X1 = i;
                skalaSiatkiX.Y1 = 0;
                skalaSiatkiX.X2 = i;
                skalaSiatkiX.Y2 = canvas1.Height;

                canvas1.Children.Add(skalaSiatkiX);
            }

            for (double i = 0.0; i <= canvas1.Height; i += skala / 4)
            {
                Line skalaSiatkiY = new Line();
                skalaSiatkiY.Stroke = Brushes.Red;
                skalaSiatkiY.StrokeThickness = 0.5;

                skalaSiatkiY.X1 = 0;
                skalaSiatkiY.Y1 = i;
                skalaSiatkiY.X2 = canvas1.Width;
                skalaSiatkiY.Y2 = i;

                canvas1.Children.Add(skalaSiatkiY);
            }
        }
        void RysujWykresFunkcji()
        {
            //Polyline connection_Line = new Polyline();
            //connection_Line.Stroke = System.Windows.Media.Brushes.Black;
            //connection_Line.StrokeThickness = 2;
            //connection_Line.FillRule = FillRule.EvenOdd;

            if (skala == 100)
            {
                foreach (Point element in punktyNaWykres100)
                {
                    Ellipse punkt = new Ellipse();
                    punkt.Fill = Brushes.Black;
                    punkt.StrokeThickness = 1;
                    punkt.Height = skala / 100 + 2;
                    punkt.Width = skala / 100 + 2;
                    punkt.Stroke = Brushes.Black;
                    punkt.Margin = new Thickness(element.X, element.Y, 0, 0);
                    canvas1.Children.Add(punkt);
                }
                //connection_Line.Points = punktyNaWykres100;
                //canvas1.Children.Add(connection_Line);
            }
            else if (skala == 200)
            {
                foreach (Point element in punktyNaWykres200)
                {
                    Ellipse punkt = new Ellipse();
                    punkt.Fill = Brushes.Black;
                    punkt.StrokeThickness = 1;
                    punkt.Height = skala / 100 + 2;
                    punkt.Width = skala / 100 + 2;
                    punkt.Stroke = Brushes.Black;
                    punkt.Margin = new Thickness(element.X, element.Y, 0, 0);
                    canvas1.Children.Add(punkt);
                }
                //connection_Line.Points = punktyNaWykres200;
                //canvas1.Children.Add(connection_Line);
            }
            else if (skala == 500)
            {
                foreach (Point element in punktyNaWykres500)
                {
                    Ellipse punkt = new Ellipse();
                    punkt.Fill = Brushes.Black;
                    punkt.StrokeThickness = 1;
                    punkt.Height = skala / 100 + 2;
                    punkt.Width = skala / 100 + 2;
                    punkt.Stroke = Brushes.Black;
                    punkt.Margin = new Thickness(element.X, element.Y, 0, 0);
                    canvas1.Children.Add(punkt);
                }
                //connection_Line.Points = punktyNaWykres500;
                //canvas1.Children.Add(connection_Line);
            }
        }
        public Rysowanie()
        {
            InitializeComponent();
            RysujUklad(canvas1.Height, canvas1.Width, skala, przesunieciePoX, przesunieciePoY);
        }

        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragging = true;
            Point s = e.GetPosition(canvas1);
            Ellipse punkt = new Ellipse();
            punkt.Fill = Brushes.Black;
            punkt.StrokeThickness = 1;
            punkt.Height = skala / 100 + 2;
            punkt.Width = skala / 100 + 2;
            punkt.Stroke = Brushes.Black;
            punkt.Margin = new Thickness(s.X, s.Y, 0, 0);
            canvas1.Children.Add(punkt);

            myPointCollection.Add(s);
            //connection_Line.Points = myPointCollection;
            //canvas1.Children.Add(connection_Line);
            if (skala == 100)
            {
                punktyNaWykres100.Add(s);
                punktyNaWykres200.Add(new Point(s.X * 2 - canvas1.Width / 2, s.Y * 2 - canvas1.Height / 2));
                punktyNaWykres500.Add(new Point(s.X * 5 - canvas1.Width * 2, s.Y * 5 - canvas1.Height * 2));
            }
            else if (skala == 200)
            {
                punktyNaWykres100.Add(new Point(s.X / 2 + canvas1.Width / 4, s.Y / 2 + canvas1.Height / 4));
                punktyNaWykres200.Add(s);
                punktyNaWykres500.Add(new Point(s.X / 2 * 5 - canvas1.Width * 4 / 5, s.Y / 2 * 5 - canvas1.Height * 4 / 5));
            }
            else if (skala == 500)
            {
                punktyNaWykres100.Add(new Point(s.X / 5 + canvas1.Width / 2 + canvas1.Width / 10, s.Y / 5 + canvas1.Height / 2 + canvas1.Height / 10));
                punktyNaWykres200.Add(new Point(s.X * 2 - canvas1.Width / 2, s.Y * 2 - canvas1.Height / 2));//TO POPRAWIC
                punktyNaWykres500.Add(s);
            }

            s.X = s.X / skala - przesunieciePoX / skala - (canvas1.Width / 2) / skala;
            s.Y = -(s.Y / skala - przesunieciePoY / skala - (canvas1.Height / 2) / skala);
            punktyDoFunkcji.Add(s);

        }

        private void canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            dragging = false;
            myPointCollection = new PointCollection();
        }

        private void canvas1_MouseMove(object sender, MouseEventArgs e)
        {
            //textBox1.Text = "x=" + e.GetPosition(canvas1).ToString() + "\ny=" + e.GetPosition(canvas1).ToString();
            //textBox1.Text = (e.GetPosition(canvas1).X/skala-przesunieciePoX/skala - (canvas1.ActualWidth / 2.0)/skala).ToString() + "#" + (-(e.GetPosition(canvas1).Y/skala-przesunieciePoY/skala - (canvas1.ActualHeight / 2.0)/skala)).ToString();
            //SystemParametersInfo(10, 0,0,0);
            if (dragging)
            {

                //Polyline connection_Line = new Polyline();
                //connection_Line.Points.Clear();
                //connection_Line.Stroke = Brushes.Black;
                //connection_Line.StrokeThickness = 2;
                //connection_Line.FillRule = FillRule.EvenOdd;
                Point s = e.GetPosition(canvas1);
                Ellipse punkt = new Ellipse();
                punkt.Fill = Brushes.Black;
                punkt.StrokeThickness = 1;
                punkt.Height = skala / 100 + 2;
                punkt.Width = skala / 100 + 2;
                punkt.Stroke = Brushes.Black;
                punkt.Margin = new Thickness(s.X, s.Y, 0, 0);
                canvas1.Children.Add(punkt);

                myPointCollection.Add(s);
                //connection_Line.Points = myPointCollection;
                //canvas1.Children.Add(connection_Line);
                if (skala == 100)
                {
                    punktyNaWykres100.Add(s);
                    punktyNaWykres200.Add(new Point(s.X * 2 - canvas1.Width / 2, s.Y * 2 - canvas1.Height / 2));
                    punktyNaWykres500.Add(new Point(s.X * 5 - canvas1.Width * 2, s.Y * 5 - canvas1.Height * 2));
                }
                else if (skala == 200)
                {
                    punktyNaWykres100.Add(new Point(s.X / 2 + canvas1.Width / 4, s.Y / 2 + canvas1.Height / 4));
                    punktyNaWykres200.Add(s);
                    punktyNaWykres500.Add(new Point(s.X / 2 * 5 - canvas1.Width * 4 / 5, s.Y / 2 * 5 - canvas1.Height * 4 / 5));
                }
                else if (skala == 500)
                {
                    punktyNaWykres100.Add(new Point(s.X / 5 + canvas1.Width / 2 + canvas1.Width / 10 - 100, s.Y / 5 + canvas1.Height / 2 + canvas1.Height / 10 - 100));
                    punktyNaWykres200.Add(new Point(s.X * 2 - canvas1.Width / 2, s.Y * 2 - canvas1.Height / 2));//TO POPRAWIC
                    punktyNaWykres500.Add(s);
                }

                s.X = s.X / skala - przesunieciePoX / skala - (canvas1.Width / 2) / skala;
                s.Y = -(s.Y / skala - przesunieciePoY / skala - (canvas1.Height / 2) / skala);
                punktyDoFunkcji.Add(s);

            }
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider1.Value < 0.5) { skala = 100; slider1.Value = 0; }
            else if (slider1.Value >= 0.5 && slider1.Value < 1.5) { slider1.Value = 1; skala = 200; }
            else if (slider1.Value >= 1.5) { slider1.Value = 2; skala = 500; }
            RysujUklad(canvas1.Height, canvas1.Width, skala, przesunieciePoX, przesunieciePoY);
        }

        private void rsiatka_Click_1(object sender, RoutedEventArgs e)
        {
            if (siatka)
            {
                siatka = false;

            }
            else
            {
                siatka = true;
            }
            RysujUklad(canvas1.Height, canvas1.Width, skala, przesunieciePoX, przesunieciePoY);
        }

        private void gora_Click(object sender, RoutedEventArgs e)
        {
            przesunieciePoY -= 1 * skala;
            PointCollection tymczasem = new PointCollection();
            PointCollection punktyDoPrzesuniec = new PointCollection();
            if (skala == 100) punktyDoPrzesuniec = punktyNaWykres100;
            else if (skala == 200) punktyDoPrzesuniec = punktyNaWykres200;
            else if (skala == 500) punktyDoPrzesuniec = punktyNaWykres500;
            for (int i = 0; i < punktyDoPrzesuniec.Count; i++)
            {

                double tempy = punktyDoPrzesuniec.ElementAt(i).Y;
                double tempx = punktyDoPrzesuniec.ElementAt(i).X;
                tempy -= 1 * skala;
                Point temppunkt = new Point(tempx, tempy);
                tymczasem.Add(temppunkt);
            }
            if (skala == 100)
            {
                punktyNaWykres100 = new PointCollection();
                punktyNaWykres100 = tymczasem;
            }
            else if (skala == 200)
            {
                punktyNaWykres200 = new PointCollection();
                punktyNaWykres200 = tymczasem;
            }
            else if (skala == 500)
            {
                punktyNaWykres500 = new PointCollection();
                punktyNaWykres500 = tymczasem;
            }
            RysujUklad(canvas1.Height, canvas1.Width, skala, przesunieciePoX, przesunieciePoY);
        
        }

        private void dol_Click(object sender, RoutedEventArgs e)
        {
            przesunieciePoY += 1 * skala;
            PointCollection tymczasem = new PointCollection();
            PointCollection punktyDoPrzesuniec = new PointCollection();
            if (skala == 100) punktyDoPrzesuniec = punktyNaWykres100;
            else if (skala == 200) punktyDoPrzesuniec = punktyNaWykres200;
            else if (skala == 500) punktyDoPrzesuniec = punktyNaWykres500;
            for (int i = 0; i < punktyDoPrzesuniec.Count; i++)
            {

                double tempy = punktyDoPrzesuniec.ElementAt(i).Y;
                double tempx = punktyDoPrzesuniec.ElementAt(i).X;
                tempy += 1 * skala;
                Point temppunkt = new Point(tempx, tempy);
                tymczasem.Add(temppunkt);
            }
            if (skala == 100)
            {
                punktyNaWykres100 = new PointCollection();
                punktyNaWykres100 = tymczasem;
            }
            else if (skala == 200)
            {
                punktyNaWykres200 = new PointCollection();
                punktyNaWykres200 = tymczasem;
            }
            else if (skala == 500)
            {
                punktyNaWykres500 = new PointCollection();
                punktyNaWykres500 = tymczasem;
            }
            RysujUklad(canvas1.Height, canvas1.Width, skala, przesunieciePoX, przesunieciePoY);
        
         }

        private void lewo_Click(object sender, RoutedEventArgs e)
        {
            przesunieciePoX -= 1 * skala;
            PointCollection tymczasem = new PointCollection();
            PointCollection punktyDoPrzesuniec = new PointCollection();
            if (skala == 100) punktyDoPrzesuniec = punktyNaWykres100;
            else if (skala == 200) punktyDoPrzesuniec = punktyNaWykres200;
            else if (skala == 500) punktyDoPrzesuniec = punktyNaWykres500;
            for (int i = 0; i < punktyDoPrzesuniec.Count; i++)
            {

                double tempy = punktyDoPrzesuniec.ElementAt(i).Y;
                double tempx = punktyDoPrzesuniec.ElementAt(i).X;
                tempx -= 1 * skala;
                Point temppunkt = new Point(tempx, tempy);
                tymczasem.Add(temppunkt);
            }

            if (skala == 100)
            {
                punktyNaWykres100 = new PointCollection();
                punktyNaWykres100 = tymczasem;
            }
            else if (skala == 200)
            {
                punktyNaWykres200 = new PointCollection();
                punktyNaWykres200 = tymczasem;
            }
            else if (skala == 500)
            {
                punktyNaWykres500 = new PointCollection();
                punktyNaWykres500 = tymczasem;
            }
            RysujUklad(canvas1.Height, canvas1.Width, skala, przesunieciePoX, przesunieciePoY);
        }

        private void prawo_Click(object sender, RoutedEventArgs e)
        {
            przesunieciePoX += 1 * skala;
            PointCollection tymczasem = new PointCollection();
            PointCollection punktyDoPrzesuniec = new PointCollection();
            if (skala == 100) punktyDoPrzesuniec = punktyNaWykres100;
            else if (skala == 200) punktyDoPrzesuniec = punktyNaWykres200;
            else if (skala == 500) punktyDoPrzesuniec = punktyNaWykres500;
            for (int i = 0; i < punktyDoPrzesuniec.Count; i++)
            {

                double tempy = punktyDoPrzesuniec.ElementAt(i).Y;
                double tempx = punktyDoPrzesuniec.ElementAt(i).X;
                tempx += 1 * skala;
                Point temppunkt = new Point(tempx, tempy);
                tymczasem.Add(temppunkt);
            }
            if (skala == 100)
            {
                punktyNaWykres100 = new PointCollection();
                punktyNaWykres100 = tymczasem;
            }
            else if (skala == 200)
            {
                punktyNaWykres200 = new PointCollection();
                punktyNaWykres200 = tymczasem;
            }
            else if (skala == 500)
            {
                punktyNaWykres500 = new PointCollection();
                punktyNaWykres500 = tymczasem;
            }

            RysujUklad(canvas1.Height, canvas1.Width, skala, przesunieciePoX, przesunieciePoY);
        }
    }
}
