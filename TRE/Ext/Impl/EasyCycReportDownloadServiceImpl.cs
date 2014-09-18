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
using Common.Logging;
using TRE.Config;
using TRE.Service.Bean;
using TRE.Tools;

namespace TRE.Ext.Impl
{
    
    class EasyCycReportDownloadServiceImpl : EasyCycReportDownloadService
    {
        private static ILog log = LogManager.GetCurrentClassLogger();

        public string downloadCycReportFile(AuthConfig authConfig, string localFilePath, string localFileName, string taskNo,
            string fileCreateTime)
        {
            ReportQueryResult queryResult = ReportExtServiceHelper.queryReport(authConfig, taskNo, fileCreateTime);
            String qresJson = queryResult.ResultJson;
            String downCode = ReportExtServiceHelper.stripDownCode(qresJson);
            if ("".Equals(downCode))
            {
                throw new TreClientException(ErrorCode.EC0001, "任务报表文件生成失败");
            }
            ReportExtServiceHelper.reportDownload(authConfig, downCode, localFilePath, localFileName);
            log.Debug(m => m("下载成功，存放路径为: {0}{1}", localFilePath, localFileName));
            return localFilePath + localFileName;
        }
    }
}
