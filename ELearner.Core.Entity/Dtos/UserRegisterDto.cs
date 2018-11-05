using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Dtos {
    public class UserRegisterDto {
        public string Username { get; set; }
        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
