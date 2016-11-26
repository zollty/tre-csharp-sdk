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

namespace TRE.Tools
{
    public class AuthConfigHelper
    {

        public const String AUTH_TYPE_OAUTH1 = "oauth1"; // 暂时用oauth1
        public const String AUTH_TYPE_ENCRYPT = "encrypt"; // 加密 encrypt
        public const String SIGNATURE_METHOD_SHA1 = "HmacSHA1"; // 签名方法

        private static Random random = new Random();


        /// <summary>
        ///  生成时间截（例如1364545552121）
        /// </summary>
        /// <returns></returns>
        public static String getTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }


        /// <summary>
        /// 生成混淆码（即6位的随机字符串）
        /// </summary>
        /// <returns></returns>
        public static String getNonce()
        {
            return RandomUtils.getRadomStrAZaz(16);
        }

    }
}
