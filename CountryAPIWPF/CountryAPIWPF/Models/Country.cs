using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace CountryAPIWPF.Models
{

    public class Country
    {
        public long id { get; set; }
        public string name { get; set; }
        public string continent { get; set; }
        public string capitalCity { get; set; }
        public string population { get; set; }
        public string language { get; set; }
        public string currency { get; set; }
        public double currencyValue { get; set; }
    }
}
