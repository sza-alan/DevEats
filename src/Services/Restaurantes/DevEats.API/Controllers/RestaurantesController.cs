using DevEats.Application.DTOs;
using DevEats.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevEats.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantesController : ControllerBase
    {
        private readonly IRestauranteService _restauranteService;

        public RestaurantesController(IRestauranteService restauranteService) => _restauranteService = restauranteService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurantes = await _restauranteService.GetAllAsync();
            return Ok(restaurantes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RestauranteCreateDTO restauranteCreateDTO)
        {
            var id = await _restauranteService.CreateAsync(restauranteCreateDTO);
            return CreatedAtAction(nameof(GetAll), new { id = id }, restauranteCreateDTO);
        }

        [HttpPost("{restauranteId}/menuitems")]
        public async Task<IActionResult> AddMenuItem(Guid restauranteId, [FromBody] MenuItemCreateDTO menuItemCreateDTO)
        {
            var menuItemId = await _restauranteService.AddMenuItemAsync(restauranteId, menuItemCreateDTO);

            if (menuItemId == null)
                return NotFound("Restaurante nao encontrado.");

            return Ok(new { id = menuItemId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var restaurante = await _restauranteService.GetByIdAsync(id);
            if (restaurante == null)
                return NotFound();

            return Ok(restaurante);
        }
    }
}
