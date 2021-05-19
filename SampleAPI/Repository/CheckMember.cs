using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Database;

namespace SampleAPI.Repository
{
    public class CheckMember : ICheckMember
    {
        private readonly DBContext _dbContext;

        public CheckMember(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> FindByMemberId(string accessId)
        {
            //Memberテーブル存在確認
            var checkMemberResult = await _dbContext.Member
                                    .Where(x => x.MembedId == accessId)
                                    .AnyAsync();

            if (!checkMemberResult)
            {
                return false;
            }

            return true;
        }
    }
}