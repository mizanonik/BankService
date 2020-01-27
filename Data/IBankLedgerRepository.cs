using System.Collections.Generic;
using System.Threading.Tasks;
using BankServiceApi.Models;

namespace BankServiceApi.Data
{
    public interface IBankLedgerRepository
    {         
         Task<BankLedger> CreateBankLedger(BankLedger bankLedger);
         BankLedger EditBankLedger(BankLedger bankLedger);
         void DeleteBankLedger(int bankLedgerId);
         Task<IEnumerable<BankLedger>> GetAllBankLedger();
         Task<BankLedger> GetBankLedgerById(int bankLedgerId); 
    }
}