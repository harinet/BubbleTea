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
using GalaSoft.MvvmLight.Ioc;

namespace ClientManager.ViewModel
{
    public class ItemEditorViewModel : ViewModelBase
    {
        private IDataRepository service = null;
        private ObservableCollection<Item> _groupItems = null;
        private List<string> _size = null;
        private string _groupType;

        public ItemEditorViewModel(string groupType, bool isSizeApplicable = false)
        {
            service = SimpleIoc.Default.GetInstance<IDataRepository>();
            _groupType = groupType;
            GetGroupItems();
            GetSize();
            SelectedItem = new Item();
            IsSizeApplicable = isSizeApplicable;
        }

        public string GroupType { get { return _groupType; } }

        private void GetGroupItems()
        {
            GroupItems = new ObservableCollection<Item>(service.GetAllGroupItems().Where(p => p.GroupOffering.Name == GroupType).Select(q => q.Item));
        }

        private void GetSize()
        {
            Size = service.GetAllSizes().Select(p => p.Name).ToList();
        }

        public List<string> Size
        {
            get { return _size; }
            set { _size = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Item> GroupItems
        {
            get { return _groupItems; }
            set { _groupItems = value; OnPropertyChanged(); }
        }

        public bool IsSizeApplicable { get; set; }

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
                    {
                        SelectedItemPrice = _selectedItem.Price;
                        if(IsSizeApplicable && _selectedItem.Id > 0)
                        {
                            var x = _selectedItem.Name.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                            SelectedItemName = x[0].Trim();
                            SelectedSize = x[1].Trim();
                        }
                    }
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

        private string _selectedSize;
        public string SelectedSize
        {
            get { return _selectedSize; }
            set { _selectedSize = value; OnPropertyChanged(); }
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
            get { return new RelayCommand((s) => { SelectedItem = new Item(); IsDisabled = false; ClearBoundedProperties(); }); }
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
                ClearBoundedProperties();
            }
        }

        private void SaveItem()
        {
            if (SelectedItem == null || SelectedItem.Id == 0)
            {
                GroupItem gi = new GroupItem();
                gi.GroupOfferingId = service.GetGroupIdByName(GroupType);
                if(IsSizeApplicable)
                    gi.Item = new Item() { Name = SelectedItemName + " - " + SelectedSize, Id = 0 };
                else
                    gi.Item = new Item() { Name = SelectedItemName, Id = 0 };
                ItemPrice ip = new ItemPrice() { Id = 0, IsActive = true, Price = SelectedItemPrice, Tax = 0 };
                gi.Item.Prices.Add(ip);
                service.AddGroupItem(gi);
                GetGroupItems();
            }
            else
            {
                var itemPrice = new ItemPrice() { IsActive = true, ItemId = SelectedItem.Id, Price = SelectedItemPrice, Tax = 0 };
                SelectedItem.Name = SelectedItemName;
                service.AddItemPrice(SelectedItem, itemPrice);
                GetGroupItems();
            }
            SelectedItem = new Item();
            ClearBoundedProperties();
        }

        private void ClearBoundedProperties()
        {
            SelectedItemName = string.Empty;
            SelectedItemPrice = 0;
        }
    }
}