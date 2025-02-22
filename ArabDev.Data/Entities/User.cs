using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArabDev.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [MaxLength(200)]

        public string Address { get; set; }
        public string Job { get; set; }

        public string? PictureUrl { get; set; }

        public List<string> Interests { get; set; } = new List<string>();


        // Navigation Properties
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<User_Learning> User_Learnings { get; set; } = new List<User_Learning>();
        public ICollection<Follower> Followers { get; set; } = new List<Follower>(); // الأشخاص الذين يتابعونني
        public ICollection<Follower> Following { get; set; } = new List<Follower>(); // الأشخاص الذين أتابعهم

        public ICollection<Skills> Skills { get; set; } = new List<Skills>();
        public ICollection<Likes> Likes { get; set; } = new List<Likes>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<SavedPost> SavedPosts { get; set; } = new List<SavedPost>();
        public ICollection<SavedPodcast> SavedPodcast { get; set; } = new List<SavedPodcast>();

        public ICollection<Shares> Shares { get; set; } = new List<Shares>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<PodCast> PodCasts { get; set; } = new List<PodCast>();








    }
}