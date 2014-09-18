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

namespace TRE.Tools
{
    public class ErrorCode
    {
    /**
     * ReadMe：
     * ----每一条错误信息应该包括：错误代码和注释。
     * 
     * ----命名规范如下：
     * 'E'开头，长度为6，E后面可以跟1~4个字母，用于区别错误所属的模块(或类型)
     * 例如：EC开头的错误信息EC0000、EC0001等，代表公用的、常见的一些错误类型。
     * 
     */
    
    // EC开头 -- 公用的、常见的错误类型
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /** 未知错误，参见：{} */
    public const String EC0000 = "EC0000";
    
    /** 未定义错误，参见：{} */
    public const String EC0001 = "EC0001";
    
    /** 无效的参数：{} */
    public const String EC0002 = "EC0002";
    
    
    // E0开头 -- 未详细分类的错误信息
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /** 参数加密失败：{} */
    public const String E00001 = "E00001";
    
    
    // ERQ开头 -- HTTP请求相关的错误信息
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /** Parameter encryption failed: {} */
    public const String ERQ001 = "ERQ001";
    
    /** Http doPost error, details: {} */
    public const String ERQ002 = "ERQ002";
    
    
    // ES开头 -- 报表引擎Server端相关的错误信息
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /** The server response info is incorrect, details: {} */
    public const String ES0001 = "ES0001";
    
    /** TRE Server Error (code={}, message=[{}]) */
    public const String ES0002 = "ES0002";
    
    
    // EX开头 -- sdk/client接口内部定义的错误信息
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    /** 已经等待了{}秒，但是任务仍未执行完毕。放弃该任务，请重试！任务编号为{} */
    public const String EX0001 = "EX0001";
    
    /** 循环{} - 任务执行失败，若有疑问，请联系航信，任务编号为{} */
    public const String EX0002 = "EX0002";

    }
}
