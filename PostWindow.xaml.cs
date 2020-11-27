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
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private static string url;
        int newid;
        public PostWindow(int id, string u)
        {
            InitializeComponent();
            url = u;
            newid = id + 1;
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Country objInsert = new Country();

            objInsert.id = newid;
            objInsert.countryName = name.Text;
            objInsert.continent = con.Text;
            objInsert.capitalCity = cap.Text;
            objInsert.population = Convert.ToDouble(pop.Text);
            objInsert.primaryLanguage = lang.Text;
            objInsert.currency = cur.Text;
            objInsert.valueToUSD = Convert.ToDouble(val.Text);

            string jsonObjectPost = JsonSerializer.Serialize(objInsert);

            var contentToPost = new StringContent(jsonObjectPost, Encoding.UTF8, "application/json");

           var response = client.PostAsync(url+ "AddCountry", contentToPost).Result;
            ((MainWindow)this.Owner).refresh();
            Close();
            

        }
    }
}
