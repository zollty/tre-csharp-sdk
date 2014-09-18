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
using System.Text;
using Common.Logging;
using TRE.OAuth;

namespace TRE.Tools
{
    public class AuthTools
    {
        private static ILog log = LogManager.GetCurrentClassLogger();

        public static String buildOauthToken(Dictionary<String, String> paramsmap, String secret)
        {
            String baseString = AuthTools.assembleSignBaseString(paramsmap);
            log.Debug(m => m("secret = {0}, baseString = {1}", secret, baseString));
            return AuthTools.sign(secret, baseString);
        }
        public static String sign(String key, String signatureBaseString)
        {
            OAuthSignature signatureMethod = new HMACSHA1Signature(key);
            return signatureMethod.sign(signatureBaseString);
        }

        /**
         * 组装方式如下：
         sorted_query_params.each  { | k, v |
           url_encode ( k ) + "%3D" +
             url_encode ( v )
        }.join("%26")
         * 注意，默认为传入的k和v是已经encode过的。调用前，请确保参数只包含
         *   字母、数字和“. - _ ~”四个url字符
         */
        public static String assembleSignBaseString(Dictionary<String, String> param)
        {
            // step1 按key字典序升序排序
            List<String> keys = new List<String>(param.Keys);
            keys.Sort();
            // step2 按顺序依次组装参数 -------- %3D代表=号，%26代表&号
            StringBuilder prestr = new StringBuilder();
            for (int i = 0; i < keys.Count; i++)
            {
                String key = (String)keys[i];
                String value = (String)param[key];
                if (i == keys.Count - 1)
                {
                    prestr.Append(key);
                    prestr.Append("%3D");
                    prestr.Append(value);
                }
                else
                {
                    prestr.Append(key);
                    prestr.Append("%3D");
                    prestr.Append(value);
                    prestr.Append("%26");
                }
            }
            Console.WriteLine("SignBaseString: {0}", prestr.ToString());
            return prestr.ToString();
        }

    }
}