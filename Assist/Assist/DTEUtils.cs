using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE80;
using EnvDTE;
using Algoritms;

namespace Assist
{
    class DTEUtils
    {
#region PROJECT_METHODS
        public static void GoToLine(DTE2 applicationObject, int lineNumber)
        {
            TextDocument currDoc = applicationObject.ActiveDocument.Object("") as TextDocument;
            currDoc.Selection.GotoLine(lineNumber, true);
        }

        public static Project GetProject(DTE2 applicationObject)
        {
            Project containingProject = applicationObject.SelectedItems.Item(1).Project;
            if (containingProject == null)
            {
                ProjectItem projectItem = applicationObject.SelectedItems.Item(1).ProjectItem;
                if (projectItem != null)
                {
                    containingProject = projectItem.ContainingProject;
                }
            }
            return containingProject;
        }

        public static ProjectItem GetCurrentProjectItem(DTE2 applicationObject)
        {
            ProjectItem file = applicationObject.ActiveWindow.ProjectItem;
            if (file == null)
            {
                return null;
            }

            return file;
        }

        public static string GetLine(TextDocument currDoc, int lineNumber)
        {
            Logger.WriteLine(string.Format("Method: GetLine {0}", lineNumber));
            TextPoint tp;
            if (lineNumber == 0)
            {
                tp = (TextPoint)currDoc.EndPoint;
            }
            else
            {
                tp = (TextPoint)currDoc.StartPoint;
            }

            EditPoint2 lineStart = (EditPoint2)tp.CreateEditPoint();
            Logger.WriteLine(string.Format("Get Line {0}", lineNumber));
            string temp = lineStart.GetLines(lineNumber, lineNumber + 1);
            Logger.WriteLine(string.Format("Choose line {0} with text {1} ", lineNumber, temp));
            return temp;
        }

        public static Dictionary<string, int> GetOpenDocuments(DTE2 applicationObject)
        {
            Dictionary<string, int> docsList = new Dictionary<string, int>();
            Documents docs = applicationObject.Documents;
            foreach (Document doc in docs)
            {
                TextDocument tdoc = doc.Object("") as TextDocument;
                int line = -1;
                try
                {
                    if (tdoc != null)
                    {
                        line = tdoc.Selection.ActivePoint.Line;
                    }
                }
                catch (System.Exception)
                {
                	
                }
                docsList.Add(doc.FullName, line);
            }
            return docsList;
        }        

        #endregion

        public static void RestoreDocuments(DTE2 applicationObject, Dictionary<string, int> docsList)
        {
            Documents docs = applicationObject.Documents;
            foreach (Document doc in docs)
            {
                if (!docsList.ContainsKey(doc.FullName))
                {
                    doc.Close(vsSaveChanges.vsSaveChangesNo);
                }
            }
        }

        public static Dictionary<string, int> GetBreakpoints(DTE2 applicationObject)
        {
            Dictionary<string, int> brsList = new Dictionary<string, int>();
            Breakpoints brs = applicationObject.Debugger.Breakpoints;
            foreach (Breakpoint br in brs)
            {
                brsList.Add(br.File, br.FileLine);
            }
            return brsList;
        }

