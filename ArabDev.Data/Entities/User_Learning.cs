using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Entities
{
    public class User_Learning : BaseEntity<int>
    {
        public List<string> LearningNames { get; set; } = new List<string>();
        [ForeignKey("UserId")]

        public User User { get; set; }

        public string UserId { get; set; }
    }
}