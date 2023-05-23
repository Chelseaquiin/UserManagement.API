using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Models.Dtos.Response
{
    public class AccountResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
