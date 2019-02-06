/*
 * Copyright (C) 2007, Dave Watson <dwatson@mimvista.com>
 * Copyright (C) 2008, Robin Rosenberg <robin.rosenberg@dewire.com>
 * Copyright (C) 2008, Shawn O. Pearce <spearce@spearce.org>
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
using System.Text;
using System.IO;
using GitSharp.Core;
using GitSharp.Core.Tests;
using GitSharp.Tests.GitSharp.Core.Util;
using NUnit.Framework;
using GitSharp.Core.Util;

namespace GitSharp.Tests.GitSharp.Core
{
    [TestFixture]
    public class T0003_Basic_Write : SampleDataRepositoryTestCase
    {
        [Test]
        public void test001_Initalize()
        {
            var gitdir = PathUtil.CombineDirectoryPath(trash, Constants.DOT_GIT);
            var objects = PathUtil.CombineDirectoryPath(gitdir, "objects");
            var objects_pack = PathUtil.CombineDirectoryPath(objects, "pack");
            var objects_info = PathUtil.CombineDirectoryPath(objects, "info");
            var refs = PathUtil.CombineDirectoryPath(gitdir, "refs");
            var refs_heads = PathUtil.CombineDirectoryPath(refs, "heads");
            var refs_tags = PathUtil.CombineDirectoryPath(refs, "tags");
            var HEAD = PathUtil.CombineFilePath(gitdir, "HEAD");

            Assert.IsTrue(trash.IsDirectory(), "Exists " + trash);
            Assert.IsTrue(objects.IsDirectory(), "Exists " + objects);
            Assert.IsTrue(objects_pack.IsDirectory(), "Exists " + objects_pack);
            Assert.IsTrue(objects_info.IsDirectory(), "Exists " + objects_info);
            Assert.AreEqual(2, objects.ListFiles().Length);
            Assert.IsTrue(refs.IsDirectory(), "Exists " + refs);
            Assert.IsTrue(refs_heads.IsDirectory(), "Exists " + refs_heads);
            Assert.IsTrue(refs_tags.IsDirectory(), "Exists " + refs_tags);
            Assert.IsTrue(HEAD.IsFile(), "Exists " + HEAD);
            Assert.AreEqual(23, HEAD.Length);
        }

        [Test]
        public void test000_openRepoBadArgs()
        {
            var e = AssertHelper.Throws<ArgumentException>(() => { using (var r = new global::GitSharp.Core.Repository(null, null)) { } });
            Assert.AreEqual("Either GIT_DIR or GIT_WORK_TREE must be passed to Repository constructor", e.Message);
        }

        /*
         * Check the default rules for looking up directories and files within a
         * repo when the gitDir is given.
         */
        [Test]
        public void test000_openrepo_default_gitDirSet()
        {
            DirectoryInfo repo1Parent = PathUtil.CombineDirectoryPath(trash.Parent, "r1");
            using (global::GitSharp.Core.Repository repo1initial = new global::GitSharp.Core.Repository(PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT)))
            {
                repo1initial.Create();
            }

            DirectoryInfo theDir = PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT);
            using (global::GitSharp.Core.Repository r = new global::GitSharp.Core.Repository(theDir, null))
            {
                assertEqualsPath(theDir, r.Directory);
                assertEqualsPath(repo1Parent, r.WorkingDirectory);
                assertEqualsPath(PathUtil.CombineFilePath(theDir, "index"), r.getIndexFile());
                assertEqualsPath(PathUtil.CombineDirectoryPath(theDir, "objects"), r.ObjectsDirectory);
            }
        }

        /*
         * Check that we can pass both a git directory and a work tree
         * repo when the gitDir is given.
         */
        [Test]
        public void test000_openrepo_default_gitDirAndWorkTreeSet()
        {
            DirectoryInfo repo1Parent = PathUtil.CombineDirectoryPath(trash.Parent, "r1");
            using (global::GitSharp.Core.Repository repo1initial = new global::GitSharp.Core.Repository(PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT)))
            {
                repo1initial.Create();
            }

            DirectoryInfo theDir = PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT);
            using (global::GitSharp.Core.Repository r = new global::GitSharp.Core.Repository(theDir, repo1Parent.Parent))
            {
                assertEqualsPath(theDir, r.Directory);
                assertEqualsPath(repo1Parent.Parent, r.WorkingDirectory);
                assertEqualsPath(PathUtil.CombineFilePath(theDir, "index"), r.getIndexFile());
                assertEqualsPath(PathUtil.CombineDirectoryPath(theDir, "objects"), r.ObjectsDirectory);
            }
        }

        /*
         * Check the default rules for looking up directories and files within a
         * repo when the workTree is given.
         */
        [Test]
        public void test000_openrepo_default_workDirSet()
        {
            DirectoryInfo repo1Parent = PathUtil.CombineDirectoryPath(trash.Parent, "r1");
            using (global::GitSharp.Core.Repository repo1initial = new global::GitSharp.Core.Repository(PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT)))
            {
                repo1initial.Create();
            }

            DirectoryInfo theDir = PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT);
            using (global::GitSharp.Core.Repository r = new global::GitSharp.Core.Repository(null, repo1Parent))
            {
                assertEqualsPath(theDir, r.Directory);
                assertEqualsPath(repo1Parent, r.WorkingDirectory);
                assertEqualsPath(PathUtil.CombineFilePath(theDir, "index"), r.getIndexFile());
                assertEqualsPath(PathUtil.CombineDirectoryPath(theDir, "objects"), r.ObjectsDirectory);
            }
        }

        /*
         * Check that worktree config has an effect, given absolute path.
         */
        [Test]
        public void test000_openrepo_default_absolute_workdirconfig()
        {
            DirectoryInfo repo1Parent = PathUtil.CombineDirectoryPath(trash.Parent, "r1");
            DirectoryInfo workdir = PathUtil.CombineDirectoryPath(trash.Parent, "rw");
            workdir.Mkdirs();

            using (global::GitSharp.Core.Repository repo1initial = new global::GitSharp.Core.Repository(PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT)))
            {
                repo1initial.Create();
                repo1initial.Config.setString("core", null, "worktree", workdir.FullName);
                repo1initial.Config.save();
            }

            DirectoryInfo theDir = PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT);
            using (global::GitSharp.Core.Repository r = new global::GitSharp.Core.Repository(theDir, null))
            {
                assertEqualsPath(theDir, r.Directory);
                assertEqualsPath(workdir, r.WorkingDirectory);
                assertEqualsPath(PathUtil.CombineFilePath(theDir, "index"), r.getIndexFile());
                assertEqualsPath(PathUtil.CombineDirectoryPath(theDir, "objects"), r.ObjectsDirectory);
            }
        }

        /*
         * Check that worktree config has an effect, given a relative path.
         */
        [Test]
        public void test000_openrepo_default_relative_workdirconfig()
        {
            DirectoryInfo repo1Parent = PathUtil.CombineDirectoryPath(trash.Parent, "r1");
            DirectoryInfo workdir = PathUtil.CombineDirectoryPath(trash.Parent, "rw");
            workdir.Mkdirs();

            using (global::GitSharp.Core.Repository repo1initial = new global::GitSharp.Core.Repository(PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT)))
            {
                repo1initial.Create();
                repo1initial.Config.setString("core", null, "worktree", "../../rw");
                repo1initial.Config.save();
            }

            DirectoryInfo theDir = PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT);
            using (global::GitSharp.Core.Repository r = new global::GitSharp.Core.Repository(theDir, null))
            {
                assertEqualsPath(theDir, r.Directory);
                assertEqualsPath(workdir, r.WorkingDirectory);
                assertEqualsPath(PathUtil.CombineFilePath(theDir, "index"), r.getIndexFile());
                assertEqualsPath(PathUtil.CombineDirectoryPath(theDir, "objects"), r.ObjectsDirectory);
            }
        }

        /*
         * Check that the given index file is honored and the alternate object
         * directories too
         */
        [Test]
        [Ignore("Repository is not properly released. Once this bug is fixed, remove final GC.Collect() call." )]
        public void test000_openrepo_alternate_index_file_and_objdirs()
        {
            DirectoryInfo repo1Parent = PathUtil.CombineDirectoryPath(trash.Parent, "r1");
            FileInfo indexFile = PathUtil.CombineFilePath(trash, "idx");
            DirectoryInfo objDir = PathUtil.CombineDirectoryPath(trash, "../obj");
            DirectoryInfo[] altObjDirs = new[] { db.ObjectsDirectory };

            using (global::GitSharp.Core.Repository repo1initial = new global::GitSharp.Core.Repository(PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT)))
            {
                repo1initial.Create();
            }

            DirectoryInfo theDir = PathUtil.CombineDirectoryPath(repo1Parent, Constants.DOT_GIT);
            using (global::GitSharp.Core.Repository r = new global::GitSharp.Core.Repository(theDir, null, objDir, altObjDirs, indexFile))
            {
                assertEqualsPath(theDir, r.Directory);
                assertEqualsPath(theDir.Parent, r.WorkingDirectory);
                assertEqualsPath(indexFile, r.getIndexFile());
                assertEqualsPath(objDir, r.ObjectsDirectory);
                Assert.IsNotNull(r.MapCommit("6db9c2ebf75590eef973081736730a9ea169a0c4"));
                // Must close or the default repo pack files created by this test gets
                // locked via the alternate object directories on Windows.
                r.Close();
            }

            GC.Collect(); // To be removed once the repository resource releasing is fixed.
        }

        protected void assertEqualsPath(FileSystemInfo expected, FileSystemInfo actual)
        {
            Assert.AreEqual(expected.FullName, actual.FullName);
        }

        [Test]
        public void Write_Blob()
        {
            ObjectId id = new ObjectWriter(db).WriteBlob(new FileInfo("Resources/single_file_commit/i-am-a-file"));
            Assert.AreEqual("95ea6a6859af6791464bd8b6de76ad5a6f9fad81", id.Name);
            Assert.AreEqual(Inspector.Inspect("Resources/single_file_commit", "95ea6a6859af6791464bd8b6de76ad5a6f9fad81"), new Inspector(db).Inspect(id));

            writeTrashFile("i-am-a-file", "and this is the data in me\r\n\r\n");
            id = new ObjectWriter(db).WriteBlob(new FileInfo(db.WorkingDirectory.FullName + "/i-am-a-file"));
            Assert.AreEqual("95ea6a6859af6791464bd8b6de76ad5a6f9fad81", id.Name);
        }

        [Test]
        public void Write_Tree()
        {
            var t = new global::GitSharp.Core.Tree(db);
            FileTreeEntry f = t.AddFile("i-am-a-file");
            writeTrashFile(f.Name, "and this is the data in me\r\n\r\n");
            Assert.AreEqual(File.ReadAllText("Resources/single_file_commit/i-am-a-file"), File.ReadAllText(db.WorkingDirectory.FullName + "/i-am-a-file"));
            t.Accept(new WriteTree(trash, db), TreeEntry.MODIFIED_ONLY);
            var id = t.Id;

            int git_w1;
            int git_w2;
            int w1;
            int w2;

            using (var b1 = new BinaryReader(new Inspector(db).ContentStream(id)))
            using (var b2 = new BinaryReader(Inspector.ContentStream("Resources/single_file_commit", "917c130bd4fa5bf2df0c399dc1b03401860aa448")))
            {
                b1.BaseStream.Position = b1.BaseStream.Length - 21;
                b2.BaseStream.Position = b2.BaseStream.Length - 21;
                Assert.AreEqual(b2.ReadByte(), b1.ReadByte());

                git_w1 = b2.ReadInt32();
                git_w2 = b2.ReadInt32();
                var git_w3 = b2.ReadInt32();
                var git_w4 = b2.ReadInt32();
                var git_w5 = b2.ReadInt32();
                b2.BaseStream.Position = b2.BaseStream.Length - 20;
                var git_id = ObjectId.FromRaw(b2.ReadBytes(20));
                w1 = b1.ReadInt32();
                w2 = b1.ReadInt32();
            }

            Assert.AreEqual(git_w1,w1);
            Assert.AreEqual(git_w2, w2);

            Assert.AreEqual("917c130bd4fa5bf2df0c399dc1b03401860aa448", id.Name);
            var s_git = Inspector.Inspect("Resources/single_file_commit", "917c130bd4fa5bf2df0c399dc1b03401860aa448");
            var s = new Inspector(db).Inspect(id);
            Assert.AreEqual(s_git, s);
        }

        [Test]
        public void test002_WriteEmptyTree()
        {
            // One of our test packs contains the empty tree object. If the pack is
            // open when we Create it we won't write the object file out as a loose
            // object (as it already exists in the pack).
            //
            global::GitSharp.Core.Repository newdb = createBareRepository();
            var t = new global::GitSharp.Core.Tree(newdb);
            t.Accept(new WriteTree(trash, newdb), TreeEntry.MODIFIED_ONLY);
            Assert.AreEqual("4b825dc642cb6eb9a060e54bf8d69288fbee4904", t.Id.Name);
            var o = new FileInfo(newdb.Directory + "/objects/4b/825dc642cb6eb9a060e54bf8d69288fbee4904");
            Assert.IsTrue(o.IsFile(), "Exists " + o);
            Assert.IsTrue(o.IsReadOnly, "Read-only " + o);
        }

        [Test]
        public void test002_WriteEmptyTree2()
        {
            // File shouldn't exist as it is in a test pack.
            //
            var t = new global::GitSharp.Core.Tree(db);
            t.Accept(new WriteTree(trash, db), TreeEntry.MODIFIED_ONLY);
            Assert.AreEqual("4b825dc642cb6eb9a060e54bf8d69288fbee4904", t.Id.Name);
            var o = new FileInfo(db.Directory + "/objects/4b/825dc642cb6eb9a060e54bf8d69288fbee4904");
            Assert.IsFalse(o.IsFile(), "Exists " + o);
        }

        [Test]
        public void test003_WriteShouldBeEmptyTree()
        {
            global::GitSharp.Core.Tree t = new global::GitSharp.Core.Tree(db);
            ObjectId emptyId = new ObjectWriter(db).WriteBlob(new byte[0]);
            t.AddFile("should-be-empty").Id = emptyId;
            t.Accept(new WriteTree(trash, db), TreeEntry.MODIFIED_ONLY);
            Assert.AreEqual("7bb943559a305bdd6bdee2cef6e5df2413c3d30a", t.Id.Name);

            var o = new FileInfo(db.Directory + "/objects/7b/b943559a305bdd6bdee2cef6e5df2413c3d30a");
            Assert.IsTrue(o.IsFile(), "Exists " + o);
            Assert.IsTrue(o.IsReadOnly, "Read-only " + o);

            o = new FileInfo(db.Directory + "/objects/e6/9de29bb2d1d6434b8b29ae775ad8c2e48c5391");
            Assert.IsTrue(o.IsFile(), "Exists " + o);
            Assert.IsTrue(o.IsReadOnly, "Read-only " + o);
        }

        [Test]
        public void Write_Simple_Commit()
        {
            var t = new global::GitSharp.Core.Tree(db);
            FileTreeEntry f = t.AddFile("i-am-a-file");
            writeTrashFile(f.Name, "and this is the data in me\r\n\r\n");
            t.Accept(new WriteTree(trash, db), TreeEntry.MODIFIED_ONLY);
            //new ObjectChecker().checkBlob(Constants.CHARSET.GetString(db.OpenObject(t.TreeId).getBytes()).ToCharArray());

            string s = new Inspector(db).Inspect(t.Id);
            string s1 = Inspector.Inspect("Resources/single_file_commit", "16c0beaf7523eb3ef5df45bd42dd4fc6343de864");
            string s2 = Inspector.Inspect("Resources/single_file_commit", "917c130bd4fa5bf2df0c399dc1b03401860aa448");
            string s3 = Inspector.Inspect("Resources/single_file_commit", "95ea6a6859af6791464bd8b6de76ad5a6f9fad81");

            //tree 917c130bd4fa5bf2df0c399dc1b03401860aa448\nauthor henon <meinrad.recheis@gmail.com> 1245946742 +0200\ncommitter henon <meinrad.recheis@gmail.com> 1245946742 +0200\n\nA Commit\n"

            Assert.AreEqual(ObjectId.FromString("917c130bd4fa5bf2df0c399dc1b03401860aa448"), t.Id);


            var c = new global::GitSharp.Core.Commit(db)
                        {
                            Author = (new PersonIdent("henon", "meinrad.recheis@gmail.com", 1245946742000L, 2*60)),
                            Committer = (new PersonIdent("henon", "meinrad.recheis@gmail.com", 1245946742000L, 2*60)),
                            Message = ("A Commit\n"),
                            TreeEntry = (t)
                        };
            Assert.AreEqual(t.TreeId, c.TreeId);
            c.Save();

            string s_c = new Inspector(db).Inspect(c.CommitId);
            ObjectId cmtid = ObjectId.FromString("16c0beaf7523eb3ef5df45bd42dd4fc6343de864");
            Assert.AreEqual(cmtid, c.CommitId);

            // Verify the commit we just wrote is in the correct format.
            //using (var xis = new XInputStream(new FileStream(db.ToFile(cmtid).FullName, System.IO.FileMode.Open, FileAccess.Read)))
            //{
            //    Assert.AreEqual(0x78, xis.ReadUInt8());
            //    Assert.AreEqual(0x9c, xis.ReadUInt8());
            //    Assert.IsTrue(0x789c % 31 == 0);
            //}

            // Verify we can Read it.
            global::GitSharp.Core.Commit c2 = db.MapCommit(cmtid.Name);
            Assert.IsNotNull(c2);
            Assert.AreEqual(c.Message, c2.Message);
            Assert.AreEqual(c.TreeId, c2.TreeId);
            Assert.AreEqual(c.Author, c2.Author);
            Assert.AreEqual(c.Committer, c2.Committer);
        }

        [Test]
        public void test009_CreateCommitOldFormat()
        {
            writeTrashFile(".git/config", "[core]\n" + "legacyHeaders=1\n");
            db.Config.load();
            Assert.AreEqual(true, db.Config.getBoolean("core", "legacyHeaders", false));

            var t = new global::GitSharp.Core.Tree(db);
            FileTreeEntry f = t.AddFile("i-am-a-file");
            writeTrashFile(f.Name, "and this is the data in me\n");
            t.Accept(new WriteTree(trash, db), TreeEntry.MODIFIED_ONLY);

            Assert.AreEqual(ObjectId.FromString("00b1f73724f493096d1ffa0b0f1f1482dbb8c936"), t.TreeId);


            var c = new global::GitSharp.Core.Commit(db)
                        {
                            Author = (new PersonIdent(author, 1154236443000L, -4*60)),
                            Committer = (new PersonIdent(committer, 1154236443000L, -4*60)),
                            Message = ("A Commit\n"),
                            TreeEntry = t
                        };
            Assert.AreEqual(t.TreeId, c.TreeId);
            c.Save();

            ObjectId cmtid = ObjectId.FromString("803aec4aba175e8ab1d666873c984c0308179099");
            Assert.AreEqual(cmtid, c.CommitId);

            // Verify the commit we just wrote is in the correct format.
            using (var xis = new XInputStream(new FileStream(db.ToFile(cmtid).FullName, System.IO.FileMode.Open, FileAccess.Read)))
            {
                Assert.AreEqual(0x78, xis.ReadUInt8());
                Assert.AreEqual(0x9c, xis.ReadUInt8());
                Assert.IsTrue(0x789c % 31 == 0);
            }

            // Verify we can Read it.
            global::GitSharp.Core.Commit c2 = db.MapCommit(cmtid);
            Assert.IsNotNull(c2);
            Assert.AreEqual(c.Message, c2.Message);
            Assert.AreEqual(c.TreeId, c2.TreeId);
            Assert.AreEqual(c.Author, c2.Author);
            Assert.AreEqual(c.Committer, c2.Committer);
        }


        [Test]
        public void test012_SubtreeExternalSorting()
        {
            ObjectId emptyBlob = new ObjectWriter(db).WriteBlob(new byte[0]);
            var t = new global::GitSharp.Core.Tree(db);
            FileTreeEntry e0 = t.AddFile("a-");
            FileTreeEntry e1 = t.AddFile("a-b");
            FileTreeEntry e2 = t.AddFile("a/b");
            FileTreeEntry e3 = t.AddFile("a=");
            FileTreeEntry e4 = t.AddFile("a=b");

            e0.Id = emptyBlob;
            e1.Id = emptyBlob;
            e2.Id = emptyBlob;
            e3.Id = emptyBlob;
            e4.Id = emptyBlob;

            t.Accept(new WriteTree(trash, db), TreeEntry.MODIFIED_ONLY);
            Assert.AreEqual(ObjectId.FromString("b47a8f0a4190f7572e11212769090523e23eb1ea"), t.Id);
        }

        [Test]
        public void test020_createBlobTag()
        {
            ObjectId emptyId = new ObjectWriter(db).WriteBlob(new byte[0]);
            var t = new global::GitSharp.Core.Tag(db)
                        {
                            Id = emptyId,
                            TagType = "blob",
                            TagName = "test020",
                            Author = new PersonIdent(author, 1154236443000L, -4*60),
                            Message = "test020 tagged\n"
                        };
            t.Save();
            Assert.AreEqual("6759556b09fbb4fd8ae5e315134481cc25d46954", t.TagId.Name);

            global::GitSharp.Core.Tag MapTag = db.MapTag("test020");
            Assert.AreEqual("blob", MapTag.TagType);
            Assert.AreEqual("test020 tagged\n", MapTag.Message);
            Assert.AreEqual(new PersonIdent(author, 1154236443000L, -4 * 60), MapTag.Author);
            Assert.AreEqual("e69de29bb2d1d6434b8b29ae775ad8c2e48c5391", MapTag.Id.Name);
        }

        [Test]
        public void test020b_createBlobPlainTag()
        {
            test020_createBlobTag();
            var t = new global::GitSharp.Core.Tag(db)
                        {
                            TagName = "test020b",
                            Id = ObjectId.FromString("e69de29bb2d1d6434b8b29ae775ad8c2e48c5391")
                        };
            t.Save();

            global::GitSharp.Core.Tag MapTag = db.MapTag("test020b");
            Assert.AreEqual("e69de29bb2d1d6434b8b29ae775ad8c2e48c5391", MapTag.Id.Name);

            // We do not repeat the plain tag test for other object types
        }

        [Test]
        public void test021_createTreeTag()
        {
            ObjectId emptyId = new ObjectWriter(db).WriteBlob(new byte[0]);
            var almostEmptyTree = new global::GitSharp.Core.Tree(db);
            almostEmptyTree.AddEntry(new FileTreeEntry(almostEmptyTree, emptyId, "empty".getBytes(), false));
            ObjectId almostEmptyTreeId = new ObjectWriter(db).WriteTree(almostEmptyTree);

            var t = new global::GitSharp.Core.Tag(db)
                        {
                            Id = almostEmptyTreeId,
                            TagType = "tree",
                            TagName = "test021",
                            Author = new PersonIdent(author, 1154236443000L, -4*60),
                            Message = "test021 tagged\n"
                        };
        	
            t.Save();
            Assert.AreEqual("b0517bc8dbe2096b419d42424cd7030733f4abe5", t.TagId.Name);

            global::GitSharp.Core.Tag MapTag = db.MapTag("test021");
            Assert.AreEqual("tree", MapTag.TagType);
            Assert.AreEqual("test021 tagged\n", MapTag.Message);
            Assert.AreEqual(new PersonIdent(author, 1154236443000L, -4 * 60), MapTag.Author);
            Assert.AreEqual("417c01c8795a35b8e835113a85a5c0c1c77f67fb", MapTag.Id.Name);
        }

        [Test]
        public void test022_createCommitTag()
        {
            ObjectId emptyId = new ObjectWriter(db).WriteBlob(new byte[0]);
            var almostEmptyTree = new global::GitSharp.Core.Tree(db);
            almostEmptyTree.AddEntry(new FileTreeEntry(almostEmptyTree, emptyId, "empty".getBytes(), false));
            ObjectId almostEmptyTreeId = new ObjectWriter(db).WriteTree(almostEmptyTree);

            var almostEmptyCommit = new global::GitSharp.Core.Commit(db)
                                        {
                                            Author = new PersonIdent(author, 1154236443000L, -2*60),
                                            Committer = new PersonIdent(author, 1154236443000L, -2*60),
                                            Message = "test022\n",
                                            TreeId = almostEmptyTreeId
                                        };

            ObjectId almostEmptyCommitId = new ObjectWriter(db).WriteCommit(almostEmptyCommit);

            var t = new global::GitSharp.Core.Tag(db)
                        {
                            Id = almostEmptyCommitId,
                            TagType = "commit",
                            TagName = "test022",
                            Author = new PersonIdent(author, 1154236443000L, -4*60),
                            Message = "test022 tagged\n"
                        };

            t.Save();
            Assert.AreEqual("0ce2ebdb36076ef0b38adbe077a07d43b43e3807", t.TagId.Name);

            global::GitSharp.Core.Tag mapTag = db.MapTag("test022");
            Assert.AreEqual("commit", mapTag.TagType);
            Assert.AreEqual("test022 tagged\n", mapTag.Message);
            Assert.AreEqual(new PersonIdent(author, 1154236443000L, -4 * 60), mapTag.Author);
            Assert.AreEqual("b5d3b45a96b340441f5abb9080411705c51cc86c", mapTag.Id.Name);
        }

        [Test]
        public void test023_createCommitNonAnullii()
        {
            ObjectId emptyId = new ObjectWriter(db).WriteBlob(new byte[0]);
            var almostEmptyTree = new global::GitSharp.Core.Tree(db);
            almostEmptyTree.AddEntry(new FileTreeEntry(almostEmptyTree, emptyId, "empty".getBytes(), false));
            ObjectId almostEmptyTreeId = new ObjectWriter(db).WriteTree(almostEmptyTree);

            var commit = new global::GitSharp.Core.Commit(db)
                             {
                                 TreeId = almostEmptyTreeId,
                                 Author = new PersonIdent("Joe H\u00e4cker", "joe@example.com", 4294967295000L, 60),
                                 Committer = new PersonIdent("Joe Hacker", "joe2@example.com", 4294967295000L, 60),
                                 Encoding = Constants.CHARSET,
                                 Message = "\u00dcbergeeks"
                             };
        	
            ObjectId cid = new ObjectWriter(db).WriteCommit(commit);
            Assert.AreEqual("4680908112778718f37e686cbebcc912730b3154", cid.Name);

            global::GitSharp.Core.Commit loadedCommit = db.MapCommit(cid);
            Assert.AreNotSame(loadedCommit, commit);
            Assert.AreEqual(commit.Message, loadedCommit.Message);
        }

        [Test]
        public void test024_createCommitNonAscii()
        {
            ObjectId emptyId = new ObjectWriter(db).WriteBlob(new byte[0]);
            var almostEmptyTree = new global::GitSharp.Core.Tree(db);
            almostEmptyTree.AddEntry(new FileTreeEntry(almostEmptyTree, emptyId, "empty".getBytes(), false));
            ObjectId almostEmptyTreeId = new ObjectWriter(db).WriteTree(almostEmptyTree);
            var commit = new global::GitSharp.Core.Commit(db)
                             {
                                 TreeId = almostEmptyTreeId,
                                 Author = new PersonIdent("Joe H\u00e4cker", "joe@example.com", 4294967295000L, 60),
                                 Committer = new PersonIdent("Joe Hacker", "joe2@example.com", 4294967295000L, 60),
                                 Encoding = Encoding.GetEncoding("ISO-8859-1"),
                                 Message = "\u00dcbergeeks"
                             };
            ObjectId cid = new ObjectWriter(db).WriteCommit(commit);
            var s = new Inspector(db).Inspect(cid);
            Assert.AreEqual("2979b39d385014b33287054b87f77bcb3ecb5ebf", cid.Name);
        }

        [Test]
        public void test025_packedRefs()
        {
            test020_createBlobTag();
            test021_createTreeTag();
            test022_createCommitTag();

            new FileInfo(db.Directory.FullName + "/refs/tags/test020").Delete();
            if (new FileInfo(db.Directory.FullName + "/refs/tags/test020").Exists) throw new IOException("Cannot delete unpacked tag");
            new FileInfo(db.Directory.FullName + "/refs/tags/test021").Delete();
            if (new FileInfo(db.Directory.FullName + "/refs/tags/test021").Exists) throw new IOException("Cannot delete unpacked tag");
            new FileInfo(db.Directory.FullName + "/refs/tags/test022").Delete();
            if (new FileInfo(db.Directory.FullName + "/refs/tags/test022").Exists) throw new IOException("Cannot delete unpacked tag");

            // We cannot Resolve it now, since we have no ref
            global::GitSharp.Core.Tag mapTag20missing = db.MapTag("test020");
            Assert.IsNull(mapTag20missing);

            // Construct packed refs file
            var fs = new FileStream(db.Directory.FullName + "/packed-refs", System.IO.FileMode.Create);
            using (var w = new StreamWriter(fs))
            {
                w.WriteLine("# packed-refs with: peeled");
                w.WriteLine("6759556b09fbb4fd8ae5e315134481cc25d46954 refs/tags/test020");
                w.WriteLine("^e69de29bb2d1d6434b8b29ae775ad8c2e48c5391");
                w.WriteLine("b0517bc8dbe2096b419d42424cd7030733f4abe5 refs/tags/test021");
                w.WriteLine("^417c01c8795a35b8e835113a85a5c0c1c77f67fb");
                w.WriteLine("0ce2ebdb36076ef0b38adbe077a07d43b43e3807 refs/tags/test022");
                w.WriteLine("^b5d3b45a96b340441f5abb9080411705c51cc86c");
            }

            ((RefDirectory)db.RefDatabase).rescan();
            global::GitSharp.Core.Tag mapTag20 = db.MapTag("test020");
            Assert.IsNotNull(mapTag20, "have tag test020");
            Assert.AreEqual("blob", mapTag20.TagType);
            Assert.AreEqual("test020 tagged\n", mapTag20.Message);
            Assert.AreEqual(new PersonIdent(author, 1154236443000L, -4 * 60), mapTag20.Author);
            Assert.AreEqual("e69de29bb2d1d6434b8b29ae775ad8c2e48c5391", mapTag20.Id.Name);

            global::GitSharp.Core.Tag mapTag21 = db.MapTag("test021");
            Assert.AreEqual("tree", mapTag21.TagType);
            Assert.AreEqual("test021 tagged\n", mapTag21.Message);
            Assert.AreEqual(new PersonIdent(author, 1154236443000L, -4 * 60), mapTag21.Author);
            Assert.AreEqual("417c01c8795a35b8e835113a85a5c0c1c77f67fb", mapTag21.Id.Name);

            global::GitSharp.Core.Tag mapTag22 = db.MapTag("test022");
            Assert.AreEqual("commit", mapTag22.TagType);
            Assert.AreEqual("test022 tagged\n", mapTag22.Message);
            Assert.AreEqual(new PersonIdent(author, 1154236443000L, -4 * 60), mapTag22.Author);
            Assert.AreEqual("b5d3b45a96b340441f5abb9080411705c51cc86c", mapTag22.Id.Name);
        }

        [Test]
        public void test025_computeSha1NoStore()
        {
            byte[] data = "test025 some data, more than 16 bytes to get good coverage".getBytes("ISO-8859-1");
            ObjectId id = new ObjectWriter(db).ComputeBlobSha1(data.Length, new MemoryStream(data));
            Assert.AreEqual("4f561df5ecf0dfbd53a0dc0f37262fef075d9dde", id.Name);
        }

        [Test]
        public void test026_CreateCommitMultipleparents()
        {
            db.Config.load();

            var t = new global::GitSharp.Core.Tree(db);
            FileTreeEntry f = t.AddFile("i-am-a-file");
            writeTrashFile(f.Name, "and this is the data in me\n");
            t.Accept(new WriteTree(trash, db), TreeEntry.MODIFIED_ONLY);
            Assert.AreEqual(ObjectId.FromString("00b1f73724f493096d1ffa0b0f1f1482dbb8c936"),
                            t.TreeId);

            var c1 = new global::GitSharp.Core.Commit(db)
                         {
                             Author = new PersonIdent(author, 1154236443000L, -4*60),
                             Committer = new PersonIdent(committer, 1154236443000L, -4*60),
                             Message = "A Commit\n",
                             TreeEntry = t
                         };

            Assert.AreEqual(t.TreeId, c1.TreeId);
            c1.Save();
            ObjectId cmtid1 = ObjectId.FromString("803aec4aba175e8ab1d666873c984c0308179099");
            Assert.AreEqual(cmtid1, c1.CommitId);

            var c2 = new global::GitSharp.Core.Commit(db)
                         {
                             Author = new PersonIdent(author, 1154236443000L, -4*60),
                             Committer = new PersonIdent(committer, 1154236443000L, -4*60),
                             Message = "A Commit 2\n",
                             TreeEntry = t
                         };

            Assert.AreEqual(t.TreeId, c2.TreeId);
            c2.ParentIds = new[] { c1.CommitId };
            c2.Save();
            ObjectId cmtid2 = ObjectId.FromString("95d068687c91c5c044fb8c77c5154d5247901553");
            Assert.AreEqual(cmtid2, c2.CommitId);

            global::GitSharp.Core.Commit rm2 = db.MapCommit(cmtid2);
            Assert.AreNotSame(c2, rm2); // assert the parsed objects is not from the cache
            Assert.AreEqual(c2.Author, rm2.Author);
            Assert.AreEqual(c2.CommitId, rm2.CommitId);
            Assert.AreEqual(c2.Message, rm2.Message);
            Assert.AreEqual(c2.TreeEntry.TreeId, rm2.TreeEntry.TreeId);
            Assert.AreEqual(1, rm2.ParentIds.Length);
            Assert.AreEqual(c1.CommitId, rm2.ParentIds[0]);

            var c3 = new global::GitSharp.Core.Commit(db)
                         {
                             Author = new PersonIdent(author, 1154236443000L, -4*60),
                             Committer = new PersonIdent(committer, 1154236443000L, -4*60),
                             Message = "A Commit 3\n",
                             TreeEntry = t
                         };

            Assert.AreEqual(t.TreeId, c3.TreeId);
            c3.ParentIds = new[] { c1.CommitId, c2.CommitId };
            c3.Save();
            ObjectId cmtid3 = ObjectId.FromString("ce6e1ce48fbeeb15a83f628dc8dc2debefa066f4");
            Assert.AreEqual(cmtid3, c3.CommitId);

            global::GitSharp.Core.Commit rm3 = db.MapCommit(cmtid3);
            Assert.AreNotSame(c3, rm3); // assert the parsed objects is not from the cache
            Assert.AreEqual(c3.Author, rm3.Author);
            Assert.AreEqual(c3.CommitId, rm3.CommitId);
            Assert.AreEqual(c3.Message, rm3.Message);
            Assert.AreEqual(c3.TreeEntry.TreeId, rm3.TreeEntry.TreeId);
            Assert.AreEqual(2, rm3.ParentIds.Length);
            Assert.AreEqual(c1.CommitId, rm3.ParentIds[0]);
            Assert.AreEqual(c2.CommitId, rm3.ParentIds[1]);

            var c4 = new global::GitSharp.Core.Commit(db)
                         {
                             Author = new PersonIdent(author, 1154236443000L, -4*60),
                             Committer = new PersonIdent(committer, 1154236443000L, -4*60),
                             Message = "A Commit 4\n",
                             TreeEntry = t
                         };

            Assert.AreEqual(t.TreeId, c3.TreeId);
            c4.ParentIds = new[] { c1.CommitId, c2.CommitId, c3.CommitId };
            c4.Save();
            ObjectId cmtid4 = ObjectId.FromString("d1fca9fe3fef54e5212eb67902c8ed3e79736e27");
            Assert.AreEqual(cmtid4, c4.CommitId);

            global::GitSharp.Core.Commit rm4 = db.MapCommit(cmtid4);
            Assert.AreNotSame(c4, rm3); // assert the parsed objects is not from the cache
            Assert.AreEqual(c4.Author, rm4.Author);
            Assert.AreEqual(c4.CommitId, rm4.CommitId);
            Assert.AreEqual(c4.Message, rm4.Message);
            Assert.AreEqual(c4.TreeEntry.TreeId, rm4.TreeEntry.TreeId);
            Assert.AreEqual(3, rm4.ParentIds.Length);
            Assert.AreEqual(c1.CommitId, rm4.ParentIds[0]);
            Assert.AreEqual(c2.CommitId, rm4.ParentIds[1]);
            Assert.AreEqual(c3.CommitId, rm4.ParentIds[2]);
        }

        [Test]
        public void test027_UnpackedRefHigherPriorityThanPacked()
        {
            const string unpackedId = "7f822839a2fe9760f386cbbbcb3f92c5fe81def7";
            using (var writer = new StreamWriter(new FileStream(db.Directory.FullName + "/refs/heads/a", System.IO.FileMode.CreateNew)))
            {
                writer.Write(unpackedId);
                writer.Write('\n');
            }

            ObjectId resolved = db.Resolve("refs/heads/a");
            Assert.AreEqual(unpackedId, resolved.Name);
        }

        [Test]
        public void test028_LockPackedRef()
        {
            writeTrashFile(".git/packed-refs", "7f822839a2fe9760f386cbbbcb3f92c5fe81def7 refs/heads/foobar");
            writeTrashFile(".git/HEAD", "ref: refs/heads/foobar\n");
            BUG_WorkAroundRacyGitIssues("packed-refs");
            BUG_WorkAroundRacyGitIssues("HEAD");

            ObjectId resolve = db.Resolve("HEAD");
            Assert.AreEqual("7f822839a2fe9760f386cbbbcb3f92c5fe81def7", resolve.Name);

            RefUpdate lockRef = db.UpdateRef("HEAD");
            ObjectId newId = ObjectId.FromString("07f822839a2fe9760f386cbbbcb3f92c5fe81def");
            lockRef.NewObjectId = newId;
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, lockRef.forceUpdate());

            Assert.IsTrue(new FileInfo(db.Directory.FullName + "/refs/heads/foobar").Exists);
            Assert.AreEqual(newId, db.Resolve("refs/heads/foobar"));

            // Again. The ref already exists
            RefUpdate lockRef2 = db.UpdateRef("HEAD");
            ObjectId newId2 = ObjectId.FromString("7f822839a2fe9760f386cbbbcb3f92c5fe81def7");
            lockRef2.NewObjectId = newId2;
            Assert.AreEqual(RefUpdate.RefUpdateResult.FORCED, lockRef2.forceUpdate());

            Assert.IsTrue(new FileInfo(db.Directory.FullName + "/refs/heads/foobar").Exists);
            Assert.AreEqual(newId2, db.Resolve("refs/heads/foobar"));
        }

        [Test]
        public void test029_mapObject()
        {
            Assert.AreEqual((new byte[0].GetType()), db.MapObject(ObjectId.FromString("5b6e7c66c276e7610d4a73c70ec1a1f7c1003259"), null).GetType());
            Assert.AreEqual(typeof(global::GitSharp.Core.Commit), db.MapObject(ObjectId.FromString("540a36d136cf413e4b064c2b0e0a4db60f77feab"), null).GetType());
            Assert.AreEqual(typeof(global::GitSharp.Core.Tree), db.MapObject(ObjectId.FromString("aabf2ffaec9b497f0950352b3e582d73035c2035"), null).GetType());
            Assert.AreEqual(typeof(global::GitSharp.Core.Tag), db.MapObject(ObjectId.FromString("17768080a2318cd89bba4c8b87834401e2095703"), null).GetType());
        }

        [Test]
        [Ignore("Fails on both Windows and Mono. Mono issue is related to WinterDom.IO.FileMap internals relying on kernel32.dll calls (which is not that Mono compatible :) ). Windows issue has to be investigated.")]
        public void test029_mapObject_using_memory_mapped_file()
        {
            WindowCacheConfig c = new WindowCacheConfig();
            c.PackedGitLimit = (128 * WindowCacheConfig.Kb);
            c.PackedGitWindowSize = (8 * WindowCacheConfig.Kb);
            c.PackedGitMMAP = (true);
            c.DeltaBaseCacheLimit = (8 * WindowCacheConfig.Kb);
            WindowCache.reconfigure(c);

            var repo = createWorkRepository();

            Assert.AreEqual((new byte[0].GetType()), repo.MapObject(ObjectId.FromString("5b6e7c66c276e7610d4a73c70ec1a1f7c1003259"), null).GetType());
            Assert.AreEqual(typeof(global::GitSharp.Core.Commit), repo.MapObject(ObjectId.FromString("540a36d136cf413e4b064c2b0e0a4db60f77feab"), null).GetType());
            Assert.AreEqual(typeof(global::GitSharp.Core.Tree), repo.MapObject(ObjectId.FromString("aabf2ffaec9b497f0950352b3e582d73035c2035"), null).GetType());
            Assert.AreEqual(typeof(global::GitSharp.Core.Tag), repo.MapObject(ObjectId.FromString("17768080a2318cd89bba4c8b87834401e2095703"), null).GetType());
        }

        [Test]
        public void test30_stripWorkDir()
        {
            DirectoryInfo relCwd = new DirectoryInfo(".");
            DirectoryInfo absCwd = new DirectoryInfo(relCwd.FullName);
            FileInfo absBase = new FileInfo(Path.Combine(new FileInfo(Path.Combine(absCwd.FullName, "repo")).FullName, "workdir"));
            FileInfo relBase = new FileInfo(Path.Combine(new FileInfo(Path.Combine(relCwd.FullName, "repo")).FullName, "workdir"));
            Assert.AreEqual(absBase.FullName, relBase.FullName);

            FileInfo relBaseFile = new FileInfo(Path.Combine(new FileInfo(Path.Combine(relBase.FullName, "other")).FullName, "module.c"));
            FileInfo absBaseFile = new FileInfo(Path.Combine(new FileInfo(Path.Combine(absBase.FullName, "other")).FullName, "module.c"));
            Assert.AreEqual("other/module.c", global::GitSharp.Core.Repository.StripWorkDir(relBase, relBaseFile));
            Assert.AreEqual("other/module.c", global::GitSharp.Core.Repository.StripWorkDir(relBase, absBaseFile));
            Assert.AreEqual("other/module.c", global::GitSharp.Core.Repository.StripWorkDir(absBase, relBaseFile));
            Assert.AreEqual("other/module.c", global::GitSharp.Core.Repository.StripWorkDir(absBase, absBaseFile));

            FileInfo relNonFile = new FileInfo(Path.Combine(new FileInfo(Path.Combine(relCwd.FullName, "not-repo")).FullName, ".gitignore"));
            FileInfo absNonFile = new FileInfo(Path.Combine(new FileInfo(Path.Combine(absCwd.FullName, "not-repo")).FullName, ".gitignore"));
            Assert.AreEqual("", global::GitSharp.Core.Repository.StripWorkDir(relBase, relNonFile));
            Assert.AreEqual("", global::GitSharp.Core.Repository.StripWorkDir(absBase, absNonFile));

            Assert.AreEqual("", global::GitSharp.Core.Repository.StripWorkDir(db.WorkingDirectory, db.WorkingDirectory));

            FileInfo file = new FileInfo(Path.Combine(new FileInfo(Path.Combine(db.WorkingDirectory.FullName, "subdir")).FullName, "File.java"));
            Assert.AreEqual("subdir/File.java", global::GitSharp.Core.Repository.StripWorkDir(db.WorkingDirectory, file));
        }

        /*
	 * Kick the timestamp of a local file.
	 * <p>
	 * We shouldn't have to make these method calls. The cache is using file
	 * system timestamps, and on many systems unit tests run faster than the
	 * modification clock. Dumping the cache after we make an edit behind
	 * RefDirectory's back allows the tests to pass.
	 *
	 * @param name
	 *            the file in the repository to force a time change on.
	 */
        private void BUG_WorkAroundRacyGitIssues(string name) {
            FileInfo path = PathUtil.CombineFilePath(db.Directory, name);
            long old = path.lastModified();
            long set = 1250379778668L; // Sat Aug 15 20:12:58 GMT-03:30 2009
            path.LastWriteTime = (set.MillisToUtcDateTime());
            Assert.IsTrue(old != path.lastModified(), "time changed");
        }

        [Test]
        public void testTwoSuccessiveCommitsLinkedToHead()
        {
            var repo = createBareRepository();
            var workingDirectory = repo.WorkingDirectory;
            repo.Create();

            var objectWriter = new ObjectWriter(repo);

            FileInfo project1_a_txt = writeTrashFile(Path.Combine(workingDirectory.FullName, "Project-1/A.txt"), "A.txt - first version\n");
            FileInfo project1_b_txt = writeTrashFile(Path.Combine(workingDirectory.FullName, "Project-1/B.txt"), "B.txt - first version\n");

            var tree = new global::GitSharp.Core.Tree(repo);
            global::GitSharp.Core.Tree projectTree = tree.AddTree("Project-1");
            addFile(projectTree, project1_a_txt, objectWriter);
            projectTree.Id = (objectWriter.WriteTree(projectTree));
            addFile(projectTree, project1_b_txt, objectWriter);
            projectTree.Id = (objectWriter.WriteTree(projectTree));
            tree.Id = (objectWriter.WriteTree(tree));

            var commit = new global::GitSharp.Core.Commit(repo)
                             {
                                 Author = new PersonIdent(author, (0L), 60),
                                 Committer = new PersonIdent(committer, (0L), 60),
                                 Message = "Foo\n\nMessage",
                                 TreeEntry = tree
                             };
            commit.Save();
            var commitId = commit.CommitId;

            FileInfo project1_b_v2_txt = writeTrashFile(Path.Combine(workingDirectory.FullName, "Project-1/B.txt"), "B.txt - second version\n");

            tree = new global::GitSharp.Core.Tree(repo);
            projectTree = tree.AddTree("Project-1");
            addFile(projectTree, project1_a_txt, objectWriter);
            projectTree.Id = (objectWriter.WriteTree(projectTree));
            addFile(projectTree, project1_b_v2_txt, objectWriter);
            projectTree.Id = (objectWriter.WriteTree(projectTree));
            tree.Id = (objectWriter.WriteTree(tree));

            commit = new global::GitSharp.Core.Commit(repo)
                         {
                             Author = new PersonIdent(author, (0L), 60),
                             Committer = new PersonIdent(committer, (0L), 60),
                             Message = "Modified",
                             ParentIds = new[] { commitId },
                             TreeEntry = tree
                         };
            commit.Save();
            commitId = commit.CommitId;

            RefUpdate lck = repo.UpdateRef("refs/heads/master");
            Assert.IsNotNull(lck, "obtained lock");
            lck.NewObjectId = commitId;
            Assert.AreEqual(RefUpdate.RefUpdateResult.NEW, lck.forceUpdate());
        }

        private void addFile(global::GitSharp.Core.Tree t, FileInfo f, ObjectWriter objectWriter)
        {
            ObjectId id = objectWriter.WriteBlob(f);
            t.AddEntry(new FileTreeEntry(t, id, f.Name.getBytes("UTF-8"), false));
        }
    }
}


