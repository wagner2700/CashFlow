using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace CashFlow.Application.UseCases.Users
{
    public class PasswordValidator<T> : PropertyValidator<T, string> // validar propriedad strig -> Senha
    {
        public override string Name => "PasswordValidator";
        private const string ERROR_MESSAGE_KEY = "ErrorMessage";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            // Valor da constante ERROR_MESSAGE_KEY
            return $"{{{ERROR_MESSAGE_KEY}}}";
        }

        // Validar senha
        public override bool IsValid(ValidationContext<T> context, string password)
        {
            // Se senha for vazia
            if (string.IsNullOrEmpty(password))
            {
                // montar mensagem 
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY , ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }   

            // Se senha menor que 8
            if(password.Length < 8)
            {
                // montar mensagem 
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }

            // Validar senha
            // Precisa ter A ate Z
            // + -> pelo menos uma quantidade
            if( Regex.IsMatch(password , @"[A-Z]+") == false)
            {
                // montar mensagem 
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }

            // Validar caracterminusculo
            if (Regex.IsMatch(password, @"[a-z]+") == false)
            {
                // montar mensagem 
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }

            // Validar numerico
            if (Regex.IsMatch(password, @"[0-9]+") == false)
            {
                // montar mensagem 
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }

            // Validar caracteres especiais
            if (Regex.IsMatch(password, @"[\!\?\*\@\.]+") == false)
            {
                // montar mensagem 
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }

            return true;
        }
    }
}
