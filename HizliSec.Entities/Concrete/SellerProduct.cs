using HizliSec.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HizliSec.Entities.Concrete
{
    public class SellerProduct : IEntity
    {
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        int quantity = 0;
        public int Quantity
        {
            get;
            set
            ;

        }
        public Seller Seller { get; set; }
        public Product Product { get; set;}
    }
}
