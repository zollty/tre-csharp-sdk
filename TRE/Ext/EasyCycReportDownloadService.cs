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
using TRE.Config;

namespace TRE.Ext
{
    public interface EasyCycReportDownloadService
    {

        /// <summary>
        ///    
        /// 远程下载报表文件<br>
        /// 
        /// downloadCycReportFile的执行步骤为：<br>
        /// 1、传入配置和查询参数，查询任务状态；<br>
        /// 2、如果任务已执行完成，则获取文件URL；<br>
        /// 3、下载文件并存储在指定位置。<br>
        /// 
        /// @param treConfig
        ///            报表引擎端提供的基础配置信息
        /// @param authConfig
        ///            报表引擎端提供的认证配置信息
        /// @param localFilePath
        ///            本地存储文件路径
        /// @param localFileName
        ///            本地存储文件名
        /// @param taskNo
        ///            指定周期报表的任务编号
        /// @param fileCreateTime           
        ///            指定文件生成时间
        /// @throws TreClientException 
        ///            失败时返回错误编码[e.getErrorCode()]，和错误信息[e.getMessage()]。
        /// </summary>
        String downloadCycReportFile(AuthConfig authConfig, String localFilePath, String localFileName, String taskNo,
            String fileCreateTime);

    }
}