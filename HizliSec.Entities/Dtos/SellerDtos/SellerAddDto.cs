using HizliSec.Core.Entities;
using HizliSec.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HizliSec.Entities.Dtos.SellerDtos
{
    public class SellerAddDto : UserDto,IDto
    {
        public string CompanyName { get; set; }
    }
}
