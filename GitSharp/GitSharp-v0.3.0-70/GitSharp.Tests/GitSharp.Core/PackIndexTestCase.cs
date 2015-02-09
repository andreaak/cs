﻿/*
 * Copyright (C) 2008, Marek Zawirski <marek.zawirski@gmail.com>
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


using System.Collections.Generic;
using System.IO;
using GitSharp.Core;
using GitSharp.Core.Tests;
using NUnit.Framework;

namespace GitSharp.Tests.GitSharp.Core
{
    public abstract class PackIndexTestCase : RepositoryTestCase
    {
        protected PackIndex smallIdx;
        private PackIndex _denseIdx;

        [SetUp]
        public override void setUp()
        {
            base.setUp();
            smallIdx = PackIndex.Open(GetFileForPack34Be9032());
            _denseIdx = PackIndex.Open(GetFileForPackdf2982F28());
        }

        /// <summary>
        /// Return File with appropriate index version for prepared pack.
        /// </summary>
        /// <returns></returns>
        protected abstract FileInfo GetFileForPack34Be9032();

        /// <summary>
        /// Return File with appropriate index version for prepared pack.
        /// </summary>
        /// <returns></returns>
        protected abstract FileInfo GetFileForPackdf2982F28();

        /// <summary>
        /// Verify CRC32 support.
        /// </summary>
        public abstract void testCRC32();

        /// <summary>
        /// Test contracts of Iterator methods and this implementation remove()
        /// limitations.
        /// </summary>
        [Test]
        public void testIteratorMethodsContract()
        {
            IEnumerator<PackIndex.MutableEntry> iter = smallIdx.GetEnumerator();
            while (iter.MoveNext())
            {
                var entry = iter.Current;
            }
            Assert.IsFalse(iter.MoveNext());
        }

        /// <summary>
        /// Test results of iterator comparing to content of well-known (prepared)
        /// small index.
        /// </summary>
        [Test]
        public void testIteratorReturnedValues1()
        {
            IEnumerator<PackIndex.MutableEntry> iter = smallIdx.GetEnumerator();
            iter.MoveNext();
            Assert.AreEqual("4b825dc642cb6eb9a060e54bf8d69288fbee4904", iter.Current.Name);
            iter.MoveNext();
            Assert.AreEqual("540a36d136cf413e4b064c2b0e0a4db60f77feab", iter.Current.Name);
            iter.MoveNext();
            Assert.AreEqual("5b6e7c66c276e7610d4a73c70ec1a1f7c1003259", iter.Current.Name);
            iter.MoveNext();
            Assert.AreEqual("6ff87c4664981e4397625791c8ea3bbb5f2279a3", iter.Current.Name);
            iter.MoveNext();
            Assert.AreEqual("82c6b885ff600be425b4ea96dee75dca255b69e7", iter.Current.Name);
            iter.MoveNext();
            Assert.AreEqual("902d5476fa249b7abc9d84c611577a81381f0327", iter.Current.Name);
            iter.MoveNext();
            Assert.AreEqual("aabf2ffaec9b497f0950352b3e582d73035c2035", iter.Current.Name);
            iter.MoveNext();
            Assert.AreEqual("c59759f143fb1fe21c197981df75a7ee00290799", iter.Current.Name);
            Assert.IsFalse(iter.MoveNext());
        }

        /// <summary>
        /// Compare offset from iterator entries with output of findOffset() method.
        /// </summary>
        [Test]
        public void testCompareEntriesOffsetsWithFindOffsets()
        {
            foreach (var me in smallIdx)
            {
                Assert.AreEqual(smallIdx.FindOffset(me.ToObjectId()), me.Offset);
            }

            foreach (var me in _denseIdx)
            {
                Assert.AreEqual(_denseIdx.FindOffset(me.ToObjectId()), me.Offset);
            }
        }

        /// <summary>
        /// Test partial results of iterator comparing to content of well-known
        /// (prepared) dense index, that may need multi-level indexing.
        /// </summary>
        [Test]
        public void testIteratorReturnedValues2()
        {
            IEnumerator<PackIndex.MutableEntry> iter = _denseIdx.GetEnumerator();
            iter.MoveNext();

            while (!iter.Current.Name.Equals("0a3d7772488b6b106fb62813c4d6d627918d9181"))
            {
                // just iterating
                iter.MoveNext();
            }

            iter.MoveNext();
            Assert.AreEqual("1004d0d7ac26fbf63050a234c9b88a46075719d3", iter.Current.Name); // same level-1
            iter.MoveNext();
            Assert.AreEqual("10da5895682013006950e7da534b705252b03be6", iter.Current.Name); // same level-1
            iter.MoveNext();
            Assert.AreEqual("1203b03dc816ccbb67773f28b3c19318654b0bc8", iter.Current.Name);
        }
    }
}