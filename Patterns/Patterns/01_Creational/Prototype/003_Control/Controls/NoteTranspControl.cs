﻿using System.Drawing;
using System.Windows.Forms;
using Patterns.Properties;
using Patterns._01_Creational.Prototype._003_Control.Classes;
using Patterns._01_Creational.Prototype._003_Control.Prototype;

namespace Patterns._01_Creational.Prototype._003_Control.Controls
{
    public partial class NoteTranspControl : TranspControl
    {
        /// <summary>
        /// Нота которая закреплена за даным контролом
        /// </summary>
        public MusicalNote ControlNote { get; set; }

        /// <summary>
        /// Стан-контрол(область стана) к которому закреплен контрол ноты
        /// </summary>
        public StaffTranspControl StaffBinded {get; set;}

        public NoteTranspControl(MusicalNote connectNote)
            : base()
        {
            ControlNote = connectNote;
            RegisteredMusicalObjects.RegisterObject(this);
        }

        /// <summary>
        /// Поднять значение ноты на полтона
        /// </summary>
        public void changeNoteValue()
        {
            ControlNote.Value++;
        }

        /// <summary>
        /// Присвоить ноте значение по умолчанию (до)
        /// </summary>
        public void resetNoteValue()
        {
            ControlNote.Value = 59;
        }
        
        /// <summary>
        /// Отменить выделение ноты
        /// </summary>
        public void DeselectNote()
        {
            Marked = false;
            this.ResetNoteImage();
            this.RecreateHandle();
        }

        public void RefreshView()
        {
            this.Hide();
            this.Show();
        }

        /// <summary>
        /// Востановить исходное изображение ноты
        /// </summary>
        public void ResetNoteImage()
        {
            if (Marked)
            {
                switch (ControlNote.NoteType)
                {
                    case NoteLength.Sixteenth:
                        this.Image = RotatingImageTool(Resources.SixteenthNoteSelected);
                        break;
                    case NoteLength.Eighth:
                        this.Image = RotatingImageTool(Resources.EighthNoteSelected);
                        break;
                    case NoteLength.HalfNote:
                        this.Image = RotatingImageTool(Resources.HalfNoteSelected);
                        break;
                    case NoteLength.QuarterNote:
                        this.Image = RotatingImageTool(Resources.QuarterNoteSelected);
                        break;
                    case NoteLength.WholeNote:
                        this.Image = RotatingImageTool(Resources.WholeNoteSelected);
                        break;
                }
            }
            else
            {

                switch (ControlNote.NoteType)
                {
                    case NoteLength.Sixteenth:
                        this.Image = RotatingImageTool(Resources.SixteenthNoteSmall);
                        break;
                    case NoteLength.HalfNote:
                        this.Image = RotatingImageTool(Resources.HalfNoteSmall);
                        break;
                    case NoteLength.QuarterNote:
                        this.Image = RotatingImageTool(Resources.QuarterNoteSmall);
                        break;
                    case NoteLength.WholeNote:
                        this.Image = RotatingImageTool(Resources.WholeNoteSmall);
                        break;
                    case NoteLength.Eighth:
                        this.Image = RotatingImageTool(Resources.EighthNoteSmall);
                        break;
                }
            }
            this.RecreateHandle();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            Marked = !Marked;
            this.ResetNoteImage();
        }

        /// <summary>
        /// Повернуть изображение ноті на 180 градусов
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private Image RotatingImageTool(Image img)
        {
            return Rotated?Rotate.RotateImage(img, new Point(0, 0), 180):img;
        }
    }
}
