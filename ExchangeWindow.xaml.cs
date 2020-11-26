using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CountryAPI_WPF
{
    /// <summary>
    /// Interaction logic for ExchangeWindow.xaml
    /// </summary>
    public partial class ExchangeWindow : Window
    {
        public ExchangeWindow()
        {
            InitializeComponent();
        }

        string cName1;
        string cName2;
        double cValue1;
        double cValue2;
        bool c1Picked = false;
        bool c2Picked = false;

        private void countries2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cName2 = countries2.SelectedItem.ToString();
            c2Picked = true;
            string sel2 = cName2.Substring(37);
            country2info.Content = sel2;

            //test code
            if (sel2 == "Item #1")
            {
                cValue2 = 1.20;
            }
            if (sel2 == "Item #2")
            {
                cValue2 = 1.0;
            }
            if (sel2 == "Item #3")
            {
                cValue2 = 0.80;
            }
        }

        private void countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cName1 = countries.SelectedItem.ToString();
            c1Picked = true;
            string sel1 = cName1.Substring(37);
            country1info.Content = sel1;

            //test code
            if (sel1 == "Item #1")
            {
                cValue1 = 1.20;
            }
            if (sel1 == "Item #2")
            {
                cValue1 = 1.0;
            }
            if (sel1 == "Item #3")
            {
                cValue1 = 0.80;
            }
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void rtnBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void excBtn_Click(object sender, RoutedEventArgs e)
        {
            if (c1Picked && c2Picked && curBox.Text != "")
            {

                string input = curBox.Text;
                double inputd = Convert.ToDouble(input);
                double us = cValue1 * inputd;
                double val = us / cValue2;
                Answer.Content = Math.Round(val, 2);

            }
        }
    }
}
