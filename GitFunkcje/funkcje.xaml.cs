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
    /// Interaction logic for funkcje.xaml
    /// </summary>
    public partial class funkcje : Window
    {
        public funkcje()
        {
            InitializeComponent();
            FillDataGrid();
        }
        public void FillDataGrid()
        {
            string ConString = ConfigurationManager.ConnectionStrings["GitFunkcje.Properties.Settings.funkcjeConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                // var imiona = "SELECT * FROM [dbo].[Table]";
                //foreach(
                CmdString = "SELECT * FROM [dbo].[funk]";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("[dbo].[funk]");

                sda.Fill(dt);
                SqlCommand command = con.CreateCommand();
                funko.ItemsSource = dt.DefaultView;

            }

        }
        public void click1(object sender, RoutedEventArgs e)
        {

            try
            {
                string id = texbox4.Text;
                string funkcja = texbox2.Text;

                string ConString = ConfigurationManager.ConnectionStrings["GitFunkcje.Properties.Settings.funkcjeConnectionString"].ConnectionString;
                string CmdString = string.Empty;
                using (SqlConnection con = new SqlConnection(ConString))
                {
                    SqlCommand command = con.CreateCommand();
                    command.Connection = con;

                    con.Open();
                    command.CommandText = "INSERT INTO [dbo].[funk]([Idfunk], [funkcja]) VALUES (" + id + ", '" + funkcja + "')";
                    command.ExecuteNonQuery();
                    con.Close();
                    FillDataGrid();
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