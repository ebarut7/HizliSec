using HizliSec.WebUI.BasketTransaction.BasketModels;
using Newtonsoft.Json;

namespace HizliSec.WebUI.BasketTransaction
{
    public class BasketTransaction : IBasketTransaction
    {
        private const string basketName = "basket";
        private string serializeItem;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BasketTransaction(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void DeleteItem(int productId)
        {
            Basket basket = GetOrCreateBasket();
            BasketItem basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            basket.BasketItems.Remove(basketItem);
            serializeItem = JsonConvert.SerializeObject(basket);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, serializeItem);
        }
        public Basket GetOrCreateBasket()
        {
            bool cookieCheck = _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(basketName);
            string cooike = _httpContextAccessor.HttpContext.Request.Cookies[basketName];
      
            return cookieCheck ? JsonConvert.DeserializeObject<Basket>(cooike) : new Basket()
            {
                BasketItems = new List<BasketItem>()
            };
        }
        
        public void AddOrUpdateItem(BasketItem _basketItem)
        {
            Basket basket = GetOrCreateBasket();
            BasketItem basketItem = basket.BasketItems.FirstOrDefault(x=>x.ProductId == _basketItem.ProductId);
            if (basketItem is not null) basketItem.Quantity += 1;
            else basket.BasketItems.Add(_basketItem);

            serializeItem = JsonConvert.SerializeObject(basket);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName,serializeItem);
        }
        public void Decrease(int productId)
        {
            Basket basket = GetOrCreateBasket();
            BasketItem basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            if (basketItem.Quantity > 1) basketItem.Quantity -= 1;
            else basket.BasketItems.Remove(basketItem);
            // Nesneyi serialize etmek
            serializeItem = JsonConvert.SerializeObject(basket);
            // Cookie uygulama
            _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, serializeItem);
        }
        public void Increase(int productId)
        {
            Basket basket = GetOrCreateBasket();
            BasketItem basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            basketItem.Quantity += 1;
            serializeItem = JsonConvert.SerializeObject(basket);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(basketName, serializeItem);
        }
        public void DeleteBasket()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(basketName);
        }
    }
}
