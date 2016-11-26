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
using Common.Logging;
using TRE.Config;
using TRE.HTTP;
using TRE.Tools;

namespace TRE.Service
{
    public class ReportDownloadService : BasicOAuthService
    {
        private String downCode; // 私有参数，指定业务处理类

        private void init()
        {
            procID = "32";
            uri = "service/request.do";
        }

        public ReportDownloadService(IOAuth1Config authConfig)
            : base(authConfig)
        {

            this.init();
        }
        private static ILog log = LogManager.GetCurrentClassLogger();
        public void doService(Stream outStream)
        {
            // 检测参数是否合法
            this.checkParams();

            ServerResponse serverResponse = this.getServerResponse();
            if (!serverResponse.isSuccess())
            {
                serverResponse.throwException();
            }

            log.Debug("OK Get response stream");

            serverResponse.doOutPut(outStream);

            log.Debug("OK response stream DONE");
        }

        private ServerResponse getServerResponse()
        {
            List<HttpParameter> nvps = buildCommonHttpParams();
            nvps.Add(new HttpParameter("s", downCode));

            return HttpTools.doPost(authConfig.getBaseURL() + uri, nvps);
        }

        /**
         * 参数校验
         */
        private void checkParams()
        {
            if (downCode == null)
            {
                throw new TreClientException(ErrorCode.EC0002, "params error, scret==null");
            }
        }

        public String getDownCode()
        {
            return downCode;
        }

        public void setDownCode(String downCode)
        {
            this.downCode = downCode;
        }
    }
}