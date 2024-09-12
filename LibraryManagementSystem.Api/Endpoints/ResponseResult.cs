using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//great use of generics for this...


//this class does not belong in the same folder as the Endpoints.  
// it  should be in a folder to represent the objects within
namespace LibraryManagementSystem.Api.Endpoints
{
    public class ResponseResult<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        
    }
}
