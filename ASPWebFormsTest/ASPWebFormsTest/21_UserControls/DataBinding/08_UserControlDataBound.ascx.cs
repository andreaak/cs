using System.Web.UI;

namespace ASPWebFormsTest._21_UserControls.DataBinding
{
    public partial class _08_UserControlDataBound : System.Web.UI.UserControl
    {
        public object DataItem { get; set; }

        public string DataFirstNameField
        {
            set { ViewState["DataFirstNameField"] = value; }
            get
            {
                object data = ViewState["DataFirstNameField"];
                if (data != null)
                {
                    return data.ToString();
                }
                else return string.Empty;
            }
        }

        public string DataLastNameField
        {
            set { ViewState["DataLastNameField"] = value; }
            get
            {
                object data = ViewState["DataLastNameField"];
                if (data != null)
                {
                    return data.ToString();
                }
                else return string.Empty;
            }
        }

        public string DataMiddleNameField
        {
            set { ViewState["DataMiddleNameField"] = value; }
            get
            {
                object data = ViewState["DataMiddleNameField"];
                if (data != null)
                {
                    return data.ToString();
                }
                else return string.Empty;
            }
        }

        public void DataBind()
        {
            FirstNameLabel.Text = DataBinder.GetPropertyValue(DataItem, DataFirstNameField).ToString();
            LastNameLabel.Text = DataBinder.GetPropertyValue(DataItem, DataLastNameField).ToString();
            MiddleNameLabel.Text = DataBinder.GetPropertyValue(DataItem, DataMiddleNameField).ToString();
        }
    }
}