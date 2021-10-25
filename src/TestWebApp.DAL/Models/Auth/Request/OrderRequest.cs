using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApp.DAL.Models.Entities;

namespace TestWebApp.DAL.Models.Auth.Request
{
    public class OrderRequest
    {
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}