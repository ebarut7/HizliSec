using HizliSec.Business.Abstract;
using HizliSec.Entities.Dtos.OrderDtos;
using HizliSec.WebUI.BasketTransaction;
using HizliSec.WebUI.BasketTransaction.BasketModels;
using Microsoft.AspNetCore.Mvc;

namespace HizliSec.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderProcessService _orderProcessService;
        private readonly IBasketTransaction _basketTransaction;

        public OrderController(IOrderProcessService orderProcessService, IBasketTransaction basketTransaction)
        {
            _orderProcessService = orderProcessService;
            _basketTransaction = basketTransaction;
        }

        public async Task<IActionResult> Index()
        {
            Basket basket = _basketTransaction.GetOrCreateBasket();
            int response = 0;
            basket.BasketItems.ForEach(async item =>
            {
                response = await _orderProcessService.AddOrderAsync(new OrderAddDto()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UserName = User.Identity.Name
                });
            });
            if (response>0)
            {
                _basketTransaction.DeleteBasket();
            }
            return response > 0 ? RedirectToAction("Index", "Basket", new { message = "Siparişiniz olusturuldu." }):RedirectToAction("Index", "Basket",new {message="Bir sorun oluştu."});
        }
    }
}
