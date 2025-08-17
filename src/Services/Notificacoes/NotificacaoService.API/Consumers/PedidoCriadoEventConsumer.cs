using MassTransit;
using Shared.Contracts;

namespace NotificacaoService.API.Consumers
{
    public class PedidoCriadoEventConsumer : IConsumer<PedidoCriadoEvent>
    {
        private readonly ILogger<PedidoCriadoEventConsumer> _logger;

        public PedidoCriadoEventConsumer(ILogger<PedidoCriadoEventConsumer> logger) => _logger = logger;

        public Task Consume(ConsumeContext<PedidoCriadoEvent> context)
        {
            var evento = context.Message;

            _logger.LogInformation("=================================================================");
            _logger.LogInformation($"[Notificação Recebida] Pedido Criado: {evento.IdPedido}");
            _logger.LogInformation($"Valor Total: {evento.ValorTotal:C}");
            _logger.LogInformation($"Enviando notificação para o restaurante: {evento.IdRestaurante}");
            _logger.LogInformation("=================================================================");

            return Task.CompletedTask;
        }
    }
}
