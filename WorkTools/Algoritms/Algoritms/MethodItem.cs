using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritms
{
    
    [Serializable]
    public class Parameter
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Parameter()
        {

        }

        public Parameter(string name, string type)
        {
            this.name = name;
            this.type = type;
        }
    }
    
    [Serializable]
    public class MethodItem : BaseItem
    {
        
        List<Parameter> parameters;
        public List<Parameter> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }
 
        public MethodItem()
        {
        }

        public MethodItem(string allData)
        {
            string[] nsc = ParseMethod(allData, ref name, ref this.parameters);
            ParseNamespaceAndClass(nsc, ref this.namespace_, ref this.class_);
        }

        public MethodItem(string namespaceString, string class_, string name, bool static_, string language, BaseItem parent)
            : base(namespaceString, class_, name, DataType.method, static_, language, 0, 0, 0, "", parent)
        {
        }

        public MethodItem(string namespaceString, string class_, string name, List<Parameter> parameters, bool static_, string language,
                        int startLine, int endLine, int currentLine, string fileName, BaseItem parent)
        : base(namespaceString, class_, name, DataType.method, static_, language, startLine, endLine, currentLine, fileName, parent)
        {
            this.parameters = parameters;
        }

        public MethodItem(string namespace_, string class_, string name, string parameters, bool static_, string language, BaseItem parent)
            : this(namespace_, class_, name, static_, language, parent)
        {
            this.parameters = ParseParameters(parameters);
        }

        public static MethodItem GetRootItem()
        {
            MethodItem root = new MethodItem();
            root.SetAsRoot();
            return root;
        }

        public override void SetParam(string param)
        {
            this.parameters = ParseParameters(param);
        }

        #region PARSERS

        private string[] ParseMethod(string allData, ref string methodName, ref List<Parameter> parameters)
        {
            if (string.IsNullOrEmpty(allData))
            {
                return null;
            }

            int methodParamsIndexStart = allData.IndexOf('(');
            string nscm = allData.Substring(0, methodParamsIndexStart);


            char[] spl = { '.' };
            string[] temp = nscm.Trim().Split(spl, StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length == 0)
            {
                return null;
            }
            methodName = temp[temp.Length - 1].Trim();

            int methodParamsIndexEnd = allData.LastIndexOf(')');
            string methodParams = allData.Substring(methodParamsIndexStart, methodParamsIndexEnd - methodParamsIndexStart + 1);
            parameters = ParseParameters(methodParams);

            string[] ret = new string[temp.Length - 1];
            for (int j = 0; j < ret.Length; j++)
            {
                ret[j] = temp[j];
            }
            return ret;
        }

        private List<Parameter> ParseParameters(string parameters)
        {
            List<Parameter> params_ = new List<Parameter>();
            if (parameters.Equals(string.Empty))
            {
                return params_;
            }

            string[] temp = parameters.Trim().TrimStart('(').TrimEnd(')').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string param in temp)
            {
                if (string.IsNullOrEmpty(param.Trim()))
                {
                    continue;
                }
                string[] paramSplit = param.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (paramSplit.Length > 1)
                {
                    params_.Add(new Parameter(paramSplit[1], paramSplit[0]));
                }
                else if (paramSplit.Length > 0)
                {
                    params_.Add(new Parameter("", paramSplit[0]));
                }
            }
            return params_;
        }

        #endregion

        public override string GetParameters()
        {
            char[] rem = {',', ' ' };
            if (parameters == null || parameters.Count == 0)
            {
                return string.Empty;
            }
            string temp = string.Empty;

            foreach (Parameter param in parameters)
            {

                temp += string.Format("{0} {1}, ", param.Type, param.Name);
            }
            return temp.TrimEnd(rem);
        }

        #region DESCRIPTION

        public override string GetDescription()
        {
            if (name.Equals(root))
            {
                return name;
            }

            string descripion = string.Format("{0}.{1}.{2}(", NamespaceStr, class_, name);
            for (int i = 0; i < parameters.Count; i++)
            {
                Parameter param = parameters[i];
                descripion += string.Format("{0}{1} {2}{3}",
                    (i == 0) ? "" : " ",
                    param.Type, param.Name,
                    (i == parameters.Count - 1) ? "" : ",");
            }

            descripion += ")";
            return descripion;
        }


        public string GetDiagrammDescription()
        {
            if (name.Equals(root))
            {
                return name;
            }

            string descripion = string.Format("{0}(", name);
            for (int i = 0; i < parameters.Count; i++)
            {
                descripion += "\n";
                Parameter param = parameters[i];
                descripion += string.Format("{0}{1} {2}{3}",
                    (i == 0) ? "" : " ",
                    param.Type, param.Name,
                    (i == parameters.Count - 1) ? "" : ",");
            }

            descripion += ")";
            return descripion;
        }

        #endregion

        public override int Width
        {
            get { return (int)(Settings_.Widthes[DataType.method] * ScaleFactor); }
        }

        public override int Height
        {
            get { return (int)(Settings_.Heights[DataType.method] * ScaleFactor); }
        }

        public override bool Drawable
        {
            get { return true; }
        }

        public override string ToString()
        {
            return GetDescription();
        }
    }
}
