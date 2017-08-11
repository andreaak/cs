using System;
using System.Collections.Generic;

namespace ASPWebFormsTest._22_CustomServerControls.DataBinding
{
    public partial class _02_SimpleDataBoundControlTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<_02_TestDataSource> source = new List<_02_TestDataSource>();

                _02_TestDataSource item = new _02_TestDataSource();
                item.A = "Тестовый заголовок сообщения";
                item.B = "Текст сообщения. ABCDEFGHIJKLMNOPQRSTUVWXYZ. 1234567890";
                source.Add(item);

                MessageBoardControl1.DataTitleField = "A";
                MessageBoardControl1.DataMessageField = "B";
                MessageBoardControl1.DataSource = source;
                MessageBoardControl1.DataBind(); 
            }
        }
    }
}