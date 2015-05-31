using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BubbleTea.Models
{
    public class OrderIndexViewModel
    {
        public OrderViewModel Cart { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class OrderLineItem
    {
        public int SerialNo { get; set; }
        public Item BaseTea { get; set; }
        public Item Flavor { get; set; }
        public List<Item> Toppings { get; set; }
        public SizeEnum Size { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderViewModel
    {
        private List<OrderLineItem> _orders = new List<OrderLineItem>();

        public OrderViewModel()
        {

        }

        public List<OrderLineItem> Lines { get { return _orders; } }

        public void AddOrder(OrderLineItem or)
        {
            _orders.Add(or);
        }

        public void Clear()
        {
            _orders.Clear();
        }

        public int Count { get { return _orders.Count; } }
        public decimal ComputeTotalValue()
        {
            decimal total = 0;
            _orders.ForEach(p => total += p.Price);
            return total;
        }
    }
}