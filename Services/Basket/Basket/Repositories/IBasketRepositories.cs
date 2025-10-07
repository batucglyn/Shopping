using Basket.Entities;

namespace Basket.Repositories;

public interface IBasketRepositories
{

    Task<ShoppingCart> GetBasket(string userName);
    Task<ShoppingCart> UpsertBasket(ShoppingCart shoppingCart);
    Task  DeleteBasket(string userName);



}

