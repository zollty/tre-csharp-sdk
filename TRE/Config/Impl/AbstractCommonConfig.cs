//-----------------------------------------------------------------------
// <copyright>
//    Copyright (c) 2014, TravelSky Technology Limited. 
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
// <website>https://github.com/TravelskyTech/tre-csharp-sdk</website>
//-----------------------------------------------------------------------
using System;

namespace TRE.Config.Impl
{
    public abstract class AbstractCommonConfig : ICommonConfig
    {
        // /////////////////
        protected String systemid;
        protected String appid;
        protected String baseURL;
        protected String orgid; // 项目子机构ID
        protected String orgidKey; // 项目子机构ID密码
        protected String lang; // 国家语言 en_US
        // ////////////////

        public virtual String getSystemid()
        {
            return systemid;
        }
        public void setSystemid(String systemid)
        {
            this.systemid = systemid;
        }
        public virtual String getAppid()
        {
            return appid;
        }
        public void setAppid(String appid)
        {
            this.appid = appid;
        }
        public virtual String getBaseURL()
        {
            return baseURL;
        }

        public abstract string getAuthType();

        public void setBaseURL(String baseURL)
        {
            this.baseURL = baseURL;
        }
        public virtual String getOrgid()
        {
            return orgid;
        }
        public void setOrgid(String orgid)
        {
            this.orgid = orgid;
        }
        public virtual String getLang()
        {
            return lang;
        }
        public void setLang(String lang)
        {
            this.lang = lang;
        }
        public virtual String getOrgidKey()
        {
            return orgidKey;
        }
        public void setOrgidKey(String orgidKey)
        {
            this.orgidKey = orgidKey;
        }
    }
}
