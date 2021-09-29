using System;
using System.Collections.Generic;

#nullable disable

namespace AIPProducts.Models
{

    public partial class Product
    {

        public int ProductsId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public int IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
    }
}
