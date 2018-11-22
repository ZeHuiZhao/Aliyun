using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace T4
{
    public partial class PlayerVideo : Page
    {
        public string VideoId { get; set; }


        protected override void OnLoad()
        {
            this.VideoId = this.Request["VideoId"];

            //Aliyun.Acs.vod.Model.V20170321.GetPlayInfoRequest request = new Aliyun.Acs.vod.Model.V20170321.GetPlayInfoRequest();
            //request.VideoId = videoId;
            //// request.AuthTimeout = 3600;
            //// 初始化客户端
            //Aliyun.Acs.Core.DefaultAcsClient client = OSSHelper.InitVodClient();
            //// 发起请求，并得到 response
            //Aliyun.Acs.vod.Model.V20170321.GetPlayInfoResponse response = client.GetAcsResponse(request);
            //var lstAdress= response.PlayInfoList;

            //Aliyun.Acs.vod.Model.V20170321.GetVideoPlayAuthRequest request = new Aliyun.Acs.vod.Model.V20170321.GetVideoPlayAuthRequest();
            //request.VideoId = this.VideoId;
            ////request.AuthInfoTimeout = 3000;
            //// 初始化客户端
            //Aliyun.Acs.Core.DefaultAcsClient client = OSSHelper.InitVodClient();
            //// 发起请求，并得到 response
            //this.ViedoInfo= client.GetAcsResponse(request);
            //this.PlayAuth=;
        }

        /// <summary>
        /// PlayAuth的播放流程
        /// </summary>
        public void PlayAuth()
        {
            this.VideoId = this.Request["VideoId"];
            Aliyun.Acs.vod.Model.V20170321.GetVideoPlayAuthRequest request = new Aliyun.Acs.vod.Model.V20170321.GetVideoPlayAuthRequest();
            request.VideoId = this.VideoId;
            //request.AuthInfoTimeout = 3000;
            // 初始化客户端
            Aliyun.Acs.Core.DefaultAcsClient client = ClouderHelper.InitVodClient();
            // 发起请求，并得到 response
            var response = client.GetAcsResponse(request);
            var rst = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                this.VideoId,
                response.PlayAuth
            });
            this.Response.Clear();
            this.Response.Write(rst);
            this.Response.ContentType = "application/json";
        }
    }
}