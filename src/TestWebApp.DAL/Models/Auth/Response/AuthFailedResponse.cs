using System.Collections.Generic;

namespace TestWebApp.DAL.Models.Auth.Response
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}