        public static List<SelectedLine> GetSelectedLineOccurrences(TextDocument currDoc, string currSelection, bool matchCase, bool wholeWord, int subRowCount)
        {
            Logger.WriteLine(string.Format("Method: GetLinesAndOffsets"));
            if (string.IsNullOrEmpty(currSelection))
            {
                Logger.WriteLine(string.Format("Nothing selected"));
                return null;
            }

            TextPoint vp = (TextPoint)currDoc.StartPoint;
            EditPoint2 currentPoint = (EditPoint2)vp.CreateEditPoint();
            TextPoint endOfdocument = (TextPoint)currDoc.EndPoint;

            List<SelectedLine> values = new List<SelectedLine>();
            EditPoint endPt = (EditPoint)currDoc.StartPoint.CreateEditPoint();
            TextRanges tags = null;
            vsFindOptions opt = vsFindOptions.vsFindOptionsMatchInHiddenText;
            if (matchCase)
            {
                opt |= vsFindOptions.vsFindOptionsMatchCase;
            }
            if (wholeWord)
            {
                opt |= vsFindOptions.vsFindOptionsMatchWholeWord;
            }
            while (currentPoint.FindPattern(currSelection, (int)opt, ref endPt, ref tags)
                )
            {
                int findInLine = currentPoint.Line;
                int findOffset = currentPoint.LineCharOffset;
                Logger.WriteLine(string.Format("Find {0} in line: {1} offset: ", currSelection, findInLine, findOffset));
                SelectedLine temp = new SelectedLine(currSelection, findInLine, findOffset);

                int i = findInLine > subRowCount ? findInLine - subRowCount : 1;
                int j = findInLine + subRowCount > endOfdocument.Line ? endOfdocument.Line : findInLine + subRowCount;

                for (; i < findInLine; i++)
                {
                    temp.AddStringBefore(DTEUtils.GetLine(currDoc, i));
                }
                temp.AddSelection(DTEUtils.GetLine(currDoc, i++));
                for (; i <= j; i++)
                {
                    temp.AddStringAfter(DTEUtils.GetLine(currDoc, i));
                }

                values.Add(temp);

                currentPoint.EndOfLine();
            }

            if (values.Count == 0)
            {
                Logger.WriteLine(string.Format("Not found"));
                return null;
            }
            return values;
        }

        public static bool IsBranch(string str)
        {
            string[] findStr = new string[]{"if("};
            return IsStartWith(str, findStr);
        }

        public static bool IsSwitch(string str)
        {
            string[] findStr = new string[] { "switch(" };
            return IsStartWith(str, findStr);
        }

        private static bool IsCase(string str)
        {
            string[] findStr = new string[] { "case" };
            return IsStartWith(str, findStr);
        }

        private static bool IsElse(string str)
        {
            string[] findStr = new string[] { "else", "}else" };
            return IsStartWith(str, findStr);
        }

        private static bool IsElseIf(string str)
        {
            string[] findStr = new string[] { "elseif(", "}elseif(" };
            return IsStartWith(str, findStr);
        }

        public static bool IsCycle(string str)
        {
            string[] findStr = new string[] { "for(", "foreach(", "while(" };
            return IsStartWith(str, findStr);
        }

