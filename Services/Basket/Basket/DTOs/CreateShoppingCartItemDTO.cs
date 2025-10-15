namespace Basket.DTOs
{
    public record class CreateShoppingCartItemDTO
    {

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageFile { get; set; }

    }

    public record class ShoppingCartDTO(

        string UserName,
       List<ShoppingCartItemDTO> Items,
        decimal TotalPrice

        );


    public record class ShoppingCartItemDTO(

        string ProductId,
        string ProductName,
        int Quantity,
        decimal Price,
        string ImageFile
     

        );

    public record BasketCheckOutDto(

          string UserName,
            decimal TotalPrice,
            string FirstName,
            string LastName,
            string EmailAddress,
            string AddressLine,
            string Country,
            string State,
            string ZipCode,
            string CardName,
            string CardNumber,
            string Expiration,
            string Cvv,
            int PaymentMethod

       );






}
