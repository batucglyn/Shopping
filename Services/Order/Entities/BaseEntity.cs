namespace Ordering.Entities
{
    public class BaseEntity
    {
 
        public Guid Id { get; protected set; } = Guid.NewGuid();

    
        public string? CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; } 
        public string? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
