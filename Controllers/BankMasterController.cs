using System;
using System.Text;
using System.Threading.Tasks;
using BankServiceApi.Data;
using BankServiceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BankServiceApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BankMasterController : ControllerBase
    {
        private readonly IBankMasterRepository _repository;
        public BankMasterController(IBankMasterRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBank([FromBody] BankMaster bankMaster){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var savedBank = await _repository.CreateBankMaster(bankMaster);
            if(savedBank == null){
                return BadRequest("Failed to save the bank");
            }
            return Ok(bankMaster);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBank(){
            var banks = await _repository.GetAllBankMaster();
            if(banks == null){
                return BadRequest("No Bank found");
            }
            return Ok(banks);
        }
        [HttpGet]
        public async Task<IActionResult> GetBankById(int bankMasterId){
            var bank = await _repository.GetBankMasterById(bankMasterId);
            if(bank == null){
                return BadRequest("Bank not found");
            }
            return Ok(bank);
        }
        [HttpPut]
        public IActionResult EditBank([FromBody] BankMaster bankMaster){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var editedBank = _repository.EditBankMaster(bankMaster);
            if(editedBank == null){
                return BadRequest("Failed to save the bank");
            }
            return Ok(editedBank);
        }
        [HttpDelete]
        public IActionResult DeleteBank(int bankMasterId){
            
            try{
                _repository.DeleteBankMaster(bankMasterId);
            }
            catch(Exception){
                return BadRequest("Failed to delete the bank");
            }
            return Ok("Bank Deleted ");
        }
        [HttpGet]
        public IActionResult GetCustomerData(){            
            try{
                var message = "";
                var factory = new ConnectionFactory(){HostName = "rabbitmq"};
                using(var connection = factory.CreateConnection()){
                    using(var channel = connection.CreateModel()){
                        channel.QueueDeclare(
                            queue : "customer_queue",
                            durable: true,
                            exclusive : false,
                            autoDelete: false,
                            arguments: null
                        );

                        channel.BasicQos(
                            prefetchSize: 0,
                            prefetchCount: 1,
                            global: false
                        );

                        var consumer = new EventingBasicConsumer(channel);
                        byte[] body;
                        consumer.Received += (model, ea) => {
                            body = ea.Body;
                            message = Encoding.UTF8.GetString(body);
                            //var customer = JsonConvert.DeserializeObject(message);
                            channel.BasicAck(
                                deliveryTag: ea.DeliveryTag,
                                multiple: false
                            );
                        };

                        channel.BasicConsume(
                            queue: "customer_queue",
                            autoAck: false,
                            consumer: consumer
                        );
                    }
                }
                return Ok(message);
            }
            catch(Exception){}
            return BadRequest("No customer data received");
        }
        // public IActionResult GetCustomerData(){            
        //     try{
        //         var message = "";
        //         var factory = new ConnectionFactory(){HostName = "localhost"};
        //         using(var connection = factory.CreateConnection()){
        //             using(var channel = connection.CreateModel()){
        //                 channel.ExchangeDeclare(
        //                     exchange: "customerInfo",
        //                     type: ExchangeType.Fanout
        //                 );

        //                 var queueName = channel.QueueDeclare().QueueName;
        //                 channel.QueueBind(
        //                     queue: queueName,
        //                     exchange: "customerInfo",
        //                     routingKey: ""
        //                 );
        //                 var consumer = new EventingBasicConsumer(channel);
        //                 byte[] body;
        //                 consumer.Received += (model, ea) => {
        //                     body = ea.Body;
        //                     message = Encoding.UTF8.GetString(body);
        //                     //var customer = JsonConvert.DeserializeObject(message);
        //                 };

        //                 channel.BasicConsume(
        //                     queue: queueName,
        //                     autoAck: true,
        //                     consumer: consumer
        //                 );
        //             }
        //         }
        //         return Ok(message);
        //     }
        //     catch(Exception){}
        //     return BadRequest("No customer data received");
        // }
    }
}