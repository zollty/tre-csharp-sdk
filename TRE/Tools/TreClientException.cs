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
    public class TreClientException : Exception
    {
        private string errorCode;

        public string ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }

        public TreClientException()
            : base()
        {
            logException();
        }
        public TreClientException(string errorCode)
            : base(errorCode)
        {
            this.ErrorCode = errorCode;
            logException();
        }

        public TreClientException(string errorCode, string errorMsg)
            : base(errorCode + ": " + errorMsg)
        {
            this.ErrorCode = errorCode;
            logException();
        }

        public TreClientException(string info, Exception innerException)
            : base(info, innerException)
        {
            this.ErrorCode = info;
            logException();
        }

        //日志的记录 
        private void logException()
        {
            // Console.WriteLine(this.ToString());
        }

    }
}