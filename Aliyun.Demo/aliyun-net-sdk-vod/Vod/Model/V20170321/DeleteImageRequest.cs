/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Transform;
using Aliyun.Acs.Core.Utils;
using Aliyun.Acs.vod.Transform;
using Aliyun.Acs.vod.Transform.V20170321;
using System.Collections.Generic;

namespace Aliyun.Acs.vod.Model.V20170321
{
    public class DeleteImageRequest : RpcAcsRequest<DeleteImageResponse>
    {
        public DeleteImageRequest()
            : base("vod", "2017-03-21", "DeleteImage", "vod", "openAPI")
        {
        }

		private long? resourceOwnerId;

		private string imageType;

		private string resourceOwnerAccount;

		private string imageURLs;

		private string action;

		private string videoId;

		private long? ownerId;

		private string deleteImageType;

		private string imageIds;

		private string accessKeyId;

		public long? ResourceOwnerId
		{
			get
			{
				return resourceOwnerId;
			}
			set	
			{
				resourceOwnerId = value;
				DictionaryUtil.Add(QueryParameters, "ResourceOwnerId", value.ToString());
			}
		}

		public string ImageType
		{
			get
			{
				return imageType;
			}
			set	
			{
				imageType = value;
				DictionaryUtil.Add(QueryParameters, "ImageType", value);
			}
		}

		public string ResourceOwnerAccount
		{
			get
			{
				return resourceOwnerAccount;
			}
			set	
			{
				resourceOwnerAccount = value;
				DictionaryUtil.Add(QueryParameters, "ResourceOwnerAccount", value);
			}
		}

		public string ImageURLs
		{
			get
			{
				return imageURLs;
			}
			set	
			{
				imageURLs = value;
				DictionaryUtil.Add(QueryParameters, "ImageURLs", value);
			}
		}

		public string Action
		{
			get
			{
				return action;
			}
			set	
			{
				action = value;
				DictionaryUtil.Add(QueryParameters, "Action", value);
			}
		}

		public string VideoId
		{
			get
			{
				return videoId;
			}
			set	
			{
				videoId = value;
				DictionaryUtil.Add(QueryParameters, "VideoId", value);
			}
		}

		public long? OwnerId
		{
			get
			{
				return ownerId;
			}
			set	
			{
				ownerId = value;
				DictionaryUtil.Add(QueryParameters, "OwnerId", value.ToString());
			}
		}

		public string DeleteImageType
		{
			get
			{
				return deleteImageType;
			}
			set	
			{
				deleteImageType = value;
				DictionaryUtil.Add(QueryParameters, "DeleteImageType", value);
			}
		}

		public string ImageIds
		{
			get
			{
				return imageIds;
			}
			set	
			{
				imageIds = value;
				DictionaryUtil.Add(QueryParameters, "ImageIds", value);
			}
		}

		public string AccessKeyId
		{
			get
			{
				return accessKeyId;
			}
			set	
			{
				accessKeyId = value;
				DictionaryUtil.Add(QueryParameters, "AccessKeyId", value);
			}
		}

        public override DeleteImageResponse GetResponse(Core.Transform.UnmarshallerContext unmarshallerContext)
        {
            return DeleteImageResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}