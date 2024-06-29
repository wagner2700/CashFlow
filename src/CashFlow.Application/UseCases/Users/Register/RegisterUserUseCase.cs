using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Infrastructure.Exceptions;

namespace CashFlow.Application.UseCases.Users.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase 
    {
        private readonly IMapper _mapper;
        private readonly IPasswordEncripter _encripter;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAcessTokenGenerator _tokenGenerator;
        
        public RegisterUserUseCase(
            IMapper mapper ,
            IPasswordEncripter encripter,
            IUserReadOnlyRepository userReadOnlyRepository,
            IUserWriteOnlyRepository userWriteOnlyRepository,
            IUnitOfWork unitOfWork,
            IAcessTokenGenerator acessTokenGenerator
            )
        {
            _mapper = mapper;
            _encripter = encripter;
            _userReadOnlyRepository = userReadOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _unitOfWork = unitOfWork;
            _tokenGenerator = acessTokenGenerator;
        }


        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
            await Validate(request);    

            
            var user = _mapper.Map<User>(request);
            // Criptografar senha
            user.Password = _encripter.Encrypt(request.Password);
            user.UserIdentifier = Guid.NewGuid();

            await _userWriteOnlyRepository.Add(user);
            await _unitOfWork.Commit();

            return new ResponseRegisterUserJson
            {
                Name = user.Name,
                Token = _tokenGenerator.Generate(user)
            };
        }


        public async Task Validate(RequestRegisterUserJson request)
        {
            var result = new RegisterUserValidator().Validate(request);

            var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

            // Adicionar mensagem caso email já cadstrado
            if (emailExist)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty,ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            }

            if (result.IsValid == false) { 
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
