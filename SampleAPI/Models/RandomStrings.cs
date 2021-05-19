using System;
using System.Linq;
using System.Security.Cryptography;

namespace SampleAPI.Models
{
    public class RandomStrings : IRandomStrings
    {
        public string RandomString(int length)
        {
            if (length > 30) throw new ArgumentException(nameof(length));

            return new string(
                Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+", length)
                    .Select(s => {
                        var cryptoResult = new byte[4];
                        using (var cryptoProvider = new RNGCryptoServiceProvider())
                            cryptoProvider.GetBytes(cryptoResult);
                        return s[new Random(BitConverter.ToInt32(cryptoResult, 0)).Next(s.Length)];
                    })
                    .ToArray());
        }
    }
}