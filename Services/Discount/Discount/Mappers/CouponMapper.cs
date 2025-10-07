using Discount.Commands;
using Discount.DTOs;
using Discount.Entities;
using System.Runtime.CompilerServices;
using Discount.Grpc.Protos;
namespace Discount.Mappers
{
    public static class CouponMapper
    {


        public static CouponDto ToDto(this Coupon coupon)
        {
            return new CouponDto(
                coupon.Id,
                coupon.ProductName,
                coupon.Description,
                coupon.Amount

                );                        
        }
        public static  Coupon ToEntity(this CreateDiscountCommand command)
        {
            return new Coupon
            {
                Amount = command.Amount,
                Description = command.Description,
                ProductName
                = command.ProductName
            };
        }
        public static Coupon ToEntity(this UpdateDiscountCommand command)
        {
            return new Coupon
            {
                Amount = command.Amount,
                Description = command.Description,
                ProductName
                = command.ProductName,
                Id = command.Id

            };
        }
        public static CouponModel ToModel(this CouponDto dto)
        {
            return new CouponModel
            {
                Id = dto.Id,
                ProductName = dto.ProductName,
                Description = dto.Description,
                Amount = dto.Amount
            };
        }
        public static CreateDiscountCommand ToCreateCommand(this CouponModel model)
        {
            return new CreateDiscountCommand(
                    model.ProductName,
                    model.Description,
                    model.Amount
                );
        }
        public static UpdateDiscountCommand ToUpdateCommand(this CouponModel model)
        {
            return new UpdateDiscountCommand(
                    model.Id,
                    model.ProductName,
                    model.Description,
                    model.Amount
                );
        }






    }
}
