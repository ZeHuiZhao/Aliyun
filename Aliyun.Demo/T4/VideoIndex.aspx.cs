using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
namespace T4
{
    public partial class VideoIndex : Page
    {
        protected override void OnLoad()
        {
            var fullpath = this.Server.MapPath(Config.DataFilePath);
            var doc = System.Xml.Linq.XDocument.Load(fullpath);
            var lstUserSalary = doc.Root.Element(nameof(UserVideo) + "s").Elements().Select(el => new UserVideo() {
                Cat = (UserVideo.Category)System.Enum.Parse(typeof(UserVideo.Category), el.Element("Cat").Value),
                CreateDateTime = DateTime.Parse(el.Element("CreateDateTime").Value),
                Id = Guid.Parse(el.Element("Id").Value),
                Name = el.Element("Name").Value,
                SSOTag = el.Element("SSOTag").Value
            }).ToList();
            this.rpt.DataSource = lstUserSalary;
            this.rpt.DataBind();

            var lst = System.Enum.GetValues(typeof(T4.UserVideo.Category)).Cast<UserVideo.Category>().ToList();
            this.rptCat.DataSource = lst;
            this.rptCat.DataBind();
        }

        

        public void Add()
        {
            //调用api，又阿里云生成一个videoid及相关的认证信息
            Aliyun.Acs.vod.Model.V20170321.CreateUploadVideoRequest request = new Aliyun.Acs.vod.Model.V20170321.CreateUploadVideoRequest();
            var file= this.Request.Files[0];
            request.Title = file.FileName;
            request.FileName = Guid.NewGuid().ToString()+System.IO.Path.GetExtension(file.FileName);
            var client = ClouderHelper.InitVodClient();
            // 发起请求，并得到响应结果
            Aliyun.Acs.vod.Model.V20170321.CreateUploadVideoResponse response = client.GetAcsResponse(request);
            var rst = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                response.RequestId,
                response.UploadAddress,
                response.UploadAuth,
                response.VideoId
            });

            UserVideo us = new UserVideo();
            us.Name = this.Request["Name"];
            UserVideo.Category ct;
            System.Enum.TryParse(this.Request["Cat"], out ct);
            us.Cat = ct;
            us.CreateDateTime = DateTime.Now;
            us.Id = Guid.NewGuid();

            us.SSOTag = response.VideoId;//自己的数据库，保存VideoId

            var fullpath = this.Server.MapPath(Config.DataFilePath);
            var doc = System.Xml.Linq.XDocument.Load(fullpath);
            var el = doc.Root.Element(nameof(UserVideo) + "s");
            el.Add(us.GetXElement());
            doc.Save(fullpath);


            this.Response.Clear();
            this.Response.Write(rst);
            this.Response.ContentType = "application/json";
        }

        public void Delete()
        {
            var id = this.Request["id"];
            var fullpath = this.Server.MapPath(Config.DataFilePath);
            var doc = System.Xml.Linq.XDocument.Load(fullpath);
            var el = doc.Root.Element(nameof(UserVideo) + "s").Elements().First(x => x.Element("Id").Value == id);
            el.Remove();
            doc.Save(fullpath);

            var videoId = el.Element("SSOTag").Value;
            //调用api删除阿里云上的视频
            Aliyun.Acs.vod.Model.V20170321.DeleteVideoRequest request = new Aliyun.Acs.vod.Model.V20170321.DeleteVideoRequest();
            request.VideoIds = videoId;
            // 初始化客户端
            Aliyun.Acs.Core.DefaultAcsClient client = ClouderHelper.InitVodClient();
            // 发起请求，并得到 response
            Aliyun.Acs.vod.Model.V20170321.DeleteVideoResponse response = client.GetAcsResponse(request);

        }

    }
}