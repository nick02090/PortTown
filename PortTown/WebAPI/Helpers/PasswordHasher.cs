using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace WebAPI.Helpers
{
    public class PasswordHasher
    {
        public readonly RandomNumberGenerator _rng;

        private const KeyDerivationPrf Pbdkdf2Prf = KeyDerivationPrf.HMACSHA1;
        private const int Pbdkdf2IterCount = 1000;
        private const int Pbdkdf2SubkeyLength = 256 / 8;
        private const int SaltSize = 128 / 8;

        public PasswordHasher() => _rng = new RNGCryptoServiceProvider();

        public virtual string HashPassword(string password)
        {
            return Convert.ToBase64String(HashPasswordV2(password, _rng));
        }

        public virtual bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);
            return VerifyHashedPasswordV2(decodedHashedPassword, password);
        }

        private static byte[] HashPasswordV2(string password, RandomNumberGenerator rng)
        {
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbdkdf2Prf, Pbdkdf2IterCount, Pbdkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbdkdf2SubkeyLength];
            outputBytes[0] = 0x00;
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbdkdf2SubkeyLength);
            return outputBytes;
        }

        private static bool VerifyHashedPasswordV2(byte[] hashedPassword, string password)
        {
            if (hashedPassword.Length != 1 + SaltSize + Pbdkdf2SubkeyLength)
            {
                return false;
            }

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbdkdf2SubkeyLength];
            Buffer.BlockCopy(hashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, Pbdkdf2Prf, Pbdkdf2IterCount, Pbdkdf2SubkeyLength);
            return ByteArraysEqual(actualSubkey, expectedSubkey);
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}