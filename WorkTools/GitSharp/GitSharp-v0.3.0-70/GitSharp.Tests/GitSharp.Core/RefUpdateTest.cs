/*
 * Copyright (C) 2008, Charles O'Farrell <charleso@charleso.org>
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
using System.Threading;
using GitSharp.Core.RevWalk;
using NUnit.Framework;

namespace GitSharp.Core.Tests
{
    [TestFixture]
    public class RefUpdateTest : SampleDataRepositoryTestCase
    {
        private void writeSymref(string src, string dst)
        {
            RefUpdate u = db.UpdateRef(src);
            switch (u.link(dst))
            {
                case RefUpdate.RefUpdateResult.NEW:
                case RefUpdate.RefUpdateResult.FORCED:
                case RefUpdate.RefUpdateResult.NO_CHANGE:
                    break;
                default:
                    Assert.Fail("link " + src + " to " + dst);
                    break;
            }
        }

        private RefUpdate updateRef(string name)
        {
            RefUpdate @ref = db.UpdateRef(name);
            @ref.setNewObjectId(db.Resolve(Constants.HEAD));
            return @ref;
        }

        private void delete(RefUpdate @ref, RefUpdate.RefUpdateResult expected)
        {
            delete(@ref, expected, true, true);
        }

        private void delete(RefUpdate @ref, RefUpdate.RefUpdateResult expected,
        bool exists, bool removed)
        {
            Assert.AreEqual(exists, db.getAllRefs().ContainsKey(@ref.getName()));
            Assert.AreEqual(expected, @ref.delete());
            Assert.AreEqual(!removed, db.getAllRefs().ContainsKey(@ref.getName()));
        }

        [Test]
        public void testNoCacheObjectIdSubclass()
        {
            string newRef = "refs/heads/abc";
            RefUpdate ru = updateRef(newRef);
            RevCommit newid = new RevCommit(ru.getNewObjectId())
            {
                // empty
            };

            ru.setNewObjectId(newid);
            RefUpdate.RefUpdateResult update = ru.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.NEW, update);
            Core.Ref r = db.getAllRefs()[newRef];
            Assert.IsNotNull(r);
            Assert.AreEqual(newRef, r.Name);
            Assert.IsNotNull(r.ObjectId);
            Assert.AreNotSame(newid, r.ObjectId);
            Assert.AreSame(typeof(ObjectId), r.ObjectId.GetType());
            Assert.AreEqual(newid.Copy(), r.ObjectId);
            IList<ReflogReader.Entry> reverseEntries1 = db.ReflogReader("refs/heads/abc").getReverseEntries();
            ReflogReader.Entry entry1 = reverseEntries1[0];
            Assert.AreEqual(1, reverseEntries1.Count);
            Assert.AreEqual(ObjectId.ZeroId, entry1.getOldId());
            Assert.AreEqual(r.ObjectId, entry1.getNewId());
            Assert.AreEqual(new PersonIdent(db).ToString(), entry1.getWho().ToString());
            Assert.AreEqual("", entry1.getComment());
            IList<ReflogReader.Entry> reverseEntries2 = db.ReflogReader("HEAD").getReverseEntries();
            Assert.AreEqual(0, reverseEntries2.Count);
        }

        [Test]
        public void testNewNamespaceConflictWithLoosePrefixNameExists()
        {
            string newRef = "refs/heads/z";
            RefUpdate ru = updateRef(newRef);
            RevCommit newid = new RevCommit(ru.getNewObjectId())
            {
                // empty
            };
            ru.setNewObjectId(newid);
            RefUpdate.RefUpdateResult update = ru.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.NEW, update);
            // end setup
            string newRef2 = "refs/heads/z/a";
            RefUpdate ru2 = updateRef(newRef2);
            RevCommit newid2 = new RevCommit(ru2.getNewObjectId())
            {
                // empty
            };
            ru.setNewObjectId(newid2);
            RefUpdate.RefUpdateResult update2 = ru2.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.LOCK_FAILURE, update2);
            Assert.AreEqual(1, db.ReflogReader("refs/heads/z").getReverseEntries().Count);
            Assert.AreEqual(0, db.ReflogReader("HEAD").getReverseEntries().Count);
        }

        [Test]
        public void testNewNamespaceConflictWithPackedPrefixNameExists()
        {
            string newRef = "refs/heads/master/x";
            RefUpdate ru = updateRef(newRef);
            RevCommit newid = new RevCommit(ru.getNewObjectId())
            {
                // empty
            };
            ru.NewObjectId = newid;
            RefUpdate.RefUpdateResult update = ru.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.LOCK_FAILURE, update);
            Assert.IsNull(db.ReflogReader("refs/heads/master/x"));
            Assert.AreEqual(0, db.ReflogReader("HEAD").getReverseEntries().Count);
        }

        [Test]
        public void testNewNamespaceConflictWithLoosePrefixOfExisting()
        {
            string newRef = "refs/heads/z/a";
            RefUpdate ru = updateRef(newRef);
            RevCommit newid = new RevCommit(ru.NewObjectId)
            {
                // empty
            };
            ru.NewObjectId = newid;
            RefUpdate.RefUpdateResult update = ru.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.NEW, update);
            // end setup
            string newRef2 = "refs/heads/z";
            RefUpdate ru2 = updateRef(newRef2);
            RevCommit newid2 = new RevCommit(ru2.NewObjectId)
            {
                // empty
            };
            ru.NewObjectId = newid2;
            RefUpdate.RefUpdateResult update2 = ru2.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.LOCK_FAILURE, update2);
            Assert.AreEqual(1, db.ReflogReader("refs/heads/z/a").getReverseEntries().Count);
            Assert.IsNull(db.ReflogReader("refs/heads/z"));
            Assert.AreEqual(0, db.ReflogReader("HEAD").getReverseEntries().Count);
        }

        [Test]
        public void testNewNamespaceConflictWithPackedPrefixOfExisting()
        {
            string newRef = "refs/heads/prefix";
            RefUpdate ru = updateRef(newRef);
            RevCommit newid = new RevCommit(ru.NewObjectId)
            {
                // empty
            };
            ru.NewObjectId = newid;
            RefUpdate.RefUpdateResult update = ru.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.LOCK_FAILURE, update);
            Assert.IsNull(db.ReflogReader("refs/heads/prefix"));
            Assert.AreEqual(0, db.ReflogReader("HEAD").getReverseEntries().Count);
        }

        /**
         * Delete a ref that is pointed to by HEAD
         *
         * @throws IOException
         */
        [Test]
        public void testDeleteHEADreferencedRef()
        {
            ObjectId pid = db.Resolve("refs/heads/master^");
            RefUpdate updateRef = db.UpdateRef("refs/heads/master");
            updateRef.NewObjectId = pid;
            updateRef.IsForceUpdate = true;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, update); // internal

            RefUpdate updateRef2 = db.UpdateRef("refs/heads/master");
            RefUpdate.RefUpdateResult delete = updateRef2.delete();
            Assert.AreEqual(RefUpdate.RefUpdateResult.REJECTED_CURRENT_BRANCH, delete);
            Assert.AreEqual(pid, db.Resolve("refs/heads/master"));
            Assert.AreEqual(1, db.ReflogReader("refs/heads/master").getReverseEntries().Count);
            Assert.AreEqual(0, db.ReflogReader("HEAD").getReverseEntries().Count);
        }

        [Test]
        public void testLooseDelete()
        {
            string newRef = "refs/heads/abc";
            RefUpdate @ref = updateRef(newRef);
            @ref.update(); // create loose ref
            @ref = updateRef(newRef); // refresh
            delete(@ref, RefUpdate.RefUpdateResult.NO_CHANGE);
            Assert.IsNull(db.ReflogReader("refs/heads/abc"));
        }

        [Test]
        public void testDeleteHead()
        {
            RefUpdate @ref = updateRef(Constants.HEAD);
            delete(@ref, RefUpdate.RefUpdateResult.REJECTED_CURRENT_BRANCH, true, false);
            Assert.AreEqual(0, db.ReflogReader("refs/heads/master").getReverseEntries().Count);
            Assert.AreEqual(0, db.ReflogReader("HEAD").getReverseEntries().Count);
        }

        /**
         * Delete a loose ref and make sure the directory in refs is deleted too,
         * and the reflog dir too
         *
         * @throws IOException
         */
        [Test]
        public void testDeleteLooseAndItsDirectory()
        {
            ObjectId pid = db.Resolve("refs/heads/c^");
            RefUpdate updateRef = db.UpdateRef("refs/heads/z/c");
            updateRef.NewObjectId = pid;
            updateRef.IsForceUpdate = true;
            updateRef.setRefLogMessage("new test ref", false);
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.NEW, update); // internal
            Assert.IsTrue(new DirectoryInfo(Path.Combine(db.Directory.FullName, Constants.R_HEADS + "z")).Exists);
            Assert.IsTrue(new DirectoryInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/z")).Exists);

            // The real test here
            RefUpdate updateRef2 = db.UpdateRef("refs/heads/z/c");
            updateRef2.IsForceUpdate = true;
            RefUpdate.RefUpdateResult delete = updateRef2.delete();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, delete);
            Assert.IsNull(db.Resolve("refs/heads/z/c"));
            Assert.IsFalse(new DirectoryInfo(Path.Combine(db.Directory.FullName, Constants.R_HEADS + "z")).Exists);
            Assert.IsFalse(new DirectoryInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/z")).Exists);
        }

        [Test]
        public void testDeleteNotFound()
        {
            RefUpdate @ref = updateRef("refs/heads/xyz");
            delete(@ref, RefUpdate.RefUpdateResult.NEW, false, true);
        }

        [Test]
        public void testDeleteFastForward()
        {
            RefUpdate @ref = updateRef("refs/heads/a");
            delete(@ref, RefUpdate.RefUpdateResult.FAST_FORWARD);
        }

        [Test]
        public void testDeleteForce()
        {
            RefUpdate @ref = db.UpdateRef("refs/heads/b");
            @ref.NewObjectId = db.Resolve("refs/heads/a");
            delete(@ref, RefUpdate.RefUpdateResult.REJECTED, true, false);
            @ref.IsForceUpdate = true;
            delete(@ref, RefUpdate.RefUpdateResult.FORCED);
        }

        [Test]
        public void testRefKeySameAsName()
        {
            foreach (var e in db.getAllRefs())
            {
                Assert.AreEqual(e.Key, e.Value.Name);

            }
        }

        /**
         * Try modify a ref forward, fast forward
         *
         * @throws IOException
         */
        [Test]
        public void testUpdateRefForward()
        {
            ObjectId ppid = db.Resolve("refs/heads/master^");
            ObjectId pid = db.Resolve("refs/heads/master");

            RefUpdate updateRef = db.UpdateRef("refs/heads/master");
            updateRef.NewObjectId = ppid;
            updateRef.IsForceUpdate = true;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, update);
            Assert.AreEqual(ppid, db.Resolve("refs/heads/master"));

            // real test
            RefUpdate updateRef2 = db.UpdateRef("refs/heads/master");
            updateRef2.NewObjectId = pid;
            RefUpdate.RefUpdateResult update2 = updateRef2.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FAST_FORWARD, update2);
            Assert.AreEqual(pid, db.Resolve("refs/heads/master"));
        }

        /*
        * Update the HEAD ref. Only it should be changed, not what it points to.
        *
        * @throws Exception
        */
        [Test]
        public void testUpdateRefDetached()
        {
            ObjectId pid = db.Resolve("refs/heads/master");
            ObjectId ppid = db.Resolve("refs/heads/master^");
            RefUpdate updateRef = db.UpdateRef("HEAD", true);
            updateRef.IsForceUpdate = true;
            updateRef.NewObjectId = ppid;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, update);
            Assert.AreEqual(ppid, db.Resolve("HEAD"));
            Ref @ref = db.getRef("HEAD");
            Assert.AreEqual("HEAD", @ref.Name);
            Assert.IsTrue(!@ref.isSymbolic(), "is detached");

            // the branch HEAD referred to is left untouched
            Assert.AreEqual(pid, db.Resolve("refs/heads/master"));
            ReflogReader reflogReader = new ReflogReader(db, "HEAD");
            ReflogReader.Entry e = reflogReader.getReverseEntries()[0];
            Assert.AreEqual(pid, e.getOldId());
            Assert.AreEqual(ppid, e.getNewId());
            Assert.AreEqual("GIT_COMMITTER_EMAIL", e.getWho().EmailAddress);
            Assert.AreEqual("GIT_COMMITTER_NAME", e.getWho().Name);
            Assert.AreEqual(1250379778000L, e.getWho().When);
        }

        /*
         * Update the HEAD ref when the referenced branch is unborn
         *
         * @throws Exception
         */
        [Test]
        public void testUpdateRefDetachedUnbornHead()
        {
            ObjectId ppid = db.Resolve("refs/heads/master^");
            writeSymref("HEAD", "refs/heads/unborn");
            RefUpdate updateRef = db.UpdateRef("HEAD", true);
            updateRef.IsForceUpdate = true;
            updateRef.NewObjectId = ppid;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.NEW, update);
            Assert.AreEqual(ppid, db.Resolve("HEAD"));
            Ref @ref = db.getRef("HEAD");
            Assert.AreEqual("HEAD", @ref.Name);
            Assert.IsTrue(!@ref.isSymbolic(), "is detached");

            // the branch HEAD referred to is left untouched
            Assert.IsNull(db.Resolve("refs/heads/unborn"));
            ReflogReader reflogReader = new ReflogReader(db, "HEAD");
            ReflogReader.Entry e = reflogReader.getReverseEntries()[0];
            Assert.AreEqual(ObjectId.ZeroId, e.getOldId());
            Assert.AreEqual(ppid, e.getNewId());
            Assert.AreEqual("GIT_COMMITTER_EMAIL", e.getWho().EmailAddress);
            Assert.AreEqual("GIT_COMMITTER_NAME", e.getWho().Name);
            Assert.AreEqual(1250379778000L, e.getWho().When);
        }

        /**
         * Delete a ref that exists both as packed and loose. Make sure the ref
         * cannot be resolved after delete.
         *
         * @throws IOException
         */
        [Test]
        public void testDeleteLoosePacked()
        {
            ObjectId pid = db.Resolve("refs/heads/c^");
            RefUpdate updateRef = db.UpdateRef("refs/heads/c");
            updateRef.NewObjectId = pid;
            updateRef.IsForceUpdate = true;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, update); // internal

            // The real test here
            RefUpdate updateRef2 = db.UpdateRef("refs/heads/c");
            updateRef2.IsForceUpdate = true;
            RefUpdate.RefUpdateResult delete = updateRef2.delete();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, delete);
            Assert.IsNull(db.Resolve("refs/heads/c"));
        }

        /**
         * Try modify a ref to same
         *
         * @throws IOException
         */
        [Test]
        public void testUpdateRefNoChange()
        {
            ObjectId pid = db.Resolve("refs/heads/master");
            RefUpdate updateRef = db.UpdateRef("refs/heads/master");
            updateRef.NewObjectId = pid;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.NO_CHANGE, update);
            Assert.AreEqual(pid, db.Resolve("refs/heads/master"));
        }

        /**
        * Test case originating from
        * <a href="http://bugs.eclipse.org/285991">bug 285991</a>
        *
        * Make sure the in memory cache is updated properly after
        * update of symref. This one did not fail because the
        * ref was packed due to implementation issues.
        *
        * @throws Exception
        */
        [Test]
        public void testRefsCacheAfterUpdate()
        {
            // Do not use the defalt repo for this case.
            IDictionary<string, Core.Ref> allRefs = db.getAllRefs();
            ObjectId oldValue = db.Resolve("HEAD");
            ObjectId newValue = db.Resolve("HEAD^");
            // first make HEAD refer to loose ref
            RefUpdate updateRef = db.UpdateRef(Constants.HEAD);
            updateRef.IsForceUpdate = true;
            updateRef.NewObjectId = newValue;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, update);

            // now update that ref
            updateRef = db.UpdateRef(Constants.HEAD);
            updateRef.IsForceUpdate = true;
            updateRef.NewObjectId = oldValue;
            update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FAST_FORWARD, update);

            allRefs = db.getAllRefs();
            Ref master = allRefs.GetValue("refs/heads/master");
            Ref head = allRefs.GetValue("HEAD");
            Assert.AreEqual("refs/heads/master", master.Name);
            Assert.AreEqual("HEAD", head.Name);
            Assert.IsTrue(head.isSymbolic(), "is symbolic reference");
            Assert.AreSame(master, head.getTarget());
        }

        /**
        * Test case originating from
        * <a href="http://bugs.eclipse.org/285991">bug 285991</a>
        *
        * Make sure the in memory cache is updated properly after
        * update of symref.
        *
        * @throws Exception
        */

        [Test]
        public void testRefsCacheAfterUpdateLooseOnly()
        {
            // Do not use the defalt repo for this case.
            IDictionary<string, Core.Ref> allRefs = db.getAllRefs();
            ObjectId oldValue = db.Resolve("HEAD");
            writeSymref(Constants.HEAD, "refs/heads/newref");
            RefUpdate updateRef = db.UpdateRef(Constants.HEAD);
            updateRef.IsForceUpdate = true;
            updateRef.NewObjectId = oldValue;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.NEW, update);

            allRefs = db.getAllRefs();
            Ref head = allRefs.GetValue("HEAD");
            Ref newref = allRefs.GetValue("refs/heads/newref");
            Assert.AreEqual("refs/heads/newref", newref.Name);
            Assert.AreEqual("HEAD", head.Name);
            Assert.IsTrue(head.isSymbolic(), "is symbolic reference");
            Assert.AreSame(newref, head.getTarget());
        }

        /**
         * Try modify a ref, but get wrong expected old value
         *
         * @throws IOException
         */
        [Test]
        public void testUpdateRefLockFailureWrongOldValue()
        {
            ObjectId pid = db.Resolve("refs/heads/master");
            RefUpdate updateRef = db.UpdateRef("refs/heads/master");
            updateRef.NewObjectId = pid;
            updateRef.ExpectedOldObjectId = db.Resolve("refs/heads/master^");
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.LOCK_FAILURE, update);
            Assert.AreEqual(pid, db.Resolve("refs/heads/master"));
        }

        /**
          * Try modify a ref forward, fast forward, checking old value first
          *
          * @throws IOException
          */
        [Test]
        public void testUpdateRefForwardWithCheck1()
        {
            ObjectId ppid = db.Resolve("refs/heads/master^");
            ObjectId pid = db.Resolve("refs/heads/master");

            RefUpdate updateRef = db.UpdateRef("refs/heads/master");
            updateRef.NewObjectId = ppid;
            updateRef.IsForceUpdate = true;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, update);
            Assert.AreEqual(ppid, db.Resolve("refs/heads/master"));

            // real test
            RefUpdate updateRef2 = db.UpdateRef("refs/heads/master");
            updateRef2.ExpectedOldObjectId = ppid;
            updateRef2.NewObjectId = pid;
            RefUpdate.RefUpdateResult update2 = updateRef2.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FAST_FORWARD, update2);
            Assert.AreEqual(pid, db.Resolve("refs/heads/master"));
        }

        /**
          * Try modify a ref forward, fast forward, checking old commit first
          *
          * @throws IOException
          */
        [Test]
        public void testUpdateRefForwardWithCheck2()
        {
            ObjectId ppid = db.Resolve("refs/heads/master^");
            ObjectId pid = db.Resolve("refs/heads/master");

            RefUpdate updateRef = db.UpdateRef("refs/heads/master");
            updateRef.NewObjectId = ppid;
            updateRef.IsForceUpdate = true;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, update);
            Assert.AreEqual(ppid, db.Resolve("refs/heads/master"));

            // real test
            RevCommit old = new Core.RevWalk.RevWalk(db).parseCommit(ppid);
            RefUpdate updateRef2 = db.UpdateRef("refs/heads/master");
            updateRef2.ExpectedOldObjectId = old;
            updateRef2.NewObjectId = pid;
            RefUpdate.RefUpdateResult update2 = updateRef2.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FAST_FORWARD, update2);
            Assert.AreEqual(pid, db.Resolve("refs/heads/master"));
        }

        /**
         * Try modify a ref that is locked
         *
         * @throws IOException
         */
        [Test]
        public void testUpdateRefLockFailureLocked()
        {
            ObjectId opid = db.Resolve("refs/heads/master");
            ObjectId pid = db.Resolve("refs/heads/master^");
            RefUpdate updateRef = db.UpdateRef("refs/heads/master");
            updateRef.NewObjectId = pid;
            var lockFile1 = new LockFile(new FileInfo(Path.Combine(db.Directory.FullName, "refs/heads/master")));
            try
            {
                Assert.IsTrue(lockFile1.Lock()); // precondition to test
                RefUpdate.RefUpdateResult update = updateRef.update();
                Assert.AreEqual(RefUpdate.RefUpdateResult.LOCK_FAILURE, update);
                Assert.AreEqual(opid, db.Resolve("refs/heads/master"));
                var lockFile2 = new LockFile(new FileInfo(Path.Combine(db.Directory.FullName, "refs/heads/master")));
                Assert.IsFalse(lockFile2.Lock()); // was locked, still is
            }
            finally
            {
                lockFile1.Unlock();
            }
        }

        /**
         * Try to delete a ref. Delete requires force.
         *
         * @throws IOException
         */
        [Test]
        public void testDeleteLoosePackedRejected()
        {
            ObjectId pid = db.Resolve("refs/heads/c^");
            ObjectId oldpid = db.Resolve("refs/heads/c");
            RefUpdate updateRef = db.UpdateRef("refs/heads/c");
            updateRef.NewObjectId = pid;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.REJECTED, update);
            Assert.AreEqual(oldpid, db.Resolve("refs/heads/c"));
        }

        [Test]
        public void testRenameBranchNoPreviousLog()
        {
            Assert.IsFalse(new FileInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/b")).Exists, "precondition, no log on old branchg");
            ObjectId rb = db.Resolve("refs/heads/b");
            ObjectId oldHead = db.Resolve(Constants.HEAD);
            Assert.IsFalse(rb.Equals(oldHead)); // assumption for this test
            RefRename renameRef = db.RenameRef("refs/heads/b",
                    "refs/heads/new/name");
            RefUpdate.RefUpdateResult result = renameRef.rename();
            Assert.AreEqual(RefUpdate.RefUpdateResult.RENAMED, result);
            Assert.AreEqual(rb, db.Resolve("refs/heads/new/name"));
            Assert.IsNull(db.Resolve("refs/heads/b"));
            Assert.AreEqual(1, db.ReflogReader("new/name").getReverseEntries().Count);
            Assert.AreEqual("Branch: renamed b to new/name", db.ReflogReader("new/name")
                    .getLastEntry().getComment());
            Assert.IsFalse(new FileInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/b")).Exists);
            Assert.AreEqual(oldHead, db.Resolve(Constants.HEAD)); // unchanged
        }

        [Test]
        public void testRenameBranchHasPreviousLog()
        {
            ObjectId rb = db.Resolve("refs/heads/b");
            ObjectId oldHead = db.Resolve(Constants.HEAD);
            Assert.IsFalse(rb
                    .Equals(oldHead), "precondition for this test, branch b != HEAD");
            writeReflog(db, rb, rb, "Just a message", "refs/heads/b");
            Assert.IsTrue(new FileInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/b")).Exists, "log on old branch");
            RefRename renameRef = db.RenameRef("refs/heads/b",
                    "refs/heads/new/name");
            RefUpdate.RefUpdateResult result = renameRef.rename();
            Assert.AreEqual(RefUpdate.RefUpdateResult.RENAMED, result);
            Assert.AreEqual(rb, db.Resolve("refs/heads/new/name"));
            Assert.IsNull(db.Resolve("refs/heads/b"));
            Assert.AreEqual(2, db.ReflogReader("new/name").getReverseEntries().Count);
            Assert.AreEqual("Branch: renamed b to new/name", db.ReflogReader("new/name")
                    .getLastEntry().getComment());
            Assert.AreEqual("Just a message", db.ReflogReader("new/name")
                    .getReverseEntries()[1].getComment());
            Assert.IsFalse(new FileInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/b")).Exists);
            Assert.AreEqual(oldHead, db.Resolve(Constants.HEAD)); // unchanged
        }

        [Test]
        public void testRenameCurrentBranch()
        {
            ObjectId rb = db.Resolve("refs/heads/b");
            writeSymref(Constants.HEAD, "refs/heads/b");
            ObjectId oldHead = db.Resolve(Constants.HEAD);
            Assert.IsTrue(rb.Equals(oldHead), "internal test condition, b == HEAD");
            writeReflog(db, rb, rb, "Just a message", "refs/heads/b");
            Assert.IsTrue(new FileInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/b")).Exists, "log on old branch");
            RefRename renameRef = db.RenameRef("refs/heads/b",
                    "refs/heads/new/name");
            RefUpdate.RefUpdateResult result = renameRef.rename();
            Assert.AreEqual(RefUpdate.RefUpdateResult.RENAMED, result);
            Assert.AreEqual(rb, db.Resolve("refs/heads/new/name"));
            Assert.IsNull(db.Resolve("refs/heads/b"));
            Assert.AreEqual("Branch: renamed b to new/name", db.ReflogReader(
                    "new/name").getLastEntry().getComment());
            Assert.IsFalse(new FileInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/b")).Exists);
            Assert.AreEqual(rb, db.Resolve(Constants.HEAD));
            Assert.AreEqual(2, db.ReflogReader("new/name").getReverseEntries().Count);
            Assert.AreEqual("Branch: renamed b to new/name", db.ReflogReader("new/name").getReverseEntries()[0].getComment());
            Assert.AreEqual("Just a message", db.ReflogReader("new/name").getReverseEntries()[1].getComment());
        }

        [Test]
        public void testRenameBranchAlsoInPack()
        {
            ObjectId rb = db.Resolve("refs/heads/b");
            ObjectId rb2 = db.Resolve("refs/heads/b~1");
            Assert.AreEqual(Storage.Packed, db.getRef("refs/heads/b").StorageFormat);
            RefUpdate updateRef = db.UpdateRef("refs/heads/b");
            updateRef.NewObjectId = rb2;
            updateRef.IsForceUpdate = true;
            RefUpdate.RefUpdateResult update = updateRef.update();
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, update, "internal check new ref is loose");
            Assert.AreEqual(Storage.Loose, db.getRef("refs/heads/b").StorageFormat);
            writeReflog(db, rb, rb, "Just a message", "refs/heads/b");
            Assert.IsTrue(new FileInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/b")).Exists, "log on old branch");
            RefRename renameRef = db.RenameRef("refs/heads/b",
                    "refs/heads/new/name");
            RefUpdate.RefUpdateResult result = renameRef.rename();
            Assert.AreEqual(RefUpdate.RefUpdateResult.RENAMED, result);
            Assert.AreEqual(rb2, db.Resolve("refs/heads/new/name"));
            Assert.IsNull(db.Resolve("refs/heads/b"));
            Assert.AreEqual("Branch: renamed b to new/name", db.ReflogReader(
                    "new/name").getLastEntry().getComment());
            Assert.AreEqual(3, db.ReflogReader("refs/heads/new/name").getReverseEntries().Count);
            Assert.AreEqual("Branch: renamed b to new/name", db.ReflogReader("refs/heads/new/name").getReverseEntries()[0].getComment());
            Assert.AreEqual(0, db.ReflogReader("HEAD").getReverseEntries().Count);
            // make sure b's log file is gone too.
            Assert.IsFalse(new FileInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/b")).Exists);

            // Create new Repository instance, to reread caches and make sure our
            // assumptions are persistent.
            using (Core.Repository ndb = new Core.Repository(db.Directory))
            {
                Assert.AreEqual(rb2, ndb.Resolve("refs/heads/new/name"));
                Assert.IsNull(ndb.Resolve("refs/heads/b"));
            }
        }

        public void tryRenameWhenLocked(string toLock, string fromName,
                string toName, string headPointsTo)
        {
            // setup
            writeSymref(Constants.HEAD, headPointsTo);
            ObjectId oldfromId = db.Resolve(fromName);
            ObjectId oldHeadId = db.Resolve(Constants.HEAD);
            writeReflog(db, oldfromId, oldfromId, "Just a message",
                    fromName);
            IList<ReflogReader.Entry> oldFromLog = db
                    .ReflogReader(fromName).getReverseEntries();
            IList<ReflogReader.Entry> oldHeadLog = oldHeadId != null ? db
                    .ReflogReader(Constants.HEAD).getReverseEntries() : null;

            Assert.IsTrue(new FileInfo(Path.Combine(db.Directory.FullName, "logs/" + fromName)).Exists, "internal check, we have a log");

            // "someone" has branch X locked
            var lockFile = new LockFile(new FileInfo(Path.Combine(db.Directory.FullName, toLock)));
            try
            {
                Assert.IsTrue(lockFile.Lock());

                // Now this is our test
                RefRename renameRef = db.RenameRef(fromName, toName);
                RefUpdate.RefUpdateResult result = renameRef.rename();
                Assert.AreEqual(RefUpdate.RefUpdateResult.LOCK_FAILURE, result);

                // Check that the involved refs are the same despite the failure
                assertExists(false, toName);
                if (!toLock.Equals(toName))
                    assertExists(false, toName + ".lock");
                assertExists(true, toLock + ".lock");
                if (!toLock.Equals(fromName))
                    assertExists(false, "logs/" + fromName + ".lock");
                assertExists(false, "logs/" + toName + ".lock");
                Assert.AreEqual(oldHeadId, db.Resolve(Constants.HEAD));
                Assert.AreEqual(oldfromId, db.Resolve(fromName));
                Assert.IsNull(db.Resolve(toName));
                Assert.AreEqual(oldFromLog.ToString(), db.ReflogReader(fromName)
                        .getReverseEntries().ToString());
                if (oldHeadId != null)
                    Assert.AreEqual(oldHeadLog.ToString(), db.ReflogReader(Constants.HEAD)
                            .getReverseEntries().ToString());
            }
            finally
            {
                lockFile.Unlock();
            }
        }

        private void assertExists(bool positive, string toName)
        {
            Assert.AreEqual(
                    positive, new FileInfo(Path.Combine(db.Directory.FullName, toName)).Exists, toName + (positive ? " " : " does not ") + "exist");
        }

        [Test]
        public void testRenameBranchCannotLockAFileHEADisFromLockHEAD()
        {
            tryRenameWhenLocked("HEAD", "refs/heads/b", "refs/heads/new/name",
                    "refs/heads/b");
        }

        [Test]
        public void testRenameBranchCannotLockAFileHEADisFromLockFrom()
        {
            tryRenameWhenLocked("refs/heads/b", "refs/heads/b",
                    "refs/heads/new/name", "refs/heads/b");
        }

        [Test]
        public void testRenameBranchCannotLockAFileHEADisFromLockTo()
        {
            tryRenameWhenLocked("refs/heads/new/name", "refs/heads/b",
                    "refs/heads/new/name", "refs/heads/b");
        }

        [Test]
        public void testRenameBranchCannotLockAFileHEADisToLockFrom()
        {
            tryRenameWhenLocked("refs/heads/b", "refs/heads/b",
                    "refs/heads/new/name", "refs/heads/new/name");
        }

        [Test]
        public void testRenameBranchCannotLockAFileHEADisToLockTo()
        {
            tryRenameWhenLocked("refs/heads/new/name", "refs/heads/b",
                    "refs/heads/new/name", "refs/heads/new/name");
        }

        [Test]
        public void testRenameBranchCannotLockAFileHEADisOtherLockFrom()
        {
            tryRenameWhenLocked("refs/heads/b", "refs/heads/b",
                    "refs/heads/new/name", "refs/heads/a");
        }

        [Test]
        public void testRenameBranchCannotLockAFileHEADisOtherLockTo()
        {
            tryRenameWhenLocked("refs/heads/new/name", "refs/heads/b",
                    "refs/heads/new/name", "refs/heads/a");
        }

        [Test]
        public void testRenameRefNameColission1avoided()
        {
            // setup
            ObjectId rb = db.Resolve("refs/heads/b");
            writeSymref(Constants.HEAD, "refs/heads/a");
            RefUpdate updateRef = db.UpdateRef("refs/heads/a");
            updateRef.NewObjectId = rb;
            updateRef.setRefLogMessage("Setup", false);
            Assert.AreEqual(RefUpdate.RefUpdateResult.FAST_FORWARD, updateRef.update());
            ObjectId oldHead = db.Resolve(Constants.HEAD);
            Assert.IsTrue(rb.Equals(oldHead)); // assumption for this test
            writeReflog(db, rb, rb, "Just a message", "refs/heads/a");
            Assert.IsTrue(new FileInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/a")).Exists, "internal check, we have a log");

            // Now this is our test
            RefRename renameRef = db.RenameRef("refs/heads/a", "refs/heads/a/b");
            RefUpdate.RefUpdateResult result = renameRef.rename();
            Assert.AreEqual(RefUpdate.RefUpdateResult.RENAMED, result);
            Assert.IsNull(db.Resolve("refs/heads/a"));
            Assert.AreEqual(rb, db.Resolve("refs/heads/a/b"));
            Assert.AreEqual(3, db.ReflogReader("a/b").getReverseEntries().Count);
            Assert.AreEqual("Branch: renamed a to a/b", db.ReflogReader("a/b")
                    .getReverseEntries()[0].getComment());
            Assert.AreEqual("Just a message", db.ReflogReader("a/b")
                    .getReverseEntries()[1].getComment());
            Assert.AreEqual("Setup", db.ReflogReader("a/b").getReverseEntries()
                    [2].getComment());
            // same thing was logged to HEAD
            Assert.AreEqual("Branch: renamed a to a/b", db.ReflogReader("HEAD")
                               .getReverseEntries()[0].getComment());
        }

        [Test]
        public void testRenameRefNameColission2avoided()
        {
            // setup
            ObjectId rb = db.Resolve("refs/heads/b");
            writeSymref(Constants.HEAD, "refs/heads/prefix/a");
            RefUpdate updateRef = db.UpdateRef("refs/heads/prefix/a");
            updateRef.NewObjectId = rb;
            updateRef.setRefLogMessage("Setup", false);
            updateRef.IsForceUpdate = true;
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, updateRef.update());
            ObjectId oldHead = db.Resolve(Constants.HEAD);
            Assert.IsTrue(rb.Equals(oldHead)); // assumption for this test
            writeReflog(db, rb, rb, "Just a message",
                    "refs/heads/prefix/a");
            Assert.IsTrue(new FileInfo(Path.Combine(db.Directory.FullName, "logs/refs/heads/prefix/a")).Exists, "internal check, we have a log");

            // Now this is our test
            RefRename renameRef = db.RenameRef("refs/heads/prefix/a",
                    "refs/heads/prefix");
            RefUpdate.RefUpdateResult result = renameRef.rename();
            Assert.AreEqual(RefUpdate.RefUpdateResult.RENAMED, result);

            Assert.IsNull(db.Resolve("refs/heads/prefix/a"));
            Assert.AreEqual(rb, db.Resolve("refs/heads/prefix"));
            Assert.AreEqual(3, db.ReflogReader("prefix").getReverseEntries().Count);
            Assert.AreEqual("Branch: renamed prefix/a to prefix", db.ReflogReader(
                    "prefix").getReverseEntries()[0].getComment());
            Assert.AreEqual("Just a message", db.ReflogReader("prefix")
                    .getReverseEntries()[1].getComment());
            Assert.AreEqual("Setup", db.ReflogReader("prefix").getReverseEntries()[2].getComment());
            Assert.AreEqual("Branch: renamed prefix/a to prefix", db.ReflogReader(
                       "HEAD").getReverseEntries()[0].getComment());
        }

        private void writeReflog(Repository db, ObjectId oldId, ObjectId newId,
                string msg, string refName)
        {
            RefDirectory refs = (RefDirectory)db.RefDatabase;
            RefUpdate update = refs.newUpdate(refName, true);
            update.setOldObjectId(oldId);
            update.setNewObjectId(newId);
            refs.log(update, msg, true);
        }
    }
}