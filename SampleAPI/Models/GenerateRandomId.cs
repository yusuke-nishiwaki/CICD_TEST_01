using System;

namespace SampleAPI.Models
{
    public class GenerateRandomId : IGenerateRandomId
    {
        private readonly IRandomStrings _randomStrings;
        public GenerateRandomId(IRandomStrings randomStrings)
        {
            _randomStrings = randomStrings;
        }

        public string Generate(string accessId, int length)
        {
            if (string.IsNullOrEmpty(accessId)) throw new ArgumentException(nameof(accessId));
            if (length == 0) throw new ArgumentException(nameof(length));

            return $"{accessId}{_randomStrings.RandomString(length)}";
        }
    }
}