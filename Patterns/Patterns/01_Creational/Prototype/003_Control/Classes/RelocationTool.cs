﻿using System.Drawing;
using System.Windows.Forms;
using Patterns._01_Creational.Prototype._003_Control.Controls;
using Patterns._01_Creational.Prototype._003_Control.Prototype;

namespace Patterns._01_Creational.Prototype._003_Control.Classes
{
    /// <summary>
    /// Инструмент изменения значения ноты и передвижения вертикально
    /// </summary>
    public class RelocationTool : Tool
    {

        public RelocationTool(Panel panel)
            : base(panel)
        {
        }
        
        public override void Manipulate()
        {
            if (scoreSheetPanel == null) return;

            foreach (Control c in scoreSheetPanel.Controls)
            {
                {
                    var TranspPB = c as TranspControl;
                    if (TranspPB != null && TranspPB is NoteTranspControl)
                    {
                        var pb = TranspPB as NoteTranspControl;
                        if (pb.Marked)
                        {
                            if (pb.Location.Y <= pb.StaffBinded.Location.Y - 11)
                            {
                                pb.Location = new Point(pb.Location.X, pb.StaffBinded.Location.Y + 12);
                                pb.resetNoteValue();
                            }
                            else
                            {
                                pb.changeNoteValue();
                                if (NotesCollections.SharpNotes.Contains(pb.ControlNote.Value)) pb.changeNoteValue(); 
                                pb.Location = new Point(pb.Location.X, pb.Location.Y - 2);                                
                            }
                        }
                    }
                }
            }
        }
    }
}
