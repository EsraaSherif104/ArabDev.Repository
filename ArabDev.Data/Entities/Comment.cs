using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Entities
{
    public class Comment : BaseEntity<int>
    {

        public string Text { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Comments")]
        public User User { get; set; }
        public int UserId { get; set; }

        [ForeignKey("PostId")]
        [InverseProperty("Comments")]
        public Post Post { get; set; }
        public int? PostId { get; set; }
        [ForeignKey("PodCastId")]
        [InverseProperty("Comments")]
        public PodCast PodCast { get; set; }
        public int? PodCastId { get; set; }






    }
}