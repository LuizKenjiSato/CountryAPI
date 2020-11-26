using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CountryAPI.Models
{
    public class CountryContext :DbContext
    {
        public CountryContext(DbContextOptions<CountryContext> options)
    : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
    }
}
