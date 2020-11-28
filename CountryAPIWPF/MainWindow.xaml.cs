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

        private static string url = "http://countryapi-dev.ca-central-1.elasticbeanstalk.com/Country/";

        //private static string url = "https://localhost:44378/Country/";
        //private static string url = "https://localhost:44349/api/Countries/";
        private static Country[] results;
        private static Country res = new Country();
        private int highestId = 0;
        private int currentId;
        public string putName;
        public MainWindow()
        {
            InitializeComponent();
            //tempPost();
            CallGetAll();

        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
        }

        /*public static void tempPost()
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

            
        }*/

        public void CallGetAll()
        {

            HttpResponseMessage response = client.GetAsync(url+"AllCountryInfo/").Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            results = JsonSerializer.Deserialize<Country[]>(jsonResponse);
            foreach (Country e in results)
            {
                countries.Items.Add(e.countryName);
                if(e.id > highestId)
                {
                    highestId = e.id;
                }
            }
        }

        private void countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (countries.SelectedItem != null) {
                countryInfoRefresh();
        }
           
        }

        public void countryInfoRefresh()
        {
            if (countries.SelectedItem != null)
            {
                string curItem = countries.SelectedItem.ToString();
                foreach (Country c in results)
                {
                    if (c.countryName == curItem)
                    {
                        countryName.Content = c.countryName;
                        continent.Content = c.continent;
                        capitalCity.Content = c.capitalCity;
                        population.Content = c.population;
                        language.Content = c.primaryLanguage;
                        currency.Content = c.currency;
                        currentId = c.id;


                    }
                }
            }
        }

        private void exchangeBtn_Click(object sender, RoutedEventArgs e)
        {
            exchangeWindow win2 = new exchangeWindow(results, url);
            win2.Show();
        }

        //Put
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Country putCountry = new Country();
            if (countries.SelectedItem != null)
            {
                foreach (Country c in results)
                {
                    if (c.id == currentId)
                    {
                        putName = c.countryName;


                    }
                }

                PutWindow win = new PutWindow(putName, url);
                win.Owner = this;
                win.Show();
            }
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
                    if (c.countryName == curItem)
                    {
                        String name = c.countryName;
                        var response = client.DeleteAsync(url + "DeleteCountry/" + name).Result;
                        refresh();
                    }
                }
            }
        }
    }
}
