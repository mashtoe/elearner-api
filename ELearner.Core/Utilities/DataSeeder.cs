using ELearner.Core.ApplicationService;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Utilities {
    public class DataSeeder : IDataSeeder {

        readonly IAuthService _authService;

        public DataSeeder(IAuthService authService) {
            _authService = authService;
        }

        public void SeedData() {
            var user1 = new UserRegisterDto() {
               Username = "BoringMan2",
               Password = "secretpassword"
            };
            var user2 = new UserRegisterDto() {
                Username = "FunnyMan2",
                Password = "huuuuuuuuu"
            };
            _authService.Register(user1);
            _authService.Register(user2);
        }
    }
}
