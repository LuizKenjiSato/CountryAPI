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
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using CountryAPIWPF.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using CountryAPIWPF.Models;

namespace CountryAPIWPF
{
    /// <summary>
    /// Interaction logic for exchangeWindow.xaml
    /// </summary>
    public partial class exchangeWindow : Window
    {
        Country[] results;
        private static readonly HttpClient client = new HttpClient();
        private static string url;
        public exchangeWindow(Country[] r, string u)
        {
            InitializeComponent();
            url = u;
            foreach (Country e in r)
            {
                countries.Items.Add(e.countryName);
                countries2.Items.Add(e.countryName);
            }
            results = r;

        }

        string cName1;
        string cName2;
        double cValue1;
        double cValue2;
        bool c1Picked = false;
        bool c2Picked = false;
        string cur;
        private void countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            c1Picked = true;
            string curItem = countries.SelectedItem.ToString();
            country1info.Content = curItem;
            cName1 = curItem;
            foreach (Country c in results)
            {
                if (c.countryName == curItem)
                {
                    cValue1 = c.valueToUSD;
                }
            }
            country1info.Content = curItem + " value: " + cValue1 +"USD";
        }

        private void countries2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            c2Picked = true;
            string curItem = countries2.SelectedItem.ToString();
            country2info.Content = curItem;
            cName2 = curItem;

            foreach (Country c in results)
            {
                if (c.countryName == curItem)
                {
                    cValue2 = c.valueToUSD;
                    cur = c.currency;
                }
            }
            country2info.Content = curItem + " value: " + cValue2 + "USD";
            
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void excBtn_Click(object sender, RoutedEventArgs e)
        {
            if(c1Picked && c2Picked && curBox.Text != "")
            {

                    string input = curBox.Text;
                    double inputd = Convert.ToDouble(input);
                   

                HttpResponseMessage response = client.GetAsync(url + "CurrencyExchange/"+cName1+ "/" +cName2+"/"+inputd).Result;
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                //results = JsonSerializer.Deserialize<Country[]>(jsonResponse);
                double final = Convert.ToDouble(jsonResponse);
                Answer.Content = Math.Round(final, 2) + " " + cur;
            }
        }

        private void rtnBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
