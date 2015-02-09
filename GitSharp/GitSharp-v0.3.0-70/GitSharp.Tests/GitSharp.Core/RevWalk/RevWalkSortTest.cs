/*
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

using GitSharp.Core.RevWalk;
using NUnit.Framework;

namespace GitSharp.Core.Tests.RevWalk
{
    [TestFixture]
    public class RevWalkSortTest : RevWalkTestCase
    {
        [Test]
        public void testSort_Default()
        {
            RevCommit a = Commit();
            RevCommit b = Commit(1, a);
            RevCommit c = Commit(1, b);
            RevCommit d = Commit(1, c);

            MarkStart(d);
            AssertCommit(d, rw.next());
            AssertCommit(c, rw.next());
            AssertCommit(b, rw.next());
            AssertCommit(a, rw.next());
            Assert.IsNull(rw.next());
        }

        [Test]
        public void testSort_COMMIT_TIME_DESC()
        {
            RevCommit a = Commit();
            RevCommit b = Commit(a);
            RevCommit c = Commit(b);
            RevCommit d = Commit(c);

            rw.sort(RevSort.COMMIT_TIME_DESC);
            MarkStart(d);
            AssertCommit(d, rw.next());
            AssertCommit(c, rw.next());
            AssertCommit(b, rw.next());
            AssertCommit(a, rw.next());
            Assert.IsNull(rw.next());
        }

        [Test]
        public void testSort_REVERSE()
        {
            RevCommit a = Commit();
            RevCommit b = Commit(a);
            RevCommit c = Commit(b);
            RevCommit d = Commit(c);

            rw.sort(RevSort.REVERSE);
            MarkStart(d);
            AssertCommit(a, rw.next());
            AssertCommit(b, rw.next());
            AssertCommit(c, rw.next());
            AssertCommit(d, rw.next());
            Assert.IsNull(rw.next());
        }

        [Test]
        public void testSort_COMMIT_TIME_DESC_OutOfOrder1()
        {
            // Despite being out of order time-wise, a strand-of-pearls must
            // still maintain topological order.
            //
            RevCommit a = Commit();
            RevCommit b = Commit(a);
            RevCommit c = Commit(-5, b);
            RevCommit d = Commit(10, c);
            Assert.IsTrue(parseBody(a).CommitTime < parseBody(d).CommitTime);
            Assert.IsTrue(parseBody(c).CommitTime < parseBody(b).CommitTime);

            rw.sort(RevSort.COMMIT_TIME_DESC);
            MarkStart(d);
            AssertCommit(d, rw.next());
            AssertCommit(c, rw.next());
            AssertCommit(b, rw.next());
            AssertCommit(a, rw.next());
            Assert.IsNull(rw.next());
        }

        [Test]
        public void testSort_COMMIT_TIME_DESC_OutOfOrder2()
        {
            // c1 is back dated before its parent.
            //
            RevCommit a = Commit();
            RevCommit b = Commit(a);
            RevCommit c1 = Commit(-5, b);
            RevCommit c2 = Commit(10, b);
            RevCommit d = Commit(c1, c2);

            rw.sort(RevSort.COMMIT_TIME_DESC);
            MarkStart(d);
            AssertCommit(d, rw.next());
            AssertCommit(c2, rw.next());
            AssertCommit(b, rw.next());
            AssertCommit(a, rw.next());
            AssertCommit(c1, rw.next());
            Assert.IsNull(rw.next());
        }

        [Test]
        public void testSort_TOPO()
        {
            // c1 is back dated before its parent.
            //
            RevCommit a = Commit();
            RevCommit b = Commit(a);
            RevCommit c1 = Commit(-5, b);
            RevCommit c2 = Commit(10, b);
            RevCommit d = Commit(c1, c2);

            rw.sort(RevSort.TOPO);
            MarkStart(d);
            AssertCommit(d, rw.next());
            AssertCommit(c2, rw.next());
            AssertCommit(c1, rw.next());
            AssertCommit(b, rw.next());
            AssertCommit(a, rw.next());
            Assert.IsNull(rw.next());
        }

        [Test]
        public void testSort_TOPO_REVERSE()
        {
            // c1 is back dated before its parent.
            //
            RevCommit a = Commit();
            RevCommit b = Commit(a);
            RevCommit c1 = Commit(-5, b);
            RevCommit c2 = Commit(10, b);
            RevCommit d = Commit(c1, c2);

            rw.sort(RevSort.TOPO);
            rw.sort(RevSort.REVERSE, true);
            MarkStart(d);
            AssertCommit(a, rw.next());
            AssertCommit(b, rw.next());
            AssertCommit(c1, rw.next());
            AssertCommit(c2, rw.next());
            AssertCommit(d, rw.next());
            Assert.IsNull(rw.next());
        }
    }
}
