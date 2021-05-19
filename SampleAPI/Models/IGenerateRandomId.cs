namespace SampleAPI.Models
{
    public interface IGenerateRandomId
    {
        /// <summary>
        /// 会員ID生成
        /// </summary>
        /// <param name="accessId"></param>
        /// <returns></returns>
        string Generate(string accessId, int length);
    }
}