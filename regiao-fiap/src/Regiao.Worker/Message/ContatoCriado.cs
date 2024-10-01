using MassTransit;

namespace Message.Contato
{
    [EntityName("contato_criado")]
    public class ContatoCriado
    {
        public string MessageId { get; set; }
        public string Ddd { get; set; }
    }
}
