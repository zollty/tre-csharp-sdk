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

namespace TRE.OAuth
{
    public class HMACSHA1Signature : OAuthSignature
    {

        public const String SIGNATURE_NAME = "hmac-sha-1";
        private String key;


        public HMACSHA1Signature(String key)
        {
            this.key = key;
        }

        /// <summary>
        /// The name of the OAuth signature method.
        /// </summary>
        /// <returns>The name of the OAuth signature method.</returns>
        public String getName()
        {
            return SIGNATURE_NAME;
        }


        /// <summary>
        /// 签名<br/>
        /// Sign the signature base string.
        /// </summary>
        /// <param name="signatureBaseString">The signature base string to sign.</param>
        /// <returns>The signature.</returns>
        public String sign(String signatureBaseString)
        {

            byte[] ipadArray = new byte[64];
            byte[] opadArray = new byte[64];
            byte[] keyArray = new byte[64];
            System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            int ex = key.Length;
            if (key.Length > 64)
            {
                byte[] temp = sha1.ComputeHash(Encoding.Default.GetBytes(key));
                ex = temp.Length;
                for (int i = 0; i < ex; i++)
                {
                    keyArray[i] = temp[i];
                }
            }
            else
            {
                byte[] temp = Encoding.Default.GetBytes(key);
                for (int i = 0; i < temp.LongLength; i++)
                {
                    keyArray[i] = temp[i];
                }
            }
            for (int i = ex; i < 64; i++)
            {
                keyArray[i] = 0;
            }
            for (int j = 0; j < 64; j++)
            {
                ipadArray[j] = (byte)(keyArray[j] ^ 0x36);
                opadArray[j] = (byte)(keyArray[j] ^ 0x5C);
            }
            byte[] tempResult = sha1.ComputeHash(join(ipadArray, Encoding.Default.GetBytes(signatureBaseString)));

            byte[] sb = sha1.ComputeHash(join(opadArray, tempResult));
            return Convert.ToBase64String(sb);
        }


        /// <summary>
        /// 检验签名<br/>
        /// Verify the specified signature on the given signature base string.
        /// </summary>
        /// <param name="signatureBaseString">The signature base string to sign.</param>
        /// <param name="signature">The signature.</param>
        /// <returns>The signature.</returns>
        public void verify(String signatureBaseString, String signature)
        {
            throw new Exception("THIS METHOD NOT SUPPORT YET!");
        }

        private static byte[] join(byte[] b1, byte[] b2)
        {

            int length = b1.Length + b2.Length;
            byte[] newer = new byte[length];
            for (int i = 0; i < b1.LongLength; i++)
            {
                newer[i] = b1[i];
            }
            for (int i = 0; i < b2.LongLength; i++)
            {
                newer[i + b1.LongLength] = b2[i];
            }
            return newer;
        }

    }

}