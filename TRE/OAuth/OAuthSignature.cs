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

namespace TRE.OAuth
{
    public interface OAuthSignature
    {
        /// <summary>
        /// The name of the OAuth signature method.
        /// </summary>
        /// <returns>The name of the OAuth signature method.</returns>
        String getName();


        /// <summary>
        /// 签名<br/>
        /// Sign the signature base string.
        /// </summary>
        /// <param name="signatureBaseString">The signature base string to sign.</param>
        /// <returns>The signature.</returns>
        String sign(String signatureBaseString);


        /// <summary>
        /// 检验签名<br/>
        /// Verify the specified signature on the given signature base string.
        /// </summary>
        /// <param name="signatureBaseString">The signature base string to sign.</param>
        /// <param name="signature">The signature.</param>
        /// <returns>The signature.</returns>
        void verify(String signatureBaseString, String signature);
    }
}