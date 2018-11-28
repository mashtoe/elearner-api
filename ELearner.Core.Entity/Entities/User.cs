using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Entities {
    public class User {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<UserCourse> Courses { get; set; }
        public Role Role { get; set; }

        //public int? ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
