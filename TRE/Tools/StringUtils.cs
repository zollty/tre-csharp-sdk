﻿//-----------------------------------------------------------------------
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
    class StringUtils
    {

        public static String removeLastStr(String str, int len)
        {

            return str.Substring(0, str.Length - len);

        }
    }
}