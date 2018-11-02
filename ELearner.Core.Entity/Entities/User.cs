using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Entities {
    public class User {
        public string Username { get; set; }
        public int Id { get; set; }

        public List<UserCourse> Courses { get; set; }
    }
}
