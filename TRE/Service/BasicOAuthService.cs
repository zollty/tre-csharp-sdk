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
using System.Web;
using Common.Logging;
using TRE.Config;
using TRE.Tools;

namespace TRE.Service
{
    public class BasicOAuthService
    {

        protected IOAuth1Config authConfig;

        protected String uri;
        protected String procID;

        public BasicOAuthService(IOAuth1Config oauthConfig)
        {
            this.authConfig = oauthConfig;
        }

        private static ILog log = LogManager.GetCurrentClassLogger();

        protected List<HttpParameter> buildCommonHttpParams()
        {
            // 组装参数生产sign
            Dictionary<String, String> paramsmap = new Dictionary<String, String>();
            String timestamp = AuthConfigHelper.getTimestamp();
            String nonce = AuthConfigHelper.getNonce();
            paramsmap.Add("sysid", authConfig.getSystemid());
            paramsmap.Add("appid", authConfig.getAppid());
            paramsmap.Add("auth_type", authConfig.getAuthType());
            paramsmap.Add("procid", procID);
            paramsmap.Add("oauth_signature_method", AuthConfigHelper.SIGNATURE_METHOD_SHA1);
            paramsmap.Add("oauth_timestamp", timestamp);
            paramsmap.Add("oauth_nonce", nonce);
            String realSecret = null;
            if (authConfig.getOrgidKey() != null)
            {
                realSecret = StringUtils.removeLastStr(authConfig.getOrgidKey(), 2);
            }
            else
            {
                realSecret = authConfig.getAppSecret();
            }
            paramsmap.Add("app_secret", realSecret);
            log.Debug(m => m("app_secret = {0}", realSecret));

            String oauthToken = AuthTools.buildOauthToken(paramsmap, authConfig.getAppid() + realSecret);
            oauthToken = HttpUtility.UrlEncode(oauthToken);
            log.Debug(m => m("oauthToken = {0}", oauthToken));


            List<HttpParameter> nvps = new List<HttpParameter>();

            nvps.Add(new HttpParameter("sysid", authConfig.getSystemid()));
            nvps.Add(new HttpParameter("appid", authConfig.getAppid()));
            if (authConfig.getOrgidKey() != null)
            {
                nvps.Add(new HttpParameter("orgid_key", authConfig.getOrgidKey()));
                log.Debug(m => m("orgid_key = {0}", authConfig.getOrgidKey()));
            }
            if (authConfig.getLang() != null)
            {
                nvps.Add(new HttpParameter("lang", authConfig.getLang()));
            }
            nvps.Add(new HttpParameter("procid", procID));
            nvps.Add(new HttpParameter("auth_type", authConfig.getAuthType()));
            nvps.Add(new HttpParameter("oauth_signature_method", AuthConfigHelper.SIGNATURE_METHOD_SHA1));
            nvps.Add(new HttpParameter("oauth_timestamp", timestamp));
            nvps.Add(new HttpParameter("oauth_nonce", nonce));
            nvps.Add(new HttpParameter("oauth_token", oauthToken));
            return nvps;
        }

    }
}