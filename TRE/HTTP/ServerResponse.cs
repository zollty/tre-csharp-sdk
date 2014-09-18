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
using System.IO;
using System.Net;
using System.Text;
using Common.Logging;
using TRE.Tools;

namespace TRE.HTTP
{
    public class ServerResponse
    {

        private String contentType;

        private String errorCode;

        private String resultStr;

        private HttpWebResponse httpResponse;


        private Boolean done = false;

        private static ILog log = LogManager.GetCurrentClassLogger();

        public ServerResponse(HttpWebResponse httpResponse)
        {
            this.httpResponse = httpResponse;
        }

        public Boolean isSuccess()
        {
            if (getErrorCode() == null)
            {
                return true;
            }
            return false;
        }

        /**
         * 判断是否成功的标识：
         * 第一，看http header里面是否有错误标识(errorCode)，有则是失败。之后再从返回信息中取出详细错误信息。
         * 第二，如果header里面没有错误标识，则可以选择进一步检查是否返回的字符串，并且包含错误信息。
         * @throws TreClientException
         */
        public void throwException()
        {
            if (this.isSuccess())
            { //只有错误时才执行该方法，否则直接返回
                return;
            }
            //下面代码目的是检查正文中是否含有json格式的标准错误字符串
            try
            {
                this.initResponseStr();
            }
            catch (Exception e)
            {
                throw new TreClientException(ErrorCode.EC0001, e.Message);
            }
            JsonTools.assumeItsAnError(resultStr);
            // 默认返回header中的错误代码
            throw new TreClientException(ErrorCode.ES0002, "ErrorCode=" + this.getErrorCode());
        }

        public String getErrorCode()
        {
            if (errorCode != null)
            {
                return errorCode;
            }

            foreach (var key in httpResponse.Headers.AllKeys)
            {
                if ("errorCode".Equals(key))
                {
                    errorCode = httpResponse.Headers[key];
                }
            }

            return errorCode;
        }

        public String getContentType()
        {
            if (this.contentType != null)
                return contentType;

            foreach (var key in httpResponse.Headers.AllKeys)
            {
                if ("content-type".Equals(key.ToLower()))
                {
                    this.contentType = httpResponse.Headers[key];
                }
            }

            return contentType;
        }

        private void init()
        {
            getErrorCode();
            getContentType();
        }

        private void initResponseStr()
        {
            if (done || resultStr != null)
            {
                return;
            }
            done = true;
            //得先初始化一遍，以防HTTP连接关闭后无法再初始化数据了
            init();
            Stream ins = httpResponse.GetResponseStream();
            StreamReader reader = null;
            if (ins != null)
            {
                try
                {
                    reader = new StreamReader(ins, Encoding.UTF8);
                    resultStr = reader.ReadToEnd();
                }
                catch (IOException e)
                {
                    throw new TreClientException(ErrorCode.EC0001, e.Message);
                }
            }
        }

        public String getResultStr()
        {
            this.initResponseStr();
            // 检查是否为json格式的标准错误字符串
            JsonTools.assumeItsAnError(resultStr);
            return resultStr;
        }



        /**
         * 处理服务器端推送过来的流
         * @param out OutputStream
         * @throws TreClientException
         */
        public void doOutPut(Stream outs)
        {
            // 该方法只允许调用一次
            if (done)
            {
                throw new TreClientException(ErrorCode.EC0001);
            }
            done = true;
            // 得先初始化一遍，以防HTTP连接关闭后无法再初始化数据了
            init();

            long contentLength = httpResponse.ContentLength;
            Stream ins = null;
            try
            {
                ins = httpResponse.GetResponseStream();

                int len = -1;
                if (contentLength > 0)
                {
                    len = (int)contentLength;
                }
                log.Debug("redeay to io clone len =  "+len);
                IOUtils.clone(ins, len, outs);
            }
            catch (Exception e)
            {
                throw new TreClientException(ErrorCode.EC0001, e.Message);
            }
            finally
            {
                try
                {
                    httpResponse.Dispose();
                }
                catch (IOException e)
                {
                    log.Debug(m => m("httpResponse.Dispose() error: {0}", e.Message));
                }
                IOUtils.close(ins, outs);
                log.Debug("ok, io closed");
            }
        }


        /**
         * 处理服务器端推送过来的流，用于从浏览器下载文件（暂时用不到）
         * @param out
         * @param hook 设置HttpServletResponse
         * @throws TreClientException
         */
        public void doOutPut(Stream outs, HttpServletResponseHook hook)
        {
            // 该方法只允许调用一次
            if (done)
            {
                throw new TreClientException(ErrorCode.EC0001);
            }
            done = true;
            // 得先初始化一遍，以防HTTP连接关闭后无法再初始化数据了
            init();


            String contentType = null;
            String contentDisposition = null;
            foreach (var key in httpResponse.Headers.AllKeys)
            {
                if ("Content-Disposition".Equals(key))
                {
                    contentDisposition = httpResponse.Headers[key];
                    log.Debug(m => m("servletDownload [contentDisposition = {0}]", contentDisposition));
                }
                if ("Content-Type".Equals(key))
                {
                    contentType = httpResponse.Headers[key];
                    log.Debug(m => m("servletDownload [contentType = {0}]", contentType));
                }
            }


            long contentLength = httpResponse.ContentLength;
            Stream ins = null;
            try
            {
                ins = httpResponse.GetResponseStream();

                // 设置输出的格式
                hook.setContentType(contentType); // e.g. application/zip
                hook.addHeader(contentDisposition); //"Content-Disposition", 

                int len = -1;
                if (contentLength > 0)
                {
                    len = (int)contentLength;
                    hook.setContentLength(len);
                }

                IOUtils.clone(ins, len, outs);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new TreClientException(ErrorCode.EC0001, e.Message);
            }
            finally
            {
                try
                {
                    httpResponse.Dispose();
                }
                catch (IOException e)
                {
                    log.Debug(m => m("httpResponse.Dispose() error: {0}", e.Message));
                }
                IOUtils.close(ins, outs);
            }
        }


    }
}