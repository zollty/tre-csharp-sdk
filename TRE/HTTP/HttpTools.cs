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
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using TRE.Tools;

namespace TRE.HTTP
{
    public class HttpTools
    {
        /// <summary>
        /// 以Post方式提交URL和参数
        /// </summary>
        /// <param name="url">服务的URL全地址，比如，http://122.119.123.29/report/open/showTask </param>
        /// <param name="formAttrs">POST提交的参数</param>
        /// <returns>封装了的ServerResponse</returns>
        public static ServerResponse doPost(String url, List<HttpParameter> formAttrs)
        {

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "POST";

            webRequest.UserAgent = "TRECsharp/1.0.0 (" + Environment.OSVersion.Version.ToString() + "; )";

            webRequest.Timeout = 1200000; // 连接timeout时间：20秒钟

            webRequest.KeepAlive = false;
            webRequest.Headers.Add("Cache-Control", "no-cache");

            //if (TRE.Conf.GetWebProxy() != null)
            //{
            //    webRequest.Proxy = TRE.Conf.GetWebProxy();
            //}

            if (formAttrs != null && formAttrs.Count > 0)
            {
                var sbr = new StringBuilder();
                foreach (HttpParameter httpParameter in formAttrs)
                {
                    sbr.Append(httpParameter.Name).Append("=").Append(httpParameter.Value).Append("&");
                }

                webRequest.ContentType = "application/x-www-form-urlencoded";

                String payload = sbr.ToString();
                payload = payload.Substring(0, payload.Length - 1);
                // Encode the data
                byte[] encodedBytes = Encoding.UTF8.GetBytes(payload);
                webRequest.ContentLength = encodedBytes.Length;

                // Write encoded data into request stream
                Stream requestStream = webRequest.GetRequestStream();
                requestStream.Write(encodedBytes, 0, encodedBytes.Length);
                requestStream.Close();
            }

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (Exception e)
            {
                throw new TreClientException(ErrorCode.ERQ002, "[url = " + url + " ] errMsg: " + e.Message);
            }

            return new ServerResponse(response);
        }


    }
}