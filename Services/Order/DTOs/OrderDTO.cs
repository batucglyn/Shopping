namespace Ordering.DTOs;

public record class OrderDTO(
     Guid Id,
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

public record class CreateOrderDTO(
    
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
public record class UpdateOrderDTO(
     Guid Id,
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