using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCORE_02_task.Models
{
    public class OrderProduct
    {

        [ForeignKey("products")]
        public int ProductId { get; set; }

        public Product products { get; set; }


        [ForeignKey("orders")]
        public int OrderId { get; set; }

        public Order orders { get; set; }
        public int Quantity { get; set; }
    }
}
