using MediatR;

namespace Catalog.Commands.Product
{
    public record class DeleteProducByIdCommand(string productId):IRequest<bool>
    {
    }
}
