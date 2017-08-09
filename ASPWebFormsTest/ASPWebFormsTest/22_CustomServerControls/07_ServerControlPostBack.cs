using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPWebFormsTest._22_CustomServerControls
{
    public class _07_ServerControlPostBack : Panel, IPostBackEventHandler
    {
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            #region Метод GetPostBackEventReference
            /*
            Добавления обработчика на событие click;
            Page.ClientScript.GetPostBackEventReference(this, String.Empty)
            
            Создает на странице JavaScript функцию __doPostBack() для Postback запроса 
            (если такого скрипта там нет).
            Также, этот метод, устанавливает в качестве обработчика нажатия по кнопке 
            вызов метода _doPostBack с передачей в него своего ID и параметров для события 
            (первый и второй аргумент метода GetPostBackEventReference соответственно)

            код добавляемый на страницу:

                <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
                <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />

                var theForm = document.forms['form1'];
                if (!theForm) {
                    theForm = document.form1;
                }
                function __doPostBack(eventTarget, eventArgument) {
                    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                        theForm.__EVENTTARGET.value = eventTarget;
                        theForm.__EVENTARGUMENT.value = eventArgument;
                        theForm.submit();
                    }
                } 
            */
            #endregion

            string javaScript = Page.ClientScript.GetPostBackEventReference(this, "");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, javaScript);

            base.AddAttributesToRender(writer);
        }

        public event EventHandler Click;

        public void OnClick()
        {
            if (Click != null)
            {
                Click.Invoke(this, EventArgs.Empty);
            }
        }

        // Этот метод будет вызван при получении PostBack запроса.
        public void RaisePostBackEvent(string eventArgument)
        {
            OnClick();
        }
    }
}
