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
using System.IO;
using Common.Logging;
using TRE.Config;
using TRE.Service;
using TRE.Service.Bean;
using TRE.Tools;

namespace TRE.Ext
{
    public class ReportExtServiceHelper
    {

        private static ILog log = LogManager.GetCurrentClassLogger();

        public static ReportQueryResult queryReport(AuthConfig authConfig, String taskNo)
        {
            ReportQueryService queryService = new ReportQueryService((IOAuth1Config)authConfig);
            queryService.Param = "{\"taskNo\":\"" + taskNo + "\"}"; // 给私有参数赋值
            return queryService.doService();
        }

        public static ReportQueryResult queryReport(AuthConfig authConfig, String taskNo, String fileCreateTime)
        {
            ReportQueryService queryService = new ReportQueryService((IOAuth1Config)authConfig);
            queryService.Param = "{\"taskNo\":\"" + taskNo + "\",\"fileTime\":\"" + fileCreateTime + "\"}";
            return queryService.doService();
        }


        public static void reportDownload(AuthConfig authConfig, String downCode, String localFilePath, String localFileName)
        {
            ReportDownloadService downloadService = new ReportDownloadService((IOAuth1Config)authConfig);
            downloadService.setDownCode(downCode); // 给私有参数赋值
            Stream outs = null;
            try
            {
                String fullPath = localFilePath.Replace("\\", "/");
                if (fullPath.LastIndexOf("/") != -1)
                {
                    fullPath = localFilePath + localFileName;
                }
                else
                {
                    fullPath = localFilePath + "/" + localFileName;
                }
                outs = new FileStream(fullPath, System.IO.FileMode.Create);
                downloadService.doService(outs);
            }
            catch (TreClientException e)
            {
                log.Error(e);
                throw e;
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            finally
            {
                IOUtils.close(outs);
            }
        }


        public static String stripDownCode(String str)
        {
            String dcode = "";
            int pos1 = str.IndexOf("downCode");
            if (pos1 != -1)
            {
                String str1 = str.Substring(pos1, str.Length - pos1);
                int pos2 = str1.IndexOf(":\"");
                String str2 = str1.Substring(pos2 + 2, str1.Length - pos2 - 2);
                int pos3 = str2.IndexOf("\"");
                dcode = str2.Substring(0, pos3);
            }
            return dcode;
        }

        public static String stripExecuteStatus(String str)
        {
            int pos1 = str.IndexOf("executeStatus");
            String str1 = str.Substring(pos1, str.Length - pos1);
            int pos2 = str1.IndexOf(":\"");
            String str2 = str1.Substring(pos2 + 2, str1.Length - pos2 - 2);
            int pos3 = str2.IndexOf("\"");
            return str2.Substring(0, pos3);
        }
    }
}