using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class ResponseResult<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        
    }
}
