using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.Users
{
    public class PasswordValidator<T> : PropertyValidator<T, string> // validar propriedad strig -> Senha
    {
        public override string Name => "PasswordValidator";

        public override bool IsValid(ValidationContext<T> context, string password)
        {
            
        }
    }
}
