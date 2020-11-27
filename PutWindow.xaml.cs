using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using CountryAPIWPF.Models;
using System.Text.RegularExpressions;

namespace CountryAPIWPF
{
    /// <summary>
    /// Interaction logic for PutWindow.xaml
    /// </summary>
    public partial class PutWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private static string url;
        //Country oldCountry;
        int oldId;
        string oldName;
        public static Country oldCountry = new Country();
        public PutWindow(String c, string u)
        {
            InitializeComponent();
            url = u;
            HttpResponseMessage response = client.GetAsync(url + "CountryInfo/"+c).Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            oldCountry = JsonSerializer.Deserialize<Country>(jsonResponse);
            oldId = oldCountry.id;
            oldName = oldCountry.countryName;
            nameLabel.Content = oldName;
            con.Text = oldCountry.continent;
            cap.Text = oldCountry.capitalCity;
            pop.Text = Convert.ToString(oldCountry.population);
            lang.Text = oldCountry.primaryLanguage;
            cur.Text = oldCountry.currency;
            val.Text = Convert.ToString(oldCountry.valueToUSD);
        }
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            


            Country objInsert = new Country();

            objInsert.id = oldId;
            objInsert.countryName = oldName;
            objInsert.continent = con.Text;
            objInsert.capitalCity = cap.Text;
            objInsert.population = Convert.ToDouble(pop.Text);
            objInsert.primaryLanguage = lang.Text;
            objInsert.currency = cur.Text;
            objInsert.valueToUSD = Convert.ToDouble(val.Text);

            string jsonObjectPut = JsonSerializer.Serialize(objInsert);

            var contentToPut = new StringContent(jsonObjectPut, Encoding.UTF8, "application/json");

            var response = client.PutAsync(url+ "UpdateCountry", contentToPut).Result;
            ((MainWindow)this.Owner).CallGetAll();
            ((MainWindow)this.Owner).countryInfoRefresh();
            ((MainWindow)this.Owner).refresh();

            Close();


        }
    }
}
