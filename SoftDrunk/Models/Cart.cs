using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftDrunk.Models
{
    public class CartPart
    {
        public Product product;
        public int quantity;
    }
    public class Cart
    {
        public List<CartPart> carts = new List<CartPart>();
        public IEnumerable<CartPart> Carts
        {
            get { return carts; }
        }
        public void AddToCart(Product pro)
        {
            var product = Carts.FirstOrDefault(s => s.product.ProductID == pro.ProductID);
            if(product == null)
            {
                carts.Add(new CartPart{
                    product = pro,
                    quantity = 1
                });

            }
            else
            {
                product.quantity += 1;
            }
        }
        public void DecreaseProduct(Product pro)
        {
            var product = Carts.FirstOrDefault(s => s.product.ProductID == pro.ProductID);
            if (product == null)
            {
                return;
            }
            if (product.quantity <= 1)
            {
                carts.Remove(product);
                return;
            }
            else
            {
                product.quantity -= 1;
            }
            
        }
        public int CountQuantity()
        {
            return Carts.Sum(s=>s.quantity);
        }
        public decimal TotalMoney()
        {
            return Carts.Sum(s => s.product.ProductPrice * s.quantity);
        }
        public void RemoveFromCart()
        {

        }
        public void ClearCart()
        {
            carts.Clear();
        }
    }

}