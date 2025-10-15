namespace Ordering.Entities
{
    public class OutboxMessage:BaseEntity
    {
        public string Type { get; set; }             // Olayın tipi (örneğin: OrderCreatedEvent)
        public string Content { get; set; }          // Olayın JSON olarak serialize edilmiş hali
        public string CorrelationId { get; set; }    // Saga sürecinde olaylar arası ilişkiyi kurmak için benzersiz ID
        public DateTime OccurredOn { get; set; }     // Olayın gerçekleştiği zaman
        public DateTime? ProcessedOn { get; set; }   // Mesaj RabbitMQ’ya gönderildiğinde işlenme zamanı
        public bool isProcessed => ProcessedOn.HasValue; // Mesaj işlendi mi kontrolü
        public string? Error { get; set; }
    }

}
