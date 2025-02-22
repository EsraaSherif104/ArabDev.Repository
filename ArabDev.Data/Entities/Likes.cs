using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Entities
{
    public class Likes : BaseEntity<int>
    {
        public List<string> Interacting { get; set; } = new List<string>();
        public Post Post { get; set; }
        public int? PostId { get; set; }
        //---------------------------------
        public User Users { get; set; }
        public string UserId { get; set; }

        public PodCast PodCast { get; set; }
        public int? PodCastId { get; set; }



    }
}