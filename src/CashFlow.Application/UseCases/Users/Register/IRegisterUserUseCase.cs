﻿using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Users.Register
{
    public interface IRegisterUserUseCase
    {

        Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
    }
}
