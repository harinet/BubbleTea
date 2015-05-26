using System;
using System.ComponentModel;

namespace BubbleTea.Models
{
    public class OrderItem
    {
        public int Id
        {
            get; set;
        }

        public int LineItemId
        {
            get; set;
        }

        public int ItemId
        {
            get; set;
        }

        public virtual LineItem LineItem
        {
            get; set;
        }

        public virtual Item Item
        {
            get; set;
        }

    }
}