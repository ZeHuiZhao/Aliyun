using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T4
{
    public class UserVideo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Cat { get; set; }

        public DateTime CreateDateTime{ get; set; }

        public string SSOTag { get; set; }

        public enum Category
        {
            中力家,
            入职培训,
            岗位专业知识
        }
    }
}