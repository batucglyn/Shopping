﻿using Discount.DTOs;
using MediatR;

namespace Discount.Commands
{
    public record class UpdateDiscountCommand(int Id, string ProductName, string Description, int Amount) : IRequest<CouponDto>;

}
