using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BubbleTea.Models
{
    public partial class Item
    {
        public Item()
        {
            Prices = new HashSet<ItemPrice>();
        }
        public int Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public virtual ICollection<ItemPrice> Prices { get; set; }
    }
}