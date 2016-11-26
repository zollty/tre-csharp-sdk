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

namespace TRE.Tools
{
    public class SimpleJsonUtils
    {

       /**
        * json 只能是类似：{"resultType": "000", "errorCode": "0x0001"}的简单格式。 即，只能有最外层一个{}，值的类型全部都是string，不能有内部对象和数组。
        * 
        * @param json
        */
        public static Dictionary<String, String> simpleJsonToMap(String json)
        {
            var map = new Dictionary<String, String>();
            char[] chars = json.ToCharArray();
            int a = -1;
            String key = null;
            for (var i = 1; i < chars.Length - 1; i++)
            {
                if (chars[i] != '\"' || chars[i - 1] == '\\') continue;
                if (a == -1)
                {
                    a = i;
                }
                else
                {
                    if (key == null)
                    {
                        key = json.Substring(a + 1, i-a-1);
                    }
                    else
                    {
                        map.Add(key, json.Substring(a + 1, i-a-1).Replace("\\\"", "\""));
                        key = null;
                    }
                    a = -1;
                }
            }
            return map;
        }
    }
}