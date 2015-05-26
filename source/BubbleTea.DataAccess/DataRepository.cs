using BubbleTea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleTea.DataAccess
{
    public class DataRepository : IDataRepository
    {
        private ModelDbContext _context;
        public DataRepository()
        {
            _context = new ModelDbContext();
        }

        public List<Size> GetAllSizes()
        {
            return _context.Sizes.ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public List<OrderItem> GetAllOrderItems()
        {
            return _context.OrderItems.ToList();
        }

        public List<Item> GetAllItems()
        {
            return _context.Items.ToList();
        }

        public List<ItemPrice> GetAllItemPrices()
        {
            return _context.ItemPrices.ToList();
        }

        public List<GroupItem> GetAllGroupItems()
        {
            return _context.GroupItems.ToList();
        }

        public List<GroupOffering> GetAllGroupOfferigs()
        {
            return _context.GroupOfferings.ToList();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public List<LineItem> GetAllLineItems()
        {
            return _context.LineItems.ToList();
        }

        public void Save(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public int GetGroupIdByName(string name)
        {
            var list = _context.GroupOfferings.Where(p => p.Name == name);
            if (list.Count() > 0)
                return list.First().Id;
            return 0;
        }

        public void AddGroupItem(GroupItem item)
        {
            _context.GroupItems.Add(item);
            _context.SaveChanges();
        }

        public void DeleteGroupItem(int itemId, int groupId)
        {
            var obj = _context.GroupItems.Where(p => p.ItemId == itemId && p.GroupOfferingId == groupId);
            if (obj.Count() > 0)
            {
                _context.GroupItems.Remove(obj.First());
                _context.SaveChanges();
            }
        }

        public void AddItemPrice(Item item, ItemPrice price)
        {
            var prices = _context.ItemPrices.Where(p => p.ItemId == item.Id);
            foreach (var itemPrice in prices)
            {
                itemPrice.IsActive = false;
            }
            var itm = _context.Items.First(p => p.Id == item.Id);
            itm.Name = item.Name;
            _context.ItemPrices.Add(price);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var usr = _context.Users.First(p => p.Id == user.Id);
            usr.FirstName = user.FirstName;
            usr.LastName = user.LastName;
            usr.DateOfBirth = user.DateOfBirth;
            _context.SaveChanges(); 
        }

    }
}
