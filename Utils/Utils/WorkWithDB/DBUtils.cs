using System;
using System.Text;
using System.IO;
using System.Data;

namespace Utils.WorkWithDB
{
    public class DBUtils
    {
        public static string GetStringFromDataset(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            ds.WriteXml(new StringWriter(sb), XmlWriteMode.WriteSchema);
            return sb.ToString();
        }

        public static string GetStringFromDataTable(DataTable ds)
        {
            StringBuilder sb = new StringBuilder();
            ds.WriteXml(new StringWriter(sb), XmlWriteMode.WriteSchema);
            return sb.ToString();
        }

        public static bool GetDatasetFromXML(string nxml, DataSet storage)
        {
            if (nxml == null)
            {
                return false;
            }
            StringReader sr = new StringReader(nxml);
            try
            {
                storage.ReadXml(sr, XmlReadMode.ReadSchema);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool GetDatasetFromXML(string nxml, string scheme, DataSet storage)
        {
            if (string.IsNullOrEmpty(nxml) || string.IsNullOrEmpty(scheme) || !nxml.Contains("NewDataSet"))
            {
                return false;
            }

            storage.Namespace = "";
            StringReader sr = new StringReader(nxml);

            try
            {
                storage.ReadXml(sr, XmlReadMode.Auto);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool GetDataTableFromXML(string tableScheme, string tableData, ref DataTable schemeTable, ref DataTable dataTable)
        {
            if (string.IsNullOrEmpty(tableScheme))
            {
                return false;
            }

            DataSet temp = null;
            try
            {
                schemeTable.Namespace = "";
                temp = new DataSet();
                StringReader srst = new StringReader(tableScheme);
                temp.ReadXml(srst, XmlReadMode.Auto);
                schemeTable = temp.Tables[0];
            }
            catch (Exception)
            {
                return false;
            }

            if (string.IsNullOrEmpty(tableData))
            {
                return true;
            }

            try
            {
                dataTable.Namespace = "";
                temp = new DataSet();
                StringReader sr = new StringReader(tableData);
                temp.ReadXml(sr, XmlReadMode.Auto);
                dataTable = temp.Tables[0];
            }
            catch (Exception)
            {
                dataTable = null;
            }
            return true;
        }
    }
}
