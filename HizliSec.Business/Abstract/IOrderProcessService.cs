

using HizliSec.Entities.Dtos.OrderDtos;

namespace HizliSec.Business.Abstract
{
    public interface IOrderProcessService
    {
        Task<int> AddOrderAsync(OrderAddDto orderAddDto);
    }
}
