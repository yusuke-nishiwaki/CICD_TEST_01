using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using SampleAPI.Models;
using SampleAPI.Repository;
using SampleAPITest.Moq;
using Xunit;

namespace SampleAPITest.Models
{
    public class AuthenticationTest
    {
        private readonly string _memberId = "AuthenticationTest";
        private readonly string _memberId_NotFound = "MemberNotFound";
        private readonly string _memberId_AspNetUsers_NotFound = "AspNetUsersNotFound";
        private readonly string _password = "Passw0rd@";
        private readonly string _password_False = "Passw0rd@False";
        private readonly IAuthentication _authentication;

        public AuthenticationTest()
        {
            //ICheckMemberのMock作成
            var checkMemberMoq = new Mock<ICheckMember>();
            //_memberIdはチェックOK
            checkMemberMoq.Setup(x => x.FindByMemberId(_memberId))
                            .ReturnsAsync(true);
            //_memberId_AspNetUsers_NotFoundはチェックOK
            checkMemberMoq.Setup(x => x.FindByMemberId(_memberId_AspNetUsers_NotFound))
                            .ReturnsAsync(true);

            //UserManagerのMock作成
            var userManagerMock = IdentityMockHelper.MockUserManager<IdentityUser>();
            userManagerMock.Setup(x => x.FindByNameAsync(_memberId))
                        .ReturnsAsync(GetDbEntity.GetIdentityUser(_memberId));
            userManagerMock.Setup(x => x.FindByNameAsync(_memberId_AspNetUsers_NotFound))
                        .ReturnsAsync((IdentityUser)null);
            userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(), _password))
                        .ReturnsAsync(true);

            //テスト対象作成
            _authentication = new Authentication(userManagerMock.Object, checkMemberMoq.Object);
        }

        [Fact(DisplayName = "正常_ユーザー認証OK")]
        public async Task 正常_要求値に1桁正常_ユーザー認証OK追加()
        {
            var result = await _authentication.ExcuteAsync(_memberId, _password);

            Assert.True(result);
        }

        [Fact(DisplayName = "異常_Memberテーブルに存在しない")]
        public async Task 異常_Memberテーブルに存在しない()
        {
            var result = await _authentication.ExcuteAsync(_memberId_NotFound, _password);

            Assert.False(result);
        }

        [Fact(DisplayName = "異常_AspNetUsersテーブルに存在しない")]
        public async Task 異常_AspNetUsersテーブルに存在しない()
        {
            var result = await _authentication.ExcuteAsync(_memberId_AspNetUsers_NotFound, _password);

            Assert.False(result);
        }

        [Fact(DisplayName = "異常_パスワード不一致")]
        public async Task 異常_パスワード不一致()
        {
            var result = await _authentication.ExcuteAsync(_memberId, _password_False);

            Assert.False(result);
        }
    }
}