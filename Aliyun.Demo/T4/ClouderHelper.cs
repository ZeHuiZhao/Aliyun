using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T4
{
    public static class ClouderHelper
    {
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="bk"></param>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static PutObjectResult PutObject(BucketEnum bk, string key, System.IO.Stream content)
        {
            var client= new Aliyun.OSS.OssClient(Config.Endpoint, Config.AccessKeyId, Config.AccessKeySecret);
            return client.PutObject(bk.ToString(), key, content);
        }

        /// <summary>
        /// 获取指定key的对象
        /// </summary>
        /// <param name="bk"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static OssObject GetObject(BucketEnum bk,string key)
        {
            var client = new Aliyun.OSS.OssClient(Config.Endpoint, Config.AccessKeyId, Config.AccessKeySecret);
            return client.GetObject(bk.ToString(), key);
        }

        /// <summary>
        /// 删除指定key的对象
        /// </summary>
        /// <param name="bk"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static void DeleteObject(BucketEnum bk, string fileName)
        {
            var client = new Aliyun.OSS.OssClient(Config.Endpoint, Config.AccessKeyId, Config.AccessKeySecret);
            client.DeleteObject(bk.ToString(), fileName);
        }

        public static Aliyun.Acs.Core.DefaultAcsClient InitVodClient()
        {
            // 构建一个 Client，用于发起请求
            string regionId = "cn-shanghai"; //目前仅支持cn-shanghai
            Aliyun.Acs.Core.Profile.IClientProfile profile = Aliyun.Acs.Core.Profile.DefaultProfile.GetProfile(regionId, Config.AccessKeyId, Config.AccessKeySecret);
            return new Aliyun.Acs.Core.DefaultAcsClient(profile);
        }
    }

    public enum BucketEnum
    {
        azerothstsm=1,

    }
}