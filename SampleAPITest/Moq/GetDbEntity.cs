using System;
using Microsoft.AspNetCore.Identity;
using SampleAPI.Database;

namespace SampleAPITest.Moq
{
    public class GetDbEntity
    {
        public static Member GetMember(string memberId)
        {
            return new Member()
            {
                MembedId = memberId,
                Name = "MemberName",
                CreateDateTime = DateTime.Now,
            };
        }

        public static IdentityUser GetIdentityUser(string memberId)
        {
            return new IdentityUser {
                UserName = memberId,
                Email = "test@test.com",
                EmailConfirmed = false,
                PhoneNumber = "99999999999",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true
            };
        }
    }
}