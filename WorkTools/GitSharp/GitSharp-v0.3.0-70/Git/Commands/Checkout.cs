﻿/*
 * Copyright (C) 2008, Shawn O. Pearce <spearce@spearce.org>
 * Copyright (C) 2009, Rolenun <rolenun@gmail.com>
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
using System.Text;
using NDesk.Options;
using GitSharp.Commands;

namespace GitSharp.CLI
{
    [Command(complete = false, common = true, requiresRepository = true, usage = "Checkout a branch or paths to the working tree")]
    public class Checkout : TextBuiltin
    {
        private CheckoutCommand cmd = new CheckoutCommand();

        private static Boolean isHelp = false;

#if ported
        private static Boolean isQuiet = false;
        private static Boolean isForced = false;
        private static Boolean isTracked = false;
        private static Boolean isNoTrack = false;
        private static Boolean isMerging = false;
        private static Boolean isOurs = false;
        private static Boolean isTheirs = false;
        private static Boolean isConflict = false;
        private static string branchName = "";
#endif

        override public void Run(string[] args)
        {
            options = new CmdParserOptionSet()
            {
                { "h|help", "Display this help information. To see online help, use: git help <command>", v=>OfflineHelp()},
                { "q|quiet", "Quiet, suppress feedback messages", v => cmd.Quiet = false },
#if ported
                { "f|force", "Force checkout and ignore unmerged changes", v=>{isForced = true;}},
                { "ours", "For unmerged paths, checkout stage #2 from the index", v=>{isOurs = true;}},
                { "theirs", "For unmerged paths, checkout stage #3 from the index", v=>{isTheirs = true;}},
                { "b|branch=", "Create a new {branch}",(string v) => branchName = v },
                { "t|track", "Set the upstream configuration", v=>{isTracked = true;}},
                { "no-track", "Do not set the upstream configuration", v=>{isNoTrack = true;}},
                { "l", "Create the new branch's reflog", v=>RefLog()},
                { "m|merge", "Perform a three-way merge between the current branch, your working tree contents " +
                    "and the new branch", v=>{isMerging = true;}},
                { "conflict","Same as merge above, but changes how the conflicting hunks are presented", isConflict = true},
                { "p|patch", "Creates a diff and applies it in reverse order to the working tree", v=>Patch()}

               // [Mr Happy] this should be compatible w/ the CommandStub, haven't checked yet tho.
               //{ "f|force", "When switching branches, proceed even if the index or the working tree differs from HEAD", v => cmd.Force = true },
               //{ "ours", "When checking out paths from the index, check out stage #2 ('ours') or #3 ('theirs') for unmerged paths", v => cmd.Ours = true },
               //{ "theirs", "When checking out paths from the index, check out stage #2 ('ours') or #3 ('theirs') for unmerged paths", v => cmd.Theirs = true },
               //{ "b=", "Create a new branch named <new_branch> and start it at <start_point>; see linkgit:git-branch[1] for details", v => cmd.B = v },
               //{ "t|track", "When creating a new branch, set up "upstream" configuration", v => cmd.Track = true },
               //{ "no-track", "Do not set up "upstream" configuration, even if the branch", v => cmd.NoTrack = true },
               //{ "l", "Create the new branch's reflog; see linkgit:git-branch[1] for details", v => cmd.L = true },
               //{ "m|merge", "When switching branches, if you have local modifications to one or more files that are different between the current branch and the branch to which you are switching, the command refuses to switch branches in order to preserve your modifications in context", v => cmd.Merge = true },
               //{ "conflict=", "The same as --merge option above, but changes the way the conflicting hunks are presented, overriding the merge", v => cmd.Conflict = v },
               //{ "p|patch", "Interactively select hunks in the difference between the <tree-ish> (or the index, if unspecified) and the working tree", v => cmd.Patch = true },
#endif
            };

            try
            {
                List<String> arguments = ParseOptions(args);
                if ((arguments.Count > 0) || (args.Length <=0))
                {
                    //Checkout the new repository
                    cmd.Arguments = arguments;
                    cmd.Execute();
                    
                    if (!cmd.Quiet)
                    {
                        //Display FileNotFound errors, but process checkout request for all found files first.
                        foreach (string file in cmd.Results.FileNotFoundList)
                            OutputStream.WriteLine("error: pathspec '" + file + "' did not match any file(s) known to GitSharp.");
                    }
                }
                else
                {
                    OfflineHelp();
                }
            } catch (OptionException e) {
                Console.WriteLine(e.Message);
            }
        }

        private void OfflineHelp()
        {
            if (!isHelp)
            {
                isHelp = true;
                Console.WriteLine("usage:");
                Console.WriteLine("       git checkout [-q] [-f] [-m] [<branch>]");
                Console.WriteLine("       git checkout [-q] [-f] [-m] [-b <new_branch>] [<start_point>]");
                Console.WriteLine("       git checkout [-f|--ours|--theirs|-m|--conflict=<style>] [<tree-ish>] [--] <paths>...");
                Console.WriteLine("       git checkout --patch [<tree-ish>] [--] [<paths>...]");
                Console.WriteLine("\nThe available options for this command are:\n");
                Console.WriteLine();
                options.WriteOptionDescriptions(Console.Out);
                Console.WriteLine();
            }
        }

        private void RefLog()
        {
        }

        private void Patch(String treeish)
        {
        }
    }
}
