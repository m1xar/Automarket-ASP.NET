﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Automarket.Domain.Helpers
{
    public static class HashPasswordHelper
    {
        public static string HashPassword(string pass)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
