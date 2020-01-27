using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankServiceApi.Data
{
    public class BankMasterRepository : IBankMasterRepository
    {
        private readonly BankServiceContext _context;
        public BankMasterRepository(BankServiceContext context)
        {
            _context = context;

        }

        public async Task<BankMaster> CreateBankMaster(BankMaster bankMaster)
        {
            try{
                await _context.BankMasters.AddAsync(bankMaster);
                await _context.SaveChangesAsync();

                return bankMaster;
            }
            catch(Exception){}
            return null;
        }

        public void DeleteBankMaster(int bankMasterId)
        {
            var bank = _context.BankMasters.FirstOrDefault(b=>b.Id == bankMasterId);
            _context.BankMasters.Remove(bank);
            _context.SaveChanges();
        }

        public BankMaster EditBankMaster(BankMaster bankMaster)
        {
            try{
                _context.BankMasters.Update(bankMaster);
                _context.SaveChanges();

                return bankMaster;
            }
            catch(Exception){}
            return null;
        }

        public async Task<IEnumerable<BankMaster>> GetAllBankMaster()
        {
            var banks = await _context.BankMasters.ToListAsync();
            if(banks!=null){
                return banks;
            }
            return null;
        }

        public async Task<BankMaster> GetBankMasterById(int bankMasterId)
        {
            var bank = await _context.BankMasters.FirstOrDefaultAsync(b=>b.Id == bankMasterId);
            if(bank!=null){
                return bank;
            }
            return null;
        }
    }
}