using BubbleTea.DataAccess;
using BubbleTea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BubbleTea.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private IDataRepository _repository;
        public MenuController(IDataRepository respository)
        {
            _repository = respository;
        }
        // GET: Menu
        public ViewResult Index()
        {
            var baseTea = _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Base Tea").ToList();
            var flavours = _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Flavor").ToList();
            var toppinngs = _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Toppings").ToList();
            var sizes = _repository.GetAllSizes();
            MenuViewModel mv = new MenuViewModel(_repository) { BaseTea=baseTea, Flavors = flavours, Toppings = toppinngs, Sizes = sizes }; 
            return View(mv);
        }

        [HttpPost]
        public ViewResult Index(MenuViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public RedirectToRouteResult AddToOrder(OrderViewModel cart, MenuViewModel model, string returnUrl, string price, string selectedBase)
        {
            var order = new OrderLineItem();
            order.SerialNo = cart.Lines.Count + 1;
            order.BaseTea = _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Base Tea").First(q => q.Item.Name == selectedBase).Item;
            order.Flavor = model.SelectedFlavor != 0 ? _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Flavor").First(q => q.ItemId == model.SelectedFlavor).Item : null;
            order.Toppings = model.SelectedToppings != null ? _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Toppings").Where(q => model.SelectedToppings.Contains(q.Item.Id)).Select(s => s.Item).ToList() : null;
            order.Size = model.SelectedSize;
            decimal dPrice = 0;
            Decimal.TryParse(price, out dPrice);
            order.Price = dPrice;
            cart.AddOrder(order);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToRouteResult RemoveFromCart(OrderViewModel cart, int serialNo, string returnUrl)
        {
            //cart.Lines.RemoveAll(p => p.BaseTea.Id == baseTeaId);
            cart.Lines.RemoveAll(p => p.SerialNo == serialNo);
            ReOrderSerialNo(cart);
            return RedirectToAction("Index");
        }

        private void ReOrderSerialNo(OrderViewModel cart)
        {
            int cnt = 0;
            foreach (var item in cart.Lines)
            {
                item.SerialNo = ++cnt;
            }
        }

        public PartialViewResult OrderItems(OrderViewModel cart, string returnUrl)
        {
            return PartialView(new OrderIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public ActionResult Checkout(OrderViewModel cart)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your order is empty!");
                return View("Completed");
            }

            if (ModelState.IsValid)
            {
                Order ord = new Order();
                foreach (var item in cart.Lines)
                {
                    var x = new LineItem();
                    x.SizeId = (int)item.Size;
                    x.Items.Add(new OrderItem() { ItemId = item.BaseTea.Id });
                    if (item.Flavor != null)
                        x.Items.Add(new OrderItem() { ItemId = item.Flavor.Id });
                    if (item.Toppings != null)
                        item.Toppings.ForEach(p => x.Items.Add(new OrderItem() { ItemId = p.Id }));
                    ord.LineItems.Add(x);
                }
                ord.Amount = cart.ComputeTotalValue();
                ord.UserName = User.Identity.Name;
                _repository.Save(ord);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
