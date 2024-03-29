﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler2.Compiler
{
    internal class ProgramNodeBuilder
    {
        protected ProgramNodeBuilder()
        {

        }

        private static ProgramNodeBuilder _instance;
        public static ProgramNodeBuilder Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ProgramNodeBuilder();
                }

                return _instance;
            }
        }

        #region Header of binary file
        byte[] _header = {   0x4D, 0x5A, 0x90, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 
                            0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB0, 0x00, 0x00, 0x00, 
                            0x0E, 0x1F, 0xBA, 0x0E, 0x00, 0xB4, 0x09, 0xCD, 0x21, 0xB8, 0x01, 0x4C, 0xCD, 0x21, 0x54, 0x68, 
                            0x69, 0x73, 0x20, 0x70, 0x72, 0x6F, 0x67, 0x72, 0x61, 0x6D, 0x20, 0x63, 0x61, 0x6E, 0x6E, 0x6F, 
                            0x74, 0x20, 0x62, 0x65, 0x20, 0x72, 0x75, 0x6E, 0x20, 0x69, 0x6E, 0x20, 0x44, 0x4F, 0x53, 0x20, 
                            0x6D, 0x6F, 0x64, 0x65, 0x2E, 0x0D, 0x0D, 0x0A, 0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x5D, 0x65, 0xFD, 0xC8, 0x19, 0x04, 0x93, 0x9B, 0x19, 0x04, 0x93, 0x9B, 0x19, 0x04, 0x93, 0x9B, 
                            0x97, 0x1B, 0x80, 0x9B, 0x11, 0x04, 0x93, 0x9B, 0xE5, 0x24, 0x81, 0x9B, 0x18, 0x04, 0x93, 0x9B, 
                            0x52, 0x69, 0x63, 0x68, 0x19, 0x04, 0x93, 0x9B, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x50, 0x45, 0x00, 0x00, 0x4C, 0x01, 0x03, 0x00, 0xD4, 0xDB, 0xA0, 0x50, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0xE0, 0x00, 0x0F, 0x01, 0x0B, 0x01, 0x05, 0x0C, 0x00, 0x02, 0x00, 0x00, 
                            0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 
                            0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 
                            0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x40, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x10, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x10, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x14, 0x20, 0x00, 0x00, 0x3C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x14, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2E, 0x74, 0x65, 0x78, 0x74, 0x00, 0x00, 0x00, 
                            0x9E, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x60, 
                            0x2E, 0x72, 0x64, 0x61, 0x74, 0x61, 0x00, 0x00, 0xA6, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 
                            0x00, 0x02, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x40, 0x2E, 0x64, 0x61, 0x74, 0x61, 0x00, 0x00, 0x00, 
                            0x1C, 0x00, 0x00, 0x00, 0x00, 0x30, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0xC0, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 
                        };
        #endregion

        //This part of code will be added to the and of the programm array
        //user32.wsprintfA, user32.MessageBoxA, kernel32.ExitProcess
        #region Definitions for used functions
        byte[] _functionDifenitions =         { 0x60, 0x50, 0x68, 0x00, 0x30, 0x40, 0x00, 0x68, 0x06, 0x30, 0x40, 0x00, 0xE8, 
                                               0x18, 0x00, 0x00, 0x00, 0x83, 0xC4, 0x0C, 0x6A, 0x00, 0x68, 0x0B, 0x30, 0x40, 0x00, 0x68, 0x06, 
                                               0x30, 0x40, 0x00, 0x6A, 0x00, 0xE8, 0x08, 0x00, 0x00, 0x00, 0x61, 0xC3, 0xFF, 0x25, 0x0C, 0x20, 
                                               0x40, 0x00, 0xFF, 0x25, 0x08, 0x20, 0x40, 0x00, 0xFF, 0x25, 0x00, 0x20, 0x40, 0x00 };
        #endregion

        //wsprintfA MessageBox user32.dll; ExitProcess kernel32.dll
        #region Defining tables that specified mapping for User32.dll and Kernerl32.dll
        byte[] _dllMapping = {  0x8A, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x70, 0x20, 0x00, 0x00, 0x64, 0x20, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x58, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x7E, 0x20, 0x00, 0x00, 0x08, 0x20, 0x00, 0x00, 0x50, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                        0x00, 0x00, 0x00, 0x00, 0x98, 0x20, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x8A, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x70, 0x20, 0x00, 0x00, 0x64, 0x20, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x7D, 0x02, 0x77, 0x73, 0x70, 0x72, 0x69, 0x6E, 0x74, 0x66, 0x41, 0x00, 
                                        0xB1, 0x01, 0x4D, 0x65, 0x73, 0x73, 0x61, 0x67, 0x65, 0x42, 0x6F, 0x78, 0x41, 0x00, 0x75, 0x73, 
                                        0x65, 0x72, 0x33, 0x32, 0x2E, 0x64, 0x6C, 0x6C, 0x00, 0x00, 0x9B, 0x00, 0x45, 0x78, 0x69, 0x74,
                                        0x50, 0x72, 0x6F, 0x63, 0x65, 0x73, 0x73, 0x00, 0x6B, 0x65, 0x72, 0x6E, 0x65, 0x6C, 0x33, 0x32,
                                        0x2E, 0x64, 0x6C, 0x6C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 
                                     };
        #endregion

        //Format for wsprintfA ("%.4lX") and title for MessageBoxA ("HexMessage")
        //I'll put this data to start of data array. It gives me posibility to putting absolute addresses to functionDifenitions array. Just for simplicity.
        #region Constant data for defined functions
        byte[] _staticData = { 0x25, 0x2E, 0x34, 0x6C, 0x58, 0x00, 0xBB, 0xBB, 0xBB, 0xBB, 0xBB, 0x48, 0x65, 0x78, 0x4D, 0x65, 
                        0x73, 0x73, 0x61, 0x67, 0x65, 0x00 };
        #endregion

        //Program parts:
        //header: 000 - 400
        //body + functionDefinotions: 400 - 600
        //dllMapping: 600 - 800
        //data: 800 - A00

        //Available registers: 
        //EAX
        //Available commands:
        //A1 16304000    MOV EAX,DWORD PTR DS:[403016]
        //B8 33221100    MOV EAX,112233
        //A3 16304000    MOV DWORD PTR DS:[403016],EAX

        //0305 16304000  ADD EAX,DWORD PTR DS:[403016]
        //05 E8030000    ADD EAX,3E8
        //0105 16304000  ADD DWORD PTR DS:[403016],EAX

        //2B05 16304000  SUB EAX,DWORD PTR DS:[403016]
        //2D E8030000    SUB EAX,3E8
        //2905 16304000  SUB DWORD PTR DS:[403016],EAX

        //90             NOP

        //E8 52000000    CALL HexMessage - 52000000 is offset in bytes to calling address (52 bytes)

        //Available functions:
        //ExitProcess 
        //HexMessage - custom function ( wsprintfA + MessageBoxA ) that shows content of EAX

        #region Global variables
        //programm array
        static byte[] _body = new byte[512];
        //data array
        byte[] _data = new byte[512];

        //user defined variables
        List<Variable> _variables = new List<Variable>();

        //function callings
        List<Function> _functions = new List<Function>();

        int _dataPointer = 0;
        int _programPointer = 0;

        #endregion

        public Stream Run(string[] nods)
        {
            Initializing();

            string[] variables = nods.Where(x => x.ToUpper().Contains(" DB ")).ToArray();

            DefineVariables(variables);

            GenerateDataSegment();

            string[] commands = nods.Where(x => !x.ToUpper().Contains(" DB ")).ToArray();

            GenerateProgramSegment(commands);

            byte[] result = _header.Concat(_body).Concat(_dllMapping).Concat(_data).ToArray();

            return new MemoryStream(result);
        }

        private void GenerateProgramSegment(string[] commands)
        {
            foreach (string command in commands)
            {
                List<string> lexemes = command.Trim().Split(Constants.LexemesDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();

                Command parsedCommand = (Command)Enum.Parse(typeof(Command), lexemes[0].ToUpper());
                lexemes.RemoveAt(0);

                string[] parameters = string.Join(string.Empty, lexemes)
                                                .Replace(" ", string.Empty)
                                                .Split(Constants.ParameterDelimiter);
                byte[] commandBytes;
                switch (parsedCommand)
                {
                    case Command.MOV:
                        commandBytes = GetMovCommandBytes(parameters);
                        break;
                    case Command.ADD:
                        commandBytes = GetAddCommandBytes(parameters);
                        break;
                    case Command.SUB:
                        commandBytes = GetSubCommandBytes(parameters);
                        break;
                    case Command.XOR:
                        commandBytes = GetXorCommandBytes(parameters);
                        break;
                    case Command.CALL:
                        {
                            Function function = _functions.Where(x => x.Name == parameters[0]).FirstOrDefault();

                            if (function != null)
                            {
                                function.Offcets.Add(_programPointer + 1);
                            }
                            else
                            {
                                function = new Function { Name = parameters[0] };
                                function.Offcets.Add(_programPointer + 1);
                                _functions.Add(function);
                            }

                            commandBytes = GetCallCommandBytes(parameters);
                        } break;
                    case Command.NOP:
                        commandBytes = GetNopCommandBytes();
                        break;
                    default:
                        throw new ApplicationException(string.Format("Unexpected command: {0}", parsedCommand));
                }

                Array.Copy(commandBytes, 0, _body, _programPointer, commandBytes.Length);
                _programPointer += commandBytes.Length;
            }

            Array.Copy(_functionDifenitions, 0, _body, _programPointer, _functionDifenitions.Length);

            FixFunctionAddresses();
        }

        private void FixFunctionAddresses()
        {
            Function exitProcess = _functions.Where(x => x.Name == "ExitProcess").FirstOrDefault();
            Function hexMessage = _functions.Where(x => x.Name == "HexMessage").FirstOrDefault();

            hexMessage.Address = Constants.ProgramSegmentStartpoint + _programPointer;
            exitProcess.Address = Constants.ProgramSegmentStartpoint + _programPointer + 0x35;

            foreach (Function function in _functions)
            {
                foreach (int offcet in function.Offcets)
                {
                    byte[] bytes = BitConverter.GetBytes(function.Address - (Constants.ProgramSegmentStartpoint + offcet + 4));
                    Array.Copy(bytes, 0, _body, offcet, 4);
                }
            }
        }

        #region Command code generators
        private byte[] GetNopCommandBytes()
        {
            return new byte[] { 0x90 };
        }

        private byte[] GetCallCommandBytes(string[] parameters)
        {
            //Dummy data to allocate memory for call command
            return new byte[] { 0xE8, 0x00, 0x00, 0x00, 0x00 };
        }

        private byte[] GetXorCommandBytes(string[] parameters)
        {
            if (parameters[0].ToUpper() == Constants.EAX)
            {
                return new byte[] { 0x33, 0xC0 };
            }

            throw new NotSupportedException("Command does not support.");
        }

        private byte[] GetSubCommandBytes(string[] parameters)
        {
            if (parameters[0].ToUpper() == Constants.EAX)
            {
                //sub eax, var1 or sub eax, 1000
                Variable var1 = _variables.Where(x => x.Name == parameters[1]).FirstOrDefault();
                int parsedValue;

                if (var1 != null)
                {
                    return (new byte[] { 0x2B, 0x05 }).Concat(var1.Address).ToArray();
                }
                else if (int.TryParse(parameters[1], out parsedValue))
                {
                    return (new byte[] { 0x2D }).Concat(BitConverter.GetBytes(parsedValue)).ToArray();
                }
            }
            else if (parameters[1].ToUpper() == Constants.EAX)
            {
                //sub var1, eax 
                Variable var1 = _variables.Where(x => x.Name == parameters[0]).FirstOrDefault();

                if (var1 != null)
                {
                    return (new byte[] { 0x29, 0x05 }).Concat(var1.Address).ToArray();
                }
            }

            throw new NotSupportedException();
        }

        private byte[] GetAddCommandBytes(string[] parameters)
        {
            if (parameters[0].ToUpper() == Constants.EAX)
            {
                //add eax, var1 or add eax, 1000
                Variable var1 = _variables.Where(x => x.Name == parameters[1]).FirstOrDefault();
                int parsedValue;

                if (var1 != null)
                {
                    return (new byte[] { 0x03, 0x05 }).Concat(var1.Address).ToArray();
                }
                else if (int.TryParse(parameters[1], out parsedValue))
                {
                    return (new byte[] { 0x05 }).Concat(BitConverter.GetBytes(parsedValue)).ToArray();
                }
            }
            else if (parameters[1].ToUpper() == Constants.EAX)
            {
                //add var1, eax 
                Variable var1 = _variables.Where(x => x.Name == parameters[0]).FirstOrDefault();

                if (var1 != null)
                {
                    return (new byte[] { 0x01, 0x05 }).Concat(var1.Address).ToArray();
                }
            }

            throw new NotSupportedException();
        }

        private byte[] GetMovCommandBytes(string[] parameters)
        {

            if (parameters[0].ToUpper() == Constants.EAX)
            {
                //mov eax, var1 or mov eax, 1000
                Variable var1 = _variables.Where(x => x.Name == parameters[1]).FirstOrDefault();
                int parsedValue;

                if (var1 != null)
                {
                    return (new byte[] { 0xA1 }).Concat(var1.Address).ToArray();
                }
                else if (int.TryParse(parameters[1], out parsedValue))
                {
                    return (new byte[] { 0xB8 }).Concat(BitConverter.GetBytes(parsedValue)).ToArray();
                }
            }
            else if (parameters[1].ToUpper() == Constants.EAX)
            {
                //mov var1, eax
                Variable var1 = _variables.Where(x => x.Name == parameters[0]).FirstOrDefault();

                if (var1 != null)
                {
                    return (new byte[] { 0xA3 }).Concat(var1.Address).ToArray();
                }
            }

            throw new NotSupportedException("Command does not support");
        }
        #endregion

        private void DefineVariables(string[] variables)
        {
            if (variables != null)
            {
                foreach (var variable in variables)
                {
                    List<string> lexemes = variable.Split(Constants.LexemesDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
                    int value = int.Parse(lexemes[2]);
                    _variables.Add(new Variable { Name = lexemes[0], Value = value });
                }
            }
        }

        private void GenerateDataSegment()
        {
            foreach (Variable variable in _variables)
            {
                List<byte> bytes = BitConverter.GetBytes(variable.Value).ToList();
                //bytes.Reverse();
                Array.Copy(bytes.ToArray(), 0, _data, _dataPointer, 4);

                variable.Address = GetAddressByOffcet(_dataPointer);
                _dataPointer += 4;
            }
        }

        private byte[] GetAddressByOffcet(int offcet)
        {
            int address = Constants.DataSegmentStartpoint + offcet;

            List<byte> addressParts = BitConverter.GetBytes(address).ToList();
            //addressParts.Reverse();

            return addressParts.ToArray();
        }

        private void Initializing()
        {
            Array.Copy(_staticData, 0, _data, 0, _staticData.Length);
            _dataPointer = _staticData.Length;
        }

        #region Nested classes
        class Variable
        {
            public string Name { get; set; }
            public int Value { get; set; }
            public byte[] Address { get; set; }
        }

        class Function
        {
            public Function()
            {
                Offcets = new List<int>();
            }

            public string Name { get; set; }
            public List<int> Offcets { get; set; }
            public int Address { get; set; }
        }
        #endregion
    }
}
