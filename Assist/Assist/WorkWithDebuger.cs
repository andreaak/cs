using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE80;
using EnvDTE;
using System.Windows.Forms;
using Algoritms;
using System.IO;
using Utils;
using System.Threading;

namespace Assist
{
    class WorkWithDebuger
    {

        List<string> stepOutDir;
        DTE2 applicationObject;
        Debugger dbr;
        BaseItem startItem;
        List<BaseItem> methods = null;
        Stack<BaseItem> stk = null;
        WorkWithProjectItem wi = null;
        bool saved;
        List<string> breakPointsList;

        public WorkWithDebuger(DTE2 applicationObject)
        {
            this.applicationObject = applicationObject;
            this.stepOutDir = Options.GetInstance.SkipInDebug;
            wi = new WorkWithProjectItem(this.applicationObject);
            dbr = this.applicationObject.Debugger;
        }

        public void GetAlgoritm()
        {
            Dictionary<string, int> docsList = DTEUtils.GetOpenDocuments(applicationObject);
            
            startItem = null;
            saved = false;
            methods = new List<BaseItem>();
            methods.Add(MethodItem.GetRootItem());
            try
            {
                startItem = SetStartItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (startItem == null)
            {
                return;
            }
            methods[0].AddItem(startItem);

            try
            {
                StartWork();
                if (!saved)
                {
                    saved = true;
                    SaveData(methods);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DTEUtils.RestoreDocuments(applicationObject, docsList);
            }

        }

        private BaseItem SetStartItem()
        {
            BaseItem item = wi.GetItem(null);
            if (item == null)
            {
                throw new Exception("Wrong start item");
            }

            return item;
        }

        private void StartWork()
        {
            try
            {
                //Visualization
                String headerText = "Работа отладчика";
                //String infoText = "Идет работа отладчика...";

                System.Threading.Thread workThread = new System.Threading.Thread(GetItems);
                CancelForm form = new CancelForm(headerText);
                ProcessEnded += form.CloseForm;
                if (!StartWorkThread(form, workThread))
                {
                    return;
                }
                if (!saved)
                {
                    saved = true;
                    SaveData(methods);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region VISUALIZATION
        
        public event Action ProcessEnded;

        private bool StartWorkThread(CancelForm form, System.Threading.Thread thread)
        {
            thread.IsBackground = true;
            thread.Name = "WorkThread";
            thread.Start();
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                AbortThread(thread);
                MessageBox.Show("USER_CANCEL", "USER_CANCEL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private static void AbortThread(System.Threading.Thread thread)
        {
            if (thread.IsAlive)
            {
                thread.Abort();
                thread.Join();
            }
        }

        private void OnProcessEnded()
        {
            if (ProcessEnded != null)
            {
                ProcessEnded();
            }
        }

        #endregion

        private void GetItems()
        {
            try
            {
                stk = new Stack<BaseItem>();
                stk.Push(startItem);
                Logger.WriteLine(string.Format("Push: {0}", startItem.GetDescription()));
                GetBreakPoints();
                bool circle = true;
                bool popPeek = false;
                while (circle)
                {
                    dbr.StepInto(true);
                    BaseItem parentItem = GetParentItem();
                    BaseItem currentitem = wi.GetItem(parentItem);
                    if (currentitem == null)
                    {
                        continue;
                    }
                    if (IsBreakInBreakPoint(currentitem))
                    {
                        break;
                    }

                    if (IsItemChanged(parentItem, currentitem))
                    {
                        CheckItem(currentitem, parentItem, popPeek);
                    }
                    else
                    {
                        Logger.WriteLine(stk.Count, string.Format("Item: {0} Line: {1}", currentitem.GetDescription(), currentitem.CurrentLine));
                        popPeek = IsPop(currentitem);
                        if (popPeek)
                        {
                            if (IsStartItem(currentitem))
                            {
                                circle = false;
                                Logger.WriteLine(stk.Count, string.Format("End: {0} Line: {1}", currentitem.GetDescription(), currentitem.CurrentLine));
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                OnProcessEnded();
            }
        }

        private BaseItem GetParentItem()
        {
            if (stk.Count > 0)
            {
                return stk.Peek();
            }
            else
            {
                return startItem;
            }
        }        
        
        private bool IsItemChanged(BaseItem parentItem, BaseItem currentitem)
        {
            string description = currentitem.GetDescription();
            if (!parentItem.GetDescription().Equals(description))
            {
                return true;
            }
            return false;
        }

        private void CheckItem(BaseItem item, BaseItem parentItem, bool popPeek)
        {
            string description = item.GetDescription();
            if ((parentItem is PropertyItem && IsEndProperty(parentItem, item))
                || popPeek)
            {
                Logger.WriteLine(stk.Count, string.Format("Pop: {0} Line: {1}", item.GetDescription(), item.CurrentLine));
                BaseItem temp = stk.Pop();
            }
            else
            {
                AddItem(parentItem, item);
            }

            if (IsStepOut(description))
            {
                dbr.StepOut(true);
                Logger.WriteLine(stk.Count, string.Format("Pop: {0} Line: {1}", item.GetDescription(), item.CurrentLine));
                BaseItem temp = stk.Pop();
            } 
        }

        private bool IsEndProperty(BaseItem propertyItem, BaseItem item)
        {
            bool popPeek = false;
            
            if (propertyItem.Parent.GetDescription().Equals(item.GetDescription()))
            {
                popPeek = true;
            }
            return popPeek;
        }

        private void AddItem(BaseItem parentItem, BaseItem item)
        {
            parentItem.AddItem(item);
            stk.Push(item);
            Logger.WriteLine(stk.Count, string.Format("Push: {0} Line: {1}", item.GetDescription(), item.CurrentLine));
        }

        private bool IsStepOut(string description)
        {
            foreach (string temp in stepOutDir)
            {
                if (description.Contains(temp))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsPop(BaseItem currentitem)
        {
            if (currentitem.IsEndOfItem)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsStartItem(BaseItem currentitem)
        {
            return currentitem.GetDescription().Equals(startItem.GetDescription());
        }

        #region BreakPoint

        private void GetBreakPoints()
        {
            breakPointsList = new List<string>();
            foreach (Breakpoint brk in applicationObject.Debugger.Breakpoints)
            {
                breakPointsList.Add(string.Format("File: {0} Line: {1}", brk.File, brk.FileLine));
            }
        }

        private bool IsBreakInBreakPoint(BaseItem currentitem)
        {
            if (CheckForBreakPoint(currentitem))
            {
                DialogResult res = MessageBox.Show("Do you want to continue? Y/Save&Yes/N", "Achtung", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (res == DialogResult.Cancel)
                {
                    return true;
                }
                else if (res == DialogResult.No)
                {
                    SaveData(methods);
                }
            }
            return false;
        }

        private bool CheckForBreakPoint(BaseItem currentitem)
        {
            string temp = string.Format("File: {0} Line: {1}", currentitem.FileName, currentitem.CurrentLine);
            if(breakPointsList.Contains(temp))
            {
                return true;
            }
            return false;
        }

        #endregion

        private void SaveData(List<BaseItem> methods)
        {
            if (methods == null)
            {
                return;
            }
            string error = null;
            Serialization.SaveDataToXML(null, methods, ref error);
        }

    }
}
