using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace NesGUI
{
    public static class NesGUI_OutputGen
    {
        static StringBuilder program = new StringBuilder();
        public static void ReadRects()
        {
            int rects = 0;
            foreach(GUIRect item in GuiMaker.Rectangles)
            {
                string varName = item.name;
                varName = new string(varName.ToCharArray().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
                
                program.AppendLine($"Rect {varName} = new Rect(new Vector2({item.pos.x}f,{item.pos.y}f),new Vector2({item.size.x}f,{item.size.y}f));");
                rects++;
            }
            Log.Message($"Read {rects} rects.");
        }

        public static void ReadButtons()
        {
            int buttons = 0;
            foreach (GUIItem button in GuiMaker.Buttons)
            {
                string rectName = button.parent.name;
                rectName= new string(rectName.ToCharArray().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
                string varName = button.name;
                varName = new string(varName.ToCharArray().Where(ch=>!char.IsWhiteSpace(ch)).ToArray());
                
                program.AppendLine($"bool {varName} = Widgets.ButtonText({rectName},\"{button.label}\");");
                buttons++;
            }
            Log.Message($"Read {buttons} buttons.");
        }



        public static void ReadLabels()
        {
            int labels = 0;
            foreach (GUIItem label in GuiMaker.Labels)
            {
                string rectName = label.parent.name;
                rectName = new string(rectName.ToCharArray().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
                string varName = label.name;
                varName = new string(varName.ToCharArray().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
                program.AppendLine($"Widgets.Label({rectName},\"{label.label}\");");
                labels++;
            }
            Log.Message($"Read {labels} labels.");
        }


        public static void ReadTextfields()
        {
            int tf = 0;
            foreach (GUIItem field in GuiMaker.Textfields)
            {
                string rectName = field.parent.name;
                rectName = new string(rectName.ToCharArray().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
                string varName = field.name;
                varName = new string(varName.ToCharArray().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
               
                program.AppendLine($"string {varName};");
                program.AppendLine($"{varName} = Widgets.TextField({rectName},{varName});");
                tf++;
            }
            Log.Message($"Read {tf} textfields.");
        }



        public static void ReadCheckBoxes()
        {
            int box = 0;
            foreach (GUIItem checkbox in GuiMaker.Checkboxes)
            {
                string rectName = checkbox.parent.name;
                rectName = new string(rectName.ToCharArray().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
                string varName = checkbox.name;
                varName = new string(varName.ToCharArray().Where(ch => !char.IsWhiteSpace(ch)).ToArray());
               
                program.AppendLine($"bool {varName} = false;");
                program.AppendLine($" Widgets.CheckboxLabeled({rectName},\"{checkbox.label}\",ref {varName});");
                box++;
            }
            Log.Message($"Read {box} boxes.");
        }

        static string path;
        public static void ReadAndWriteGUI()
        {
            path = Path.GetFullPath(Path.Combine(Application.dataPath, @"..\"));
            path = $"{path}NesGUI/Output";
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
            path += "/output.txt";
            program.AppendLine("//COMPILED BY NESGUI");
            program.AppendLine("//Rect pass");
            ReadRects();
            program.AppendLine("//Button pass");
            ReadButtons();
            program.AppendLine("//Checkbox pass");
            ReadCheckBoxes();
            program.AppendLine("//Label pass");
            ReadLabels();
            program.AppendLine("//Textfield pass");
            ReadTextfields();
            program.AppendLine("//END NESGUI CODE");

            Log.Error($"Hey! This isn't an error. Just wanted to say:\n Wrote code file to: {path}");
            File.WriteAllText(path, program.ToString());
            program.Clear();
        }
    }
}
