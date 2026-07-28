using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCORE_02_task.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderDated { get; set; }

        public List<OrderProduct> Products { get; set; }



        [ForeignKey("users")]
        public int UserId { get; set; }

        public User users { get; set; }

        public Review reviews { get; set; }
    }
}
