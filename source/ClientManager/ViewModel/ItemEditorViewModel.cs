using BubbleTea.DataAccess;
using BubbleTea.Models;
using ClientManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientManager.ViewModel
{
    public class ItemEditorViewModel : ViewModelBase
    {
        private IDataRepository service = null;
        private ObservableCollection<Item> _groupItems = null;
        private string _groupType;

        public ItemEditorViewModel(string groupType)
        {
            service = DataService.Repository;
            _groupType = groupType;
            GetGroupItems();
            SelectedItem = new Item();
        }

        public string GroupType { get { return _groupType; } }

        private void GetGroupItems()
        {
            GroupItems = new ObservableCollection<Item>(service.GetAllGroupItems().Where(p => p.GroupOffering.Name == GroupType).Select(q => q.Item));
        }

        public ObservableCollection<Item> GroupItems
        {
            get { return _groupItems; }
            set { _groupItems = value; OnPropertyChanged(); }
        }

        private Item _selectedItem;
        public Item SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    IsDisabled = true;
                    if (_selectedItem != null)
                        SelectedItemPrice = _selectedItem.Price;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedItemName;
        public string SelectedItemName
        {
            get { return _selectedItemName; }
            set { _selectedItemName = value; OnPropertyChanged(); }
        }

        private decimal _selectedItemPrice;
        public decimal SelectedItemPrice
        {
            get { return _selectedItemPrice; }
            set { _selectedItemPrice = value; OnPropertyChanged(); }
        }

        private bool _isDisabled;
        public bool IsDisabled
        {
            get
            { return _isDisabled; }
            set
            {
                _isDisabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand
        {
            get { return new RelayCommand((s) => { SelectedItem = new Item(); IsDisabled = false; }); }
        }

        public ICommand DeleteCommand
        {
            get { return new RelayCommand((s) => DeleteItem(), (p) => { return (SelectedItem != null && SelectedItem.Id != 0) ? true : false; }); }
        }

        public ICommand EditCommand
        {
            get { return new RelayCommand((s) => { IsDisabled = false; }, (p) => { return (SelectedItem != null && SelectedItem.Id != 0) ? true : false; }); }
        }

        public ICommand SaveCommand
        {
            get { return new RelayCommand((s) => SaveItem(), (p) => { return !IsDisabled; }); }
        }

        private void DeleteItem()
        {
            if (SelectedItem != null && SelectedItem.Id != 0)
            {
                var groupOfferingId = service.GetGroupIdByName(GroupType);
                service.DeleteGroupItem(SelectedItem.Id, groupOfferingId);
                GetGroupItems();
                SelectedItemPrice = 0;
            }
        }

        private void SaveItem()
        {
            if (SelectedItem == null || SelectedItem.Id == 0)
            {
                GroupItem gi = new GroupItem();
                gi.GroupOfferingId = service.GetGroupIdByName(GroupType);
                gi.Item = new Item() { Name = SelectedItem.Name, Id = 0 };
                ItemPrice ip = new ItemPrice() { Id = 0, IsActive = true, Price = SelectedItemPrice, Tax = 0 };
                gi.Item.Prices.Add(ip);
                service.AddGroupItem(gi);
                GetGroupItems();
            }
            else
            {
                var itemPrice = new ItemPrice() { IsActive = true, ItemId = SelectedItem.Id, Price = SelectedItemPrice, Tax = 0 };
                service.AddItemPrice(SelectedItem, itemPrice);
                GetGroupItems();
            }
            SelectedItem = new Item();
        }
    }
}