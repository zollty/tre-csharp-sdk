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
using System.Text;

namespace TRE.Tools
{
    public class RandomUtils
    {

        /// <summary>
        /// 获取长度为{len}的随机字符串（A_Za_z组成）
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static String getRadomStrAZaz(int len)
        {
            StringBuilder bstr = new StringBuilder(len);
            int n;
            for (int i = 0; i < len; i++)
            {
                n = 65 + nextInt(52); // 65-116（122-5）
                if (n >= 91 && n <= 96)
                { // 将91-96映射成117-122
                    n += 26;
                }
                bstr.Append((char)n);
            }
            return bstr.ToString();
        }

        /// <summary>
        ///  获取长度为{len}的随机字符串（0_9A_Za_z组成）
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static String getRadomStr09AZaz(int len)
        {
            StringBuilder bstr = new StringBuilder(len);
            int n;
            for (int i = 0; i < len; i++)
            {
                n = 48 + nextInt(62);
                if (n >= 58 && n <= 64)
                {
                    n += 58;
                }
                else if (n >= 91 && n <= 96)
                {
                    n += 19;
                }
                bstr.Append((char)n);
            }
            return bstr.ToString();
        }


        /// <summary>
        ///  Generate radom number string 生成随机数字符串 <br>
        ///  the param (start, end) must in region [0,9] or ['A','Z'] or ['a', 'z'] <br>
        ///  e.g. (2,8) or ('A','K')
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static String getRadomStr(int start, int end, int len)
        {
            if (start > -1 && end < 10 && start < end)
            {
                StringBuilder bstr = new StringBuilder(len);
                int num = end + 1 - start;
                for (int i = 0; i < len; i++)
                {
                    bstr.Append(nextInt(num) + start);
                }
                return bstr.ToString();
            }

            if (start > 64 && end < 91 && start < end)
            {
                StringBuilder bstr = new StringBuilder(len);
                int num = end + 1 - start;
                int n;
                for (int i = 0; i < len; i++)
                {
                    n = nextInt(num) + start;
                    bstr.Append((char)n);
                }
                return bstr.ToString();
            }

            if (start > 96 && end < 123 && start < end)
            {
                StringBuilder bstr = new StringBuilder(len);
                int num = end + 1 - start;
                int n;
                for (int i = 0; i < len; i++)
                {
                    n = nextInt(num) + start;
                    bstr.Append((char)n);
                }
                return bstr.ToString();
            }

            throw new Exception("start=" + start + ", end=" + end
                    + ". Arguments not correct, should in region [0,9] or ['A','Z'] or ['a', 'z']");
        }

        /// <summary>
        /// 获取长度为len的，范围为 'A'到'Z'的随机字母字符串
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static String getRadomStrAZ(int len)
        {
            return getRadomStr('A', 'Z', len);
        }

        public static String getRadomStr09(int len)
        {
            StringBuilder bstr = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                bstr.Append(nextInt(10));
            }
            return bstr.ToString();
        }

        public static String getRadomStr09AZ(int len)
        {
            StringBuilder bstr = new StringBuilder(len);
            int n;
            for (int i = 0; i < len; i++)
            {
                n = 48 + nextInt(36);
                if (n >= 58 && n <= 64)
                {
                    n += 26;
                }
                bstr.Append((char)n);
            }
            return bstr.ToString();
        }

        public static String getRadomStr09az(int len)
        {
            StringBuilder bstr = new StringBuilder(len);
            int n;
            for (int i = 0; i < len; i++)
            {
                n = 48 + nextInt(36);
                if (n <= 57)
                {
                    bstr.Append((char)n);
                }
                else
                {
                    if (n >= 58 && n <= 64)
                    {
                        n += 26;
                    }
                    n += 32;
                    bstr.Append((char)n);
                }
            }
            return bstr.ToString();
        }


        private static object lockThis = new object();
        private static Random randomNumberGenerator;

        private static void initRNG()
        {
            lock (lockThis)
            {
                if (randomNumberGenerator == null)
                {
                    randomNumberGenerator = new Random();
                }
            }
        }

        /// <summary>
        /// 获取 [0,n-1] 范围的随机数
        /// </summary>
        /// <param name="n">最大值</param>
        /// <returns></returns>
        public static int nextInt(int n)
        {
            if (randomNumberGenerator == null)
            {
                initRNG();
            }
            return randomNumberGenerator.Next(n);
        }
    }
}