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
using System.Windows.Ink;
using System.Data;
using System.Text.RegularExpressions;

namespace GitFunkcje
{
    /// <summary>
    /// Interaction logic for noweRysowanie.xaml
    /// </summary>
    public partial class noweRysowanie : Page
    {
        int skala = 100;
        int ziarnistosc = 2;
        bool siatka = false;
        StrokeCollection sc = new StrokeCollection();
        StrokeCollection wykresPrawidlowy = new StrokeCollection();
        PointCollection punktyDoFunkcji = new PointCollection();
        public string Sprawdz(string wyrazenie,string zmienna)
        {
            //PODSTAWIANIE X
            Regex wyrX = new Regex("x");
            wyrazenie = wyrX.Replace(wyrazenie, zmienna); //Zamiana x w wyrazeniu na dany punkt z wykresu


            Regex potega = new Regex(@"(\-?[0-9]+\,?[0-9]*\^[0-9]+\,?[0-9]*)");
            Regex pierwiastek = new Regex(@"(sqrt\(\-?[0-9]+\,?[0-9]*(\;[0-9]+\,?[0-9]*)?\))");
            Regex prostenawiasy = new Regex(@"((\+|\-|\*|\/|\(|\||\))\(\d+\,?\d*\)|(\(|\||\))\(\-\d+\,?\d*\))");
            Regex nawiasy = new Regex(@"\(\-?[0-9]+\,?[0-9]*((\+|\-|\/|\*)\d+\,?\d*)+\)");
            Regex liczbyWAbs = new Regex(@"\|[0-9-+*/(),]+\|");
            Regex abs = new Regex(@"\|\-?\d+\,?\d*\|");
            Regex log = new Regex(@"log\(\-?\d+\,?\d*(\;\-?\d+\,?\d*)?\)");
            Regex trygonometria = new Regex(@"(sin\(\-?\d+\,?\d*\))|(cos\(\-?\d+\,?\d*\))|(tag\(\-?\d+\,?\d*\))|(ctag\(\-?\d+\,?\d*\))");
            bool stop = false;
            while ((liczbyWAbs.IsMatch(wyrazenie) == true || potega.IsMatch(wyrazenie) == true || pierwiastek.IsMatch(wyrazenie) == true || prostenawiasy.IsMatch(wyrazenie) == true || nawiasy.IsMatch(wyrazenie) == true || abs.IsMatch(wyrazenie) == true || log.IsMatch(wyrazenie) == true || trygonometria.IsMatch(wyrazenie))&&stop==false)
            {
                // PROSTE NAWIASY
                MatchCollection matches;
                matches = prostenawiasy.Matches(wyrazenie);
                foreach (Match m in matches)
                {
                    string t = m.ToString().Substring(2, m.ToString().Length - 3);
                    string w = m.ToString().Substring(1, m.ToString().Length - 1);
                    m.ToString().Replace(",", ".");
                    wyrazenie = wyrazenie.Replace(w.ToString(), t.ToString());

                }
                //NAWIASY
                matches = nawiasy.Matches(wyrazenie);
                foreach (Match m in matches)
                {
                    DataTable dt = new DataTable();
                    string t = m.ToString().Replace(",", ".");
                    var t0 = dt.Compute(t, "");
                    wyrazenie = wyrazenie.Replace(m.ToString(), "(" + t0.ToString() + ")");
                }
                //Liczby w ABS
                matches = liczbyWAbs.Matches(wyrazenie);
                foreach (Match m in matches)
                {
                    DataTable dt = new DataTable();
                    string t = m.ToString().Replace(",", ".");
                    string d = t.Substring(1, m.ToString().Length - 2);
                    var t0 = dt.Compute(d, "");
                    wyrazenie = wyrazenie.Replace(m.ToString().Substring(1, m.ToString().Length - 2), t0.ToString());
                }
                // POTEGI
                matches = potega.Matches(wyrazenie);
                foreach (Match m in matches)
                {
                    double pot = 0.0;
                    string[] zmienne = m.ToString().Split("^".ToCharArray());
                    try
                    {
                        pot = Math.Pow(Double.Parse(zmienne[0]), Double.Parse(zmienne[1]));
                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(e.ToString());
                    }
                    wyrazenie = wyrazenie.Replace(m.ToString(), "(" + pot.ToString() + ")");
                }

                //WARTOSC BEZWZGLEDNA
                matches = abs.Matches(wyrazenie);
                foreach (Match m in matches)
                {
                    string a = m.ToString().Substring(1, m.ToString().Length - 2);
                    double w = Math.Abs(Double.Parse(a));
                    wyrazenie = wyrazenie.Replace(m.ToString(), "(" + w.ToString() + ")");
                }
                //PIERWIASTKI
                matches = pierwiastek.Matches(wyrazenie);
                foreach (Match m in matches)
                {
                    string a = m.ToString().Substring(5, m.ToString().Length - 6);
                    string[] zmienne = a.Split(";".ToCharArray());
                    double w = 0.0;
                    switch (zmienne.Count())
                    {
                        case 1:
                            w = Math.Sqrt(Double.Parse(zmienne[0]));
                            break;
                        case 2:
                            w = Math.Pow(Double.Parse(zmienne[0]), 1.0 / Double.Parse(zmienne[1]));
                            break;
                    }
                    wyrazenie = wyrazenie.Replace(m.ToString(), "(" + w.ToString() + ")");
                }
                // TRYGONOMETRIA
                matches = trygonometria.Matches(wyrazenie);
                foreach (Match m in matches)
                {
                    double w = 0.0;
                    string a, b, c;
                    a = m.ToString();
                    b = a.Substring(0, 3);
                    if (a.ElementAt(3) == 'g')
                        c = a.Substring(5, a.Length - 6);
                    else c = a.Substring(4, a.Length - 5);

                    switch (b)
                    {
                        case "sin":
                            w = Math.Sin(Double.Parse(c));
                            break;
                        case "cos":
                            w = Math.Cos(Double.Parse(c));
                            break;
                        //case "tan":
                        //    if (Double.Parse(c) % (Math.PI / 2) == 0 || double.Parse(c)==0)
                        //    {
                        //    w = 0;
                        //    }
                        //    else
                        //    w = Math.Tan(Double.Parse(c));
                        //    break;
                        //case "cta":
                        //    double ctg = Math.Tan(Double.Parse(c));
                        //    if (Double.Parse(c)%Math.PI==0)
                        //        w = 0;
                        //    else 
                        //        w = 1.0/ctg;
                        //    break;
                    }
                    wyrazenie = wyrazenie.Replace(m.ToString(), "(" + w.ToString().Replace(",", ".") + ")");
                }
                //LOGARYTMY
                matches = log.Matches(wyrazenie);
                foreach (Match m in matches)
                {
                    string a = m.ToString().Substring(4, m.ToString().Length - 5);
                    string[] zmienne = a.Split(";".ToCharArray());
                    double w = 0.0;
                    double aa;
                    switch (zmienne.Count())
                    {
                        case 1:
                            aa = Double.Parse(zmienne[0]);
                            if (aa <= 0)
                            {
                                wyrazenie = "UJ";
                                stop = true;
                                break;
                            }
                            w = Math.Log10(aa);
                            break;
                        case 2:
                            w = Math.Log(Double.Parse(zmienne[0]), Double.Parse(zmienne[1]));
                            break;
                    }
                    wyrazenie = wyrazenie.Replace(m.ToString(), "(" + w.ToString() + ")");
                }

            }
            wyrazenie = wyrazenie.Replace(",", ".");
            DataTable blablalba = new DataTable();
            try
            {
                var v = blablalba.Compute(wyrazenie, "");
            return v.ToString();
            }
            catch (Exception e)
            {
                return wyrazenie;
            }
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
        public noweRysowanie()
        {
            InitializeComponent();
            wybwzor.Text = "f(x)=" + App.globalfunk;
            RysujUklad(Canva.Height,Canva.Width);
        }

        private void Canva_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            sc = new StrokeCollection();
            punktyDoFunkcji.Clear();
            sc.Add(Canva.Strokes);
            //Punkty ze strokow dodawane do punktow do obliczania poprawnosci funkcji

            foreach (Stroke bStroke in sc)
            {
                foreach (Point p in bStroke.StylusPoints)
                {
                    Point temp = new Point(p.X / skala  - (Canva.ActualWidth / 2.0) / skala, -(p.Y / skala  - (Canva.ActualHeight / 2.0) / skala));
                    punktyDoFunkcji.Add(temp);
                }
            }
            StylusPointCollection poprwanyspc = new StylusPointCollection();
            for (int i = 0; i < Canva.Width; i += 10)
            {

                StylusPoint temp = new StylusPoint();
                string wyn = Sprawdz(App.globalfunk, ((i - (Canva.ActualWidth / 2.0)) / skala).ToString());
                if (wyn == "UJ") continue;
                double y = Double.Parse(wyn);
                temp.X = i;
                temp.Y=Canva.ActualHeight/2.0-y*skala;
                poprwanyspc.Add(temp);
            }
            Stroke poprawnyStroke = new Stroke(poprwanyspc);
            wykresPrawidlowy.Add(poprawnyStroke);
        }

