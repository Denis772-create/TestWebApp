using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using TestWebApp.DAL.Models.Entities;

namespace TestWebApp.DAL.Models.ReviewModels
{
    public class Room
    {
        public Room()
        {
            Messages = new List<Message>();
        }
        public  int Id { get; set; }
        public  string Name { get; set; }
        public  User Admin { get; set; }
        public  ICollection<Message> Messages { get; set; }
    }
}
