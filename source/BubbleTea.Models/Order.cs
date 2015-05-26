using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BubbleTea.Models
{
    public class Order
    {
        public Order()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int Id
        {
            get; set;
        }

        public string UserName
        {
            get; set;
        }

        public Decimal Amount
        {
            get; set;
        }

        public Decimal? Discount
        {
            get; set;
        }

        public bool IsCancelled
        {
            get; set;
        }

        public virtual ICollection<LineItem> LineItems { get; set; }
        public virtual User User { get; set; }
    }
}