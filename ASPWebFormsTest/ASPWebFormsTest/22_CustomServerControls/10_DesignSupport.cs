using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace ASPWebFormsTest._22_CustomServerControls
{
    // Обработчик на данное событие будет создан при двойном клике.
    [DefaultEvent("Click")]
    // Данное свойство будет выделено при открытии окна Properties
    [DefaultProperty("Text")]
    // Разметка, которая добавиться при переносе элемента из Tollbox
    [ToolboxData("<{0}:TestControl runat=\"server\" Text=\"Hello World!\"></{0}:TestControl>")]
    public class _10_DesignSupport : Control
    {
        #region Свойства

        /*Category - определяет категорию, к которой будет относиться данное свойство 
        при редактировании элемента управления в окне PropertyWindow*/
        [Category("Parameters")]
        /*Description - описание, которое будет отображаться в нижней части окна PropertyWindow 
        если выделить данное свойство курсором */
        [Description("Control Width")]
        public int Width
        {
            get
            {
                object data = ViewState["Width"];
                if (data == null)
                {
                    return 100;
                }
                return (int)data;
            }
            set { ViewState["Width"] = value; }
        }

        [Category("Parameters")]
        [Description("Высота элемента управления")]
        public int Height
        {
            get
            {
                object data = ViewState["Height"];
                if (data == null)
                {
                    return 50;
                }
                return (int)data;
            }
            set { ViewState["Height"] = value; }
        }

        [Category("Color Tunning")]
        [Description("Back Color")]
        public Color BackColor
        {
            get
            {
                object data = ViewState["BackColor"];
                if (data == null)
                {
                    return Color.White;
                }
                return (Color)data;
            }
            set { ViewState["BackColor"] = value; }
        }

        [Category("Color Tunning")]
        [Description("Fore Color")]
        public Color ForeColor
        {
            get
            {
                object data = ViewState["ForeColor"];
                if (data == null)
                {
                    return Color.Black;
                }
                return (Color)data;
            }
            set { ViewState["ForeColor"] = value; }
        }

        [Category("Color Tunning")]
        [Description("Border Color")]
        public Color BorderColor
        {
            get
            {
                object data = ViewState["BorderColor"];
                if (data == null)
                {
                    return Color.Black;
                }
                return (Color)data;
            }
            set { ViewState["BorderColor"] = value; }
        }

        /*DefaultValue - данный атрибут не задает значения свойству. 
        Атрибут нужен для того, что бы Visual Studio сравнивала значение, 
        которое было установлено в окне Properies со значением атрибута 
        и если они отличаться, выводила значение в окне
        полужирным шрифтом, указывая, что оно теперь не равно значению по умолчанию.*/
        [DefaultValue("Empty")]
        // Themeable - свойство не может использоваться в темах и скин файлах
        [Themeable(false)]
        // Bindable - определяет может ли свойство использоваться для привязки данных 
        [Bindable(false)]
        public string Text
        {
            get
            {
                object data = ViewState["text"];
                if (data == null)
                {
                    return "Empty";
                }
                return data.ToString();
            }
            set { ViewState["text"] = value; }
        }

        // Browsable - свойство не будет отображаться в окне Property Window
        [Browsable(false)]
        public int Foo { get; set; }

        #endregion

        #region Событие
        public void RaisePostBackEvent(string eventArgument)
        {
            OnClick(new EventArgs());
        }

        private EventHandler _event = null;
        public event EventHandler Click
        {
            add { _event += value; }
            remove { _event -= value; }
        }

        protected virtual void OnClick(EventArgs args)
        {
            EventHandler handler = _event;
            if (handler != null)
            {
                handler.Invoke(this, args);
            }
        }
        #endregion


        protected override void Render(HtmlTextWriter writer)
        {
            // установка значений для атрибута style
            writer.AddStyleAttribute("padding", "20px");
            writer.AddStyleAttribute("width", this.Width + "px");
            writer.AddStyleAttribute("height", this.Height + "px");
            writer.AddStyleAttribute("color", this.ForeColor.Name);
            writer.AddStyleAttribute("background-color", this.BackColor.Name);
            writer.AddStyleAttribute("border", "1px solid " + this.BorderColor.Name);

            /*добавление атрибута onclick c вызовом функции __doPostBack. 
            Если функции нет на странице она создается.*/
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, Page.ClientScript.GetPostBackEventReference(this, string.Empty));

            // создание элемента div с применением значений style заданных выше
            writer.RenderBeginTag("div");

            base.Render(writer);

            writer.Write(Text);

            // создание закрывающего элемента div
            writer.RenderEndTag();
        }

    }
}
