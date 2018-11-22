using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T4
{
    public class UserSalary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Cat { get; set; }

        public DateTime CreateDateTime{ get; set; }

        public string SSOTag { get; set; }

        public enum Category
        {
            津贴,
            奖惩,
            调薪
        }
    }
}