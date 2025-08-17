using Microsoft.AspNetCore.Mvc;
using PedidoService.Application.DTOs;
using PedidoService.Application.Interfaces;

namespace PedidoService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService) => _pedidoService = pedidoService;

        [HttpPost]
        public async Task<IActionResult> CreatePedido([FromBody] PedidoCreateDTO pedidoCreateDTO)
        {
            var pedidoId = await _pedidoService.CreateAsync(pedidoCreateDTO);
            if (pedidoId == null)
            {
                return BadRequest("Não foi possível criar o pedido. Verifique os dados do restaurante e dos itens.");
            }
            return Ok(new { PedidoId = pedidoId });
        }
    }
}
