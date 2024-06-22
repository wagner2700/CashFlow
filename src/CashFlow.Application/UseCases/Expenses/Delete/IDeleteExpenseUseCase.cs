﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    internal interface IDeleteExpenseUseCase
    {
        Task Execute(long id);
    }
}
