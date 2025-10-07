using Basket.Entities;

namespace Basket.Repositories;

public interface IBasketRepositories
{

    Task<ShoppingCart> GetBasket(string userName);
    Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart);
    Task  DeleteBasket(string userName);



}

