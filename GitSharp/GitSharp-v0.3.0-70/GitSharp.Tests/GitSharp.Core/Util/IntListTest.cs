﻿/*
 * Copyright (C) 2008, Google Inc.
 * Copyright (C) 2009, Gil Ran <gilrun@gmail.com>
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
using GitSharp.Core.Util;
using GitSharp.Tests.GitSharp.Core.Util;
using NUnit.Framework;

namespace GitSharp.Core.Tests.Util
{
    [TestFixture]
    public class IntListTest
    {
        [Test]
        public void testEmpty_DefaultCapacity()
        {
            IntList i = new IntList();
            Assert.AreEqual(0, i.size());
            try
            {
                i.get(0);
                Assert.Fail("Accepted 0 index on empty list");
            }
            catch (IndexOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void testEmpty_SpecificCapacity()
        {
            IntList i = new IntList(5);
            Assert.AreEqual(0, i.size());
            try
            {
                i.get(0);
                Assert.Fail("Accepted 0 index on empty list");
            }
            catch (IndexOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void testAdd_SmallGroup()
        {
            IntList i = new IntList();
            int n = 5;
            for (int v = 0; v < n; v++)
                i.add(10 + v);

            Assert.AreEqual(n, i.size());

            for (int v = 0; v < n; v++)
                Assert.AreEqual(10 + v, i.get(v));

            try
            {
                i.get(n);
                Assert.Fail("Accepted out of bound index on list");
            }
            catch (IndexOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void testAdd_ZeroCapacity()
        {
            IntList i = new IntList(0);
            Assert.AreEqual(0, i.size());
            i.add(1);
            Assert.AreEqual(1, i.get(0));
        }

        [Test]
        public void testAdd_LargeGroup()
        {
            IntList i = new IntList();
            int n = 500;
            for (int v = 0; v < n; v++)
                i.add(10 + v);

            Assert.AreEqual(n, i.size());

            for (int v = 0; v < n; v++)
                Assert.AreEqual(10 + v, i.get(v));

            try
            {
                i.get(n);
                Assert.Fail("Accepted out of bound index on list");
            }
            catch (IndexOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void testFillTo0()
        {
            IntList i = new IntList();
            i.fillTo(0, int.MinValue);
            Assert.AreEqual(0, i.size());
        }

        [Test]
        public void testFillTo1()
        {
            IntList i = new IntList();
            i.fillTo(1, int.MinValue);
            Assert.AreEqual(1, i.size());
            i.add(0);
            Assert.AreEqual(int.MinValue, i.get(0));
            Assert.AreEqual(0, i.get(1));
        }

        [Test]
        public void testFillTo100()
        {
            IntList i = new IntList();
            i.fillTo(100, int.MinValue);
            Assert.AreEqual(100, i.size());
            i.add(3);
            Assert.AreEqual(int.MinValue, i.get(99));
            Assert.AreEqual(3, i.get(100));
        }

        [Test]
        public void testClear()
        {
            IntList i = new IntList();
            int n = 5;
            for (int v = 0; v < n; v++)
                i.add(10 + v);
            Assert.AreEqual(n, i.size());

            i.clear();
            Assert.AreEqual(0, i.size());

            try
            {
                i.get(0);
                Assert.Fail("Accepted 0 index on empty list");
            }
            catch (IndexOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void testSet()
        {
            IntList i = new IntList();
            i.add(1);
            Assert.AreEqual(1, i.size());
            Assert.AreEqual(1, i.get(0));

            i.set(0, 5);
            Assert.AreEqual(5, i.get(0));

            AssertHelper.Throws<ArgumentException>(() => i.set(5, 5), "accepted set of 5 beyond end of list");

            i.set(1, 2);
            Assert.AreEqual(2, i.size());
            Assert.AreEqual(2, i.get(1));
        }

        [Test]
        public void testToString()
        {
            IntList i = new IntList();
            i.add(1);
            Assert.AreEqual("[1]", i.toString());
            i.add(13);
            i.add(5);
            Assert.AreEqual("[1, 13, 5]", i.toString());
        }
    }
}