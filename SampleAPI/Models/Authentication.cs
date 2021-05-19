using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Database;
using SampleAPI.Repository;

namespace SampleAPI.Models
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICheckMember _checkMember;

        public Authentication(UserManager<IdentityUser> userManager,
                                ICheckMember checkMember)
        {
            _checkMember = checkMember;
            _userManager = userManager;
        }

        public async Task<bool> ExcuteAsync(string accessId, string password)
        {
            //テスト用ユーザー登録
            //var user = new IdentityUser {
            //            UserName = accessId,
            //            Email = "test@test.com",
            //            EmailConfirmed = false,
            //            PhoneNumber = "99999999999",
            //            PhoneNumberConfirmed = true,
            //            TwoFactorEnabled = false,
            //            LockoutEnabled = true
            //        };
            //var result = await _userManager.CreateAsync(user, "PassW0rd@");

            if (!(await _checkMember.FindByMemberId(accessId)))
            {
                return false;
            }

            //AspNetUsersテーブル存在確認
            var identityUser = await _userManager.FindByNameAsync(accessId);
            if (identityUser == null)
            {
                return false;
            }

            //パスワードチェック
            var checkIdentityResult = await _userManager.CheckPasswordAsync(identityUser, password);
            if (!checkIdentityResult)
            {
                return false;
            }

            return true;
        }
    }
}