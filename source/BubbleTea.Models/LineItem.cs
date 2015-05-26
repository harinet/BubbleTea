using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbleTea.Models
{
    public class LineItem
    {
        public LineItem()
        {
            Items = new HashSet<OrderItem>();
        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SizeId { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public virtual Order Order { get; set; }
        public virtual Size Size { get; set; }
    }
}
