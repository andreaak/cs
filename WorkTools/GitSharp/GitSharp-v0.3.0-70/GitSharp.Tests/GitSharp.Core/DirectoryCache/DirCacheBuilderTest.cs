/*
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
using System.IO;
using GitSharp.Core;
using GitSharp.Core.DirectoryCache;
using GitSharp.Core.Tests;
using NUnit.Framework;
using FileMode = GitSharp.Core.FileMode;

namespace GitSharp.Tests.GitSharp.Core.DirectoryCache
{
    [TestFixture]
    public class DirCacheBuilderTest : RepositoryTestCase
    {

        [Test]
        public void testBuildEmpty()
        {
            DirCache dc = DirCache.Lock(db);
            DirCacheBuilder b = dc.builder();
            Assert.IsNotNull(b);
            b.finish();
            dc.write();
            Assert.IsTrue(dc.commit());

            dc = DirCache.read(db);
            Assert.AreEqual(0, dc.getEntryCount());
        }

        [Test]
        public void testBuildRejectsUnsetFileMode()
        {
            DirCache dc = DirCache.newInCore();
            DirCacheBuilder b = dc.builder();
            Assert.IsNotNull(b);

            DirCacheEntry e = new DirCacheEntry("a");
            Assert.AreEqual(0, e.getRawMode());
            try
            {
                b.add(e);
            }
            catch (ArgumentException err)
            {
                Assert.AreEqual("FileMode not set for path a", err.Message);
            }
        }

        [Test]
        public void testBuildOneFile_FinishWriteCommit()
        {
            string path = "a-File-path";
            var mode = FileMode.RegularFile;
            long lastModified = 1218123387057L;
            int Length = 1342;
            DirCacheEntry entOrig;

            DirCache dc = DirCache.Lock(db);
            DirCacheBuilder b = dc.builder();
            Assert.IsNotNull(b);

            entOrig = new DirCacheEntry(path);
            entOrig.setFileMode(mode);
            entOrig.setLastModified(lastModified);
            entOrig.setLength(Length);

            Assert.AreNotSame(path, entOrig.getPathString());
            Assert.AreEqual(path, entOrig.getPathString());
            Assert.AreEqual(ObjectId.ZeroId, entOrig.getObjectId());
            Assert.AreEqual(mode.Bits, entOrig.getRawMode());
            Assert.AreEqual(0, entOrig.getStage());
            Assert.AreEqual(lastModified, entOrig.getLastModified());
            Assert.AreEqual(Length, entOrig.getLength());
            Assert.IsFalse(entOrig.isAssumeValid());
            b.add(entOrig);

            b.finish();
            Assert.AreEqual(1, dc.getEntryCount());
            Assert.AreSame(entOrig, dc.getEntry(0));

            dc.write();
            Assert.IsTrue(dc.commit());

            dc = DirCache.read(db);
            Assert.AreEqual(1, dc.getEntryCount());

            DirCacheEntry entRead = dc.getEntry(0);
            Assert.AreNotSame(entOrig, entRead);
            Assert.AreEqual(path, entRead.getPathString());
            Assert.AreEqual(ObjectId.ZeroId, entOrig.getObjectId());
            Assert.AreEqual(mode.Bits, entOrig.getRawMode());
            Assert.AreEqual(0, entOrig.getStage());
            Assert.AreEqual(lastModified, entOrig.getLastModified());
            Assert.AreEqual(Length, entOrig.getLength());
            Assert.IsFalse(entOrig.isAssumeValid());
        }

        [Test]
        public void testBuildOneFile_Commit()
        {
            string path = "a-File-path";
            var mode = FileMode.RegularFile;
            long lastModified = 1218123387057L;
            int Length = 1342;
            DirCacheEntry entOrig;

            DirCache dc = DirCache.Lock(db);
            DirCacheBuilder b = dc.builder();
            Assert.IsNotNull(b);

            entOrig = new DirCacheEntry(path);
            entOrig.setFileMode(mode);
            entOrig.setLastModified(lastModified);
            entOrig.setLength(Length);

            Assert.AreNotSame(path, entOrig.getPathString());
            Assert.AreEqual(path, entOrig.getPathString());
            Assert.AreEqual(ObjectId.ZeroId, entOrig.getObjectId());
            Assert.AreEqual(mode.Bits, entOrig.getRawMode());
            Assert.AreEqual(0, entOrig.getStage());
            Assert.AreEqual(lastModified, entOrig.getLastModified());
            Assert.AreEqual(Length, entOrig.getLength());
            Assert.IsFalse(entOrig.isAssumeValid());
            b.add(entOrig);

            Assert.IsTrue(b.commit());
            Assert.AreEqual(1, dc.getEntryCount());
            Assert.AreSame(entOrig, dc.getEntry(0));
            Assert.IsFalse(new FileInfo(db.Directory + "/index.lock").Exists);

            dc = DirCache.read(db);
            Assert.AreEqual(1, dc.getEntryCount());

            DirCacheEntry entRead = dc.getEntry(0);
            Assert.AreNotSame(entOrig, entRead);
            Assert.AreEqual(path, entRead.getPathString());
            Assert.AreEqual(ObjectId.ZeroId, entOrig.getObjectId());
            Assert.AreEqual(mode.Bits, entOrig.getRawMode());
            Assert.AreEqual(0, entOrig.getStage());
            Assert.AreEqual(lastModified, entOrig.getLastModified());
            Assert.AreEqual(Length, entOrig.getLength());
            Assert.IsFalse(entOrig.isAssumeValid());
        }

        [Test]
        public void testFindSingleFile()
        {
            string path = "a-File-path";
            DirCache dc = DirCache.read(db);
            DirCacheBuilder b = dc.builder();
            Assert.IsNotNull(b);

            DirCacheEntry entOrig = new DirCacheEntry(path);
            entOrig.setFileMode(FileMode.RegularFile);
            Assert.AreNotSame(path, entOrig.getPathString());
            Assert.AreEqual(path, entOrig.getPathString());
            b.add(entOrig);
            b.finish();

            Assert.AreEqual(1, dc.getEntryCount());
            Assert.AreSame(entOrig, dc.getEntry(0));
            Assert.AreEqual(0, dc.findEntry(path));

            Assert.AreEqual(-1, dc.findEntry("@@-before"));
            Assert.AreEqual(0, real(dc.findEntry("@@-before")));

            Assert.AreEqual(-2, dc.findEntry("a-zoo"));
            Assert.AreEqual(1, real(dc.findEntry("a-zoo")));

            Assert.AreSame(entOrig, dc.getEntry(path));
        }

        [Test]
        public void testAdd_InGitSortOrder()
        {
            DirCache dc = DirCache.read(db);

            string[] paths = { "a.", "a.b", "a/b", "a0b" };
            DirCacheEntry[] ents = new DirCacheEntry[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                ents[i] = new DirCacheEntry(paths[i]);
                ents[i].setFileMode(FileMode.RegularFile);
            }

            DirCacheBuilder b = dc.builder();
            for (int i = 0; i < ents.Length; i++)
                b.add(ents[i]);
            b.finish();

            Assert.AreEqual(paths.Length, dc.getEntryCount());
            for (int i = 0; i < paths.Length; i++)
            {
                Assert.AreSame(ents[i], dc.getEntry(i));
                Assert.AreEqual(paths[i], dc.getEntry(i).getPathString());
                Assert.AreEqual(i, dc.findEntry(paths[i]));
                Assert.AreSame(ents[i], dc.getEntry(paths[i]));
            }
        }

        [Test]
        public void testAdd_ReverseGitSortOrder()
        {
            DirCache dc = DirCache.read(db);

            string[] paths = { "a.", "a.b", "a/b", "a0b" };
            DirCacheEntry[] ents = new DirCacheEntry[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                ents[i] = new DirCacheEntry(paths[i]);
                ents[i].setFileMode(FileMode.RegularFile);
            }

            DirCacheBuilder b = dc.builder();
            for (int i = ents.Length - 1; i >= 0; i--)
                b.add(ents[i]);
            b.finish();

            Assert.AreEqual(paths.Length, dc.getEntryCount());
            for (int i = 0; i < paths.Length; i++)
            {
                Assert.AreSame(ents[i], dc.getEntry(i));
                Assert.AreEqual(paths[i], dc.getEntry(i).getPathString());
                Assert.AreEqual(i, dc.findEntry(paths[i]));
                Assert.AreSame(ents[i], dc.getEntry(paths[i]));
            }
        }

        [Test]
        public void testBuilderClear()
        {
            DirCache dc = DirCache.read(db);

            string[] paths = { "a.", "a.b", "a/b", "a0b" };
            DirCacheEntry[] ents = new DirCacheEntry[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                ents[i] = new DirCacheEntry(paths[i]);
                ents[i].setFileMode(FileMode.RegularFile);
            }
            {
                DirCacheBuilder b = dc.builder();
                for (int i = 0; i < ents.Length; i++)
                    b.add(ents[i]);
                b.finish();
            }
            Assert.AreEqual(paths.Length, dc.getEntryCount());
            {
                DirCacheBuilder b = dc.builder();
                b.finish();
            }
            Assert.AreEqual(0, dc.getEntryCount());
        }

        private static int real(int eIdx)
        {
            if (eIdx < 0)
                eIdx = -(eIdx + 1);
            return eIdx;
        }
    }
}


