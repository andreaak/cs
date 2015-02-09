/*
 * Copyright (C) 2007, Dave Watson <dwatson@mimvista.com>
 * Copyright (C) 2008, Robin Rosenberg <robin.rosenberg@dewire.com>
 * Copyright (C) 2008, Shawn O. Pearce <spearce@spearce.org>
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

using System.IO;
using GitSharp.Core;
using NUnit.Framework;

namespace GitSharp.Core.Tests
{
	[TestFixture]
	public class IndexDiffTest : RepositoryTestCase
	{
		// Methods
		[Test]
		public void testAdded()
		{
			var index = new GitIndex(db);
			writeTrashFile("file1", "file1");
			writeTrashFile("dir/subfile", "dir/subfile");
			var tree = new Core.Tree(db);

			index.add(trash, new FileInfo(Path.Combine(trash.FullName, "file1")));
			index.add(trash, new FileInfo(Path.Combine(trash.FullName, "dir/subfile")));
			var diff = new IndexDiff(tree, index);
			diff.Diff();

			Assert.AreEqual(2, diff.Added.Count);
			Assert.IsTrue(diff.Added.Contains("file1"));
			Assert.IsTrue(diff.Added.Contains("dir/subfile"));
			Assert.AreEqual(0, diff.Changed.Count);
			Assert.AreEqual(0, diff.Modified.Count);
			Assert.AreEqual(0, diff.Removed.Count);
		}

        [Test]
        public void testAdded2()
        {
            var index = new GitIndex(db);
            writeTrashFile("test/f�r henon.txt", "Wei�ebier");
            var tree = new Core.Tree(db);

            index.add(trash, new FileInfo(Path.Combine(trash.FullName, "test/f�r henon.txt")));
            var diff = new IndexDiff(tree, index);
            diff.Diff();

            Assert.AreEqual(1, diff.Added.Count);
            Assert.IsTrue(diff.Added.Contains("test/f�r henon.txt"));
            Assert.AreEqual(0, diff.Changed.Count);
            Assert.AreEqual(0, diff.Modified.Count);
            Assert.AreEqual(0, diff.Removed.Count);
        }

        [Test]
        public void testUpdateExistingMsysgitIndex()
        {
            var index_path = Path.Combine(trash.FullName + "/.git", "index");
            new FileInfo("Resources/index_originating_from_msysgit").CopyTo(index_path); 
            
            var index = new GitIndex(db);
            index.Read();
            var a = writeTrashFile("a.txt", "Data:a");
            index.add(trash, a);
            index.write();
            index.Read();

            byte[] content = File.ReadAllBytes(index_path);

            Assert.AreEqual(352, content.Length);
        }



		[Test]
		public void testModified()
		{
			var index = new GitIndex(db);

			index.add(trash, writeTrashFile("file2", "file2"));
			index.add(trash, writeTrashFile("dir/file3", "dir/file3"));

			writeTrashFile("dir/file3", "changed");

			var t = new Core.Tree(db);
			t.AddFile("file2").Id = ObjectId.FromString("0123456789012345678901234567890123456789");
			t.AddFile("dir/file3").Id = ObjectId.FromString("0123456789012345678901234567890123456789");
			Assert.AreEqual(2, t.MemberCount);

            var tree2 = (Core.Tree)t.findTreeMember("dir");
			tree2.Id = new ObjectWriter(db).WriteTree(tree2);
			t.Id = new ObjectWriter(db).WriteTree(t);
			var diff = new IndexDiff(t, index);
			diff.Diff();
			Assert.AreEqual(2, diff.Changed.Count);
			Assert.IsTrue(diff.Changed.Contains("file2"));
			Assert.IsTrue(diff.Changed.Contains("dir/file3"));
			Assert.AreEqual(1, diff.Modified.Count);
			Assert.IsTrue(diff.Modified.Contains("dir/file3"));
			Assert.AreEqual(0, diff.Added.Count);
			Assert.AreEqual(0, diff.Removed.Count);
			Assert.AreEqual(0, diff.Missing.Count);
		}

		[Test]
		public void testRemoved()
		{
			var index = new GitIndex(db);
			writeTrashFile("file2", "file2");
			writeTrashFile("dir/file3", "dir/file3");

            var t = new Core.Tree(db);
			t.AddFile("file2");
			t.AddFile("dir/file3");
			Assert.AreEqual(2, t.MemberCount);
			t.FindBlobMember("file2").Id = ObjectId.FromString("30d67d4672d5c05833b7192cc77a79eaafb5c7ad");
            var tree2 = (Core.Tree)t.findTreeMember("dir");
			tree2.FindBlobMember("file3").Id = ObjectId.FromString("873fb8d667d05436d728c52b1d7a09528e6eb59b");
			tree2.Id = new ObjectWriter(db).WriteTree(tree2);
			t.Id = new ObjectWriter(db).WriteTree(t);

			var diff = new IndexDiff(t, index);
			diff.Diff();
			Assert.AreEqual(2, diff.Removed.Count);
			Assert.IsTrue(diff.Removed.Contains("file2"));
			Assert.IsTrue(diff.Removed.Contains("dir/file3"));
			Assert.AreEqual(0, diff.Changed.Count);
			Assert.AreEqual(0, diff.Modified.Count);
			Assert.AreEqual(0, diff.Added.Count);
		}

		[Test]
		public void testUnchangedComplex()
		{
			var index = new GitIndex(db);
			index.add(trash, writeTrashFile("a.b", "a.b"));
			index.add(trash, writeTrashFile("a.c", "a.c"));
			index.add(trash, writeTrashFile("a/b.b/b", "a/b.b/b"));
			index.add(trash, writeTrashFile("a/b", "a/b"));
			index.add(trash, writeTrashFile("a/c", "a/c"));
			index.add(trash, writeTrashFile("a=c", "a=c"));
			index.add(trash, writeTrashFile("a=d", "a=d"));

            var t = new Core.Tree(db);
			t.AddFile("a.b").Id = ObjectId.FromString("f6f28df96c2b40c951164286e08be7c38ec74851");
			t.AddFile("a.c").Id = ObjectId.FromString("6bc0e647512d2a0bef4f26111e484dc87df7f5ca");
			t.AddFile("a/b.b/b").Id = ObjectId.FromString("8d840bd4e2f3a48ff417c8e927d94996849933fd");
			t.AddFile("a/b").Id = ObjectId.FromString("db89c972fc57862eae378f45b74aca228037d415");
			t.AddFile("a/c").Id = ObjectId.FromString("52ad142a008aeb39694bafff8e8f1be75ed7f007");
			t.AddFile("a=c").Id = ObjectId.FromString("06022365ddbd7fb126761319633bf73517770714");
			t.AddFile("a=d").Id = ObjectId.FromString("fa6414df3da87840700e9eeb7fc261dd77ccd5c2");

            var tree2 = (Core.Tree)t.findTreeMember("a/b.b");
			tree2.Id = new ObjectWriter(db).WriteTree(tree2);

            var tree3 = (Core.Tree)t.findTreeMember("a");
			tree3.Id = new ObjectWriter(db).WriteTree(tree3);
			t.Id = new ObjectWriter(db).WriteTree(t);

			var diff = new IndexDiff(t, index);
			diff.Diff();

			Assert.AreEqual(0, diff.Changed.Count);
			Assert.AreEqual(0, diff.Added.Count);
			Assert.AreEqual(0, diff.Removed.Count);
			Assert.AreEqual(0, diff.Missing.Count);
			Assert.AreEqual(0, diff.Modified.Count);
		}

		[Test]
		public void testUnchangedSimple()
		{
			var index = new GitIndex(db);

			index.add(trash, writeTrashFile("a.b", "a.b"));
			index.add(trash, writeTrashFile("a.c", "a.c"));
			index.add(trash, writeTrashFile("a=c", "a=c"));
			index.add(trash, writeTrashFile("a=d", "a=d"));

            var t = new Core.Tree(db);
			t.AddFile("a.b").Id = ObjectId.FromString("f6f28df96c2b40c951164286e08be7c38ec74851");
			t.AddFile("a.c").Id = ObjectId.FromString("6bc0e647512d2a0bef4f26111e484dc87df7f5ca");
			t.AddFile("a=c").Id = ObjectId.FromString("06022365ddbd7fb126761319633bf73517770714");
			t.AddFile("a=d").Id = ObjectId.FromString("fa6414df3da87840700e9eeb7fc261dd77ccd5c2");
			t.Id = new ObjectWriter(db).WriteTree(t);
			
			var diff = new IndexDiff(t, index);
			diff.Diff();

			Assert.AreEqual(0, diff.Changed.Count);
			Assert.AreEqual(0, diff.Added.Count);
			Assert.AreEqual(0, diff.Removed.Count);
			Assert.AreEqual(0, diff.Missing.Count);
			Assert.AreEqual(0, diff.Modified.Count);
		}
	}
}