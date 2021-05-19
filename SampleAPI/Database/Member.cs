using System;
using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Database
{
    public class Member
    {
        [Key]
        public string MembedId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}