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
        Texfield,
        Label,
        Line
    }


    public class GUIItem
    {
        public GUIItem(GUIType type, string label, string name, Rect rect)
        {
            guiType = type;
            this.label = label;
            this.name = name;
            this.rect = rect;
        }

        public GUIItem(GUIType type, string name, Vector2 size, Vector2 position)
        {
            guiType = type;
            this.name = name;
            this.size = size;
            pos = position;
        }


        Rect rect;
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
