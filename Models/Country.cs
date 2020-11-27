using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace CountryAPIWPF.Models
{

    public class Country
    {
        public int id { get; set; }
        public string countryName { get; set; }
        public string continent { get; set; }
        public string capitalCity { get; set; }
        public double population { get; set; }
        public string primaryLanguage { get; set; }
        public string currency { get; set; }
        public double valueToUSD { get; set; }
        public string secondaryLanguage { get; set; }
    }
}
