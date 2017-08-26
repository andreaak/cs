using System.Data;
using System.Windows.Forms;
using Utils.WorkWithDB;

namespace GrabDatabase.Grab
{
   
    public class DbStructureReader
    {
        private DBService dbService;
        
        private DbStructureDataset dbStructure;
        public DbStructureDataset DbStructure
        {
            get { return dbStructure; }
            private set { dbStructure = value; }
        }

        public DbStructureReader(DBService dbService)
        {
            this.dbService = dbService;
        }

        public string ReadData()
        {
            DataSet dataset = new DataSet();
            string[] restrictions = GetUserRestriction(dataset);

            string error = null;

            dbStructure = new DbStructureDataset();
            if (ReadItem(restrictions, dbService.GetMetaDataObject(DBObjects.Tables), dataset, ref  error)
                && ReadItem(restrictions, dbService.GetMetaDataObject(DBObjects.Columns), dataset, ref  error))
            {
                dbService.FillTables(dataset, dbStructure);
            }

            if (ReadItem(restrictions, dbService.GetMetaDataObject(DBObjects.Views), dataset, ref  error))
            {
                dbService.FillViews(dataset, dbStructure);
            }

            if (ReadItem(restrictions, dbService.GetMetaDataObject(DBObjects.Triggers), dataset, ref  error))
            {
                dbService.FillTriggers(dataset, dbStructure);
            }
            return error;
        }

        private string[] GetUserRestriction(DataSet dataset)
        {
            string[] restrictions = null;
            string schemeName = "Users";
            string xml = null;
            string response = UTILS.GetDbScheme(dbService, schemeName, restrictions, ref xml);
            DBUtils.GetDatasetFromXML(xml, dataset);
            if (string.IsNullOrEmpty(response))
            {
                SelectUser form_ = new SelectUser(dataset);
                if (form_.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(form_.User))
                {
                    restrictions = new string[] { form_.User };
                }
            }
            return restrictions;
        }

        private bool ReadItem(string[] restrictions, string schemeName, DataSet dataset, ref string error)
        {
            bool res = false;
            if (!string.IsNullOrEmpty(schemeName))
            { 
                string xml = null;
                string response = UTILS.GetDbScheme(dbService, schemeName, restrictions, ref xml);
                if (string.IsNullOrEmpty(response))
                {
                    DBUtils.GetDatasetFromXML(xml, dataset);
                    res = true;
                }
                else
                {
                    error += response;
                }
            }
            return res;
        }
    }



}
