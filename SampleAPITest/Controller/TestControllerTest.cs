using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using ETCMemberTest.Moq;
using Microsoft.AspNetCore.Identity;
using Moq;
using SampleAPI.Database;
using SampleAPITest.Moq;
using Xunit;

namespace SampleAPITest.Controller
{
    public class TestControllerTest : IDisposable
    {
        private readonly string testUrl = "api/test";
        private static readonly string _memberId_20 = "TestControllerTest12";
        private static readonly string _Password_20 = "PassW0rd@aaaaaaaaaaa";

        private readonly UserManager<IdentityUser> _userManager;

        public TestControllerTest(UserManager<IdentityUser> userManager)
        {
            //DI
            _userManager = userManager;

            //事前データ登録
            var options_Insert = GetDbContext.Do();
            using var context = new DBContext(options_Insert);

            context.Add(GetDbEntity.GetMember(_memberId_20));
            var result = _userManager.CreateAsync(GetDbEntity.GetIdentityUser(_memberId_20), _Password_20).Result;

            context.SaveChanges();
        }

        public void Dispose()
        {
            //テスト後データ削除
            var options_Insert = GetDbContext.Do();
            using var context = new DBContext(options_Insert);

            context.Remove(GetDbEntity.GetMember(_memberId_20));
            var user = _userManager.FindByNameAsync(_memberId_20).Result;
            var result = _userManager.DeleteAsync(user).Result;

            context.SaveChanges();
        }

        [Theory(DisplayName = "正常_通常リクエスト")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", "1")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", "9")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", "10")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", "19")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", "20")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", "29")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", "30")]
        public void 正常_通常リクエスト(string accessId, string password, string length)
        {
            //リクエスト内容作成
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                //アクセスID 必須 20桁以内
                {"AccessID", accessId},
                //パスワード 必須 20桁以内
                {"Password", password},
                //長さ 必須 1～9または10～29または30(正規表現)
                {"Length", length},
            });

            //APIリクエスト
            var (result, httpStatusCode) = ApiRequest.GetAsync(testUrl, content).Result;

            Assert.Equal(HttpStatusCode.OK, httpStatusCode);
            Assert.Equal(int.Parse(length) + accessId.Length, result.Length);
        }

        [Theory(DisplayName = "異常_BadRequest")]
        [InlineData("", "PassW0rd@aaaaaaaaaaa", "30")]
        [InlineData(null, "PassW0rd@aaaaaaaaaaa", "30")]
        [InlineData("TestControllerTest123", "PassW0rd@aaaaaaaaaaa", "30")]
        [InlineData("TestControllerTest12", "", "30")]
        [InlineData("TestControllerTest12", null, "30")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa1", "30")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", "0")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", "")]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", null)]
        [InlineData("TestControllerTest12", "PassW0rd@aaaaaaaaaaa", "31")]
        public void 異常_BadRequest(string accessId, string password, string length)
        {
            //リクエスト内容作成
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                //アクセスID 必須 20桁以内
                {"AccessID", accessId},
                //パスワード 必須 20桁以内
                {"Password", password},
                //長さ 必須 1～9または10～29または30(正規表現)
                {"Length", length},
            });

            //APIリクエスト
            var (result, httpStatusCode) = ApiRequest.GetAsync(testUrl, content).Result;

            Assert.Equal(HttpStatusCode.BadRequest, httpStatusCode);
        }
    }
}