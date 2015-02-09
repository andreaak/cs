using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE80;
using EnvDTE;
using System.Windows.Forms;
using Algoritms;

namespace Assist
{

    public class WorkWithProjectItem
    {
        DTE2 applicationObject = null;
        public WorkWithProjectItem(DTE2 applicationObject)
        {
            this.applicationObject = applicationObject;
        }
        
        #region ITEM_DESCRIPTION_METHODS
        
        public void SetItemDescriptionToClipboard()
        {
            
            string description = GetItemDescription();
            if (!string.IsNullOrEmpty(description))
            {
                Clipboard.SetText(description, TextDataFormat.UnicodeText);
            }
        }

        public string GetItemDescription()
        {
            Window activeWindow = applicationObject.ActiveWindow;
            if (activeWindow.ProjectItem == null)
            {
                return null;
            }

            string fileName = activeWindow.ProjectItem.get_FileNames(1);
            TextDocument currDoc = applicationObject.ActiveDocument.Object("") as TextDocument;
            ProjectItem prjItem = activeWindow.ProjectItem;
            FileCodeModel model = prjItem.FileCodeModel;
            string description = GetItemDescription(currDoc, model);
            return description;
        }

        private string GetItemDescription(TextDocument currDoc, FileCodeModel model)
        {
            BaseItem item = GetFunction(currDoc, model, null);
            if (item != null)
            {
                string currentLineStr = DTEUtils.GetLine(currDoc, item.CurrentLine);
                if (DTEUtils.IsBranch(currentLineStr))
                {
                    DTEUtils.ParseIf(currDoc, item.CurrentLine, item.EndLine);
                }
                return item.GetDescription();
            }
            item = GetProperty(currDoc, model, null);
            if (item != null)
            {
                return item.GetDescription();
            }
            CodeClass cls = GetClass(currDoc, model, null);
            if (cls != null)
            {
                return cls.FullName;
            }
            return null;
        }


        #endregion

        #region ITEM_METHODS
        public BaseItem GetItem(BaseItem parentItem)
        {
            Window activeWindow = applicationObject.ActiveWindow;
            if (activeWindow.ProjectItem == null)
            {
                return null;
            }
            string fileName = activeWindow.ProjectItem.get_FileNames(1);
            TextDocument currDoc = applicationObject.ActiveDocument.Object("") as TextDocument;
            ProjectItem prjItem = activeWindow.ProjectItem;
            FileCodeModel model = prjItem.FileCodeModel;

            return GetItem(currDoc, model, fileName, parentItem);
        }

        private BaseItem GetItem(TextDocument currDoc, FileCodeModel model, string fileName, BaseItem parentItem)
        {
            BaseItem item = GetFunction(currDoc, model, fileName);
            if (item != null)
            {
                return item;
                //generalItem = item;
                //if (parentItem != null && !parentItem.GetDescription().Equals(item.GetDescription()))
                //{
                //    return item;
                //}

            }
            item = GetProperty(currDoc, model, fileName);
            if (item != null)
            {
                item.Parent = parentItem;
                return item;
            }
            return null;
        }
        
        #endregion

        #region PROJECT_ITEM_METHODS

        private MethodItem GetFunction(TextDocument currDoc, FileCodeModel model, string fileName)
        {
            try
            {
                CodeElement element = model.CodeElementFromPoint(currDoc.Selection.ActivePoint, vsCMElement.vsCMElementFunction);
                CodeFunction functionItem = element as CodeFunction;
                CodeClass classItem = functionItem.Parent as CodeClass;
                List<Parameter> parameters = new List<Parameter>();
                foreach (CodeParameter param in functionItem.Parameters)
                {
                    parameters.Add(new Parameter(param.Name, param.Type.AsFullName));
                }
                string namespaceName = classItem.Namespace != null?classItem.Namespace.Name: "-";
                MethodItem func = new MethodItem(namespaceName, classItem.Name, functionItem.Name, parameters, false, BaseItem.langCollection[Algoritms.Language.CS]
                    , functionItem.StartPoint.Line, functionItem.EndPoint.Line, currDoc.Selection.ActivePoint.Line, fileName, null);
                return func;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        private PropertyItem GetProperty(TextDocument currDoc, FileCodeModel model, string fileName)
        {
            try
            {
                CodeElement element = model.CodeElementFromPoint(currDoc.Selection.ActivePoint, vsCMElement.vsCMElementProperty);
                CodeProperty propertyItem = element as CodeProperty;
                CodeClass classItem = propertyItem.Parent as CodeClass;
                string namespaceName = classItem.Namespace != null ? classItem.Namespace.Name : "-";
                PropertyItem prop = new PropertyItem(namespaceName, classItem.Name, propertyItem.Name, false, BaseItem.langCollection[Algoritms.Language.CS]
                    , propertyItem.StartPoint.Line, propertyItem.EndPoint.Line, currDoc.Selection.ActivePoint.Line, fileName, null);
                return prop;
            }
            catch (System.Exception)
            {
                return null;
            }
        }


        private CodeClass GetClass(TextDocument currDoc, FileCodeModel model, string fileName)
        {
            try
            {
                CodeElement element = model.CodeElementFromPoint(currDoc.Selection.ActivePoint, vsCMElement.vsCMElementClass);
                return element as CodeClass;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        
        #endregion
    }


}
