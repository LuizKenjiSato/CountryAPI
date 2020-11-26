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

namespace CountryAPIWPF
{
    /// <summary>
    /// Interaction logic for exchangeWindow.xaml
    /// </summary>
    public partial class exchangeWindow : Window
    {
        Country[] results;
        public exchangeWindow(Country[] r)
        {
            InitializeComponent();

            foreach (Country e in r)
            {
                countries.Items.Add(e.name);
                countries2.Items.Add(e.name);
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
            
            foreach (Country c in results)
            {
                if (c.name == curItem)
                {
                    cValue1 = c.currencyValue;
                }
            }
            country1info.Content = curItem + " value: " + cValue1 +"USD";
        }

        private void countries2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            c2Picked = true;
            string curItem = countries2.SelectedItem.ToString();
            country2info.Content = curItem;

            foreach (Country c in results)
            {
                if (c.name == curItem)
                {
                    cValue2 = c.currencyValue;
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
                    double us = cValue1 * inputd;
                    double val = us / cValue2;
                    Answer.Content = Math.Round(val,2) +" " +cur;
                
            }
        }

        private void rtnBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
