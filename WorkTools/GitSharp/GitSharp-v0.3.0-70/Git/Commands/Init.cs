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
using System.Collections.Generic;
using System.IO;
using GitSharp.Commands;

namespace GitSharp.CLI
{

    [Command(common = true, complete = false, usage = "Create an empty git repository")]
    class Init : TextBuiltin
    {
        private InitCommand cmd = new InitCommand();

        private static Boolean isHelp = false;

        public override void Run(string[] args)
        {
            cmd.Quiet = false; // [henon] the api defines the commands quiet by default. thus we need to override with git's default here.
            
            options = new CmdParserOptionSet
            {
                {"bare", "Create a bare repository", v => cmd.Bare = true},
                {"quiet|q", "Only print error and warning messages, all other output will be suppressed.", v => cmd.Quiet = true},
                {"template", "Not supported.", var => OutputStream.WriteLine("--template=<template dir> is not supported")},
                {"shared", "Not supported.", var => OutputStream.WriteLine("--shared is not supported")},
            };

            try
            {
                List<String> arguments = ParseOptions(args);
                cmd.Execute();
				cmd.OutputStream.WriteLine(cmd.Repository.Directory);
            }
            catch (Exception e)
            {
                cmd.OutputStream.WriteLine(e.Message);
            }

        }

        private void OfflineHelp()
        {
            if (!isHelp)
            {
                isHelp = true;
                cmd.OutputStream.WriteLine("usage: git init [options] [directory]");
                cmd.OutputStream.WriteLine();
                options.WriteOptionDescriptions(Console.Out);
                cmd.OutputStream.WriteLine();
            }
        }
        //private void create()
        //{
        //    if (gitdir == null)
        //        gitdir = bare ? Environment.CurrentDirectory : Path.Combine(Environment.CurrentDirectory, Constants.DOT_GIT);
        //    db = new Repository(new DirectoryInfo(gitdir));
        //    db.Create(bare);
        //    Console.WriteLine("Initialized empty Git repository in " + (new DirectoryInfo(gitdir)).FullName);
        //}
    }

}