using Basket.Commands.Basket;
using Basket.DTOs;
using Basket.Mappers;
using Basket.Queries.Basket;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IMediator _mediator;

    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }




    [HttpGet("{userName}")]
    public async Task<ActionResult<ShoppingCartDTO>>GetBasket(string userName)
    {

        var query=new GetBasketByUserNameQuery(userName);
        var result=await _mediator.Send(query);
        return Ok(result.ToDto());


    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCartDTO>> CreateOrUpdateBasket([FromBody] CreateShoppingCartCommand command)
    {

        var result= await _mediator.Send(command);
        return Ok(result.ToDto()); 

    }
    [HttpDelete("{userName}")]
    public async Task<IActionResult>DeleteBasket(string userName)
    {
        var cmd=new DeleteBasketByUserNameCommand(userName);
        await _mediator.Send(cmd);
        return Ok();


    }






}

