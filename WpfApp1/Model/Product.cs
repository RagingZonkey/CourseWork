﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
   public class Product
    {

        
        public int Id { get; set; }
        public string Title { get; set; }

        public string Cost { get; set; }

        public string CostEdit { get; set; }
        public string Costedit { get; internal set; }
        public string Description { get; set; }

        public string DescriptionEdit { get; set; }

        public string MainImagePath { get; set; }

        public string IsActive { get; set; }

        public string ManufacturerID { get; set; }



        
    }
}
