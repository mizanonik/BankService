using System;

namespace BankServiceApi.Models
{
    public class BankMaster
    {        
        public int Id { get; set; }
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public DateTime LastTransactionDate { get; set; }
    }
}