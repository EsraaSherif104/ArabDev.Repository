using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Services.Services.DTOS
{
    public class UserDetailsDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }


        public string Job { get; set; }

        public string PictureUrl { get; set; }



        public DateTime CreatAt { get; set; }
        public List<string> Interests { get; set; }

    }
}