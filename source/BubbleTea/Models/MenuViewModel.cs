using BubbleTea.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BubbleTea.Models
{
    public class MenuViewModel
    {
        private IDataRepository _repository;
        private Dictionary<string, decimal> _itemPrices = new Dictionary<string, decimal>();
        public MenuViewModel(IDataRepository respository)
        {
            _repository = respository;
            Toppings = _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Flavor").ToList();
        }
        public MenuViewModel()
        {

        }


        public List<GroupItem> BaseTea { get; set; }
        public List<GroupItem> Flavors { get; set; }
        public List<GroupItem> Toppings { get; set; }
        public List<Size> Sizes { get; set; }

        //public IEnumerable<SelectListItem> BaseTeaItems
        //{
        //    get
        //    {
        //        return BaseTea.Select(q => new SelectListItem() { Text = q.Item.Name, Value = q.Item.Id.ToString() });
        //    }
        //}

        public IEnumerable<string> BaseTeaItems
        {
            get
            {
                var items = BaseTea.Select(r => r.Item.Name).Select(p => p.Substring(0, p.IndexOf('-') - 1)).GroupBy(q => q).Select(w => w.Key);
                return items;
            }
        }

        public IEnumerable<SelectListItem> FlavorItems
        {
            get
            {
                var flavors = Flavors.Select(q => new SelectListItem() { Text = q.Item.Name, Value = q.Item.Id.ToString() });
                return DefaultFlavorItem.Concat(flavors);
            }
        }

        public Dictionary<string, decimal> ItemPrices
        {
            get
            {
                //foreach (var item in BaseTea)
                //{
                //    _itemPrices.Add(item.ItemId.ToString(), item.Item.Prices.First(p => p.IsActive).Price);
                //}
                foreach (var item in BaseTea)
                {
                    _itemPrices.Add(item.Item.Name.ToString(), item.Item.Prices.First(p => p.IsActive).Price);
                }
                foreach (var item in Flavors)
                {
                    _itemPrices.Add(item.ItemId.ToString(), item.Item.Prices.First(p => p.IsActive).Price);
                }
                foreach (var item in Toppings)
                {
                    _itemPrices.Add(item.ItemId.ToString(), item.Item.Prices.First(p => p.IsActive).Price);
                }
                return _itemPrices;
            }
        }

        public List<CheckBoxItem> ToppingItems
        {
            get
            {
                if (Toppings == null)
                    return new List<CheckBoxItem>();
                else
                    return Toppings.Select(q => new CheckBoxItem() { Id = q.Item.Id, Name= q.Item.Name, IsChecked = false }).ToList();
            }
        }

        public int SelectedBaseTea { get; set; }
        public int SelectedFlavor { get; set; }
        public SizeEnum SelectedSize { get; set; }
        public List<int> SelectedToppings { get; set; }

        public IEnumerable<SelectListItem> DefaultFlavorItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "0",
                    Text = "- None -"
                }, count: 1);
            }
        }

        public decimal Price { get; set; }
    }

    public enum SizeEnum
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public class CheckBoxItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}