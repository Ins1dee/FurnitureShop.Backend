using FluentValidation;
using System.Runtime.CompilerServices;

namespace FurnitureShop.WebApi.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static void Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 10)
        {
            ruleBuilder
                .MinimumLength(minimumLength)
                .WithMessage($"Minimum length is {minimumLength}")
                .Matches("[a-z]")
                .WithMessage("Password must have at least one lowercase letter")
                .Matches("[A-Z]")
                .WithMessage("Password must have at least one uppercase letter")
                .Matches("[0-9]")
                .WithMessage("Password must have at least one digit")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("Password must have at least one special character");
        }
    }
}
