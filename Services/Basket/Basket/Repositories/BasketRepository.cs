﻿using Basket.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Repositories;

public class BasketRepository : IBasketRepositories
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
       var basket= await _redisCache.GetStringAsync(userName);

        if (string.IsNullOrEmpty(basket)) {

            return null;
        }
        return JsonConvert.DeserializeObject<ShoppingCart>(basket);

    }

    public async Task DeleteBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
    {
        await _redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));

        return await GetBasket(shoppingCart.UserName);

    }
}

