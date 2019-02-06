﻿/*
 * Copyright (C) 2009, Google Inc.
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or
 * without modification, are permitted provided that the following
 * conditions are met:
 *
 * - Redistributions of source code must retain the above copyright
 *   notice, this list of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above
 *   copyright notice, this list of conditions and the following
 *   disclaimer in the documentation and/or other materials provided
 *   with the distribution.
 *
 * - Neither the name of the Git Development Community nor the
 *   names of its contributors may be used to endorse or promote
 *   products derived from this software without specific prior
 *   written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using GitSharp.Core.Util;
using NUnit.Framework;

namespace GitSharp.Core.Tests.Util
{

    [TestFixture]
    public class StringUtilsTest
    {
        
        [Test]
        public void testToLowerCaseChar()
        {
            Assert.AreEqual('a', StringUtils.toLowerCase('A'));
            Assert.AreEqual('z', StringUtils.toLowerCase('Z'));

            Assert.AreEqual('a', StringUtils.toLowerCase('a'));
            Assert.AreEqual('z', StringUtils.toLowerCase('z'));

            Assert.AreEqual((char)0, StringUtils.toLowerCase((char)0));
            Assert.AreEqual((char)0xffff, StringUtils.toLowerCase((char)0xffff));
        }

        [Test]
        public void testToLowerCaseString()
        {
            Assert.AreEqual("\n abcdefghijklmnopqrstuvwxyz\n", StringUtils.toLowerCase("\n ABCDEFGHIJKLMNOPQRSTUVWXYZ\n"));
        }

        [Test]
        public void testEqualsIgnoreCase1()
        {
            const string a = "FOO";
            Assert.IsTrue(StringUtils.equalsIgnoreCase(a, a));
        }

        [Test]
        public void testEqualsIgnoreCase2()
        {
            Assert.IsFalse(StringUtils.equalsIgnoreCase("a", ""));
        }

        [Test]
        public void testEqualsIgnoreCase3()
        {
            Assert.IsFalse(StringUtils.equalsIgnoreCase("a", "b"));
            Assert.IsFalse(StringUtils.equalsIgnoreCase("ac", "ab"));
        }
        [Test]
        public void testEqualsIgnoreCase4()
        {
            Assert.IsTrue(StringUtils.equalsIgnoreCase("a", "a"));
            Assert.IsTrue(StringUtils.equalsIgnoreCase("A", "a"));
            Assert.IsTrue(StringUtils.equalsIgnoreCase("a", "A"));
        }

    }

}