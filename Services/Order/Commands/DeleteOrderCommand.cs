using MediatR;

namespace Ordering.Commands
{
    public record class DeleteOrderCommand:IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
