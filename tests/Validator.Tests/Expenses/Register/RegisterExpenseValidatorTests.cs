using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validator.Tests.Expenses.Register
{
    public class RegisterExpenseValidatorTests
    {
        [Fact]
        public void Sucess()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();

            //act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Error_Title_Empty()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Title = string.Empty;

            //act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            // garantir apenas um erro
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
        }

        [Fact]
        public void Error_Date_Future()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            //act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            // garantir apenas um erro
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE));
        }

        [Fact]
        public void Error_Payment_Type_Invalid()
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.paymentType = (PaymentsType)700;

            //act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            // garantir apenas um erro
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
        }

        // Passr parametros
        [Theory]
        // Passar parametros e executar a função quantas vezes necessário
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-7)]
        public void Error_Amount_Invalid(decimal amount)
        {
            // Arrange
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Amount = amount;

            //act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            // garantir apenas um erro
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GRATHER_TAHN_ZERO));
        }
    }


    
}
