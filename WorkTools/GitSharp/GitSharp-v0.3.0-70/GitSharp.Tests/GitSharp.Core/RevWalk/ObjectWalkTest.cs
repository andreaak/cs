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

using GitSharp.Core;
using GitSharp.Core.RevWalk;
using GitSharp.Core.Tests.RevWalk;
using NUnit.Framework;

namespace GitSharp.Tests.GitSharp.Core.RevWalk
{
    [TestFixture]
    public class ObjectWalkTest : RevWalkTestCase
    {
        protected ObjectWalk objw;

        protected override global::GitSharp.Core.RevWalk.RevWalk createRevWalk()
        {
            return objw = new ObjectWalk(db);
        }

        [Test]
        public void testNoCommits()
        {
            Assert.IsNull(objw.next());
            Assert.IsNull(objw.nextObject());
        }

        [Test]
        public void testTwoCommitsEmptyTree()
        {
            RevCommit a = Commit();
            RevCommit b = Commit(a);
            MarkStart(b);

            AssertCommit(b, objw.next());
            AssertCommit(a, objw.next());
            Assert.IsNull(objw.next());

            Assert.AreSame(tree(), objw.nextObject());
            Assert.IsNull(objw.nextObject());
        }

        [Test]
        public void testOneCommitOneTreeTwoBlob()
        {
            RevBlob f0 = blob("0");
            RevBlob f1 = blob("1");
            RevTree t = tree(File("0", f0), File("1", f1), File("2", f1));
            RevCommit a = Commit(t);
            MarkStart(a);

            AssertCommit(a, objw.next());
            Assert.IsNull(objw.next());

            Assert.AreSame(t, objw.nextObject());
            Assert.AreSame(f0, objw.nextObject());
            Assert.AreSame(f1, objw.nextObject());
            Assert.IsNull(objw.nextObject());
        }

        [Test]
        public void testTwoCommitTwoTreeTwoBlob()
        {
            RevBlob f0 = blob("0");
            RevBlob f1 = blob("1");
            RevBlob f2 = blob("0v2");
            RevTree ta = tree(File("0", f0), File("1", f1), File("2", f1));
            RevTree tb = tree(File("0", f2), File("1", f1), File("2", f1));
            RevCommit a = Commit(ta);
            RevCommit b = Commit(tb, a);
            MarkStart(b);

            AssertCommit(b, objw.next());
            AssertCommit(a, objw.next());
            Assert.IsNull(objw.next());

            Assert.AreSame(tb, objw.nextObject());
            Assert.AreSame(f2, objw.nextObject());
            Assert.AreSame(f1, objw.nextObject());

            Assert.AreSame(ta, objw.nextObject());
            Assert.AreSame(f0, objw.nextObject());

            Assert.IsNull(objw.nextObject());
        }

        [Test]
        public void testTwoCommitDeepTree1()
        {
            RevBlob f0 = blob("0");
            RevBlob f1 = blob("0v2");
            RevTree ta = tree(File("a/b/0", f0));
            RevTree tb = tree(File("a/b/1", f1));
            RevCommit a = Commit(ta);
            RevCommit b = Commit(tb, a);
            MarkStart(b);

            AssertCommit(b, objw.next());
            AssertCommit(a, objw.next());
            Assert.IsNull(objw.next());

            Assert.AreSame(tb, objw.nextObject());
            Assert.AreSame(get(tb, "a"), objw.nextObject());
            Assert.AreSame(get(tb, "a/b"), objw.nextObject());
            Assert.AreSame(f1, objw.nextObject());

            Assert.AreSame(ta, objw.nextObject());
            Assert.AreSame(get(ta, "a"), objw.nextObject());
            Assert.AreSame(get(ta, "a/b"), objw.nextObject());
            Assert.AreSame(f0, objw.nextObject());

            Assert.IsNull(objw.nextObject());
        }

