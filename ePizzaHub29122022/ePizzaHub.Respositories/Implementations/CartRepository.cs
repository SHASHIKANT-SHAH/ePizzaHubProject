﻿
using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Respositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ePizzaHub.Respositories.Implementations
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        AppDbContext context
        {
            get
            {
                return _db as AppDbContext;
            }
        }
        public CartRepository(AppDbContext db):base(db) { }
       
        public int DeleteItem(Guid cartId, int itemId)
        {
            var item = context.CartItems.Where(ci=>ci.CartId==cartId&&ci.ItemId==itemId).FirstOrDefault();
            if(item != null)
            {
                context.CartItems.Remove(item);
                return context.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public Cart GetCart(Guid CartId)
        {
            return context.Carts.Include(c => c.CartItems).Where(p => p.Id == CartId && p.IsActive == true).FirstOrDefault();
        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = (from cart in context.Carts
                         where cart.Id == CartId && cart.IsActive == true
                         select new CartModel
                         {
                             Id = cart.Id,
                             UserId = cart.UserId,
                             CreateDate = cart.CreatedDate,
                             Items = (from cartItem in context.CartItems
                                      join item in context.Items
                                      on cartItem.ItemId equals item.Id
                                      select new ItemModel
                                      {
                                          Id = cartItem.Id,
                                          Quantity = cartItem.Quantity,
                                          UnitPrice = cartItem.UnitPrice,
                                          ItemId = item.Id,
                                          Name = item.Name,
                                          Description = item.Description,
                                          ImageUrl = item.ImageUrl
                                      }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        public int UpdateCart(Guid cartId, int userId)
        {
            Cart cart = GetCart(cartId);
            cart.UserId = userId;
            return context.SaveChanges();
        }

        public int UpdateQuantity(Guid cartId, int itemId, int Quantity)
        {
            bool flag = false;
            var cart = GetCart(cartId);
            if (cart != null)
            {
                var cartItems = cart.CartItems.ToList();
                for(int i=0; i<cartItems.Count; i++)
                {
                    if (cartItems[i].Id == itemId)
                    {
                        flag = true;
                        cartItems[i].Quantity += (Quantity);
                        break;
                    }
                }
                if(flag)
                {
                    cart.CartItems = cartItems;
                    return context.SaveChanges();
                }
            }
            return 0;
        }
    }
}
