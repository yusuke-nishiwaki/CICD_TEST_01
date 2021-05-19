using Xunit;
using SampleAPI.Models;
using System;

namespace SampleAPITest.Models
{
    public class RandomStringsTest
    {
        private readonly IRandomStrings _randomStrings;

        public RandomStringsTest()
        {
            _randomStrings = new RandomStrings();
        }

        [Fact(DisplayName = "正常応答_30文字以内_Fact")]
        public void 正常応答_30文字以内_Fact()
        {
            Assert.Equal(1, _randomStrings.RandomString(1).Length);
            Assert.Equal(2, _randomStrings.RandomString(2).Length);
            Assert.Equal(3, _randomStrings.RandomString(3).Length);
            Assert.Equal(4, _randomStrings.RandomString(4).Length);
            Assert.Equal(5, _randomStrings.RandomString(5).Length);
            Assert.Equal(30, _randomStrings.RandomString(30).Length);
        }

        [Theory(DisplayName = "正常応答_30文字以内_Theory")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(30)]
        public void 正常応答_30文字以内_Theory(int length)
        {
            Assert.Equal(length, _randomStrings.RandomString(length).Length);
        }

        [Theory(DisplayName = "正常応答_大小英字数字記号")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(30)]
        public void 正常応答_大小英字数字記号(int length)
        {
            Assert.Matches(@"[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789\-._@+]{" + length + "}$", _randomStrings.RandomString(length));
        }

        [Fact(DisplayName = "異常_31文字以上")]
        public void 異常_31文字以上()
        {
            string code() => _randomStrings.RandomString(31);

            Assert.Throws<ArgumentException>(code);
        }
    }
}