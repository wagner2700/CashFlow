﻿using CashFlow.Domain.Entities.Enums;

namespace CashFlow.Domain.Entities
{
    public class Expense
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public DateTime Date { get; set; } 
        public Decimal Amount { get; set; } 
        public PaymentsType PaymentType { get; set; } 
    }
}
