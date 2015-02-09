/*
 * Copyright (C) 2008, Robin Rosenberg
 * Copyright (C) 2009, Dan Rigby <dan@danrigby.com>
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
using System.IO;
using GitSharp.Core;
using GitSharp.Core.DirectoryCache;
using GitSharp.Core.Merge;
using NUnit.Framework;
using FileMode=GitSharp.Core.FileMode;

namespace GitSharp.Core.Tests.Merge
{
	[TestFixture]
	public class SimpleMergeTest : SampleDataRepositoryTestCase
	{
		[Test]
		public void TestOurs()
		{
			Merger ourMerger = MergeStrategy.Ours.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { db.Resolve("a"), db.Resolve("c") });
			Assert.IsTrue(merge);
			Assert.AreEqual(db.MapTree("a").Id, ourMerger.GetResultTreeId());
		}

		[Test]
		public void TestTheirs()
		{
			Merger ourMerger = MergeStrategy.Theirs.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { db.Resolve("a"), db.Resolve("c") });
			Assert.IsTrue(merge);
			Assert.AreEqual(db.MapTree("c").Id, ourMerger.GetResultTreeId());
		}

		[Test]
		public void TestTrivialTwoWay()
		{
			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { db.Resolve("a"), db.Resolve("c") });
			Assert.IsTrue(merge);
			Assert.AreEqual("02ba32d3649e510002c21651936b7077aa75ffa9", ourMerger.GetResultTreeId().Name);
		}

		[Test]
        public void testTrivialTwoWay_disjointhistories()
		{
			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { db.Resolve("a"), db.Resolve("c~4") });
			Assert.IsTrue(merge);
			Assert.AreEqual("86265c33b19b2be71bdd7b8cb95823f2743d03a8", ourMerger.GetResultTreeId().Name);
		}

		[Test]
        public void testTrivialTwoWay_ok()
		{
			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { db.Resolve("a^0^0^0"), db.Resolve("a^0^0^1") });
			Assert.IsTrue(merge);
			Assert.AreEqual(db.MapTree("a^0^0").Id, ourMerger.GetResultTreeId());
		}

		[Test]
        public void testTrivialTwoWay_conflict()
		{
			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { db.Resolve("f"), db.Resolve("g") });
			Assert.IsFalse(merge);
		}

		[Test]
        public void testTrivialTwoWay_validSubtreeSort()
		{
			DirCache treeB = DirCache.read(db);
			DirCache treeO = DirCache.read(db);
			DirCache treeT = DirCache.read(db);
			{
				DirCacheBuilder b = treeB.builder();
				DirCacheBuilder o = treeO.builder();
				DirCacheBuilder t = treeT.builder();

				b.add(MakeEntry("libelf-po/a", FileMode.RegularFile));
				b.add(MakeEntry("libelf/c", FileMode.RegularFile));

				o.add(MakeEntry("Makefile", FileMode.RegularFile));
				o.add(MakeEntry("libelf-po/a", FileMode.RegularFile));
				o.add(MakeEntry("libelf/c", FileMode.RegularFile));

				t.add(MakeEntry("libelf-po/a", FileMode.RegularFile));
				t.add(MakeEntry("libelf/c", FileMode.RegularFile, "blah"));

				b.finish();
				o.finish();
				t.finish();
			}

			var ow = new ObjectWriter(db);
			ObjectId B = Commit(ow, treeB, new ObjectId[] { });
			ObjectId O = Commit(ow, treeO, new[] { B });
			ObjectId T = Commit(ow, treeT, new[] { B });

			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { O, T });
			Assert.IsTrue(merge);

			var tw = new GitSharp.Core.TreeWalk.TreeWalk(db) { Recursive = true };
			tw.reset(ourMerger.GetResultTreeId());

			Assert.IsTrue(tw.next());
			Assert.AreEqual("Makefile", tw.getPathString());
			AssertCorrectId(treeO, tw);

			Assert.IsTrue(tw.next());
			Assert.AreEqual("libelf-po/a", tw.getPathString());
			AssertCorrectId(treeO, tw);

			Assert.IsTrue(tw.next());
			Assert.AreEqual("libelf/c", tw.getPathString());
			AssertCorrectId(treeT, tw);

			Assert.IsFalse(tw.next());
		}

		[Test]
        public void testTrivialTwoWay_concurrentSubtreeChange()
		{
			DirCache treeB = DirCache.read(db);
			DirCache treeO = DirCache.read(db);
			DirCache treeT = DirCache.read(db);
			{
				DirCacheBuilder b = treeB.builder();
				DirCacheBuilder o = treeO.builder();
				DirCacheBuilder t = treeT.builder();

				b.add(MakeEntry("d/o", FileMode.RegularFile));
				b.add(MakeEntry("d/t", FileMode.RegularFile));

				o.add(MakeEntry("d/o", FileMode.RegularFile, "o !"));
				o.add(MakeEntry("d/t", FileMode.RegularFile));

				t.add(MakeEntry("d/o", FileMode.RegularFile));
				t.add(MakeEntry("d/t", FileMode.RegularFile, "t !"));

				b.finish();
				o.finish();
				t.finish();
			}

			var ow = new ObjectWriter(db);
			ObjectId B = Commit(ow, treeB, new ObjectId[] { });
			ObjectId O = Commit(ow, treeO, new[] { B });
			ObjectId T = Commit(ow, treeT, new[] { B });

			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { O, T });
			Assert.IsTrue(merge);

			var tw = new GitSharp.Core.TreeWalk.TreeWalk(db) { Recursive = true };
			tw.reset(ourMerger.GetResultTreeId());

			Assert.IsTrue(tw.next());
			Assert.AreEqual("d/o", tw.getPathString());
			AssertCorrectId(treeO, tw);

			Assert.IsTrue(tw.next());
			Assert.AreEqual("d/t", tw.getPathString());
			AssertCorrectId(treeT, tw);

			Assert.IsFalse(tw.next());
		}

		[Test]
        public void testTrivialTwoWay_conflictSubtreeChange()
		{
			DirCache treeB = DirCache.read(db);
			DirCache treeO = DirCache.read(db);
			DirCache treeT = DirCache.read(db);
			{
				DirCacheBuilder b = treeB.builder();
				DirCacheBuilder o = treeO.builder();
				DirCacheBuilder t = treeT.builder();

				b.add(MakeEntry("d/o", FileMode.RegularFile));
				b.add(MakeEntry("d/t", FileMode.RegularFile));

				o.add(MakeEntry("d/o", FileMode.RegularFile));
				o.add(MakeEntry("d/t", FileMode.RegularFile, "o !"));

				t.add(MakeEntry("d/o", FileMode.RegularFile, "t !"));
				t.add(MakeEntry("d/t", FileMode.RegularFile, "t !"));

				b.finish();
				o.finish();
				t.finish();
			}

			var ow = new ObjectWriter(db);
			ObjectId B = Commit(ow, treeB, new ObjectId[] { });
			ObjectId O = Commit(ow, treeO, new[] { B });
			ObjectId T = Commit(ow, treeT, new[] { B });

			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { O, T });
			Assert.IsFalse(merge);
		}

		[Test]
        public void testTrivialTwoWay_leftDFconflict1()
		{
			DirCache treeB = DirCache.read(db);
			DirCache treeO = DirCache.read(db);
			DirCache treeT = DirCache.read(db);
			{
				DirCacheBuilder b = treeB.builder();
				DirCacheBuilder o = treeO.builder();
				DirCacheBuilder t = treeT.builder();

				b.add(MakeEntry("d/o", FileMode.RegularFile));
				b.add(MakeEntry("d/t", FileMode.RegularFile));

				o.add(MakeEntry("d", FileMode.RegularFile));

				t.add(MakeEntry("d/o", FileMode.RegularFile));
				t.add(MakeEntry("d/t", FileMode.RegularFile, "t !"));

				b.finish();
				o.finish();
				t.finish();
			}

			var ow = new ObjectWriter(db);
			ObjectId B = Commit(ow, treeB, new ObjectId[] { });
			ObjectId O = Commit(ow, treeO, new[] { B });
			ObjectId T = Commit(ow, treeT, new[] { B });

			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { O, T });
			Assert.IsFalse(merge);
		}

		[Test]
        public void testTrivialTwoWay_rightDFconflict1()
		{
			DirCache treeB = DirCache.read(db);
			DirCache treeO = DirCache.read(db);
			DirCache treeT = DirCache.read(db);
			{
				DirCacheBuilder b = treeB.builder();
				DirCacheBuilder o = treeO.builder();
				DirCacheBuilder t = treeT.builder();

				b.add(MakeEntry("d/o", FileMode.RegularFile));
				b.add(MakeEntry("d/t", FileMode.RegularFile));

				o.add(MakeEntry("d/o", FileMode.RegularFile));
				o.add(MakeEntry("d/t", FileMode.RegularFile, "o !"));

				t.add(MakeEntry("d", FileMode.RegularFile));

				b.finish();
				o.finish();
				t.finish();
			}

			var ow = new ObjectWriter(db);
			ObjectId B = Commit(ow, treeB, new ObjectId[] { });
			ObjectId O = Commit(ow, treeO, new[] { B });
			ObjectId T = Commit(ow, treeT, new[] { B });

			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { O, T });
			Assert.IsFalse(merge);
		}

		[Test]
        public void testTrivialTwoWay_leftDFconflict2()
		{
			DirCache treeB = DirCache.read(db);
			DirCache treeO = DirCache.read(db);
			DirCache treeT = DirCache.read(db);
			{
				DirCacheBuilder b = treeB.builder();
				DirCacheBuilder o = treeO.builder();
				DirCacheBuilder t = treeT.builder();

				b.add(MakeEntry("d", FileMode.RegularFile));

				o.add(MakeEntry("d", FileMode.RegularFile, "o !"));

				t.add(MakeEntry("d/o", FileMode.RegularFile));

				b.finish();
				o.finish();
				t.finish();
			}

			var ow = new ObjectWriter(db);
			ObjectId B = Commit(ow, treeB, new ObjectId[] { });
			ObjectId O = Commit(ow, treeO, new[] { B });
			ObjectId T = Commit(ow, treeT, new[] { B });

			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { O, T });
			Assert.IsFalse(merge);
		}

		[Test]
        public void testTrivialTwoWay_rightDFconflict2()
		{
			DirCache treeB = DirCache.read(db);
			DirCache treeO = DirCache.read(db);
			DirCache treeT = DirCache.read(db);
			{
				DirCacheBuilder b = treeB.builder();
				DirCacheBuilder o = treeO.builder();
				DirCacheBuilder t = treeT.builder();

				b.add(MakeEntry("d", FileMode.RegularFile));

				o.add(MakeEntry("d/o", FileMode.RegularFile));

				t.add(MakeEntry("d", FileMode.RegularFile, "t !"));

				b.finish();
				o.finish();
				t.finish();
			}

			var ow = new ObjectWriter(db);
			ObjectId B = Commit(ow, treeB, new ObjectId[] { });
			ObjectId O = Commit(ow, treeO, new[] { B });
			ObjectId T = Commit(ow, treeT, new[] { B });

			Merger ourMerger = MergeStrategy.SimpleTwoWayInCore.NewMerger(db);
			bool merge = ourMerger.Merge(new[] { O, T });
			Assert.IsFalse(merge);
		}

		private static void AssertCorrectId(DirCache treeT, GitSharp.Core.TreeWalk.TreeWalk tw)
		{
			Assert.AreEqual(treeT.getEntry(tw.getPathString()).getObjectId(), tw.getObjectId(0));
		}

		private ObjectId Commit(ObjectWriter ow, DirCache treeB, ObjectId[] parentIds)
		{
            var c = new Core.Commit(db) { TreeId = treeB.writeTree(ow), Author = new PersonIdent("A U Thor", "a.u.thor", 1L, 0) };
			c.Committer = c.Author;
			c.ParentIds = parentIds;
			c.Message = "Tree " + c.TreeId.Name;
			return ow.WriteCommit(c);
		}

		private DirCacheEntry MakeEntry(string path, FileMode mode)
		{
			return MakeEntry(path, mode, path);
		}

		private DirCacheEntry MakeEntry(string path, FileMode mode, String content)
		{
			var ent = new DirCacheEntry(path);
			ent.setFileMode(mode);
			byte[] contentBytes = Constants.encode(content);
			ent.setObjectId(new ObjectWriter(db).ComputeBlobSha1(contentBytes.Length, new MemoryStream(contentBytes)));
			return ent;
		}
	}
}