using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
    {
    public class Currency
        {
        [Key]
        public string Currency_Name { get; set; }
        public double Currency_Value { get; set; }
        }
    }
