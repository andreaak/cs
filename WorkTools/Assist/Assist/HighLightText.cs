using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE80;
using EnvDTE;

namespace Assist
{
    public class HighLightText
    {
        private MarkerManager markerManager;
        public Assist.MarkerManager MarkerManager_
        {
            get { return markerManager; }
        }
        private char separator = ';';
        private int subRowCount = 2;
        private string guidR;
        private string guidL;


        public HighLightText(/*int subRowCount, string guidR, string guidL, */HighLightText highLightText)
        {
            this.subRowCount = Options.GetInstance.SubRowCount;
            if (highLightText != null)
            {
                markerManager = highLightText.MarkerManager_;
            }
            this.guidR = Options.GetInstance.GuidR;
            this.guidL = Options.GetInstance.GuidL;
        }
        
        public void HandledSelectedText(DTE2 applicationObject, bool matchCase, bool wholeWord, RTFTextControl rtfTextControl)
        {
            Window activeWindow = applicationObject.ActiveWindow;
            TextDocument currDoc = (TextDocument)applicationObject.ActiveDocument.Object("");
            //
            string currSelection = currDoc.Selection.Text;
            if (string.IsNullOrEmpty(currSelection))
            {
                HighlightTextInIDE(applicationObject, activeWindow, null);

                Logger.WriteLine(string.Format("Nothing selected"));
                return;
            }
            List<SelectedLine> values = DTEUtils.GetSelectedLineOccurrences(currDoc, currSelection, matchCase, wholeWord, subRowCount);
            if (Options.GetInstance.SetTextInControl)
            {
                rtfTextControl.SetTextInControl(values, matchCase, wholeWord);
            }
            if (Options.GetInstance.HighlightText)
            {
                HighlightTextInIDE(applicationObject, activeWindow, values);
            }
        }

        private void HighlightTextInIDE(DTE2 applicationObject, Window activeWindow, List<SelectedLine> values)
        {
            if (markerManager != null)
            {
                markerManager.DeleteMarkers();
            }

            EnvDTE.Project project = DTEUtils.GetProject(applicationObject);
            if (project == null)
            {
                return;
            }
            IServiceProvider serviceProvider = new Microsoft.VisualStudio.Shell.ServiceProvider(project.DTE as Microsoft.VisualStudio.OLE.Interop.IServiceProvider);
            markerManager = new MarkerManager(project.DTE, serviceProvider);

            markerManager.DrawMarkers(activeWindow, values, separator, guidR, guidL);
        }
    }
}
