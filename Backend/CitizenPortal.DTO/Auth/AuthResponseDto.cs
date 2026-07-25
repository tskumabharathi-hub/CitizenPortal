using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.DTO.Auth
{
    public class AuthResponseDto
    {
        public bool IsSuccess {  get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
