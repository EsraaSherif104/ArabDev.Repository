using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Entities
{
    public class Notification : BaseEntity<int>
    {
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public User Users { get; set; }
        public string UserId { get; set; }
        public Post Post { get; set; }
        public int? PostId { get; set; }
    }
}