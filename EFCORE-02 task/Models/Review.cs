using System;
using System.Collections.Generic;
using System.Text;

namespace EFCORE_02_task.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Ratings { get; set; }
        public string Comment { get; set; }
    }
}
