using HizliSec.Business.Abstract;
using HizliSec.Entities.Concrete;
using HizliSec.Entities.Dtos.OrderDtos;

namespace HizliSec.Business.Concrete
{
    public class OrderProcessManager : IOrderProcessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public OrderProcessManager(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        //public async Task<int> AddOrderAsync(OrderAddDto orderAddDto)
        //{
        //    using (TransactionScope ts = new TransactionScope())
        //    {
        //        int response;
        //        try
        //        {
        //            AppUser user = _unitOfWork.AuthService.GetUserAsync(userName).Result;
        //            Product product = _unitOfWork.ProductDal.GetAsync(x => x.Id == productId).Result;
        //            Order order = new Order()
        //            {
        //                CustomerId = user.Id
        //            };
        //            await _unitOfWork.OrderDal.AddAsync(order);
        //            await _unitOfWork.SaveAsync();
        //            OrderDetail orderDetail = new OrderDetail()
        //            {
        //                ProductId = product.Id,
        //                OrderId = order.Id,
        //                Price = product.Price * quantity,
        //                ProductName = product.Name,
        //                Quantity = quantity
        //            };
        //            await _unitOfWork.OrderDetailDal.AddAsync(orderDetail);
        //            product.StockAmount -= quantity;
        //            await _unitOfWork.ProductDal.UpdateAsync(product);
        //            response = await _unitOfWork.SaveAsync();
        //            ts.Complete();
        //        }
        //        catch (Exception)
        //        {
        //            response = 0;
        //            ts.Dispose();
        //        }
        //        return response;
        //    }
        //}
        public async Task<int> AddOrderAsync(OrderAddDto orderAddDto)
        {
                int response;
                try
                {
                    AppUser user = _authService.GetUserAsync(orderAddDto.UserName).Result;
                    Product product = _unitOfWork.ProductDal.GetAsync(x => x.Id == orderAddDto.ProductId).Result;
                    Order order = new Order()
                    {
                        CustomerId = user.Id
                    };
                    await _unitOfWork.OrderDal.AddAsync(order);
                    await _unitOfWork.SaveAsync();
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        ProductId = product.Id,
                        OrderId = order.Id,
                        Price = product.Price * orderAddDto.Quantity,
                        ProductName = product.Name,
                        Quantity = orderAddDto.Quantity
                    };
                    await _unitOfWork.OrderDetailDal.AddAsync(orderDetail);
                    product.StockAmount -= orderAddDto.Quantity;
                    await _unitOfWork.ProductDal.UpdateAsync(product);
                    response = await _unitOfWork.SaveAsync();
                }
                catch (Exception)
                {
                    response = 0;
                    _unitOfWork.Dispose();
                }
                return response;
            }
        }
    }