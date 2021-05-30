﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class OrderedProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cost { get; set; }
        public string TotalPrice { get; set; }
        public string Description { get; set; }
        public string MainImagePath { get; set; }
        public int Quantity { get; set; }
        public string Login { get; set; }
    }
}
