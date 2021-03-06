﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
    {
    public class SearchModel
        {
        [Key]
        public int Conversion_ID { get; set; }
        public double Conv_Input_Value { get; set; }
        public string Conv_Input_Currency { get; set; }
        public double Conv_Output_Value { get; set; }
        public string Conv_Output_Currency { get; set; }
        public DateTime? Search_Start_Date { get; set; }
        public DateTime? Search_End_Date { get; set; }
        public virtual Currency CurrencyModel { get; set; }
        }
    }
