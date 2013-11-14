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
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace GitFunkcje
{
    /// <summary>
    /// Interaction logic for wyborklas.xaml
    /// </summary>
    public partial class wyborklas : Page
    {
        public wyborklas()
        {
            InitializeComponent();
        }
        public void clickkk(object sender, RoutedEventArgs e)
        {
           {
                App.klasa = Texbox1.Text;
               App.liter = texbox2.Text;

               try
               {
                   string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
                   string CmdString = string.Empty;
                   using (SqlConnection con = new SqlConnection(ConString))
                   {
                       SqlCommand command = con.CreateCommand();
                       command.Connection = con;
                       con.Open();
                       command.CommandText = "SELECT [nazwisko] FROM [dbo].[Table] WHERE [kklasa]=" + App.klasa + "and [lklasa]='" + App.liter + "';";
                       string c = command.ExecuteScalar().ToString();

                       con.Close();
                       if (c != null)
                       {
                           App.GlowneOkno.Content = new Zarzadzanie();
                       }
                       else
                           App.GlowneOkno.Content = new wyborklas();

                   }
               }
               catch (System.Data.SqlClient.SqlException)
               {
                   MessageBox.Show("Niepoprawne Dane");
               }

               catch (NullReferenceException)
               {
                   MessageBox.Show("Nie ma takiej klasy. Wybierz inna.");

               }
               //App.GlowneOkno.Content = new Zarzadzanie();
            }
        }
    }
}
