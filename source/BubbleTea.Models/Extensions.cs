using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleTea.Models
{
    public partial class Item
    {
        public decimal Price
        {
            get
            {
                var x = Prices.Where(p => p.IsActive);
                if (x.Count() > 0)
                    return x.First().Price;
                return 0;
            }
        }
    }
}