        private static bool IsStartWith(string str, string[] findStr)
        {
            char[] del = new char[] { '\t', '\n', ' ' };
            string temp = str.Trim(del).Replace(" ", "");

            foreach (string occ in findStr)
            {
                if (temp.StartsWith(occ, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }                
            }
            return false;
        }

        public static BranchItem ParseIf(TextDocument currDoc, int startLineNumber, int endLineNumber)
        {

            List<BlockData> branchesBlocks = new List<BlockData>();
            
            Dictionary<int, int> lineNumbers = new Dictionary<int, int>();
            lineNumbers.Add(startLineNumber, -1);
            List<string> data = GetCodeBlockLines(currDoc, lineNumbers, "(", ")", endLineNumber);
            branchesBlocks.Add(new BlockData(lineNumbers, data));


            lineNumbers = new Dictionary<int, int>();
            lineNumbers.Add(startLineNumber, -1);
            data = GetCodeBlockLines(currDoc, lineNumbers, "{", "}", endLineNumber);
            branchesBlocks.Add(new BlockData(lineNumbers, data));


            int elseIfLine = lineNumbers[0];
            bool end = false;
            while (!end)
            {
                string temp1 = GetLine(currDoc, elseIfLine);
                string temp2 = GetLine(currDoc, elseIfLine + 1);
                if (!IsElseIf(temp1))
                {
                    if (!IsElseIf(temp2))
                    {
                        break;
                    }
                    ++elseIfLine;
                }

                lineNumbers = new Dictionary<int, int>();
                lineNumbers.Add(elseIfLine, -1);
                data = GetCodeBlockLines(currDoc, lineNumbers, "{", "}", endLineNumber);
                branchesBlocks.Add(new BlockData(lineNumbers, data, true));
                
                elseIfLine = lineNumbers[0];
            }

            string temp3 = GetLine(currDoc, elseIfLine);
            string temp4 = GetLine(currDoc, elseIfLine + 1);

            if (!IsElse(temp3))
            {
                if (!IsElse(temp4))
                {
                    elseIfLine = -1;
                }
                ++elseIfLine;
            }

            if (elseIfLine != -1)
            {
                lineNumbers = new Dictionary<int, int>();
                lineNumbers.Add(elseIfLine, -1);
                data = GetCodeBlockLines(currDoc, lineNumbers, "{", "}", endLineNumber);
                branchesBlocks.Add(new BlockData(lineNumbers, data));
            }

            
            return new BranchItem();
        }

        //private static bool IsElseLine(TextDocument currDoc, int startLineNumber)
        //{
        //    char[] del = new char[]{'\t','\n',' '};
        //    string temp = str.Trim(del).Replace(" ", "");
            
        //    if(temp.StartsWith("if(", StringComparison.OrdinalIgnoreCase)
        //        || temp.StartsWith("case(", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return true;
        //    }
        //    return false;;
        //}

        private static List<string> GetCodeBlockLines(TextDocument currDoc, Dictionary<int, int> lineNumbers, string borderLeft, string borderRight, int endLineNumber)
        {
            char[] del = new char[] { '\t', '\n', ' ' };

            int lineNumber = -1;
            foreach(int ln in lineNumbers.Keys)
			{
			    lineNumber = ln;
                break;
			}

            bool end = false;
            int bracketCount = 0;
            bool startBrecketFound = false;
            List<string> lineList = new List<string>();
            int firstIndex;
            int tempIndex;
            int lastIndex;

            while (!end)
            {
                string temp = GetLine(currDoc, lineNumber).Trim(del);
                firstIndex = tempIndex = -1;
                int left = GetNumberOfOccurenceInString(temp, borderLeft, ref firstIndex, ref tempIndex);
                lastIndex = tempIndex = -1;
                int right = GetNumberOfOccurenceInString(temp, borderRight, ref tempIndex, ref lastIndex);
                int diff = left - right;
                if (!startBrecketFound && left > 0)
                {
                    startBrecketFound = true;
                    lineNumbers.Clear();
                    lineNumbers.Add(lineNumber, -1);
                    temp = temp.Substring(firstIndex, temp.Length - firstIndex);
                }
                bracketCount += diff;
                
                if (startBrecketFound)
                {
                    if (bracketCount == 0)
                    {
                        temp = temp.Substring(0, lastIndex);
                        lineNumbers[0] = lineNumber;
                        end = true;
                    }

                    lineList.Add(temp);
                }
                lineNumber++;
                if (endLineNumber > -1 && lineNumber >= endLineNumber)
                {
                    lineNumbers[0] = -1;
                    string temp2 = lineList.Count > 0 ?lineList[0]:"";
                    lineList.Clear();
                    lineList.Add(temp);
                    end = true;
                }
            }

            return lineList;
        }

        private static int GetNumberOfOccurenceInString(string str, string findString, ref int firstIndex, ref int lastIndex)
        {
            int count = 0;
            int position = 0;
            while(position != -1)
            {
                position = str.IndexOf(findString, position);
                if (position == -1)
                {
                    break;
                }
                if(firstIndex < 0)
                {
                    firstIndex = position;
                }
                lastIndex = position;

                ++count;
                    
                if (position + 1 >= str.Length)
                {
                    break;
                }
                ++position;
            }
            return count;
        }



    }
}
