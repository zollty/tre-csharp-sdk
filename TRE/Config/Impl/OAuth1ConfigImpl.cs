//-----------------------------------------------------------------------
// <copyright>
//    Copyright (c) 2014, Anonymous Technology Limited. 
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// <author>Zollty Tsou (blog.zollty.com)</author>
// <website>https://github.com/zollty/tre-csharp-sdk</website>
//-----------------------------------------------------------------------
using System;

namespace TRE.Config.Impl
{
    public class OAuth1ConfigImpl : AbstractOAuth1Config
    {
        public OAuth1ConfigImpl()
            : base()
        {
        }

        /**
         * @param orgid 项目子机构ID
         */
        public OAuth1ConfigImpl(String orgid)
            : base()
        {
            this.orgid = orgid;
        }

        /**
         * @param orgid 项目子机构ID
         * @param lang 国家语言 例如en_US
         */
        public OAuth1ConfigImpl(String orgid, String lang)
            : base()
        {
            this.orgid = orgid;
            this.lang = lang;
        }


        public override String getSystemid()
        {
            return OAuth1ConfigLoader.getInstance().getSystemid();
        }


        public override String getAppid()
        {
            return OAuth1ConfigLoader.getInstance().getAppid();
        }


        public override String getBaseURL()
        {
            return OAuth1ConfigLoader.getInstance().getBaseURL();
        }


        public override String getOrgidKey()
        {
            return OAuth1ConfigLoader.getInstance().getParamsMap()[orgid];
        }


        public override String getAppSecret()
        {
            return OAuth1ConfigLoader.getInstance().getAppSecret();
        }
    }
}