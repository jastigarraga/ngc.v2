using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Linq;
using NGC.Model;
using NGC.Common.Classes;

namespace NGC.Common.Helpers
{
    public static class SecurityHelper
    {
        public static Password Hash(string password, string salt = null)
        {
            byte[] saltBytes;
            if(salt == null)
            {
                using(var rnd = RandomNumberGenerator.Create())
                {
                    saltBytes = new byte[128 / 8];
                    rnd.GetBytes(saltBytes);
                }
            }
            else
            {
                saltBytes = salt.Select(c => (byte)c).ToArray();
            }

            return new Password()
            {
                Salt = new string(saltBytes.Select(b=>(char)b).ToArray()),
                Hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 1000,
                    numBytesRequested: 132
                    ))
            };
        }
        public static string CreateSessionID()
        {
            byte[] bytes = new byte[32];
            using (var rnd = RandomNumberGenerator.Create())
            {
                rnd.GetBytes(bytes);
            }
            return new string(bytes.Select(b => (char)b).ToArray());
        }
        public static bool PasswordVerify(this User user, string password)
        {
            return Verify(password, user.Salt, user.Password);
        }
        public static void UsetRawPassword(this User user,string password, string salt = null)
        {
            var p = Hash(password, salt);
            user.Password = p.Hash;
            user.Salt = p.Salt;
        }
        public static bool Verify(string raw,string salt,string hash)
        {
            Password password = Hash(raw, salt);
            return hash == password.Hash;
        }
        public static bool Verify(this Password password, string raw)
        {
            return Verify(raw, password.Salt, password.Hash);
        }
    }
}
