using Catalog.Commands.Product;
using Catalog.DTOs;
using Catalog.Extensions;
using Catalog.Mappers;
using Catalog.Queries.Brand;
using Catalog.Queries.Product;
using Catalog.Queries.Type;
using Catalog.Specification;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ZstdSharp.Unsafe;

namespace Catalog.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{

    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;

    }

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecificationParams catalogSpecificationParams)
    {

        var query = new GetAllProductsQuery(catalogSpecificationParams);
        var result = await _mediator.Send(query);
        //dto cevır
        return Ok(result);

    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
        if (result is null)
            return NotFound();
        return Ok(result.ToDto());


    }

    [HttpGet("GetProductsByProductName/{productname}")]
    public async Task<ActionResult<IList<ProductDto>>> GetProductsByName(string productname)
    {

        var query = new
            GetProductsByNameQuery(productname);


        var result = await _mediator.Send(query);
        if (result == null || !result.Any())
        {

            return NotFound();
        }

        var pList = result.Select(p => p.ToDto()).ToList();
        return Ok(pList);

    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand command)
    {

        var result = await _mediator.Send(command);
        return Ok(result.ToDto());

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var command = new DeleteProducByIdCommand(id);
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound(id);

        }

        return NoContent();
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct(string productId, UpdateProductDto updateProductDto)
    {

        var command = updateProductDto.ToCommand(productId);
        var result = await _mediator.Send(command);
        if (!result)
        {

            return NotFound(productId);
        }
        return NoContent();


    }

    [HttpGet("GetAllBrands")]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);
      var resultDto=  result.ToDtoList();
        return Ok(resultDto);
    }
    [HttpGet("BrandByID/{id}")]
    public async Task<ActionResult<BrandDto>> GetByIdAsync(string id)
    {
        var query = new GetByIdBrandQuery(id);
        var result=await _mediator.Send(query);
        return Ok(result.ToDto());
    }

    [HttpGet("GetAllTypes")]
    public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);

        var resultDto = result.ToDtoList();
        return Ok(resultDto);
    }

    [HttpGet("brand/{brand}", Name = "GetProductsByBrandName")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByBrand(string brand)
    {
        // First get the products
        var query = new GetProductByBrandNameQuery(brand);
       var result = await _mediator.Send(query);
      var resultDto=result.ResponseToDtoList();
        return Ok(resultDto);
    }









}

