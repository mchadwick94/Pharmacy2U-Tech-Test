using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CurrencyConverter.Models;

namespace CurrencyConverter.Data
{
    public class CurrencyConverterContext : DbContext
    {
        public CurrencyConverterContext (DbContextOptions<CurrencyConverterContext> options)
            : base(options)
        {
        }

        public DbSet<CurrencyConverter.Models.Currency> Currency { get; set; }

        public DbSet<CurrencyConverter.Models.Conversion> Conversion { get; set; }
        public DbSet<CurrencyConverter.Models.SearchModel> SearchModel { get; set; }
        }
    }
