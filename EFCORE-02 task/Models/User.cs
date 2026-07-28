using System;
using System.Collections.Generic;
using System.Text;

namespace EFCORE_02_task.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public List<Order> Orderss { get; set; }
    }
}
