using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NesGUI
{
    public class GuiMaker
    {
        public static List<GUIItem> rectangles = new List<GUIItem>();
        public static void MakeRect(Vector2 size, Vector2 pos, string name)
        {
            rectangles.Add(new GUIItem(GUIType.Rect, name, size, pos));
        }
    }
}
