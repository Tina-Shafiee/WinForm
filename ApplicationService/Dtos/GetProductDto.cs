using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Dtos
{
    public class GetProductDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int UnitPrice { get; set; }

        public int Quantity { get; set; }

        public long TotalPrice => UnitPrice * Quantity;
    }
}
