using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    public class Repsonse
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public object? developerMessage { get; set; }
        public object? data { get; set; }

        public Repsonse(string message, object developerMessage = null, object data = null)
        {
            this.statusCode = "200";
            this.message = message;
            this.developerMessage = developerMessage;
            this.data = data;
        }
    }
}