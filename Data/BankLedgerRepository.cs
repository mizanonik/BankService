using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankServiceApi.Data
{
    public class BankLedgerRepository : IBankLedgerRepository
    {
        private readonly BankServiceContext _context;
        public BankLedgerRepository(BankServiceContext context)
        {
            _context = context;

        }
        public async Task<BankLedger> CreateBankLedger(BankLedger bankLedger)
        {
            try{
                await _context.BankLedgers.AddAsync(bankLedger);
                await _context.SaveChangesAsync();

                return bankLedger;
            }
            catch(Exception){}
            return null;
        }

        public void DeleteBankLedger(int bankLedgerId)
        {
            var bankLedger = _context.BankLedgers.FirstOrDefault(b=>b.Id == bankLedgerId);
            _context.BankLedgers.Remove(bankLedger);
            _context.SaveChanges();
        }

        public BankLedger EditBankLedger(BankLedger bankLedger)
        {
            try{
                _context.BankLedgers.Update(bankLedger);
                _context.SaveChanges();

                return bankLedger;
            }
            catch(Exception){}
            return null;
        }

        public async Task<IEnumerable<BankLedger>> GetAllBankLedger()
        {
            var bankLedgers = await _context.BankLedgers.ToListAsync();
            if(bankLedgers!=null){
                return bankLedgers;
            }
            return null;
        }

        public async Task<BankLedger> GetBankLedgerById(int bankLedgerId)
        {
            var bankLedger = await _context.BankLedgers.FirstOrDefaultAsync(b=>b.Id == bankLedgerId);
            if(bankLedger!=null){
                return bankLedger;
            }
            return null;
        }
    }
}