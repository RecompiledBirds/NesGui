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

        private static Vector2 rectSize = new Vector2(100, 100);
        private static Vector2 rectPos = new Vector2(0, 0);
        private string xSizeBuffer;
        private string ySizeBuffer;
        private string xPosBuffer;
        private string yPosBuffer;


        #region editor windows
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



            
            public CreateItemWinow(Type type, GUITextElement.TextElemType textType = GUITextElement.TextElemType.Button, TextAnchor anchor = TextAnchor.MiddleLeft, GameFont font =GameFont.Tiny)
            {
                itemToMakeType = type;
                elemType = textType;
                this.font = font;
                this.anchor = anchor;
            }
            private Type itemToMakeType;
            private TextAnchor anchor;
            private GameFont font;
            private GUITextElement.TextElemType elemType;
            private string xSizeBuffer;
            private string ySizeBuffer;
            private string xTwoSizeBuffer;
            private string yTwoSizeBuffer;
            private GUIRect rectTouse;



            public List<FloatMenuOption> rectList()
            {
                List<FloatMenuOption> result = new List<FloatMenuOption>();
                foreach (GUIRect i in GuiMaker.Rectangles)
                {
                    result.Add(new FloatMenuOption(i.name, delegate ()
                    {
                        rectTouse = i;
                    }));
                }
                return result;
            }

            

            string name;
            public override void DoWindowContents(Rect inRect)
            {
                Text.Font = GameFont.Small;
                Rect closeRect = new Rect(new Vector2(inRect.xMax - 25, 5), new Vector2(20, 20));
                if (Widgets.ButtonImage(closeRect, Widgets.CheckboxOffTex))
                {
                    Close();
                }
                Rect labelRect = new Rect(new Vector2(inRect.x, 10), new Vector2(inRect.xMax, 40));
                if (itemToMakeType == typeof(GUIRect))
                {
                    GameFont prevFont = Text.Font;
                    Vector2 size = new Vector2(100, 100);
                    Vector2 pos = new Vector2(0,0);
                    Text.Font = GameFont.Medium;

                    Widgets.LabelFit(labelRect, "Create Rectangle");
                    Text.Font = prevFont;
                    float rectSizePos = 40;
                    Rect labelSizeRect = new Rect(new Vector2(inRect.x, rectSizePos), new Vector2(40, 40));
                    Rect xSizeRect = new Rect(new Vector2(labelSizeRect.xMax + 10, rectSizePos), new Vector2(30, 40));
                    Rect ySizeRect = new Rect(new Vector2(xSizeRect.xMax, rectSizePos), new Vector2(30, 40));
                    Rect labelNameRect = new Rect(new Vector2(ySizeRect.xMax + 20, rectSizePos), new Vector2(40, 40));
                    Rect inputNameRect = new Rect(new Vector2(labelNameRect.xMax + 10, rectSizePos), new Vector2(inRect.xMax - labelNameRect.xMax, 40));

                    Rect createRect = new Rect(new Vector2(inRect.x, 80), new Vector2(inRect.xMax, 40));
                    Widgets.Label(labelSizeRect, "Rect size:");
                    Widgets.TextFieldNumeric(xSizeRect, ref size.x, ref xSizeBuffer);
                    Widgets.TextFieldNumeric(ySizeRect, ref size.y, ref ySizeBuffer);
                    Widgets.Label(labelNameRect, "Name:");

                    Widgets.DrawBox(new Rect(new Vector2(pos.x, pos.y + 80 + 40), size));
                    if (Widgets.ButtonText(createRect, "Create!") && name != null)
                    {
                        GuiMaker.MakeRect(size,pos, name);
                        this.Close();
                    }
                    name = Widgets.TextField(inputNameRect, name);
                    return;
                }
                if (itemToMakeType == typeof(GUILine))
                {
                    GameFont prevFont = Text.Font;
                    Text.Font = GameFont.Medium;
                    Vector2 posOne = new Vector2(0,0);
                    Vector2 posTwo = new Vector2(0,0);
                    Widgets.LabelFit(labelRect, "Create Line");
                    Text.Font = prevFont;
                    float rectSizePos = 40;
                    Rect labelPosOneRect = new Rect(new Vector2(inRect.x, rectSizePos), new Vector2(40, 40));
                    Rect xPosOneRect = new Rect(new Vector2(labelPosOneRect.xMax + 10, rectSizePos), new Vector2(30, 40));
                    Rect yPosOneRect = new Rect(new Vector2(xPosOneRect.xMax, rectSizePos), new Vector2(30, 40));
                    Rect labelPosTwoRect = new Rect(new Vector2(yPosOneRect.xMax+20, rectSizePos), new Vector2(40, 40));
                    Rect xPosTwoRect = new Rect(new Vector2(labelPosTwoRect.xMax + 10, rectSizePos), new Vector2(30, 40));
                    Rect yPosTwoRect = new Rect(new Vector2(xPosTwoRect.xMax, rectSizePos), new Vector2(30, 40));

                    Rect nameLabelRect = new Rect(new Vector2(inRect.x, rectSizePos * 2), new Vector2(40, 40));
                    Rect nameFieldRect = new Rect(new Vector2(nameLabelRect.xMax + 5, rectSizePos * 2), new Vector2(100, 40));

                    Widgets.Label(nameLabelRect, "Name:");
                    name = Widgets.TextField(nameFieldRect, name);

                    Widgets.Label(labelPosOneRect, "Start point:");
                    Widgets.TextFieldNumeric(xPosOneRect, ref posOne.x, ref xSizeBuffer);
                    Widgets.TextFieldNumeric(yPosOneRect, ref posOne.y, ref ySizeBuffer);
                    
                    Widgets.Label(labelPosTwoRect, "End point:");
                    Widgets.TextFieldNumeric(xPosTwoRect, ref posTwo.x, ref xTwoSizeBuffer);
                    Widgets.TextFieldNumeric(yPosTwoRect, ref posTwo.y, ref yTwoSizeBuffer);

                    Widgets.DrawLine(new Vector2(posOne.x,posOne.y+80), new Vector2(posTwo.x,posTwo.y+80), Color.white, 1);
                   
                    Rect createRect = new Rect(new Vector2(inRect.x, rectSizePos*3), new Vector2(inRect.xMax, 40));
                    if (Widgets.ButtonText(createRect, "Create!")&&rectTouse!=null)
                    {
                        //GuiMaker.MakeLine(posOne, posTwo, name);
                        this.Close();
                    }
                    return;
                }
                if (itemToMakeType ==typeof(GUITextElement))
                {
                    GameFont prevFont = Text.Font;

                    Text.Font = GameFont.Medium;

                    Widgets.LabelFit(labelRect, $"Create {( elemType==GUITextElement.TextElemType.Button ? "button"  : elemType == GUITextElement.TextElemType.Label ? "label" : elemType == GUITextElement.TextElemType.Checkbox? "checkbox" : "text field"  )}");
                    Text.Font = prevFont;
                    float rectSizePos = 40;

                    Rect selectRect = new Rect(new Vector2(inRect.x, rectSizePos), new Vector2(90, 40));
                    Rect labelSetLabelRect = new Rect(new Vector2(selectRect.xMax + 20, rectSizePos), new Vector2(40, 40));
                    Rect labelTF = new Rect(new Vector2(labelSetLabelRect.xMax, rectSizePos), new Vector2(90, 40));

                    bool setRect;
                    if (rectTouse == null)
                    {
                        setRect = Widgets.ButtonText(selectRect, "Select rect");
                    }
                    else
                    {
                        setRect = Widgets.ButtonText(selectRect, $"Using rec: {rectTouse.name}");
                    }
                    if (setRect)
                    {
                        Find.WindowStack.Add(new FloatMenu(rectList()));
                    }
                    Widgets.Label(labelSetLabelRect, "Set label:");
                    name = Widgets.TextField(labelTF, name);

                    Rect createRect = new Rect(new Vector2(inRect.x, 80), new Vector2(inRect.xMax, 40));
                    if (Widgets.ButtonText(createRect, "Create!") && name != null &&rectTouse!=null )
                    {
                        if (elemType==GUITextElement.TextElemType.Button)
                        {
                            GuiMaker.MakeButton(rectTouse, name,anchor,font);
                        }
                        else if(elemType == GUITextElement.TextElemType.Label)
                        {
                            GuiMaker.MakeLabel(rectTouse, name,anchor,font);
                        }else if(elemType == GUITextElement.TextElemType.Checkbox)
                        {
                            GuiMaker.MakeCheckBox(rectTouse, name,anchor,font);
                        }else if (elemType == GUITextElement.TextElemType.Textfield)
                        {
                            GuiMaker.MakeTextField(rectTouse, name,anchor,font);
                        }
                        this.Close();
                    }
                }
            }



        }

        public List<FloatMenuOption> GetItemOptions()
        {
            List<FloatMenuOption> result = new List<FloatMenuOption>
            {
                new FloatMenuOption("Rectangle", delegate ()
                {
                    Find.WindowStack.Add(new CreateItemWinow(typeof(GUIRect)));
                }),
                new FloatMenuOption("Button", delegate ()
                {
                    Find.WindowStack.Add(new CreateItemWinow(typeof(GUITextElement)));
                }),
                new FloatMenuOption("CheckBox", delegate ()
                {
                    Find.WindowStack.Add(new CreateItemWinow(typeof(GUITextElement),GUITextElement.TextElemType.Checkbox));
                }),
                new FloatMenuOption("Label", delegate ()
                {
                    Find.WindowStack.Add(new CreateItemWinow(typeof(GUITextElement),GUITextElement.TextElemType.Label));
                }),
                /*new FloatMenuOption("Line", delegate ()
                {
                    Find.WindowStack.Add(new CreateItemWinow(GUIType.Line));
                }),*/
                  new FloatMenuOption("Textfield", delegate ()
                {
                    Find.WindowStack.Add(new CreateItemWinow(typeof(GUITextElement),GUITextElement.TextElemType.Textfield));
                })
            };

            return result;
        }


        public List<FloatMenuOption> GetEditableItems()
        {
            List<FloatMenuOption> result = new List<FloatMenuOption>();

            foreach (GUIItem i in GuiMaker.Items)
            {
                result.Add(new FloatMenuOption(i.name, delegate () {
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
                Text.Font = GameFont.Small;
                Rect labelRect = new Rect(new Vector2(inRect.x, 10), new Vector2(inRect.xMax - 60, 40));
                Rect closeRect = new Rect(new Vector2(inRect.xMax-25, 5), new Vector2(20, 20));
                if (Widgets.ButtonImage(closeRect, Widgets.CheckboxOffTex))
                {
                    Close();
                }
                float rectSizePos = 40;
                Widgets.LabelFit(labelRect, $"Editing: {gI.name}");
                if (gI is GUIRect rect)
                {

                    Rect labelSizeRect = new Rect(new Vector2(inRect.x, rectSizePos), new Vector2(40, 40));
                    Rect xSizeRect = new Rect(new Vector2(labelSizeRect.xMax + 10, rectSizePos), new Vector2(30, 40));
                    Rect ySizeRect = new Rect(new Vector2(xSizeRect.xMax, rectSizePos), new Vector2(30, 40));
                    Rect labelPosRect = new Rect(new Vector2(ySizeRect.xMax + 20, rectSizePos), new Vector2(40, 40));
                    Rect xPosRect = new Rect(new Vector2(labelPosRect.xMax + 10, rectSizePos), new Vector2(30, 40));
                    Rect yPosRect = new Rect(new Vector2(xPosRect.xMax + 10, rectSizePos), new Vector2(30, 40));

                    Widgets.Label(labelSizeRect, "Rect size:");
                    Widgets.Label(labelPosRect, "Rect pos:");

                    Widgets.TextFieldNumeric(xSizeRect, ref rect.size.x, ref xSizeBuffer);
                    Widgets.TextFieldNumeric(ySizeRect, ref rect.size.y, ref ySizeBuffer);

                    Widgets.TextFieldNumeric(xPosRect, ref rect.pos.x, ref xPosBuffer);
                    Widgets.TextFieldNumeric(yPosRect, ref rect.pos.y, ref yPosBuffer);

                    rect.UpdateRect();
 
                    return;
                }
                Rect selectRect = new Rect(new Vector2(inRect.x, rectSizePos), new Vector2(90, 40));


                bool setRect;
                if (gI.parent == null)
                {
                    setRect = Widgets.ButtonText(selectRect, "Select rect");
                }
                else
                {
                    setRect = Widgets.ButtonText(selectRect, $"Using rec: {gI.parent.name}");
                }
                if (setRect)
                {
                    Find.WindowStack.Add(new FloatMenu(SetRect()));
                }
                if (gI.GetType() == typeof(GUITextElement))
                {
                    Rect newLabelRect = new Rect(new Vector2(selectRect.xMax, rectSizePos), new Vector2(50, 40));
                    Rect newLabelInput = new Rect(new Vector2(newLabelRect.xMax, rectSizePos), new Vector2(90, 40));

                    Widgets.Label(newLabelRect, "Edit label:");

                    gI.label = Widgets.TextField(newLabelInput, gI.label);
                    gI.name = gI.label;
                }
            }
            public List<FloatMenuOption> SetRect()
            {
                List<FloatMenuOption> result = new List<FloatMenuOption>();
                foreach (GUIRect i in GuiMaker.Rectangles)
                {
                    result.Add(new FloatMenuOption(i.name, delegate ()
                    {
                        gI.parent = i;
                        
                    }));
                }
                return result;
            }
        }

        #endregion
        public override void DoWindowContents(Rect inRect)
        {
            Text.Font = GameFont.Tiny;
            Rect closeRect = new Rect(new Vector2(inRect.xMax - 25, 5), new Vector2(20, 20));
            if (Widgets.ButtonImage(closeRect, Widgets.CheckboxOffTex))
            {
                Close();
            }

            Widgets.DrawLine(new Vector2(inRect.x, 50), new Vector2(inRect.xMax, 50), Color.white, 1f);
            Rect labelRect = new Rect(new Vector2(inRect.x, 10), new Vector2(50, 40));
            Rect addItemButtonRect = new Rect(new Vector2(labelRect.xMax + 5, 10), new Vector2(80, 40));
            Rect labelSizeRect = new Rect(new Vector2(addItemButtonRect.xMax + 10, 10), new Vector2(100, 40));

            Rect xSizeInputField = new Rect(new Vector2(labelSizeRect.xMax + 5, 10), new Vector2(90, 40));
            Rect ySizeInputField = new Rect(new Vector2(xSizeInputField.xMax + 5, 10), new Vector2(90, 40));

            Rect labelPosRect = new Rect(new Vector2(ySizeInputField.xMax + 5, 10), new Vector2(90, 40));
            Rect xPosInputField = new Rect(new Vector2(labelPosRect.xMax + 5, 10), new Vector2(90, 40));
            Rect yPosInputField = new Rect(new Vector2(xPosInputField.xMax + 5, 10), new Vector2(90, 40));

            Rect changeRectPos = new Rect(new Vector2(yPosInputField.xMax + 5, 10), new Vector2(80, 40));

            

            Rect enableOrDisableDrawnRects = new Rect(new Vector2(changeRectPos.xMax + 10, 10), new Vector2(80, 40));

            Rect exportButtonRect = new Rect(new Vector2(inRect.xMax - 80, 10), new Vector2(40, 40));
            Rect deleteItemRect = new Rect(new Vector2(enableOrDisableDrawnRects.xMax + 10, 10), new Vector2(80, 40));
            bool deleteItem = Widgets.ButtonText(deleteItemRect, "Delete item");

            bool export = Widgets.ButtonText(exportButtonRect, "Export GUI");
            if (export)
            {
                NesGUI_OutputGen.ReadAndWriteGUI();
            }
            
            bool toggleRect = Widgets.ButtonText(enableOrDisableDrawnRects, "Toggle rects");
            

            Widgets.Label(labelRect, "NesGUI \n V 0.0.2A");
            Widgets.Label(labelSizeRect, "Change window size:");
            Widgets.Label(labelPosRect, "Change window pos:");
            bool makeNewItem = Widgets.ButtonText(addItemButtonRect, "New item");
            Widgets.TextFieldNumeric(xSizeInputField, ref rectSize.x, ref xSizeBuffer);
            Widgets.TextFieldNumeric(ySizeInputField, ref rectSize.y, ref ySizeBuffer);

            Widgets.TextFieldNumeric(xPosInputField, ref rectPos.x, ref xPosBuffer);
            Widgets.TextFieldNumeric(yPosInputField, ref rectPos.y, ref yPosBuffer, min: 50);

            bool changePosOfRect = Widgets.ButtonText(changeRectPos, "Edit item");

            Widgets.DrawBox(new Rect(rectPos, rectSize), 4);
            if (toggleRect)
            {
                Find.WindowStack.Add(new FloatMenu(ToggleRects()));
            }
            if (makeNewItem)
            {
                Find.WindowStack.Add(new FloatMenu(GetItemOptions()));
            }
            if (changePosOfRect)
            {
                Find.WindowStack.Add(new FloatMenu(GetEditableItems()));
            }
            if (deleteItem)
            {
                Find.WindowStack.Add(new FloatMenu(DeleteItems()));
            }
            foreach(GUIItem item in GuiMaker.Items)
            {
                item.Draw();
            }
        }

        public bool IsEnabled(GUIItem rect)
        {
            return ( !GuiMaker.enabledRects.ContainsKey(rect) || GuiMaker.enabledRects[rect]);
        }
        
        public List<FloatMenuOption > DeleteItems()
        {
            List<FloatMenuOption> res = new List<FloatMenuOption>();

            foreach (GUIItem item in GuiMaker.Items)
            {
                res.Add(new FloatMenuOption($"{item.name}", delegate ()
                {
                    GuiMaker.DeleteItem(item);
                }));
            }

            return res;
        }
        public List<FloatMenuOption> ToggleRects()
        {
            List<FloatMenuOption> res = new List<FloatMenuOption>();

            foreach (GUIItem rect in GuiMaker.Rectangles)
            {
                res.Add(new FloatMenuOption($"{rect.name} {(IsEnabled(rect) ? "(on)":"(off)")}", delegate()
                {
                    if (!GuiMaker.enabledRects.ContainsKey(rect))
                    {
                        GuiMaker.enabledRects.Add(rect, false);
                    }
                    else
                    {
                        GuiMaker.enabledRects[rect] = !GuiMaker.enabledRects[rect];
                    }
                }));
            }

            return res;
        }
    }
}
