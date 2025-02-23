using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Services.Services.DTOS
{
    public class UserupdataPictureDTo
    {
        public string UserId { get; set; }
        public IFormFile Picture { get; set; }
    }
}
