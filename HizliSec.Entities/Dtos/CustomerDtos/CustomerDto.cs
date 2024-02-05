using HizliSec.Core.Entities;
using HizliSec.Entities.Abstract;
namespace HizliSec.Entities.Dtos.CustomerDtos
{
    public class CustomerDto : UserDto,IDto
    {
        public int Id { get; set; }
    }
}
