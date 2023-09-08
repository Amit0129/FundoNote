using CommonLayer.Models;
using MassTransit;
using System.Drawing;
using System.Threading.Tasks;

namespace ColabConsumer.Consumer
{
    public class ConsumerColab : IConsumer<AddCollabModel>
    {
        public async Task Consume(ConsumeContext<AddCollabModel> context)
        {
            var data = context.Message;
            //SendMessageRabbitMq sendMessage = new SendMessageRabbitMq();
            //sendMessage.EmailService(colab.CollabEmail);
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
        }
    }
}
