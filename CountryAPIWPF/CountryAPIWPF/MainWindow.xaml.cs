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
using System.Net.Http;
using System.Text;
using System.Text.Json;
using CountryAPIWPF.Models;



namespace CountryAPIWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static readonly HttpClient client = new HttpClient();

        private static string url = "https://localhost:44349/api/Countries/";
        private static Country[] results;
        private static Country res = new Country();
        private long highestId = 0;
        private long currentId;
        public MainWindow()
        {
            InitializeComponent();
            tempPut();
            CallGetAll();

        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
        }

        public static void tempPut()
        {

            Country objInsert = new Country();
            Country objInsert2 = new Country();

            objInsert.id = 4;
            objInsert.name = "China";
            objInsert.continent = "Asia";
            objInsert.capitalCity = "Beijing";
            objInsert.population = "100001";
            objInsert.language = "Mandarin";
            objInsert.currency = "CNY";
            objInsert.currencyValue = 0.15;

            objInsert2.id = 5;
            objInsert2.name = "Japan";
            objInsert2.continent = "Asia";
            objInsert2.capitalCity = "Tokyo";
            objInsert2.population = "100006";
            objInsert2.language = "Japanese";
            objInsert2.currency = "JPY";
            objInsert2.currencyValue = 0.0096;

            string jsonObjectPost = JsonSerializer.Serialize(objInsert);
            string jsonObjectPost2 = JsonSerializer.Serialize(objInsert2);

            var contentToPost = new StringContent(jsonObjectPost, Encoding.UTF8, "application/json");
            var contentToPost2 = new StringContent(jsonObjectPost2, Encoding.UTF8, "application/json");

            var response = client.PostAsync(url, contentToPost).Result;
            var response2 = client.PostAsync(url, contentToPost2).Result;

            
        }

        private void CallGetAll()
        {

            HttpResponseMessage response = client.GetAsync(url).Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            results = JsonSerializer.Deserialize<Country[]>(jsonResponse);
            foreach (Country e in results)
            {
                countries.Items.Add(e.name);
                if(e.id > highestId)
                {
                    highestId = e.id;
                }
            }
        }

        private void countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (countries.SelectedItem != null) { 
            string curItem = countries.SelectedItem.ToString();
            foreach (Country c in results)
            {
                if (c.name == curItem)
                {
                    countryName.Content = c.name;
                    continent.Content = c.continent;
                    capitalCity.Content = c.capitalCity;
                    population.Content = c.population;
                    language.Content = c.language;
                    currency.Content = c.currency;
                        currentId = c.id;
                }
            }
            //string sel = curItem.Substring(37);
        }
           
        }

        private void exchangeBtn_Click(object sender, RoutedEventArgs e)
        {
            exchangeWindow win2 = new exchangeWindow(results);
            win2.Show();
        }

        //Put
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Country putCountry = new Country();
            foreach (Country c in results)
            {
                if (c.id == currentId)
                {
                    putCountry.id = c.id;
                    putCountry.name = c.name;
                    putCountry.continent = c.continent;
                    putCountry.capitalCity = c.capitalCity;
                    putCountry.population = c.population;
                    putCountry.language = c.language;
                    putCountry.currency = c.currency;
                    putCountry.currencyValue = c.currencyValue;


                }
            }

            PutWindow win = new PutWindow(putCountry, url);
            win.Owner = this;
            win.Show();
        }

        //Post
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            PostWindow win = new PostWindow(highestId, url); ;
            win.Owner = this;
            win.Show();
        }

        private void refreshbtn_Click(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        public void refresh()
        {
            countries.Items.Clear();
            CallGetAll();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (countries.SelectedItem != null)
            {
                string curItem = countries.SelectedItem.ToString();
                foreach (Country c in results)
                {
                    if (c.name == curItem)
                    {
                        long id = c.id;
                        var response = client.DeleteAsync(url + id).Result;
                        refresh();
                    }
                }
            }
        }
    }
}
