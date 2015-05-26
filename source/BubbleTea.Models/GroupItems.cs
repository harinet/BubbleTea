using System;
using System.ComponentModel;

namespace BubbleTea.Models
{
    public class GroupItem
    {
        public int GroupOfferingId
        {
            get; set;
        }

        public int ItemId
        {
            get; set;
        }

        public virtual GroupOffering GroupOffering
        {
            get; set;
        }

        public virtual Item Item
        {
            get; set;
        }

        public int Id
        {
            get; set;
        }
    }
}