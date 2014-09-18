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
using System.Web;
using TRE.Config;
using TRE.HTTP;
using TRE.Service.Bean;
using TRE.Tools;

namespace TRE.Service
{
    public class ReportQueryService : BasicOAuthService
    {
        private string param; // 私有参数

        public string Param
        {
            get { return param; }
            set { param = value; }
        }


        private void init()
        {
            procID = "16";
            uri = "service/request.do";
        }

        public ReportQueryService(IOAuth1Config authConfig)
            : base(authConfig)
        {
            this.init();
        }

        public ReportQueryResult doService()
        {
            // 检测参数是否合法
            this.checkParams();

            ServerResponse serverResponse = this.getServerResponse();
            if (!serverResponse.isSuccess())
            {
                serverResponse.throwException();
            }

            return this.assembleResultBean(serverResponse.getResultStr());
        }

        private ServerResponse getServerResponse()
        {
            List<HttpParameter> nvps = buildCommonHttpParams();
            nvps.Add(new HttpParameter("params", HttpUtility.UrlEncode(param, Encoding.GetEncoding("UTF-8"))));

            return HttpTools.doPost(authConfig.getBaseURL() + uri, nvps);
        }

        private ReportQueryResult assembleResultBean(String responseStr)
        {
            try
            {
                return new ReportQueryResult(responseStr);
            }
            catch (Exception e)
            {
                throw new TreClientException(ErrorCode.ES0001, "The result string parse error! message: " + e.Message);
            }
        }

        /**
         * 参数校验
         */
        private void checkParams()
        {
            if (param == null)
            {
                throw new TreClientException(ErrorCode.EC0002 + " params error, params==null");
            }
        }

    }
}