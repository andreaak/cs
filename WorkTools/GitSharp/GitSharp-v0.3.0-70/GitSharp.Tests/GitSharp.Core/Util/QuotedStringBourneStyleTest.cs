﻿/*
 * Copyright (C) 2008, Google Inc.
 * Copyright (C) 2009, Gil Ran <gilrun@gmail.com>
 *
 * All rights reserved.
 *
 * Redistribution and use in_str source and binary forms, with or
 * without modification, are permitted provided that the following
 * conditions are met:
 *
 * - Redistributions of source code must retain the above copyright
 *   notice, this list of conditions and the following disclaimer.
 *
 * - Redistributions in_str binary form must reproduce the above
 *   copyright notice, this list of conditions and the following
 *   disclaimer in_str the documentation and/or other materials provided
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

using System;
using GitSharp.Core;
using GitSharp.Core.Util;
using NUnit.Framework;

namespace GitSharp.Core.Tests.Util
{
    [TestFixture]
    public class QuotedStringBourneStyleTest
    {
        private static void AssertQuote(string inStr, string exp)
        {
            string r = QuotedString.BOURNE.quote(inStr);
            Assert.AreNotSame(inStr, r);
            Assert.IsFalse(inStr.Equals(r));
            Assert.AreEqual('\'' + exp + '\'', r);
        }

        private static void AssertDequote(string exp, string inStr)
        {
            byte[] b = Constants.encode('\'' + inStr + '\'');
            String r = QuotedString.BOURNE.dequote(b, 0, b.Length);
            Assert.AreEqual(exp, r);
        }

        [Test]
        public void testDequote_BareA()
        {
            const string in_str = "a";
            byte[] b = Constants.encode(in_str);
            Assert.AreEqual(in_str, QuotedString.BOURNE.dequote(b, 0, b.Length));
        }

        [Test]
        public void testDequote_BareABCZ_OnlyBC()
        {
            const string in_str = "abcz";
            byte[] b = Constants.encode(in_str);
            int p = in_str.IndexOf('b');
            Assert.AreEqual("bc", QuotedString.BOURNE.dequote(b, p, p + 2));
        }

        [Test]
        public void testDequote_Empty1()
        {
            Assert.AreEqual(string.Empty, QuotedString.BOURNE.dequote(new byte[0], 0, 0));
        }

        [Test]
        public void testDequote_Empty2()
        {
            Assert.AreEqual(string.Empty, QuotedString.BOURNE.dequote(new[] {(byte) '\'', (byte) '\''}, 0, 2));
        }

        [Test]
        public void testDequote_LoneBackslash()
        {
            AssertDequote("\\", "\\");
        }

        [Test]
        public void testDequote_NamedEscapes()
        {
            AssertDequote("'", "'\\''");
            AssertDequote("!", "'\\!'");

            AssertDequote("a'b", "a'\\''b");
            AssertDequote("a!b", "a'\\!'b");
        }

        [Test]
        public void testDequote_SoleSq()
        {
            Assert.AreEqual(string.Empty, QuotedString.BOURNE.dequote(new[] {(byte) '\''}, 0, 1));
        }

        [Test]
        public void testQuote_BareA()
        {
            AssertQuote("a", "a");
        }

        [Test]
        public void testQuote_Empty()
        {
            Assert.AreEqual("''", QuotedString.BOURNE.quote(string.Empty));
        }

        [Test]
        public void testQuote_NamedEscapes()
        {
            AssertQuote("'", "'\\''");
            AssertQuote("!", "'\\!'");

            AssertQuote("a'b", "a'\\''b");
            AssertQuote("a!b", "a'\\!'b");
        }
    }
}