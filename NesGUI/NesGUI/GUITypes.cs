using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;
namespace NesGUI
{
    public enum GUIType
    {
        Rect,
        Checkbox,
        Button,
        Textfield,
        Label,
        Line
    }


    public class GUIItem
    {
        public GUIItem(GUIType type, string label, string name, GUIItem parent)
        {
            this.parent = parent;
            guiType = type;
            this.label = label;
            this.name = name;
            this.rect = new Rect(parent.Pos, parent.Size);
        }

        public GUIItem parent;

        public void UpdateRectChildren()
        {
            foreach(GUIItem i in GuiMaker.items.Where(x => x.parent == this))
            {
                i.rect = new Rect(this.Pos, this.Size);
            }
        }

        public GUIItem(GUIType type, string name, Vector2 size, Vector2 position)
        {
            guiType = type;
            this.name = name;
            this.size = size;
            pos = position;
        }


        Rect rect;
        public Rect GetRect
        {
            get
            {
                return rect;
            }
        }
        public Vector2 size;
        public Vector2 pos;
        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }

        public Vector2 Pos
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }

        

        public string label;
        public string name;
        GUIType guiType;

        public GUIType GuiType
        {
            get
            {
                return guiType;
            }
        }
    }
}
