using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryAPI.Models
{
    public class Country
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string CapitalCity { get; set; }
        public string Population { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public double CurrencyValue { get; set; }
    }
}
