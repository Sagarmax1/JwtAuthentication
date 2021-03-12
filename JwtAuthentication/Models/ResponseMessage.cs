using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Models
{
    public class ResponseData
    {
        public string status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        
        public string errorMessage { get; set; }


        public ResponseData(object Data, string Status = "success", string Message = "")
        {
            this.status = Status;
            if (Status == "success")
            {
                this.data = Data;
                this.message = Message;
            }
            else
            {
                this.errorMessage = Message;
            }
        }

    }
}
