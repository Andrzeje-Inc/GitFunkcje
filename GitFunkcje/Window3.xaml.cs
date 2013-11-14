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
namespace GitFunkcje
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Page
    {
        public Window3()
        {
            InitializeComponent();
        }
        public string EncodePassword(string originalPassword)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;


            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);


            return BitConverter.ToString(encodedBytes);
        }
        public void click1(object sender, RoutedEventArgs e)
        {


            string login = texbox1.Text;
            string pw2 = pwbox.Password;
            string pw = EncodePassword(pwbox.Password);
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
                command.CommandText = "SELECT MAX(idnau) FROM [dbo].[nau]";
                pom3 = command.ExecuteScalar().ToString();
                con.Close();

                if (login == null)
                {
                    MessageBox.Show("Wpisz login");}
                    else if(pw == null)
                    {
                        MessageBox.Show("Wpisz haslo");}
                    else{

                        idmax = Convert.ToInt32(pom3);

                        int pom2 = idmax + 1;
                        con.Open();
                        command.CommandText = "INSERT INTO [dbo].[nau](idnau, loginn, password) VALUES (" + pom2 + ", '" + login + "', '" + pw + "');";
                        command.ExecuteNonQuery();
                        con.Close();

                        App.GlowneOkno.Content = new Window1();
                    }
                    
                
                
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Height = 350;
            this.Width = 350;
        }
    }
}
