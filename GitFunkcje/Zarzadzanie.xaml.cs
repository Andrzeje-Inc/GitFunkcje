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
using System.Security.Cryptography;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;


namespace GitFunkcje
{
    /// <summary>
    /// Interaction logic for Zarzadzanie.xaml
    /// </summary>
    public partial class Zarzadzanie : Page
    {
        public Zarzadzanie()
        {
            InitializeComponent();
            uzupelnij();
            uzupelnij2();

        }

        private void uzupelnij()
        {
            string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {

                //CmdString = "SELECT * FROM [dbo].[Table] WHERE [kklasa]=" + App.klasa + "and [lklasa]='" + App.liter + "';";
                CmdString = "SELECT uc.[id],uc.[imie],uc.[nazwisko],oc.[ocena] FROM (SELECT * FROM [dbo].[Table] WHERE [kklasa]=" + App.klasa + "and [lklasa]='" + App.liter + "')  AS uc INNER JOIN [dbo].[oceny] AS oc ON uc.[id] = oc.[iducznia]";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("[dbo].[Table]");

                sda.Fill(dt);
                uczniowie.ItemsSource = dt.DefaultView;

            }
        }
        private void uzupelnij2()
        {
            string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {

                CmdString = "SELECT * FROM [dbo].[Table] WHERE [kklasa]=" + App.klasa + "and [lklasa]='" + App.liter + "';";
                //CmdString = "SELECT uc.[id],uc.[imie],uc.[nazwisko],oc.[ocena] FROM (SELECT * FROM [dbo].[Table] WHERE [kklasa]=" + App.klasa + "and [lklasa]='" + App.liter + "')  AS uc INNER JOIN [dbo].[oceny] AS oc ON uc.[id] = oc.[iducznia]";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("[dbo].[Table]");

                sda.Fill(dt);
                uczniowie2.ItemsSource = dt.DefaultView;

            }
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            App.Tablica.Content = new Quiz();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

            Random gen = new Random();
            {
                string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
                string CmdString = string.Empty;
                using (SqlConnection con = new SqlConnection(ConString))

                {
                    string ConString2 = ConfigurationManager.ConnectionStrings["GitFunkcje.Properties.Settings.funkcjeConnectionString"].ConnectionString;
                    string CmdString2 = string.Empty;
                    SqlConnection con2 = new SqlConnection(ConString2);
                    SqlCommand command = con.CreateCommand();
                    command.Connection = con;

                    string pom3;
                    con.Open();
                    command.CommandText = "SELECT MAX([id]) FROM [dbo].[Table] WHERE lklasa='" + App.liter + "'";
                    pom3 = command.ExecuteScalar().ToString();
                    con.Close();
                    App.wylosowanyUczen = gen.Next(1, Int32.Parse(pom3) + 1);
                    SqlCommand command2 = con2.CreateCommand();
                    command2.Connection = con2;
                    int pom4;
                    con2.Open();
                    command2.CommandText = "SELECT MAX([Idfunk]) FROM [dbo].[funk]";
                    pom3 = command2.ExecuteScalar().ToString();
                    con2.Close();
                    pom4 = gen.Next(1, Int32.Parse(pom3) + 1);
                    MessageBoxResult result = MessageBox.Show("Uczen o numerze " + App.wylosowanyUczen + " zostal wylosowany do funkcji numer "+pom4+".\nCzy chcesz, aby dany uczen odpowiadał?", "Wybor ucznia", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        con2.Open();
                        command2.CommandText = "SELECT [funkcja] FROM [dbo].[funk] WHERE [Idfunk]=" + pom4 + "";
                        App.globalfunk = command2.ExecuteScalar().ToString();
                        con2.Close();
                        App.Tablica.Content = new noweRysowanie();
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        this.MenuItem_Click_2(sender, e);
                    }
                    else
                    {
                    }
                }
            }
        }

       


        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            App.Tablica.Content = new Uklad();
        }

        private void baza(object sender, RoutedEventArgs e)
        {
            Window wydbaz = new wydbaz();
            wydbaz.Show();
        }
        private void funkcje(object sender, RoutedEventArgs e)
        {
            Window funkcje = new funkcje();
            funkcje.Show();
        }
        private void ocen(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
                    string CmdString = string.Empty;
                    using (SqlConnection con = new SqlConnection(ConString))
                    {
                        string iducz = texbox1.Text;
                        string ocena = texbox2.Text;
                        int ocena2 = Convert.ToInt32(ocena);
                        if (ocena2 < 7 && ocena2 > 0)
                        {
                            
                            SqlCommand command = con.CreateCommand();
                            command.Connection = con;
                            int idmax;
                            string pom3;
                            con.Open();
                            command.CommandText = "SELECT MAX([numeroceny]) FROM [dbo].[oceny]";
                            pom3 = command.ExecuteScalar().ToString();
                            con.Close();

                            idmax = Convert.ToInt32(pom3);
                            // idmax = 0;
                            int pom2 = idmax + 1;
                            con.Open();
                            command.CommandText = "INSERT INTO [dbo].[oceny]([numeroceny],[iducznia],[ocena]) VALUES(" + pom2 + ",'" + iducz + "','" + ocena2 + "')";
                            command.ExecuteNonQuery();

                            con.Close();
                            uzupelnij();
                        }
                        else
                        {
                            MessageBox.Show("Ocena z przedzialu 1-6");
                        }
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Niepoprawne Dane");
                }
                 catch (System.Data.SqlClient.SqlException)
                  {
                      MessageBox.Show("Niepoprawne Dane");
                  }
                  
                catch (NullReferenceException)
                {
                    MessageBox.Show("Nie ma takiej klasy. Wybierz inna.");
                }

                }
            }

        }
    }

