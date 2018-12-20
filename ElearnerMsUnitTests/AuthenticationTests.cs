using ELearner.Core.ApplicationService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElearnerMsUnitTests {

    [TestClass]
    public class AuthenticationTests {

        [TestMethod]
        public void VerifyPasswordTest() {
            var service = new AuthService(null, null);
            string password = "supersecretpassword";
            byte[] hash, salt;
            service.CreatePasswordHash(password, out hash, out salt);
            bool isVerified = service.VerifyPasswordHash(password, hash, salt);
            Assert.IsTrue(isVerified);
        }
        
        [TestMethod]
        public void CheckIfSaltingWorks() {
            var service = new AuthService(null, null);
            string password = "supersecretpassword";
            byte[] hash, salt;
            byte[] hash2, salt2;
            service.CreatePasswordHash(password, out hash, out salt);
            service.CreatePasswordHash(password, out hash2, out salt2);

            bool difference = false;
            for (int i = 0; i < hash.Length; i++) {
                if (hash[i] != hash2[i]) {
                    difference = true;
                    break;
                }
            }
            Assert.IsTrue(difference);
        }
    }


}
