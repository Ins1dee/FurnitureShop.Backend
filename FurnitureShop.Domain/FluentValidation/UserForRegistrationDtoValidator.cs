using FluentValidation;
using FurnitureShop.Domain.Dtos.UserDtos;
using FurnitureShop.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Domain.FluentValidation
{
    public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationDtoValidator()
        {
            RuleFor(user => user.Password)
                .NotEmpty()
                .NotNull()
                .Password();

            RuleFor(user => user.Email)
                .NotNull()
                .EmailAddress();
        }
    }
}
