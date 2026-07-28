using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFCORE_02_task.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public double ProductPrice { get; set; }
        public string ProductName { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List <OrderProduct> OrderProduct { get; set; }

    }
}
