using HizliSec.Business.Abstract;
using HizliSec.WebUI.BasketTransaction;
using HizliSec.WebUI.BasketTransaction.BasketModels;
using Microsoft.AspNetCore.Mvc;

namespace HizliSec.WebUI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketTransaction _basketTransaction;

        public BasketController(IBasketTransaction basketTransaction)
        {
            _basketTransaction = basketTransaction;
        }
        public IActionResult Index(string message="")
        {
            ViewBag.Message = message;
            Basket basket = _basketTransaction.GetOrCreateBasket();
            return View(basket);
        }
        public IActionResult Decrease(int id) 
        {
            _basketTransaction.Decrease(id);
            return RedirectToAction("Basket");
        }
        public IActionResult Increase(int id)
        {
            _basketTransaction.Increase(id);
            return RedirectToAction("Basket");
        }
        public IActionResult RemoveItem(int id)
        {
            _basketTransaction.DeleteItem(id);
            return RedirectToAction("Basket");
        }
        public IActionResult RemoveBasket()
        {
            _basketTransaction.DeleteBasket();
            return RedirectToAction("Basket");
        }

        public IActionResult AddBasketItem(BasketItem basketItem)
        {
            _basketTransaction.AddOrUpdateItem(basketItem);
            return RedirectToAction("Index","Product");
        }

    }
}
