using System;
using EnvDTE;
using Microsoft.VisualStudio.TextManager.Interop;
using System.Collections.Generic;

namespace Assist
{
    public class MarkerManager : IVsTextMarkerClient
    {
        private _DTE dte;
        private System.IServiceProvider serviceProvider;
        private List<IVsTextLineMarker> currentMarkers;
        
        private IVsTextLines FindTextBuffer(String filePath)
        {
            if (String.IsNullOrEmpty(filePath))
                return null;
            Microsoft.VisualStudio.Shell.Interop.IVsRunningDocumentTable runningDocumentTable = serviceProvider.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.IVsRunningDocumentTable)) as Microsoft.VisualStudio.Shell.Interop.IVsRunningDocumentTable;
            if (runningDocumentTable == null)
                return null;
            Microsoft.VisualStudio.Shell.Interop.IVsHierarchy pHier;
            uint itemid;
            IntPtr punkDocData;
            uint dwCookie;
            int hRetVal = runningDocumentTable.FindAndLockDocument((uint)Microsoft.VisualStudio.Shell.Interop._VSRDTFLAGS.RDT_NoLock, filePath, out pHier, out itemid, out punkDocData, out dwCookie);
            if (hRetVal != 0)
            {
                return null;
            }
            IVsTextLines textBuffer = null;
            if (punkDocData != IntPtr.Zero)
            {
                object textBufferObject = System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(punkDocData);
                System.Runtime.InteropServices.Marshal.Release(punkDocData);
                textBuffer = textBufferObject as IVsTextLines;
                if (textBuffer == null)
                {
                    Microsoft.VisualStudio.Shell.Interop.IVsTextBufferProvider textBufferProvider = textBufferObject as Microsoft.VisualStudio.Shell.Interop.IVsTextBufferProvider;
                    if (textBufferProvider != null)
                    {
                        if (textBufferProvider.GetTextBuffer(out textBuffer) != 0)
                            textBuffer = null;
                    }
                }
            }
            return textBuffer;
        }

        private int GetMarkerType(Guid guidMarker)
        {
            int result = -1;
            IVsTextManager manager = serviceProvider.GetService(typeof(VsTextManagerClass)) as IVsTextManager;
            if (manager != null)
            {
                int hRetVal = manager.GetRegisteredMarkerTypeID(ref guidMarker, out result);
                if (hRetVal != 0)
                {
                    result = -1;
                }
            }
            return result;
        }

        public MarkerManager(_DTE dte, System.IServiceProvider serviceProvider)
        {
            this.dte = dte;
            this.serviceProvider = serviceProvider;
        }

        public void DeleteMarkers()
        {
            if (currentMarkers == null)
            {
                return;
            }
            foreach (IVsTextLineMarker currentMarker in currentMarkers)
            {
                if (currentMarker != null)
                {
                    try
                    {
                        currentMarker.UnadviseClient();
                        currentMarker.Invalidate();
                    }
                    catch { }
                }            
            }
            currentMarkers = null;
        }

        public void DrawMarker(Window window, Int32 startLine, Int32 startOffset, Int32 endLine, Int32 endOffset, string guid)
        {
            if (window == null || window.ProjectItem == null)
            {
                return;
            }
            string fileName = window.ProjectItem.get_FileNames(1);
            IVsTextLines textBuffer = FindTextBuffer(fileName);
            
            int markerType = GetMarkerType(new Guid(guid));
            
            if ((textBuffer != null) && (markerType != -1))
            {
                IVsTextLineMarker[] markers = new IVsTextLineMarker[1];
                textBuffer.CreateLineMarker(markerType, startLine, startOffset, endLine, endOffset, this, markers);
                currentMarkers.Add(markers[0]);
            }
        }

        public void DrawMarkers(Window activeWindow, List<SelectedLine> values, char separator, string guidR, string guidL)
        {
            if (values == null)
            {
                return;
            }
            currentMarkers = new List<IVsTextLineMarker>();
            foreach (SelectedLine value in values)
            {
                int findInLine = value._lineNumber - 1;
                int findOffset = value._lineOffset - 1;

                string guid = value._isRight ? guidR : guidL;
                DrawMarker(activeWindow, findInLine, findOffset, findInLine, findOffset + value._selection.Length, guid);
            }
        }

        #region IVsTextMarkerClient Members
        public int ExecMarkerCommand(IVsTextMarker pMarker, int iItem)
        {
            return 0;
        }
        public int GetMarkerCommandInfo(IVsTextMarker pMarker, int iItem, string[] pbstrText, uint[] pcmdf)
        {
            return 0;
        }
        public int GetTipText(IVsTextMarker pMarker, string[] pbstrText)
        {
            return 0;
        }
        public void MarkerInvalidated()
        {
        }
        public int OnAfterMarkerChange(IVsTextMarker pMarker)
        {
            return 0;
        }
        public void OnAfterSpanReload()
        {
        }
        public void OnBeforeBufferClose()
        {
        }
        public void OnBufferSave(string pszFileName)
        {
        }
        #endregion

    }
}
