﻿/*
 * Copyright (C) 2008, Google Inc.
 * Copyright (C) 2009, Henon <meinrad.recheis@gmail.com>
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

using System;
using GitSharp.Core;
using NUnit.Framework;

namespace GitSharp.Core.Tests
{
    [TestFixture]
    public class AbbreviatedObjectIdTest
    {

        [Test]
        public void testEmpty_FromByteArray()
        {
        	AbbreviatedObjectId i = AbbreviatedObjectId.FromString(new byte[] { }, 0, 0);
            Assert.IsNotNull(i);
            Assert.AreEqual(0, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(string.Empty, i.name());
        }

        [Test]
        public void testEmpty_FromString()
        {
			AbbreviatedObjectId i = AbbreviatedObjectId.FromString(string.Empty);
            Assert.IsNotNull(i);
            Assert.AreEqual(0, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(string.Empty, i.name());
        }

        [Test]
        public void testFull_FromByteArray()
        {
            const string s = "7b6e8067ec96acef9a4184b43210d583b6d2f99a";
            byte[] b = Constants.encodeASCII(s);
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(b, 0, b.Length);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsTrue(i.isComplete());
            Assert.AreEqual(s, i.name());

            ObjectId f = i.ToObjectId();
            Assert.IsNotNull(f);
            Assert.AreEqual(ObjectId.FromString(s), f);
            Assert.AreEqual(f.GetHashCode(), i.GetHashCode());
        }

        [Test]
        public void testFull_FromString()
        {
            const string s = "7b6e8067ec96acef9a4184b43210d583b6d2f99a";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsTrue(i.isComplete());
            Assert.AreEqual(s, i.name());

            ObjectId f = i.ToObjectId();
            Assert.IsNotNull(f);
            Assert.AreEqual(ObjectId.FromString(s), f);
            Assert.AreEqual(f.GetHashCode(), i.GetHashCode());
        }

        [Test]
        public void test1_FromString()
        {
            const string s = "7";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(s, i.name());
            Assert.IsNull(i.ToObjectId());
        }

        [Test]
        public void test2_FromString()
        {
            const string s = "7b";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(s, i.name());
            Assert.IsNull(i.ToObjectId());
        }

        [Test]
        public void test3_FromString()
        {
            const string s = "7b6";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(s, i.name());
            Assert.IsNull(i.ToObjectId());
        }

        [Test]
        public void test4_FromString()
        {
            const string s = "7b6e";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(s, i.name());
            Assert.IsNull(i.ToObjectId());
        }

        [Test]
        public void test5_FromString()
        {
            const string s = "7b6e8";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(s, i.name());
            Assert.IsNull(i.ToObjectId());
        }

        [Test]
        public void test6_FromString()
        {
            const string s = "7b6e80";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(s, i.name());
            Assert.IsNull(i.ToObjectId());
        }

        [Test]
        public void test7_FromString()
        {
            const string s = "7b6e806";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(s, i.name());
            Assert.IsNull(i.ToObjectId());
        }

        [Test]
        public void test8_FromString()
        {
            const string s = "7b6e8067";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(s, i.name());
            Assert.IsNull(i.ToObjectId());
        }

        [Test]
        public void test9_FromString()
        {
            const string s = "7b6e8067e";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(s, i.name());
            Assert.IsNull(i.ToObjectId());
        }

        [Test]
        public void test17_FromString()
        {
            const string s = "7b6e8067ec96acef9";
            AbbreviatedObjectId i = AbbreviatedObjectId.FromString(s);
            Assert.IsNotNull(i);
            Assert.AreEqual(s.Length, i.Length);
            Assert.IsFalse(i.isComplete());
            Assert.AreEqual(s, i.name());
            Assert.IsNull(i.ToObjectId());
        }

        [Test]
        public void testEquals_Short()
        {
            const string s = "7b6e8067";
            AbbreviatedObjectId a = AbbreviatedObjectId.FromString(s);
            AbbreviatedObjectId b = AbbreviatedObjectId.FromString(s);
            Assert.AreNotSame(a, b);
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
        }

        [Test]
        public void testEquals_Full()
        {
            const string s = "7b6e8067ec96acef9a4184b43210d583b6d2f99a";
            AbbreviatedObjectId a = AbbreviatedObjectId.FromString(s);
            AbbreviatedObjectId b = AbbreviatedObjectId.FromString(s);
            Assert.AreNotSame(a, b);
            Assert.IsTrue(a.GetHashCode() == b.GetHashCode());
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
        }

        [Test]
        public void testNotEquals_SameLength()
        {
            const string sa = "7b6e8067";
            const string sb = "7b6e806e";
            AbbreviatedObjectId a = AbbreviatedObjectId.FromString(sa);
            AbbreviatedObjectId b = AbbreviatedObjectId.FromString(sb);
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(b.Equals(a));
        }

        [Test]
        public void testNotEquals_DiffLength()
        {
            const string sa = "7b6e8067abcd";
            const string sb = "7b6e8067";
            AbbreviatedObjectId a = AbbreviatedObjectId.FromString(sa);
            AbbreviatedObjectId b = AbbreviatedObjectId.FromString(sb);
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(b.Equals(a));
        }

        [Test]
        public void testPrefixCompare_Full()
        {
            const string s1 = "7b6e8067ec96acef9a4184b43210d583b6d2f99a";
            AbbreviatedObjectId a = AbbreviatedObjectId.FromString(s1);
            ObjectId i1 = ObjectId.FromString(s1);
            Assert.AreEqual(0, a.prefixCompare(i1));
            Assert.IsTrue(i1.startsWith(a));

            const string s2 = "7b6e8067ec96acef9a4184b43210d583b6d2f99b";
            ObjectId i2 = ObjectId.FromString(s2);
            Assert.IsTrue(a.prefixCompare(i2) < 0);
            Assert.IsFalse(i2.startsWith(a));

            const string s3 = "7b6e8067ec96acef9a4184b43210d583b6d2f999";
            ObjectId i3 = ObjectId.FromString(s3);
            Assert.IsTrue(a.prefixCompare(i3) > 0);
            Assert.IsFalse(i3.startsWith(a));
        }

        [Test]
        public void testPrefixCompare_1()
        {
            const string sa = "7";
            AbbreviatedObjectId a = AbbreviatedObjectId.FromString(sa);

            const string s1 = "7b6e8067ec96acef9a4184b43210d583b6d2f99a";
            ObjectId i1 = ObjectId.FromString(s1);
            Assert.AreEqual(0, a.prefixCompare(i1));
            Assert.IsTrue(i1.startsWith(a));

            const string s2 = "8b6e8067ec96acef9a4184b43210d583b6d2f99a";
            ObjectId i2 = ObjectId.FromString(s2);
            Assert.IsTrue(a.prefixCompare(i2) < 0);
            Assert.IsFalse(i2.startsWith(a));

            const string s3 = "6b6e8067ec96acef9a4184b43210d583b6d2f99a";
            ObjectId i3 = ObjectId.FromString(s3);
            Assert.IsTrue(a.prefixCompare(i3) > 0);
            Assert.IsFalse(i3.startsWith(a));
        }

        [Test]
        public void testPrefixCompare_7()
        {
            const string sa = "7b6e806";
            AbbreviatedObjectId a = AbbreviatedObjectId.FromString(sa);

            const string s1 = "7b6e8067ec96acef9a4184b43210d583b6d2f99a";
            ObjectId i1 = ObjectId.FromString(s1);
            Assert.AreEqual(0, a.prefixCompare(i1));
            Assert.IsTrue(i1.startsWith(a));

            const string s2 = "7b6e8167ec86acef9a4184b43210d583b6d2f99a";
            ObjectId i2 = ObjectId.FromString(s2);
            Assert.IsTrue(a.prefixCompare(i2) < 0);
            Assert.IsFalse(i2.startsWith(a));

            const string s3 = "7b6e8057eca6acef9a4184b43210d583b6d2f99a";
            ObjectId i3 = ObjectId.FromString(s3);
            Assert.IsTrue(a.prefixCompare(i3) > 0);
            Assert.IsFalse(i3.startsWith(a));
        }

        [Test]
        public void testPrefixCompare_8()
        {
            const string sa = "7b6e8067";
            AbbreviatedObjectId a = AbbreviatedObjectId.FromString(sa);

            const string s1 = "7b6e8067ec96acef9a4184b43210d583b6d2f99a";
            ObjectId i1 = ObjectId.FromString(s1);
            Assert.AreEqual(0, a.prefixCompare(i1));
            Assert.IsTrue(i1.startsWith(a));

            const string s2 = "7b6e8167ec86acef9a4184b43210d583b6d2f99a";
            ObjectId i2 = ObjectId.FromString(s2);
            Assert.IsTrue(a.prefixCompare(i2) < 0);
            Assert.IsFalse(i2.startsWith(a));

            const string s3 = "7b6e8057eca6acef9a4184b43210d583b6d2f99a";
            ObjectId i3 = ObjectId.FromString(s3);
            Assert.IsTrue(a.prefixCompare(i3) > 0);
            Assert.IsFalse(i3.startsWith(a));
        }

        [Test]
        public void testPrefixCompare_9()
        {
            const string sa = "7b6e8067e";
            AbbreviatedObjectId a = AbbreviatedObjectId.FromString(sa);

            const string s1 = "7b6e8067ec96acef9a4184b43210d583b6d2f99a";
            ObjectId i1 = ObjectId.FromString(s1);
            Assert.AreEqual(0, a.prefixCompare(i1));
            Assert.IsTrue(i1.startsWith(a));

            const string s2 = "7b6e8167ec86acef9a4184b43210d583b6d2f99a";
            ObjectId i2 = ObjectId.FromString(s2);
            Assert.IsTrue(a.prefixCompare(i2) < 0);
            Assert.IsFalse(i2.startsWith(a));

            const string s3 = "7b6e8057eca6acef9a4184b43210d583b6d2f99a";
            ObjectId i3 = ObjectId.FromString(s3);
            Assert.IsTrue(a.prefixCompare(i3) > 0);
            Assert.IsFalse(i3.startsWith(a));
        }

        [Test]
        public void testPrefixCompare_17()
        {
            const string sa = "7b6e8067ec96acef9";
            AbbreviatedObjectId a = AbbreviatedObjectId.FromString(sa);

            const string s1 = "7b6e8067ec96acef9a4184b43210d583b6d2f99a";
            ObjectId i1 = ObjectId.FromString(s1);
            Assert.AreEqual(0, a.prefixCompare(i1));
            Assert.IsTrue(i1.startsWith(a));

            const string s2 = "7b6e8067eca6acef9a4184b43210d583b6d2f99a";
            ObjectId i2 = ObjectId.FromString(s2);
            Assert.IsTrue(a.prefixCompare(i2) < 0);
            Assert.IsFalse(i2.startsWith(a));

            const string s3 = "7b6e8067ec86acef9a4184b43210d583b6d2f99a";
            ObjectId i3 = ObjectId.FromString(s3);
            Assert.IsTrue(a.prefixCompare(i3) > 0);
            Assert.IsFalse(i3.startsWith(a));
        }
    }
}