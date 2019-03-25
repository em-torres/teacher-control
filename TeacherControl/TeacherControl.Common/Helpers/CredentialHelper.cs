using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TeacherControl.Core.Models;

namespace TeacherControl.Common.Helpers
{
    public static class CredentialHelper
    {
        public static string CreatePasswordHash(string Password, string SaltKey)
        {
            if (Password is null) throw new ArgumentException("Password");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value can not be Null Or Empty Or WhiteSpace", Password);

            using (HMACSHA512 hmac = new HMACSHA512(Encoding.UTF8.GetBytes(SaltKey)))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return Convert.ToBase64String(computedHash);
            }
        }

        public static bool VerifyPasswordHash(string Password, string StoredHash, string StoredSalt)
        {
            if (Password is null) throw new ArgumentException("Password");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value can not be Null Or Empty Or WhiteSpace", Password);
            if (StoredHash.Length != 88) throw new ArgumentException("Invalid length of Password HASH (64 bytes expected)", StoredHash);
            if (StoredSalt.Length != 32) throw new ArgumentException("Invalid length of Password Salt (64 bytes expected)", StoredSalt);

            using (HMACSHA512 hmac = new HMACSHA512(Encoding.UTF8.GetBytes(StoredSalt)))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return computedHash.SequenceEqual(Encoding.UTF8.GetBytes(StoredHash));
            }

        }
    }
}
