using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Entities
{
    public class PodCast : BaseEntity<int>
    {
        public string PodCastDetails { get; set; }
        public ICollection<string> PodCastUrl { get; set; } = new List<string>();


        public User User { get; set; }
        public int? UserId { get; set; }


        public ICollection<Likes> Likes { get; set; } = new List<Likes>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<SavedPodcast> SavedPodcasts { get; set; } = new List<SavedPodcast>();


    }
}