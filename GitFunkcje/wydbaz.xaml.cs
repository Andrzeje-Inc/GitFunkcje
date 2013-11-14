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
    /// Interaction logic for wydbaz.xaml
    /// </summary>
    public partial class wydbaz : Window
    {
        public wydbaz()
        {
            InitializeComponent();
            FillDataGrid();
        }
        public void FillDataGrid()
        {
            string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT * FROM [dbo].[Table]";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("[dbo].[Table]");

                sda.Fill(dt);
                SqlCommand command = con.CreateCommand();
                uczniowie.ItemsSource = dt.DefaultView;
            }
                 }

        public void click2(object sender, RoutedEventArgs e)
        {

            string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand command = con.CreateCommand();
                    string k = klas.Text;
                    string l = lit.Text;
                    command.Connection = con;
                    //  command.Transaction = transaction;
                    con.Open();
                    command.CommandText = "SELECT [nazwisko] FROM [dbo].[Table] WHERE kklasa = " + k + " AND lklasa='" + l + "';";
                    string c = command.ExecuteScalar().ToString();
                    command.CommandText = "DELETE FROM [dbo].[Table] WHERE kklasa = " + k + " AND lklasa='" + l + "';";
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Nie ma takiej klasy");
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Niepoprawne dane");
                }
                FillDataGrid();
               

            }



            
            
        }
        public void click3(object sender, RoutedEventArgs e)
        {

            string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand command = con.CreateCommand();
                    string k = klas2.Text;
                    string l = lit2.Text;
                    command.Connection = con;
                    con.Open();
                    command.CommandText = "SELECT [nazwisko] FROM [dbo].[Table] WHERE kklasa = " + k + " AND lklasa='" + l + "';";
                    string c = command.ExecuteScalar().ToString();
                    command.CommandText = "DELETE FROM [dbo].[Table] WHERE imie = '" + k + "' AND nazwisko='" + l + "';";
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Nie ma takiego ucznia");
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Niepoprawne dane");
                }
                FillDataGrid();


            }



            
        }
        public void dodaj(object sender, RoutedEventArgs e)
        {
            try
            {
                int kklasa = int.Parse(texbox4.Text);
                        string login = texbox3.Text;
            string password = texbox2.Text;
            string lliter = texbox5.Text;
            string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                // var imiona = "SELECT * FROM [dbo].[Table]";
                //foreach(
                SqlCommand command = con.CreateCommand();
                command.Connection = con;

                int idmax;
                string pom3;
                con.Open();
                command.CommandText = "SELECT MAX([id]) FROM [dbo].[Table]";
                pom3 = command.ExecuteScalar().ToString();
                con.Close();

                idmax = Convert.ToInt32(pom3);
              //  idmax = 0;
               int pom2 = idmax + 1;
                try
                {
                    con.Open();
                    command.CommandText = "INSERT INTO [dbo].[Table]([id], [imie], [nazwisko], [kklasa], [lklasa]) VALUES (" + pom2 + ", '" + login + "', '" + password + "', '" + kklasa + "','" + lliter + "');";
                    command.ExecuteNonQuery();
                    con.Close();
                    FillDataGrid();
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Niepoprawne dane");
                }
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Niepoprawne dane");
            }
            
        
    



           
        }
    }
}
