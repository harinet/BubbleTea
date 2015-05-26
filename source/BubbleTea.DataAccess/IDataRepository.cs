using BubbleTea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleTea.DataAccess
{
    public interface IDataRepository
    {
        List<Size> GetAllSizes();
        List<Order> GetAllOrders();
        List<OrderItem> GetAllOrderItems();
        List<Item> GetAllItems();
        List<ItemPrice> GetAllItemPrices();
        List<GroupItem> GetAllGroupItems();
        List<GroupOffering> GetAllGroupOfferigs();
        List<User> GetAllUsers();
        List<LineItem> GetAllLineItems();
        void Save(Order order);
        void AddUser(User user);
        void AddGroupItem(GroupItem item);
        void DeleteGroupItem(int itemId, int groupId);
        void AddItemPrice(Item item, ItemPrice price);
        int GetGroupIdByName(string name);
        void UpdateUser(User user);
    }
}
