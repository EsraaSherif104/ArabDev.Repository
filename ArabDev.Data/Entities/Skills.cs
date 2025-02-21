using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Entities
{
    public class Skills : BaseEntity<int>
    {
        public List<string> SkillName { get; set; } = new List<string>();

        public User Users { get; set; }
        public int UserId { get; set; }
    }
}