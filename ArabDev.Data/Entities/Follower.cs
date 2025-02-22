using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabDev.Data.Entities
{
    public class Follower
    {
        [ForeignKey("FollowerUser")]
        public string FollowerUserId { get; set; }  // المستخدم الذي يقوم بالمتابعة

        [ForeignKey("FollowedUser")]
        public string FollowedUserId { get; set; }  // المستخدم الذي يتم متابعته

        public User FollowerUser { get; set; }  // المستخدم المتابع
        public User FollowedUser { get; set; }  // المستخدم المتابع له


    }
}