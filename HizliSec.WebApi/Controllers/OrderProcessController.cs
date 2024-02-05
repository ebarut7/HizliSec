using HizliSec.Business.Abstract;
using HizliSec.Entities.Dtos.OrderDtos;
using Microsoft.AspNetCore.Mvc;

namespace HizliSec.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProcessController : ControllerBase
    {
        private readonly IOrderProcessService _orderProcessService;

        public OrderProcessController(IOrderProcessService orderProcessService)
        {
            _orderProcessService = orderProcessService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderAddDto orderAddDto)
        {
            int response = await _orderProcessService.AddOrderAsync(orderAddDto);
            return Ok(response);
        }
    }
}
