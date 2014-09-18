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
using System.Collections.Generic;

namespace TRE.Config.Impl
{
    public class OAuth1ConfigLoader : AbstractOAuth1Config, IOAuth1Config
    {
        private static OAuth1ConfigLoader instance = null;
        private Boolean loaded = false;

        protected const String CONFIG_FILENAME = "App.config";


        private static object lockThis = new object();

        private static Dictionary<String, String> paramsMap = new Dictionary<string, string>(); 


        /**
         * 获取配置的入口方法 [需要获取通用的、与orgId特定的配置无关的配置时，调用此方法，否则调用getInstance(String orgId)方法]
         */
        public static OAuth1ConfigLoader getInstance()
        {
            lock (lockThis)
            {
                if (instance == null)
                {
                    instance = new OAuth1ConfigLoader();
                    instance.loadCommonConfig();
                }
                if (!instance.loaded)
                {
                    throw new Exception("配置不可用，请先正确初始化配置");
                }
                return instance;
            }
        }

        /**
         * 单模块刷新[CommonConfig]
         */
        public void loadCommonConfig()
        {
            lock (lockThis)
            {
                initCommonConfig();
                instance.loaded = true;
            }
        }

        private void initCommonConfig()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["appid"] != null)
            {
                this.setAppid(System.Configuration.ConfigurationManager.AppSettings["appid"]);
            }
            if (System.Configuration.ConfigurationManager.AppSettings["baseURL"] != null)
            {
                this.setBaseURL(System.Configuration.ConfigurationManager.AppSettings["baseURL"]);
            }
            if (System.Configuration.ConfigurationManager.AppSettings["appSecret"] != null)
            {
                this.setAppSecret(System.Configuration.ConfigurationManager.AppSettings["appSecret"]);
            }
            if (System.Configuration.ConfigurationManager.AppSettings["systemid"] != null)
            {
                this.setSystemid(System.Configuration.ConfigurationManager.AppSettings["systemid"]);
            }
            String[] keys = System.Configuration.ConfigurationManager.AppSettings.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                paramsMap.Add(keys[i], System.Configuration.ConfigurationManager.AppSettings[keys[i]]);
            }

        }


        public Dictionary<String, String> getParamsMap()
        {
            return paramsMap;
        }

    }
}