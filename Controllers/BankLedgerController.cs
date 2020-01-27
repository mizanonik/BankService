using System;
using System.Threading.Tasks;
using BankServiceApi.Data;
using BankServiceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankServiceApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BankLedgerController : ControllerBase
    {
        private readonly IBankLedgerRepository _repository;
        public BankLedgerController(IBankLedgerRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBankLedger([FromBody] BankLedger bankLedger){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var savedBank = await _repository.CreateBankLedger(bankLedger);
            if(savedBank == null){
                return BadRequest("Failed to save the bank");
            }
            return Ok(bankLedger);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBankLedger(){
            var bankLedgers = await _repository.GetAllBankLedger();
            if(bankLedgers == null){
                return BadRequest("No Bank Ledger found");
            }
            return Ok(bankLedgers);
        }
        [HttpGet]
        public async Task<IActionResult> GetBankLedgerById(int bankLedgerId){
            var bankLedger = await _repository.GetBankLedgerById(bankLedgerId);
            if(bankLedger == null){
                return BadRequest("Bank Ledger not found");
            }
            return Ok(bankLedger);
        }
        [HttpPut]
        public IActionResult EditBankLedger([FromBody] BankLedger bankLedger){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var editedBankLedger = _repository.EditBankLedger(bankLedger);
            if(editedBankLedger == null){
                return BadRequest("Failed to save the bank ledger");
            }
            return Ok(editedBankLedger);
        }
        [HttpDelete]
        public IActionResult DeleteBankLedger(int bankLedgerId){
            
            try{
                _repository.DeleteBankLedger(bankLedgerId);
            }
            catch(Exception){
                return BadRequest("Failed to delete the bank Ledger");
            }
            return Ok("Bank Ledger Deleted ");
        }
    }
}