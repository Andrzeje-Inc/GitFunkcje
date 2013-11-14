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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Window1 : Page
    {
        public Window1()
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




        private void log2(object sender, RoutedEventArgs e)
        {
            string ConString = ConfigurationManager.ConnectionStrings["WpfApplication3.Properties.Settings.ucznConnectionString"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                try
                {
                    SqlCommand command = con.CreateCommand();
                    command.Connection = con;
                    con.Open();
                    string pom3;

                    string login = pw.Text;
                    string pw2 = EncodePassword(pwbox.Password);
                    command.CommandText = "SELECT password FROM [dbo].[nau] where loginn='" + login + "'";
                    pom3 = command.ExecuteScalar().ToString();

                    if (pw2 != pom3)
                    {
                        MessageBox.Show("Zly Login lub haslo");
                    }
                    else
                    {
                        App.GlowneOkno.Content = new wyborklas();
                    }
                    con.Close();
                }

                catch (NullReferenceException)
                {
                    MessageBox.Show("Zly Login lub haslo");
                }

            }
        }

        private void rej(object sender, RoutedEventArgs e)
        {
           // Window wydbaz = new wydbaz();
            //wydbaz.Show();
           App.GlowneOkno.Content = new Window3();
            
        }
        private void uklad(object sender, RoutedEventArgs e)
        {
            //App.Tablica.Content = new Rysowanie();
            App.Tablica.Content = new Uklad();
        }

    }
}

