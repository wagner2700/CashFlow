using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            // Origem - destino
            CreateMap<RequestExpenseJson, Expense>();
            // Desconsiderar senha
            CreateMap<RequestRegisterUserJson, User>().ForMember( dest => dest.Password, config => config.Ignore());
        }

        private void EntityToResponse()
        {
            // Origem - destino
            CreateMap<Expense  , ResponseRegisterExpenseJson>();
            CreateMap<Expense  , ResponseShortExpenseJson>();
            CreateMap<Expense  , ResponseExpenseJson>();
            
        }
    }
}
