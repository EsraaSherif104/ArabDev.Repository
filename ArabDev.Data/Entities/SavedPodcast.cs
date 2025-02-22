using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Entities
{
    public class SavedPodcast : BaseEntity<int>
    {
        public DateTime SavedTime { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

        public PodCast PodCast { get; set; }
        public int PodCastId { get; set; }



    }
}