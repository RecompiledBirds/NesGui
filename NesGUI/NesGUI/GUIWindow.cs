using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace NesGUI
{
    public class OpenGUIWindow : MainButtonWorker
    {
        static bool isOpen = false;
        public override void Activate()
        {
            if (!isOpen)
            {
                Find.WindowStack.Add(new NesGUIWindow());
                isOpen = true;
            }
            else
            {
                if(Find.WindowStack.WindowOfType< NesGUIWindow>()!= null)
                {
                    Find.WindowStack.TryRemove(typeof(NesGUIWindow));
                }
                isOpen = false;
            }
        }
    }


    public class NesGUIWindow : Window
    {
        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(1500, 700);
            }
        }

        private static Vector2 rectSize = new Vector2(100,100);
        private static Vector2 rectPos = new Vector2(50,50);
        private string xSizeBuffer;
        private string ySizeBuffer;
        private string xPosBuffer;
        private string yPosBuffer;



        public class CreateItemWinow : Window
        {
            public override void OnCancelKeyPressed()
            {
                //Do nothing
            }
            public override void OnAcceptKeyPressed()
            {
                //Do nothing
            }
            public override Vector2 InitialSize
            {
                get
                {
                    return new Vector2(500, 500);
                }
            }

            

            private GUIType itemToMakeType;
            public CreateItemWinow(GUIType type)
            {
                itemToMakeType = type;
            }
            private string xSizeBuffer;
            private string ySizeBuffer;
            
            string name;
            public override void DoWindowContents(Rect inRect)
            {
                Rect labelRect = new Rect(new Vector2(inRect.x, 10), new Vector2(inRect.xMax, 40));
                if (itemToMakeType == GUIType.Rect)
                {
                    GameFont prevFont = Text.Font;
                    Vector2 size = new Vector2(100,100);
                    Vector2 pos = rectPos;
                    Text.Font = GameFont.Medium;
                   
                    Widgets.LabelFit(labelRect, "Create Rectangle");
                    Text.Font = prevFont;
                    float rectSizePos = 40;
                    Rect labelSizeRect = new Rect(new Vector2(inRect.x, rectSizePos), new Vector2(40, 40));
                    Rect xSizeRect = new Rect(new Vector2(labelSizeRect.xMax+10, rectSizePos), new Vector2(30, 40));
                    Rect ySizeRect = new Rect(new Vector2(xSizeRect.xMax, rectSizePos), new Vector2(30, 40));
                    Rect labelNameRect = new Rect(new Vector2(ySizeRect.xMax + 20, rectSizePos), new Vector2(40,40));
                    Rect inputNameRect = new Rect(new Vector2(labelNameRect.xMax + 10, rectSizePos), new Vector2(inRect.xMax-labelNameRect.xMax, 40));

                    Rect createRect = new Rect(new Vector2(inRect.x, 80), new Vector2(inRect.xMax, 40));
                    Widgets.Label(labelSizeRect, "Rect size:");
                    Widgets.TextFieldNumeric(xSizeRect, ref size.x, ref xSizeBuffer);
                    Widgets.TextFieldNumeric(ySizeRect, ref size.y, ref ySizeBuffer);
                    Widgets.Label(labelNameRect, "Name:");

                    Widgets.DrawBox(new Rect(new Vector2(pos.x,pos.y+80+40), size));
                    if(Widgets.ButtonText(createRect, "Create!") && name!=null)
                    {
                        GuiMaker.MakeRect(size, pos, name);
                        this.Close();
                    }
                    name = Widgets.TextField(inputNameRect, name); 
                }
            }
        }
        
        public List<FloatMenuOption> GetItemOptions()
        {
            List<FloatMenuOption> result = new List<FloatMenuOption>
            {
                new FloatMenuOption("Rectangle", delegate ()
                {

                    Find.WindowStack.Add(new CreateItemWinow(GUIType.Rect));
                }),

                new FloatMenuOption("Button", delegate ()
                {

                })
            };

            return result;
        }


        public List<FloatMenuOption> GetEditableItems()
        {
            List<FloatMenuOption> result = new List<FloatMenuOption>();

            foreach (GUIItem i in GuiMaker.rectangles)
            {
                result.Add(new FloatMenuOption(i.name, delegate() {
                    Find.WindowStack.Add(new EditableItemWindow(i));
                }));
            }

            return result;
        }


        public class EditableItemWindow : Window
        {
            private string xSizeBuffer;
            private string ySizeBuffer;
            private string xPosBuffer;
            private string yPosBuffer;
            public override void OnCancelKeyPressed()
            {
                //Do nothing
            }
            public override void OnAcceptKeyPressed()
            {
                //Do nothing
            }
            public override Vector2 InitialSize
            {
                get
                {
                    return new Vector2(500, 500);
                }
            }
            private GUIItem gI;
            public EditableItemWindow(GUIItem item)
            {
                gI = item;
            }
            public override void DoWindowContents(Rect inRect)
            {
                Rect labelRect = new Rect(new Vector2(inRect.x, 10), new Vector2(inRect.xMax-60, 40));
                Rect closeRect = new Rect(new Vector2(labelRect.xMax, 10), new Vector2(60, 40));
                if (Widgets.ButtonText(closeRect, "Exit"))
                {
                    Close();
                }
                Widgets.LabelFit(labelRect, $"Editing: {gI.name}");
                if (gI.GuiType == GUIType.Rect)
                {
                    float rectSizePos = 40;
                    Rect labelSizeRect = new Rect(new Vector2(inRect.x, rectSizePos), new Vector2(40, 40));
                    Rect xSizeRect = new Rect(new Vector2(labelSizeRect.xMax + 10, rectSizePos), new Vector2(30, 40));
                    Rect ySizeRect = new Rect(new Vector2(xSizeRect.xMax, rectSizePos), new Vector2(30, 40));

                    Rect labelPosRect = new Rect(new Vector2(ySizeRect.xMax, rectSizePos), new Vector2(40, 40));
                    Rect xPosRect = new Rect(new Vector2(labelPosRect.xMax + 20, rectSizePos), new Vector2(30, 40));
                    Rect yPosRect = new Rect(new Vector2(xPosRect.xMax + 10, rectSizePos), new Vector2(30,40));

                    Rect createRect = new Rect(new Vector2(inRect.x, 80), new Vector2(inRect.xMax, 40));
                    Widgets.Label(labelSizeRect, "Rect size:");
                    Widgets.Label(labelPosRect, "Rect pos:");
                    Widgets.TextFieldNumeric(xSizeRect, ref gI.size.x, ref xSizeBuffer);
                    Widgets.TextFieldNumeric(ySizeRect, ref gI.size.y, ref ySizeBuffer);
                    Widgets.TextFieldNumeric(xPosRect, ref gI.pos.x, ref xPosBuffer, rectPos.x);
                    Widgets.TextFieldNumeric(yPosRect, ref gI.pos.y, ref yPosBuffer, rectPos.y);
                    
                }
            }
        }
        public override void DoWindowContents(Rect inRect)
        {
         
            
            Widgets.DrawLine(new Vector2(inRect.x, 50), new Vector2(inRect.xMax, 50),Color.white,1f);
            Rect labelRect = new Rect(new Vector2(inRect.x,10), new Vector2(50,40));
            Rect addItemButtonRect = new Rect(new Vector2(labelRect.xMax+5, 10), new Vector2(80, 40));
            Rect labelSizeRect = new Rect(new Vector2(addItemButtonRect.xMax+10, 10), new Vector2(100, 40));
            
            Rect xSizeInputField = new Rect(new Vector2(labelSizeRect.xMax+5,10), new Vector2(90, 40));
            Rect ySizeInputField = new Rect(new Vector2(xSizeInputField.xMax+5, 10), new Vector2(90, 40));

            Rect labelPosRect = new Rect(new Vector2(ySizeInputField.xMax + 5, 10), new Vector2(90, 40));
            Rect xPosInputField = new Rect(new Vector2(labelPosRect.xMax + 5, 10), new Vector2(90, 40));
            Rect yPosInputField = new Rect(new Vector2(xPosInputField.xMax + 5, 10), new Vector2(90, 40));

            Rect changeRectPos = new Rect(new Vector2(yPosInputField.xMax + 5, 10), new Vector2(80, 40));

            Widgets.Label(labelRect, "NesGUI");
            Widgets.Label(labelSizeRect, "Change window size:");
            Widgets.Label(labelPosRect, "Change window pos:");
            bool makeNewItem = Widgets.ButtonText(addItemButtonRect, "new item");
            Widgets.TextFieldNumeric(xSizeInputField, ref rectSize.x, ref xSizeBuffer);
            Widgets.TextFieldNumeric(ySizeInputField, ref rectSize.y, ref ySizeBuffer);

            Widgets.TextFieldNumeric(xPosInputField, ref rectPos.x, ref xPosBuffer);
            Widgets.TextFieldNumeric(yPosInputField, ref rectPos.y, ref yPosBuffer,min:50);

            bool changePosOfRect = Widgets.ButtonText(changeRectPos, "Edit item");

            Widgets.DrawBox(new Rect(rectPos, rectSize),4);
            foreach(GUIItem rect in GuiMaker.rectangles)
            {
                Rect r = new Rect(new Vector2(rect.Pos.x, rect.Pos.y + 50),rect.Size);
                
                Widgets.DrawBoxSolidWithOutline(r, Color.clear, Color.red);
                Widgets.Label(r, rect.name);
            }
            if (makeNewItem)
            {
                Find.WindowStack.Add(new FloatMenu(GetItemOptions()));
            }
            if (changePosOfRect)
            {
                Find.WindowStack.Add(new FloatMenu(GetEditableItems()));
            }
        }

        
    }
}
