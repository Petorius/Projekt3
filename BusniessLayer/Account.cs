using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Server.Domain;

namespace Server.BusinessLogic {
    public class Account {
        private const int SaltByteLength = 24;
        private const int DerivedKeyLength = 24;
        private const int IterationCount = 32000;

        public string CreatePasswordHash(string password) {
            var salt = GenerateRandomSalt();
            byte[] hashValue = GenerateHashValue(password, salt, IterationCount);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hashValue);
        }

     public bool ValidateUserLogin(User user, string password) {
            bool res = false;
            byte[] userSalt = Convert.FromBase64String(user.Salt);
            byte[] userHash = Convert.FromBase64String(user.HashPassword);
            byte[] hashValue = GenerateHashValue(password, userSalt, IterationCount);
            if(userHash.SequenceEqual(hashValue)) {
                res = true;
            }
            return res;
        }

        private static byte[] GenerateRandomSalt() {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteLength];
            csp.GetBytes(salt);
            return salt;
        }

        private static byte[] GenerateHashValue(string password, byte[] salt, int iterationCount) {
            byte[] hashValue;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterationCount)) {
                hashValue = pbkdf2.GetBytes(DerivedKeyLength);
            }
            return hashValue;
        }


    }
}
