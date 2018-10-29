﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.BusinessObjects
{
    // In our Onion architecture we have independent object models that is in center (Our entities).  
    // And we build our application around these entities.

    public class StudentBO
    {
        public string Username { get; set; }
        public int Id { get; set; }

        public List<int> CourseIds { get; set; }
        public List<CourseBO> Courses { get; set; }
    }
}
