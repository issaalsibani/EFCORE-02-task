using System;
using System.Collections.Generic;
using System.Text;

namespace EFCORE_02_task.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }
    }
}
