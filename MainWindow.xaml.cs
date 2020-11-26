using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
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
using Table = Amazon.DynamoDBv2.DocumentModel.Table;


namespace CountryAPI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static AmazonDynamoDBClient client;
        public MainWindow()
        {
            InitializeComponent();
            PopulateListBox();
        }
        private void PopulateListBox()
        {
            var credentials = new BasicAWSCredentials("AKIAWLA4W37UM7AHKYY2", "nL4cQdNbWLGpUpE/3v1oRKjGmHiG/4++rWjgS8jw");
            client = new AmazonDynamoDBClient(credentials, RegionEndpoint.CACentral1);


            Table table = Table.LoadTable(client, "Countries");

            Console.WriteLine(table);


            ScanFilter scanFilter = new ScanFilter();
            //scanFilter.AddCondition("id", ScanOperator.GreaterThan, 0);

            Search search = table.Scan(scanFilter);

            List<Document> documentList = new List<Document>();

            do
            {
                documentList = search.GetNextSet();

            } while (!search.IsDone);

            foreach (var documents in documentList)
            {
                countries.Items.Add(documents["CountryName"]);
            }

            

        }
        private void countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string curItem = countries.SelectedItem.ToString();

            Table table = Table.LoadTable(client, "Countries");

            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("CountryName", ScanOperator.Equal, curItem);

            Search search = table.Scan(scanFilter);

            List<Document> documentList = new List<Document>();
            do
            {
                documentList = search.GetNextSet();

            } while (!search.IsDone);

            countryName.Content = documentList[0]["CountryName"];
            continent.Content = documentList[0]["Continent"];
            capitalCity.Content = documentList[0]["CapitalCity"];
            population.Content = documentList[0]["Population"];
            language.Content = documentList[0]["PrimaryLanguage"];
            currency.Content = documentList[0]["Currency"];

        }

        private void exchangeBtn_Click(object sender, RoutedEventArgs e)
        {
            ExchangeWindow win2 = new ExchangeWindow();
            win2.Show();
        }
    }
}
