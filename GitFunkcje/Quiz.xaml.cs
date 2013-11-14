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
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Page
    {
        public struct Pytania
        {
            public string tresc;
            public string odpA;
            public string odpB;
            public string odpC;
            public string odpD;
            public string poprawna;
        }
        int nrPytania = 0;
        int pkt = 0;
        Pytania[] pytDoOdp = new Pytania[10];
        int[] nrWylosowane = new int[10];
        Pytania[] pytanka = new Pytania[]
        {
        new Pytania(){tresc="Prosta l ma rownanie y=2x-11. Wskaz rownanie prostej rownoleglej do prostej l.",odpA="y=2x",odpB="y=-2x",odpC="y=-1x/2",odpD="y=1x/2",poprawna="2x" },
        new Pytania(){tresc="Wskaz funkcje liniowa o tym samym miejscu zerowym co funkcja y=-1x/2+2/3",odpA="y=3x+4",odpB="y=3x-4",odpC="y=4x-3",odpD="y=x+4/3",poprawna="3x-4" },
        new Pytania(){tresc="Dana jest funkcja f(x)=y=-x*x*x-x*x wówczas",odpA="f(-3)=-36",odpB="f(-3)=-18",odpC="f(-3)=18",odpD="f(-3)=36",poprawna="f(-3)=18" },
        new Pytania(){tresc="funkcja liniowa f(x)=(m-2)x-11 jest rosnaca dla",odpA="m>2",odpB="m>0",odpC="m<13",odpD="m<11",poprawna="m>2" },
        new Pytania(){tresc="Do wykresu funkcji liniowej f naleza punkty A=(1,2) i B (-2,5). Funkcja f ma wzor",odpA="f(x)=x+3",odpB="f(x)=x-3",odpC="f(x)=-x-3",odpD="f(x)=-x+3",poprawna="f(x)=-x+3" },
        new Pytania(){tresc="Delta jest rowna 0 dla trojmianu kwadratowego:",odpA="y=x*x+9",odpB="y=x*x-9",odpC="y=x*x-6x+9",odpD="y=x*x+9",poprawna="y=x*x-6x+9" },
        new Pytania(){tresc="Punkt P jest punktem przeciecia sie wykresow funkcji y=-2x+4 i y=-x-2. Punkt P lezy w ukladzie wspolrzednych w cwiartce",odpA="1",odpB="2",odpC="3",odpD="4",poprawna="4" },
        new Pytania(){tresc="Dziedzina funkcji f, okreslonej wzorem f(x) (x-5)/(x*x+4)",odpA="R\\{-4,4}",odpB="R\\{-4}",odpC="R",odpD="R\\{5}",poprawna="R" },
        new Pytania(){tresc="Funkcje f(x)=3x-1 i g(x)=2x+5 przyjmuja rowna wartosc dla",odpA="x=1",odpB="x=4",odpC="x=5",odpD="x=6",poprawna="x=6" },
        new Pytania(){tresc="Prosta o rownaniu y=-4x+(2x-7) przechodzi przez punkt A=(2,-1)",odpA="m=7",odpB="m=2.5",odpC="m=-0.5",odpD="m=-17",poprawna="m=7"},
        new Pytania(){tresc="Prosta o rownaniu y=5x-m+3 przechodzi przez punkt A=(4,3), wtedy:",odpA="m=20",odpB="m=14",odpC="m=3",odpD="m=0",poprawna="m=20"},
        new Pytania(){tresc="Liczba 1 jest miejscem zerowym funkcji liniowej f(x)=(2-m)x+1. Wynika stąd, że:",odpA="m=0",odpB="m=1",odpC="m=2",odpD="m=3",poprawna="m=3"},
        new Pytania(){tresc="Liczby x1 i x2 sa pierwistkami rownania x*x+10x-24=0 i x1<x2. Oblicz 2x1+x2",odpA="m=-22",odpB="-17",odpC="m=-8",odpD="13",poprawna="-22"},
        new Pytania(){tresc="Wskaz m, dla ktorego funkcja liniowa okreslona wzorem f(x)=(m-1)x+3 jest stala.",odpA="m=1",odpB="m=2",odpC="m=3",odpD="m=-1",poprawna="m=1"},
        new Pytania(){tresc="Wskaz rownanie prostej, ktora jest osia symetrii paraboli o rownaniu y=x*x-4x+2010",odpA="m=4",odpB="m=-4",odpC="m=2",odpD="m=-2",poprawna="m=2"},
        new Pytania(){tresc="Wskaz rownanie osi symetrii paraboli okreslonej rownaniem y=-x*x+4x-11",odpA="x=-4",odpB="x=-2",odpC="x=2",odpD="x=4",poprawna="x=2"},
        new Pytania(){tresc="Do wykresu funkcji f(x)=x*x+x-2 nalezy punkt",odpA="(-1,-4)",odpB="(-1,1)",odpC="(-1,-1)",odpD="(-1,-2)",poprawna="(-1,-2)"},
        new Pytania(){tresc="Wykres funkcji f(x)=-3/x znajduje sie w cwiartkach",odpA="II i IV",odpB="II i III",odpC="I i III",odpD="I i II",poprawna="II i IV"},
        new Pytania(){tresc="Prosta o rownaniu y=a ma jeden punkt wspolny z wykresem funkcji kwadratowej f(x)=-x*x+6x-10. Wynika stad, ze:",odpA="a=3",odpB="a=0",odpC="a=-1",odpD="a=-3",poprawna="-1"},
        new Pytania(){tresc="Liczba x=-7 jest miejscem zerowym funkcji liniowej f(x)=(4-a)x+7 dla",odpA="a=-7",odpB="a=2",odpC="a=3",odpD="a=-1",poprawna="a=2"},
        new Pytania(){tresc="Ile miejsc zerowych ma funkcja f(x)=-2*x*x+3x-2",odpA="0",odpB="1",odpC="2",odpD="3",poprawna="0"},
        new Pytania(){tresc="Ile miejsc zerowych ma funkcja przyjmujaca dla x =<3 wartosci y=x-4, a dla x>3 wartosci y=-x+4",odpA="0",odpB="1",odpC="2",odpD="3",poprawna="0"},
        new Pytania(){tresc="Punkt P=(a+1,2) nalezy do wykresu funkcji f(x)=4/x. Liczba a jest równa",odpA="0",odpB="-1",odpC="2",odpD="1",poprawna="1"},
        new Pytania(){tresc="Dane sa punkty A=(6,1) i B=(3,3). Wspolczynnik kierunkowy prostej AB jest rowny",odpA="-2/3",odpB="-3/2",odpC="3/2",odpD="2/3",poprawna="-2/3"},
        new Pytania(){tresc="Wzor funkcji, ktorej wykres powstaje przez przesuniecie wykresu funkcji f o 10 jednostek w dol, to:",odpA="y=f(x+10)",odpB="y=f(x)+10",odpC="f(x-10)",odpD="y=f(x)-10",poprawna="y=f(x)-10"},
        };
        string odpowiedz = "";

        public void wczytajPytanie(int nrPytania)
        {
            Pytanie.Text = pytDoOdp[nrPytania].tresc;
            A.Content = pytDoOdp[nrPytania].odpA;
            B.Content = pytDoOdp[nrPytania].odpB;
            C.Content = pytDoOdp[nrPytania].odpC;
            D.Content = pytDoOdp[nrPytania].odpD;
        }

        public void losujPyt()
        {
            Random gen = new Random();
            for (int i = 0; i < 10; )
            {
                int w = 0;
                w = gen.Next(0, 25);
                bool dostepny = true;
                for (int j = 0; j <= i; j++)
                {
                    if (nrWylosowane[j] == w)
                    {
                        dostepny = false;
                        break;
                    }
                    else continue;
                }
                if (dostepny)
                {
                    nrWylosowane[i] = w;
                    pytDoOdp[i] = pytanka[w];
                    i++;
                }
                else continue;

            }
        }
        public Quiz()
        {
            InitializeComponent();
            losujPyt();
            wczytajPytanie(nrPytania);
        }

        private void A_Checked(object sender, RoutedEventArgs e)
        {
            odpowiedz = "A";
            //odpowiedz = A;
            B.IsChecked = false;
            C.IsChecked = false;
            D.IsChecked = false;
        }

        private void B_Checked(object sender, RoutedEventArgs e)
        {
            odpowiedz = "B";
            A.IsChecked = false;
            C.IsChecked = false;
            D.IsChecked = false;
        }


        private void C_Checked(object sender, RoutedEventArgs e)
        {
            odpowiedz = "C";
            A.IsChecked = false;
            B.IsChecked = false;
            D.IsChecked = false;
        }

        private void D_Checked(object sender, RoutedEventArgs e)
        {
            odpowiedz = "D";
            A.IsChecked = false;
            B.IsChecked = false;
            C.IsChecked = false;
        }

        private void spr_Click(object sender, RoutedEventArgs e)
        {
            if (A.IsChecked == true || B.IsChecked == true || C.IsChecked == true || D.IsChecked == true)
            {

                switch (odpowiedz)
                {
                    case "A":
                        odpowiedz = pytDoOdp[nrPytania].odpA;
                        break;
                    case "B":
                        odpowiedz = pytDoOdp[nrPytania].odpB;
                        break;
                    case "C":
                        odpowiedz = pytDoOdp[nrPytania].odpC;
                        break;
                    case "D":
                        odpowiedz = pytDoOdp[nrPytania].odpD;
                        break;
                }
                if (pytDoOdp[nrPytania].poprawna == odpowiedz)
                {
                    MessageBox.Show("Brawo.\nOtrzymujesz punkt.");
                    pkt++;
                    punkty.Content = "Punkty " + pkt;
                    nrPytania++;
                    wczytajPytanie(nrPytania);
                }
                else
                {
                    MessageBox.Show("Niestety, ale to błędna odpowiedz.");
                    nrPytania++;
                    wczytajPytanie(nrPytania);
                }
            }
            else
            {
                MessageBox.Show("Zadna odpowiedz nie zostala zaznaczona.\nProsze o zaznaczenie jednej z odpowiedzi.", "Brak odpowiedzi");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Zdobyles " + pkt + " punktow");
            App.Tablica.Content = new Wygaszacz();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Thickness t;

            Pytanie.MaxWidth = this.ActualWidth * 0.4;
            Pytanie.Width = this.ActualWidth * 0.4;

            t = spr.Margin;
            t.Top = ActualHeight * 0.85;
            spr.Margin = t;

            t = A.Margin;
            t.Top = ActualHeight * 0.5;
            A.Margin = t;

            t = B.Margin;
            t.Top = ActualHeight * 0.5;
            B.Margin = t;

            t = C.Margin;
            t.Top = ActualHeight * 0.6;
            C.Margin = t;

            t = D.Margin;
            t.Top = ActualHeight * 0.6;
            D.Margin = t;
        }

    }
}
