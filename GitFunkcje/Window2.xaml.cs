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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Page
    {
        public Window2()
        {
            InitializeComponent();
        }
        public void clickkk(object sender, RoutedEventArgs e)
        {
            string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string klasa = texbox41.Text;
                string liter = texbox51.Text;
                CmdString = "SELECT * FROM [dbo].[Table] WHERE [kklasa]=" + klasa + "and [lklasa]='" + liter + "';";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("[dbo].[Table]");

                sda.Fill(dt);
                lista.ItemsSource = dt.DefaultView;

            }
        }


        public void click1(object sender, RoutedEventArgs e)
        {

            int klasa = int.Parse(texbox4.Text);

            string login = texbox3.Text;
            string password = texbox2.Text;
            string liter = texbox5.Text;
            string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                // var imiona = "SELECT * FROM [dbo].[Table]";
                //foreach(
                SqlCommand command = con.CreateCommand();
                // SqlTransaction transaction;

                // Start a local transaction.
                // transaction = con.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection 
                // to Command object for a pending local transaction
                command.Connection = con;
                //  command.Transaction = transaction;
                int idmax;
                string pom3;
                con.Open();
                command.CommandText = "SELECT MAX([id]) FROM [dbo].[Table]";
                pom3 = command.ExecuteScalar().ToString();
                con.Close();

                idmax = Convert.ToInt32(pom3);
                // idmax = 0;
                int pom2 = idmax + 1;

                con.Open();
                command.CommandText = "INSERT INTO [dbo].[Table]([id], [imie], [nazwisko], [kklasa], [lklasa]) VALUES (" + pom2 + ", '" + login + "', '" + password + "', '" + klasa + "','" + liter + "');";
                command.ExecuteNonQuery();
                con.Close();
               

            }



            string result = "Zarejestrowano";
            MessageBox.Show(result);
        }
        public void click2(object sender, RoutedEventArgs e)
        {


            int id;
            id = Convert.ToInt32(pom.Text);
            id = int.Parse(pom.Text);


            string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                // var imiona = "SELECT * FROM [dbo].[Table]";
                //foreach(
                SqlCommand command = con.CreateCommand();
                // SqlTransaction transaction;

                // Start a local transaction.
                // transaction = con.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection 
                // to Command object for a pending local transaction
                command.Connection = con;
                //  command.Transaction = transaction;
                con.Open();
                command.CommandText = "DELETE FROM [Table] WHERE [id] = " + id + ";";
                command.ExecuteNonQuery();
                con.Close();
                this.Content = new Window1();

            }



            string result = "Zarejestrowano";
            MessageBox.Show(result);
        }
    }
}
