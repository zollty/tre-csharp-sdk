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

namespace TRE.Tools
{
    public class JsonTools
    {
        public static Boolean isValidJsonObject(String jsonStr)
        {
            if (jsonStr == null || jsonStr.Trim().Length == 0)
            {
                return false;
            }
            if (!jsonStr.StartsWith("{") || !jsonStr.EndsWith("}"))
            {
                return false;
            }
            return true;
        }

        //private static ILog log = LogManager.GetCurrentClassLogger();

        public static void assumeItsAnError(String jsonStr)
        {
            if (!isValidJsonObject(jsonStr))
            {
                return;
            }
            Dictionary<String, String> map = SimpleJsonUtils.simpleJsonToMap(jsonStr);

            String errorCode = null;
            String resultType = null;
            String errorMsg = null;

            map.TryGetValue("errorCode", out errorCode);
            if (errorCode != null )
            {
                map.TryGetValue("resultType", out resultType);
                if ("000".Equals(resultType))
                {
                    map.TryGetValue("errorMsg", out errorMsg);
                    //log.Error(jsonStr);
                    //log.Error(errorMsg);
                    throw new TreClientException(errorCode, errorMsg);
                }
            }
        }

    }
}