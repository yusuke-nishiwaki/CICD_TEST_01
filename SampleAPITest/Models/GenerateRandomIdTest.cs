using System;
using Moq;
using SampleAPI.Models;
using Xunit;

namespace SampleAPITest.Models
{
    public class GenerateRandomIdTest
    {
        private readonly IGenerateRandomId _generateRandomId;
        public GenerateRandomIdTest()
        {
            //IRandomStringsのMock作成
            var randomStringsMoq = new Mock<IRandomStrings>();
            randomStringsMoq.Setup(x => x.RandomString(1))
                            .Returns("a");
            randomStringsMoq.Setup(x => x.RandomString(30))
                            .Returns("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            randomStringsMoq.Setup(x => x.RandomString(31))
                            .Throws<ArgumentException>();

            _generateRandomId = new GenerateRandomId(randomStringsMoq.Object);
        }

        [Fact(DisplayName = "正常_要求値に1桁追加")]
        public void 正常_要求値に1桁追加()
        {
            var result = _generateRandomId.Generate("AccessId", 1);

            Assert.Equal("AccessIda", result);
        }

        [Fact(DisplayName = "正常_要求値に30桁追加")]
        public void 正常_要求値に20桁追加()
        {
            var result = _generateRandomId.Generate("AccessId", 30);

            Assert.Equal("AccessIdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", result);
        }

        [Fact(DisplayName = "異常_アクセスIDが空")]
        public void 異常_アクセスIDが空()
        {
            string result() => _generateRandomId.Generate("", 30);

            Assert.Throws<ArgumentException>(result);
        }

        [Fact(DisplayName = "異常_アクセスIDがNULL")]
        public void 異常_アクセスIDがNULL()
        {
            string result() => _generateRandomId.Generate(null, 30);

            Assert.Throws<ArgumentException>(result);
        }

        [Fact(DisplayName = "異常_Lengthが0")]
        public void 異常_Lengthが0()
        {
            string result() => _generateRandomId.Generate("AccessId", 0);

            Assert.Throws<ArgumentException>(result);
        }

        [Fact(DisplayName = "異常_Lengthが31")]
        public void 異常_Lengthが31()
        {
            string result() => _generateRandomId.Generate("AccessId", 31);

            Assert.Throws<ArgumentException>(result);
        }
    }
}