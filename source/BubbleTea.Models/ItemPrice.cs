using System;
using System.ComponentModel;

namespace BubbleTea.Models
{
    public class ItemPrice
    {
        public int Id
        {
            get; set;
        }

        public int ItemId
        {
            get; set;
        }

        public Decimal? Tax
        {
            get; set;
        }

        public Decimal Price
        {
            get; set;
        }

        public bool IsActive
        {
            get; set;
        }

        public virtual Item Item
        {
            get; set;
        }
    }
}