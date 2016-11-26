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

namespace TRE.Tools
{
    public class IOUtils
    {

        /**
         * max read file buffer size. default 500k
         */
        private const int MAX_BUFFER_SIZE = 512000;

        /**
         * min read file buffer size. default 1k
         */
        private const int MIN_BUFFER_SIZE = 1024;

        private static ILog log = LogManager.GetCurrentClassLogger();

        public static void clone(Stream ins, Stream outs)
        {
            clone(ins, MIN_BUFFER_SIZE, outs);
        }

        /**
         * @param len in-source-length e.g. long len = fileIn.length()
         */
        public static void clone(Stream ins, long len, Stream outs)
        {
            byte[] buf;
            // 动态缓存大小
            // case1 LEN>200000kb(195M) -- BUF=500kb e.g. 200M--500k
            // case2 400kb< LEN <200000kb -- BUF=LEN/400 e.g. 100M--250k,
            // 10M--25k, 400kb--1kb
            // case3 LEN<400kb -- BUF=1kb e.g. 300kb--1kb, 0kb-1kb
            if (len > MAX_BUFFER_SIZE * 400)
            {
                buf = new byte[MAX_BUFFER_SIZE];
            }
            else if (len > MIN_BUFFER_SIZE * 400)
            {
                buf = new byte[(int)len / 400];
            }
            else
            {
                buf = new byte[MIN_BUFFER_SIZE];
            }
            int numRead = 0;
            while ( (numRead = ins.Read(buf, 0, buf.Length)) >0 )
            {
                outs.Write(buf, 0, numRead);
            }
        }

        public static void close(Stream ins)
        {
            if (null != ins)
            {
                try
                {
                    ins.Close();
                }
                catch (Exception e)
                {
                    log.Debug(m => m("IO close error: {0}", e.ToString()));
                }
            }
        }

        public static void close(Stream ins, Stream outs)
        {
           close(ins);
           close(outs);
        }

    }
}