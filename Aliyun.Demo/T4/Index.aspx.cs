using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace T4
{
    public partial class Index : Page
    {
        //static Dictionary<string,System.Reflection.MethodInfo> DictMethod =
        //    typeof(Index).GetMethods(System.Reflection.BindingFlags.Public| System.Reflection.BindingFlags.Instance).ToDictionary(x=>x.Name.ToLower(),x=>x);

        protected override void OnLoad()
        {
            var fullpath = this.Server.MapPath(Config.DataFilePath);
            var doc = System.Xml.Linq.XDocument.Load(fullpath);
            var lstUserSalary = doc.Root.Element(nameof(UserSalary) + "s").Elements().Select(el => new UserSalary() {
                Cat = (UserSalary.Category)System.Enum.Parse(typeof(UserSalary.Category), el.Element("Cat").Value),
                CreateDateTime = DateTime.Parse(el.Element("CreateDateTime").Value),
                Id = Guid.Parse(el.Element("Id").Value),
                Name = el.Element("Name").Value,
                SSOTag = el.Element("SSOTag").Value
            }).ToList();
            this.rpt.DataSource = lstUserSalary;
            this.rpt.DataBind();

            var lst = System.Enum.GetValues(typeof(T4.UserSalary.Category)).Cast<UserSalary.Category>().ToList();
            this.rptCat.DataSource = lst;
            this.rptCat.DataBind();
        }
        /// <summary>
        /// 增加
        /// </summary>
        public void Add()
        {
            UserSalary us = new UserSalary();
            us.Name = this.Request["Name"];

            UserSalary.Category ct;
            System.Enum.TryParse(this.Request["Cat"], out ct);
            us.Cat = ct;

            us.CreateDateTime = DateTime.Now;
            us.Id = Guid.NewGuid();

            var file= this.Request.Files[0];
            //场景：按照不同的业务，不同的月份 归类 放文件
            string fileName = $"{us.Cat.ToString()}/{DateTime.Now.ToString("yyyy-MM")}/{Guid.NewGuid().ToString()}{System.IO.Path.GetExtension(file.FileName)}";
            var rt =  ClouderHelper.PutObject(BucketEnum.azerothstsm, fileName, file.InputStream);

            us.SSOTag = fileName;

            var fullpath = this.Server.MapPath(Config.DataFilePath);
            var doc = System.Xml.Linq.XDocument.Load(fullpath);
            var el = doc.Root.Element(nameof(UserSalary) + "s");
            el.Add(us.GetXElement());
            doc.Save(fullpath);


        }

        public void DownloadFile()
        {
            var key = this.Request["key"];
            var rt = ClouderHelper.GetObject(BucketEnum.azerothstsm, key);
            this.Response.Clear();
            rt.Content.CopyTo(this.Response.OutputStream);
            this.Response.Headers.Add("Content-Disposition", "attachment;filename=" + System.IO.Path.GetFileName(key));
            this.Response.Headers.Add("Content-Type", "application/octet-stream");
        }

        public void Delete()
        {
            var id = this.Request["id"];
            var fullpath = this.Server.MapPath(Config.DataFilePath);
            var doc = System.Xml.Linq.XDocument.Load(fullpath);
            var el = doc.Root.Element(nameof(UserSalary) + "s").Elements().First(x=>x.Element("Id").Value==id);
            el.Remove();
            doc.Save(fullpath);
            var fileName= el.Element("SSOTag").Value;
            ClouderHelper.DeleteObject(BucketEnum.azerothstsm, fileName);
        }
    }
}