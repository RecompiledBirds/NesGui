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
                    return new Vector2(575, 155);
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

            public List<FloatMenuOption> GetFonts()
            {
                List<FloatMenuOption> res = new List<FloatMenuOption>();

                foreach (GameFont font in Enum.GetValues(typeof(GameFont)))
                {
                    res.Add(new FloatMenuOption($"{font}  {(this.font == font ? "(current)" : "")}", delegate ()
                    {
                        this.font = font;
                    }));
                }
                return res;
            }

            public List<FloatMenuOption> GetAnchors()
            {

                List<FloatMenuOption> res = new List<FloatMenuOption>();

                foreach (TextAnchor anchor in Enum.GetValues(typeof(TextAnchor)))
                {
                    res.Add(new FloatMenuOption($"{anchor}  {(this.anchor == anchor ? "(current)" : "")}", delegate ()
                    {
                        this.anchor = anchor;
                    }));
                }
                return res;
            }

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
                    Vector2 size = new Vector2(100, 100);
                    Vector2 pos = new Vector2(0,0);
                    //COMPILED BY NESGUI
                    //Prepare varibles

                    GameFont prevFont = Text.Font;
                    TextAnchor textAnchor = Text.Anchor;

                    //Rect pass

                    Rect createrectanglelabel = new Rect(new Vector2(10f, 10f), new Vector2(540f, 30f));
                    Rect namelabelrect = new Rect(new Vector2(10f, 45f), new Vector2(100f, 30f));
                    Rect inputfieldname = new Rect(new Vector2(115f, 45f), new Vector2(420f, 30f));
                    Rect Setsizelabel = new Rect(new Vector2(10f, 80f), new Vector2(100f, 30f));
                    Rect sizex = new Rect(new Vector2(115f, 80f), new Vector2(30f, 30f));
                    Rect sizey = new Rect(new Vector2(155f, 80f), new Vector2(30f, 30f));
                    Rect createbuttonrect = new Rect(new Vector2(195f, 80f), new Vector2(340f, 30f));

                    //Button pass

                    prevFont = Text.Font;
                    textAnchor = Text.Anchor;
                    Text.Font = GameFont.Tiny;
                    Text.Anchor = TextAnchor.MiddleLeft;

                    bool Createbutton = Widgets.ButtonText(createbuttonrect, "Create button");

                    Text.Font = prevFont;
                    Text.Anchor = textAnchor;

                    //Checkbox pass


                    //Label pass

                    prevFont = Text.Font;
                    textAnchor = Text.Anchor;
                    Text.Font = GameFont.Small;
                    Text.Anchor = TextAnchor.MiddleLeft;

                    Widgets.Label(namelabelrect, "Set name:");

                    Text.Font = prevFont;
                    Text.Anchor = textAnchor;
                    prevFont = Text.Font;
                    textAnchor = Text.Anchor;
                    Text.Font = GameFont.Small;
                    Text.Anchor = TextAnchor.MiddleRight;

                    Widgets.Label(Setsizelabel, "Size:");

                    Text.Font = prevFont;
                    Text.Anchor = textAnchor;

                    Widgets.Label(createrectanglelabel, "Create rectangle");

                    //Textfield pass
                    name = Widgets.TextField(inputfieldname, name);

                    Widgets.TextFieldNumeric(sizex, ref size.y, ref ySizeBuffer);


                    Widgets.TextFieldNumeric(sizey, ref size.x, ref xSizeBuffer);


                    //END NESGUI CODE

                    if (Createbutton && name != null)
                    {
                        GuiMaker.MakeRect(size, pos, name);
                    }
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
                    //COMPILED BY NESGUI
                    //Prepare varibles

                    GameFont prevFont = Text.Font;
                    TextAnchor textAnchor = Text.Anchor;

                    //Rect pass

                    Rect creatinglabelrect = new Rect(new Vector2(5f, 5f), new Vector2(488f, 30f));
                    Rect namelabel = new Rect(new Vector2(5f, 40f), new Vector2(100f, 30f));
                    Rect namefield = new Rect(new Vector2(110f, 40f), new Vector2(383f, 30f));
                    Rect assignrectbuttonrect = new Rect(new Vector2(5f, 75f), new Vector2(100f, 30f));
                    Rect createbuttonrect = new Rect(new Vector2(393f, 75f), new Vector2(100f, 30f));
                    Rect Setfontrect = new Rect(new Vector2(110f, 75f), new Vector2(130f, 30f));
                    Rect Setanchorrect = new Rect(new Vector2(250f, 75f), new Vector2(130f, 30f));

                    //Button pass


                    bool AssignRect = Widgets.ButtonText(assignrectbuttonrect, "Assign Rect");



                    bool Create = Widgets.ButtonText(createbuttonrect, "Create!");


                    bool Setfont = Widgets.ButtonText(Setfontrect, "Set font");



                    bool Setanchor = Widgets.ButtonText(Setanchorrect, "Set anchor");

                    //Label pass


                    Widgets.Label(namelabel, "Set name/label:");

                    prevFont = Text.Font;
                    textAnchor = Text.Anchor;
                    Text.Font = GameFont.Small;
                    Text.Anchor = TextAnchor.MiddleLeft;

                    Widgets.Label(creatinglabelrect, $"Creating {elemType}");

                    Text.Font = prevFont;
                    Text.Anchor = textAnchor;

                    //Textfield pass


                    name = Widgets.TextField(namefield, name);


                    //END NESGUI CODE
                    if (AssignRect)
                    {
                        Find.WindowStack.Add(new FloatMenu(rectList()));
                    }
                    if (Setfont)
                    {
                        Find.WindowStack.Add(new FloatMenu(GetFonts()));
                    }
                    if (Setanchor)
                    {
                        Find.WindowStack.Add(new FloatMenu(GetAnchors()));
                    }
                    if (Create && rectTouse!= null && !name.NullOrEmpty())
                    {
                        if (GUITextElement.TextElemType.Button == elemType)
                        {
                            GuiMaker.MakeButton(rectTouse, name, anchor, font);
                            return;
                        }
                        if (GUITextElement.TextElemType.Label == elemType)
                        {
                            GuiMaker.MakeLabel(rectTouse, name, anchor, font);
                            return;
                        }
                        if (GUITextElement.TextElemType.Checkbox == elemType)
                        {
                            GuiMaker.MakeCheckBox(rectTouse, name, anchor, font);
                            return;
                        }
                        if (GUITextElement.TextElemType.Textfield == elemType)
                        {
                            GuiMaker.MakeTextField(rectTouse, name, anchor, font);
                            return;
                        }
                        Close();
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
                    return new Vector2(440, 190);
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
                Rect editingitemlabel = new Rect(new Vector2(10f, 5f), new Vector2(480f, 30f));
                Rect closeRect = new Rect(new Vector2(inRect.xMax-25, 5), new Vector2(20, 20));
                if (Widgets.ButtonImage(closeRect, Widgets.CheckboxOffTex))
                {
                    Close();
                }
                Widgets.Label(editingitemlabel, $"Editing: {gI.name}");
                if (gI is GUIRect rect)
                {
                    //COMPILED BY NESGUI
                    //Rect pass

                    Rect setPosLabel = new Rect(new Vector2(15f, 35f), new Vector2(100f, 30f));
                    Rect posXRect = new Rect(new Vector2(105f, 35f), new Vector2(40f, 30f));
                    Rect posyrect = new Rect(new Vector2(155f, 35f), new Vector2(40f, 30f));
                    Rect setSizeRect = new Rect(new Vector2(15f, 70f), new Vector2(100f, 30f));
                    Rect sizeXRect = new Rect(new Vector2(105f, 70f), new Vector2(40f, 30f));
                    Rect sizeYRect = new Rect(new Vector2(155f, 70f), new Vector2(40f, 30f));

                    //Button pass


                    //Checkbox pass


                    //Label pass

                    GameFont prevFont = Text.Font;
                    TextAnchor textAnchor = Text.Anchor;
                    Text.Font = GameFont.Small;
                    Text.Anchor = TextAnchor.MiddleLeft;

                    Widgets.Label(setPosLabel, "Set position:");

                    Text.Font = prevFont;
                    Text.Anchor = textAnchor;
                    prevFont = Text.Font;
                    textAnchor = Text.Anchor;
                    Text.Font = GameFont.Small;
                    Text.Anchor = TextAnchor.MiddleLeft;

                    Widgets.Label(setSizeRect, "Set size:");

                    Text.Font = prevFont;
                    Text.Anchor = textAnchor;

                    //Textfield pass
                    //These weren't done by NesGUI 100%, but I did use it to position them.
                    Widgets.TextFieldNumeric(posXRect, ref rect.pos.x, ref xPosBuffer);
                    Widgets.TextFieldNumeric(posyrect, ref rect.pos.y, ref yPosBuffer);
                    Widgets.TextFieldNumeric(sizeXRect, ref rect.size.x, ref xSizeBuffer);
                    Widgets.TextFieldNumeric(sizeYRect, ref rect.size.y, ref ySizeBuffer);
                    //END NESGUI CODE
                    rect.UpdateRect();
 
                    return;
                }
                
                
                if (gI.GetType() == typeof(GUITextElement))
                {
                    //COMPILED BY NESGUI

                    //As a note, each varible name is made from the name the user gave the editor.

                    
                    Rect selectRectButton = new Rect(new Vector2(240f, 90f), new Vector2(120f, 40f));
                    Rect nameInputLabel = new Rect(new Vector2(10f, 45f), new Vector2(100f, 40f));
                    Rect inputField = new Rect(new Vector2(130f, 45f), new Vector2(230f, 40f));
                    Rect selectFontRect = new Rect(new Vector2(10f, 90f), new Vector2(120f, 40f));
                    Rect selectAnchorRect = new Rect(new Vector2(135f, 90f), new Vector2(100f, 40f));

                    bool selectRect = Widgets.ButtonText(selectRectButton, gI.parent==null ? "Aelect rect" : $"Using rec: {gI.parent.name}");
                    bool selectFont = Widgets.ButtonText(selectFontRect, "Select font");
                    bool selectAnchor = Widgets.ButtonText(selectAnchorRect, "Select anchor");

                    Widgets.Label(nameInputLabel, "Set name/label:");
                    //END NESGUI CODE
                    gI.name = Widgets.TextField(inputField, gI.name);
                    gI.label = gI.name;

                    if (selectRect)
                    {
                        Find.WindowStack.Add(new FloatMenu(SetRect()));
                    }

                    if (selectFont)
                    {
                        Find.WindowStack.Add(new FloatMenu(GetFonts()));
                    }

                    if (selectAnchor)
                    {
                        Find.WindowStack.Add(new FloatMenu(GetAnchors()));
                    }

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

            public List<FloatMenuOption> GetFonts()
            {
                List<FloatMenuOption> res = new List<FloatMenuOption>();
                if (gI is GUITextElement elem)
                {
                    foreach (GameFont font in Enum.GetValues(typeof(GameFont)))
                    {
                        res.Add(new FloatMenuOption($"{font}  {(elem.GetGameFont == font ? "(current)" : "")}", delegate ()
                        {
                            elem.SetFont(font);
                        }));
                    }
                }
                return res;
            }

            public List<FloatMenuOption> GetAnchors()
            {

                List<FloatMenuOption> res = new List<FloatMenuOption>();
                if (gI is GUITextElement elem)
                {
                    foreach (TextAnchor anchor in Enum.GetValues(typeof(TextAnchor)))
                    {
                        res.Add(new FloatMenuOption($"{anchor}  {(elem.GetTextAnchor == anchor ? "(current)" : "")}", delegate ()
                        {
                            elem.SetAnchor(anchor);
                        }


                    ));
                    }
                }

                return res;
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
