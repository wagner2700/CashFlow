﻿using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public  ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            // to do validation


            return new ResponseRegisterExpenseJson();
        }
    }
}
