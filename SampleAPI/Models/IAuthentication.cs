using System.Threading.Tasks;

namespace SampleAPI.Models
{
    public interface IAuthentication
    {
        /// <summary>
        /// ユーザー認証処理
        /// </summary>
        /// <param name="accessId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> ExcuteAsync(string accessId, string password);
    }
}