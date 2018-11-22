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
    public class GetVideoPlayAuthRequest : RpcAcsRequest<GetVideoPlayAuthResponse>
    {
        public GetVideoPlayAuthRequest()
            : base("vod", "2017-03-21", "GetVideoPlayAuth", "vod", "openAPI")
        {
        }

		private long? resourceOwnerId;

		private string resourceOwnerAccount;

		private string reAuthInfo;

		private string playConfig;

		private long? authInfoTimeout;

		private string action;

		private string videoId;

		private long? ownerId;

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

		public string ReAuthInfo
		{
			get
			{
				return reAuthInfo;
			}
			set	
			{
				reAuthInfo = value;
				DictionaryUtil.Add(QueryParameters, "ReAuthInfo", value);
			}
		}

		public string PlayConfig
		{
			get
			{
				return playConfig;
			}
			set	
			{
				playConfig = value;
				DictionaryUtil.Add(QueryParameters, "PlayConfig", value);
			}
		}

		public long? AuthInfoTimeout
		{
			get
			{
				return authInfoTimeout;
			}
			set	
			{
				authInfoTimeout = value;
				DictionaryUtil.Add(QueryParameters, "AuthInfoTimeout", value.ToString());
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

        public override GetVideoPlayAuthResponse GetResponse(Core.Transform.UnmarshallerContext unmarshallerContext)
        {
            return GetVideoPlayAuthResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}