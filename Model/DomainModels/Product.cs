using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DomainModels
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int UnitPrice { get; set; }

        public int Quantity { get; set; }

    }
}
