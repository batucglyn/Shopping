using Basket.Commands.Basket;
using Basket.DTOs;
using Basket.Entities;
using Basket.Responses;

namespace Basket.Mappers;

public static class BasketMapper
{
    public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart)
    {

        return new ShoppingCartResponse
        {
            UserName = shoppingCart.UserName,
            Items = shoppingCart.Items.Select(item => new ShoppingCartItemResponse
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Price = item.Price,
                ImageFile = item.ImageFile


            }).ToList()

        };


    }

    public static ShoppingCart ToEntity(this CreateShoppingCartCommand command)
    {
        return new ShoppingCart
        {
            UserName = command.UserName,
            Items = command.Items.Select(item => new ShoppingCartItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                Price = item.Price,
                ImageFile = item.ImageFile

            }).ToList()



        };

    }

    public static ShoppingCartDTO ToDto(this ShoppingCartResponse response)
    {
        return new ShoppingCartDTO(
       response.UserName,
       response.Items.Select(item => new ShoppingCartItemDTO(
           item.ProductId,
           item.ProductName,
           item.Quantity,
           item.Price,
           item.ImageFile
       )).ToList(),
       response.TotalPrice
   );

    }
    

   

}

