using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BubbleTea.Models;
using BubbleTea.DataAccess;

namespace BubbleTea.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IDataRepository _repository;

        public OrderController(IDataRepository respository)
        {
            _repository = respository;
        }

        public ViewResult Index(OrderViewModel cart, string returnUrl)
        {
            return View(new OrderIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult AddToOrder(OrderViewModel cart, MenuViewModel model, string returnUrl, string price)
        {
            var order = new OrderLineItem();
            order.BaseTea = _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Base Tea").First(q => q.ItemId == model.SelectedBaseTea).Item;
            order.Flavor = model.SelectedFlavor != 0 ? _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Flavor").First(q => q.ItemId == model.SelectedFlavor).Item : null;
            order.Toppings = model.SelectedToppings != null ? _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Toppings").Where(q => model.SelectedToppings.Contains(q.Item.Id)).Select(s => s.Item).ToList() : null;
            order.Size = model.SelectedSize;
            decimal dPrice = 0;
            Decimal.TryParse(price, out dPrice);
            order.Price = dPrice;
            cart.AddOrder(order);
            return RedirectToAction("Index", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult RemoveFromCart(OrderViewModel cart, int baseTeaId, string returnUrl)
        {
            cart.Lines.RemoveAll(p => p.BaseTea.Id == baseTeaId);
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(OrderViewModel cart)
        {
            return PartialView(cart);
        }

        public ActionResult Checkout(OrderViewModel cart)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your order is empty!");
            }

            if (ModelState.IsValid)
            {
                Order ord = new Order();
                foreach (var item in cart.Lines)
                {
                    var x = new LineItem();
                    x.SizeId = (int)item.Size;
                    x.Items.Add(new OrderItem() { ItemId = item.BaseTea.Id });
                    if(item.Flavor != null)
                        x.Items.Add(new OrderItem() { ItemId = item.Flavor.Id });
                    if(item.Toppings != null)
                        item.Toppings.ForEach(p => x.Items.Add(new OrderItem() { ItemId = p.Id}));
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
                return RedirectToAction("Index","Home");
            }
        }
    }
}