        [Test]
        public void testTwoCommitDeepTree2()
        {
            RevBlob f1 = blob("1");
            RevTree ta = tree(File("a/b/0", f1), File("a/c/q", f1));
            RevTree tb = tree(File("a/b/1", f1), File("a/c/q", f1));
            RevCommit a = Commit(ta);
            RevCommit b = Commit(tb, a);
            MarkStart(b);

            AssertCommit(b, objw.next());
            AssertCommit(a, objw.next());
            Assert.IsNull(objw.next());

            Assert.AreSame(tb, objw.nextObject());
            Assert.AreSame(get(tb, "a"), objw.nextObject());
            Assert.AreSame(get(tb, "a/b"), objw.nextObject());
            Assert.AreSame(f1, objw.nextObject());
            Assert.AreSame(get(tb, "a/c"), objw.nextObject());

            Assert.AreSame(ta, objw.nextObject());
            Assert.AreSame(get(ta, "a"), objw.nextObject());
            Assert.AreSame(get(ta, "a/b"), objw.nextObject());

            Assert.IsNull(objw.nextObject());
        }

        [Test]
        public void testCull()
        {
            RevBlob f1 = blob("1");
            RevBlob f2 = blob("2");
            RevBlob f3 = blob("3");
            RevBlob f4 = blob("4");

            RevTree ta = tree(File("a/1", f1), File("c/3", f3));
            RevCommit a = Commit(ta);

            RevTree tb = tree(File("a/1", f2), File("c/3", f3));
            RevCommit b1 = Commit(tb, a);
            RevCommit b2 = Commit(tb, b1);

            RevTree tc = tree(File("a/1", f4));
            RevCommit c1 = Commit(tc, a);
            RevCommit c2 = Commit(tc, c1);

            MarkStart(b2);
            MarkUninteresting(c2);

            AssertCommit(b2, objw.next());
            AssertCommit(b1, objw.next());
            Assert.IsNull(objw.next());

            Assert.IsTrue(a.has(RevFlag.UNINTERESTING));
            Assert.IsTrue(ta.has(RevFlag.UNINTERESTING));
            Assert.IsTrue(f1.has(RevFlag.UNINTERESTING));
            Assert.IsTrue(f3.has(RevFlag.UNINTERESTING));

            Assert.AreSame(tb, objw.nextObject());
            Assert.AreSame(get(tb, "a"), objw.nextObject());
            Assert.AreSame(f2, objw.nextObject());
            Assert.IsNull(objw.nextObject());
        }

        [Test]
        public void testEmptyTreeCorruption()
        {
            ObjectId bId = ObjectId.FromString("abbbfafe3129f85747aba7bfac992af77134c607");
            RevTree tree_root, tree_A, tree_AB;
            RevCommit b;
            {
                global::GitSharp.Core.Tree root = new global::GitSharp.Core.Tree(db);
                global::GitSharp.Core.Tree A = root.AddTree("A");
                FileTreeEntry B = root.AddFile("B");
                B.Id = (bId);

                global::GitSharp.Core.Tree A_A = A.AddTree("A");
                global::GitSharp.Core.Tree A_B = A.AddTree("B");

                var ow = new ObjectWriter(db);
                A_A.Id = (ow.WriteTree(A_A));
                A_B.Id = (ow.WriteTree(A_B));
                A.Id = (ow.WriteTree(A));
                root.Id = (ow.WriteTree(root));

                tree_root = rw.parseTree(root.Id);
                tree_A = rw.parseTree(A.Id);
                tree_AB = rw.parseTree(A_A.Id);
                Assert.AreSame(tree_AB, rw.parseTree(A_B.Id));
                b = Commit(rw.parseTree(root.Id));
            }

            MarkStart(b);

            AssertCommit(b, objw.next());
            Assert.IsNull(objw.next());

            Assert.AreSame(tree_root, objw.nextObject());
            Assert.AreSame(tree_A, objw.nextObject());
            Assert.AreSame(tree_AB, objw.nextObject());
            Assert.AreSame(rw.lookupBlob(bId), objw.nextObject());
            Assert.IsNull(objw.nextObject());
        }
    }
}


