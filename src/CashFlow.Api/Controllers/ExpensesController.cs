﻿using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterExpenseJson ),  StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson ),  StatusCodes.Status400BadRequest)]
        public async Task< IActionResult > Register([FromServices] IRegisterExpenseUseCase useCase , [FromBody] RequestExpenseJson request)
        {
            

            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
    

        [HttpGet]
        [ProducesResponseType(typeof(ResponseExpensesJson) , StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpenseUseCase useCase)
        {
            var response = await useCase.Execute();

            if (response.Expenses.Count != 0)
            {
                return Ok(response);
            }
            

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromServices] IGetExpenseByIdUseCase useCase, [FromRoute] long id)
        {
            var result = await useCase.Execute(id);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {


            return NoContent();
        }


        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateExpenseUseCase useCase,
            [FromRoute] long id,
            [FromBody] RequestExpenseJson request
            )
        {
            var result = useCase.Execute(id, request);
            return Ok(result);

        }
    }
}
