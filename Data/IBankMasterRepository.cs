using System.Collections.Generic;
using System.Threading.Tasks;
using BankServiceApi.Models;

namespace BankServiceApi.Data
{
    public interface IBankMasterRepository
    {
         Task<BankMaster> CreateBankMaster(BankMaster bankMaster);
         BankMaster EditBankMaster(BankMaster bankMaster);
         void DeleteBankMaster(int bankMasterId);
         Task<IEnumerable<BankMaster>> GetAllBankMaster();
         Task<BankMaster> GetBankMasterById(int bankMasterId);
    }
}