        private void Canva_MouseMove(object sender, MouseEventArgs e)
        {
            //Aktualna pozycja z wykresu
            //X.Text = (e.GetPosition(Canva).X / skala  - (Canva.ActualWidth / 2.0) / skala).ToString();
            //Y.Text = (-(e.GetPosition(Canva).Y / skala -(Canva.ActualHeight / 2.0) / skala)).ToString();
        }
        //Czyszczenie wykresu
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Canva.Strokes.Clear();
            if (spr.IsEnabled == false) spr.IsEnabled = true;
        }
        //Sprawdzanie wykresu
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            int poprawne=0;
            foreach (Point p in punktyDoFunkcji)
            {
                double x = Double.Parse(Sprawdz(App.globalfunk, p.X.ToString()));
                if (x <= p.Y + 0.44 && x >= p.Y - 0.4)
                {
                    poprawne++;
                }
            }
            Canva.Strokes.Add(wykresPrawidlowy);
            
            if (punktyDoFunkcji.Count > 0)
            {
                double procPoprawnosci = (poprawne * 100) / punktyDoFunkcji.Count;
                MessageBox.Show("Wzor jest zgodny z poprawnym wykresem w " + procPoprawnosci + "%.");
            }
            else MessageBox.Show("Nie narysowano jeszcze wykresu");
            spr.IsEnabled = false;
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

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            //scroll.ScrollToVerticalOffset(Canva.Width / 2.0);
            //scroll.ScrollToHorizontalOffset(Canva.Height / 2.0);
            //scroll.HorizontalContentAlignment = HorizontalAlignment.Center;
            //scroll.VerticalContentAlignment = VerticalAlignment.Center;
            //if (slider.Value < 1.5) { slider.Value = 1; ziarnistosc = 2; }
            //else if (slider.Value >= 1.5 && slider.Value < 2.5) { slider.Value = 3; ziarnistosc = 2; }
            //else if (slider.Value >= 2.5) { slider.Value = 3; ziarnistosc = 4; }
            //RysujUklad(Canva.Height, Canva.Width); 
        }

        private void koniec_Click(object sender, RoutedEventArgs e)
        {
            App.Tablica.Content = new Wygaszacz();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //scroll.HorizontalOffset = Canva.ActualWidth / 2;
            //scroll.
            //this.scroll.se
        }
    }
}
