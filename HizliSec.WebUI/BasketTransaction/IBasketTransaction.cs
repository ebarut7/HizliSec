using HizliSec.WebUI.BasketTransaction.BasketModels;

namespace HizliSec.WebUI.BasketTransaction
{
    public interface IBasketTransaction
    {
        Basket GetOrCreateBasket();
        void AddOrUpdateItem(BasketItem basketItem);
        void Decrease(int productId);
        void Increase(int productId);
        void DeleteItem(int productId);
        void DeleteBasket();
    }
} 
