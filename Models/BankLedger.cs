using System;

namespace BankServiceApi.Models
{
    public class BankLedger
    {        
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int BankMasterId { get; set; }
        public BankMaster BankMaster { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}