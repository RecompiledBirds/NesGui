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
        public static List<GUIItem> items = new List<GUIItem>();
        public static List<GUIItem> buttons = new List<GUIItem>();
        public static List<GUIItem> rectangles = new List<GUIItem>();
        public static List<GUIItem> labels = new List<GUIItem>();
        public static List<GUIItem> checkboxes = new List<GUIItem>();
        public static List<GUIItem> lines = new List<GUIItem>();
        public static List<GUIItem> textfields = new List<GUIItem>();

        public static Dictionary<GUIItem, bool> enabledRects = new Dictionary<GUIItem, bool>();
        public static void MakeRect(Vector2 size, Vector2 pos, string name)
        {
            GUIItem GI = new GUIItem(GUIType.Rect, name, size, pos);
            items.Add(GI);
            rectangles.Add(GI);
        }

        public static void MakeButton(GUIItem rect, string label)
        {
            GUIItem GI = new GUIItem(GUIType.Button, label, label,rect);
            items.Add(GI);
            buttons.Add(GI);
        }
        public static void MakeTextField(GUIItem rect, string label)
        {
            GUIItem GI = new GUIItem(GUIType.Textfield, label, label, rect);
            items.Add(GI);
            textfields.Add(GI);
        }


        public static void MakeLabel(GUIItem rect, string label)
        {
            GUIItem GI = new GUIItem(GUIType.Label, label, label, rect);
            items.Add(GI);
            labels.Add(GI);
        }
        public static void MakeLine(Vector2 posOne, Vector2 posTwo, string label)
        {
            GUIItem GI = new GUIItem(posOne, posTwo, label, Color.white);
            lines.Add(GI);
            items.Add(GI);
        }
        public static void MakeCheckBox(GUIItem rect, string label)
        {
            GUIItem GI = new GUIItem(GUIType.Checkbox, label, label, rect);
            checkboxes.Add(GI);
            items.Add(GI);
        }

    }
}
