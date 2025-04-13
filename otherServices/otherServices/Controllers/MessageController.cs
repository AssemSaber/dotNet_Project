using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using otherServices.Data_Project.Models;
using Confluent.Kafka;
using Newtonsoft.Json;
using otherServices.Data_Project.service;

namespace otherServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly AppDbContext2 _db;
        private readonly KafkaProducerService _kafkaService;
        public MessageController(AppDbContext2 db, KafkaProducerService kafkaService)
        {
            this._db = db;
            _kafkaService = kafkaService;
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> sendMessageAsync([FromBody] MessageDTo MessageDTo)
        {
            var message = new Message() {
                LandlordId= MessageDTo.sender_Id,
                TenantId = MessageDTo.reciever_Id,
                Message1= MessageDTo.Message,
                DateMessge= DateTime.Now
            };
            _db.Messages.Add(message);
            _db.SaveChanges(); // we save in other service db


            var kafkaMessage = new
            {
                sender_Id = MessageDTo.sender_Id,
                receiver_Id = MessageDTo.reciever_Id,
                message = MessageDTo.Message,
                date_message = DateTime.Now
            };


            string jsonMessage = JsonConvert.SerializeObject(kafkaMessage);

            // 3. Send to Kafka topic "messages"
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            await producer.ProduceAsync("messages", new Message<Null, string> { Value = jsonMessage }); // we send

            return Ok(new { status = "Message saved and sent to Kafka" });

        }


    }
}
