using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Basket.Entities;

    public class ShoppingCart
    {
    //alısveris sepeti
    public string  UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } =new List<ShoppingCartItem>();
    public ShoppingCart()
    {
        
    }
    public ShoppingCart(string username)
    {
            
        UserName = username;
    }



}

