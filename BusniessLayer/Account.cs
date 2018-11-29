using System;
using System.Linq;
using System.Security.Cryptography;
using Server.Domain;

namespace Server.BusinessLogic {
    public class Account {
        private const int SaltByteLength = 24;
        private const int DerivedKeyLength = 24;
        private const int IterationCount = 32000;

        // Hashes the password with random generated salt and returns
        // a string with salt and hashvalue.
        public string CreatePasswordHash(string password) {
            var salt = GenerateRandomSalt();
            byte[] hashValue = GenerateHashValue(password, salt, IterationCount);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hashValue);
        }

        // Checks if the given password matches the users password by 
        // hashing it with the same salt and iterations.
        public bool ValidateLogin(string databaseSalt, string databaseHash, string password) {
            bool res = false;
            byte[] salt = Convert.FromBase64String(databaseSalt);
            byte[] hash = Convert.FromBase64String(databaseHash);
            byte[] newHashValue = GenerateHashValue(password, salt, IterationCount);
            if (hash.SequenceEqual(newHashValue)) {
                res = true;
            }
            return res;
        }

        // Helping method used to generate random salt
        private static byte[] GenerateRandomSalt() {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteLength];
            csp.GetBytes(salt);
            return salt;
        }

        // Helping method used to generate a hashvalue for the given password
        private static byte[] GenerateHashValue(string password, byte[] salt, int iterationCount) {
            byte[] hashValue;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterationCount)) {
                hashValue = pbkdf2.GetBytes(DerivedKeyLength);
            }
            return hashValue;
        }


    }
}
