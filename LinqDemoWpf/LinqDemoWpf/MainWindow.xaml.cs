using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace LinqDemoWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] numerot = { 5, 13, 8, 4, 10, 1, 9, 3, 5, 11, 6 };
            /*suurempi kuin 5, tulokset lajiteltuna vrt. "SELECT luku FROM numerot WHERE luku > 5 ORDER BY luku” */
            var suuretNumerot = from n in numerot
                                where n > 5
                                orderby n
                                select n;
            //var suuretNumerot2 = numerot.Where(n => n > 5).OrderBy(n => n); 
            foreach (var numero in suuretNumerot)
            {
                MessageBox.Show(numero.ToString());
            }

        }

        private void BtnHaeAsiakkaat_Click(object sender, RoutedEventArgs e)
        {
            string connStr = "Server=DESKTOP-G0R5GEC\\SQLEXJOHANNAME;Database=Northwind;Trusted_Connection=True;";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string sql = "SELECT * FROM Customers WHERE Country = 'Finland'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string companyName = reader.GetString(1);
                string contactName = reader.GetString(2);
                MessageBox.Show("Löytyi rivi: " + companyName + " " + contactName);
            }
            // resurssien vapautus 
            reader.Close();
            cmd.Dispose();
            conn.Dispose();

        }

        private void BtnGetCustomers_Click(object sender, RoutedEventArgs e)
        {
            northwindEntities entities = new northwindEntities();

            var finnishCustomers = from c in entities.Customers
                                   where c.Country == "Finland"
                                   select c;
            foreach (var cust in finnishCustomers)
            {
                MessageBox.Show(cust.CompanyName);
            }
        }

        private void BtnLinqOliot_Click(object sender, RoutedEventArgs e)
        {
            List<Henkilo> henkilot = new List<Henkilo>()
            {
                new Henkilo()
                {

                    Nimi= "Teppo Testaaja",
                    Osoite = "Testietie 1",
                    Ika = 34
                },
                new Henkilo()
                {

                    Nimi= "Maija Mallikas",
                    Osoite = "Mallitie 5",
                    Ika = 25
                },
                new Henkilo()
                {

                    Nimi= "Pirkko Piste",
                    Osoite = "Pistetie 10",
                    Ika = 40
                }

            };

            var henkiloLista = from hlo in henkilot
                               orderby hlo.Ika
                               select hlo;

            foreach (var henk in henkiloLista)
            {
                MessageBox.Show(henk.Nimi);
            }
        }
    }
}
