using FluentValidation;
using FurnitureShop.Domain.Dtos.UserDtos;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Domain.FluentValidation
{
    public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginDtoValidator()
        {
            RuleFor(user => user.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
