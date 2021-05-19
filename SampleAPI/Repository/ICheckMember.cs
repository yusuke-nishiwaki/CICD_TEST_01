using System.Threading.Tasks;

namespace SampleAPI.Repository
{
    public interface ICheckMember
    {
        /// <summary>
        /// メンバー存在確認
        /// </summary>
        /// <param name="accessId"></param>
        /// <returns></returns>
        Task<bool> FindByMemberId(string accessId);
    }
}