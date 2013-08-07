using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UIAutomation;

namespace WindowsStoreCrawler
{
    class ControlTypeConverter
    {
        public static string convert2string(int id)
        {
            string type;
            switch (id)
            {
                case UIA_ControlTypeIds.UIA_ButtonControlTypeId:
                    type = "button";
                    break;
                case UIA_ControlTypeIds.UIA_CalendarControlTypeId:
                    type = "calendar";
                    break;
                case UIA_ControlTypeIds.UIA_CheckBoxControlTypeId:
                    type = "checkbox";
                    break;
                case UIA_ControlTypeIds.UIA_ComboBoxControlTypeId:
                    type = "combobox";
                    break;
                case UIA_ControlTypeIds.UIA_CustomControlTypeId:
                    type = "custom";
                    break;
                case UIA_ControlTypeIds.UIA_DataGridControlTypeId:
                    type = "datagrid";
                    break;
                case UIA_ControlTypeIds.UIA_DataItemControlTypeId:
                    type = "dataitem";
                    break;
                case UIA_ControlTypeIds.UIA_DocumentControlTypeId:
                    type = "document";
                    break;
                case UIA_ControlTypeIds.UIA_EditControlTypeId:
                    type = "edit";
                    break;
                case UIA_ControlTypeIds.UIA_GroupControlTypeId:
                    type = "group";
                    break;
                case UIA_ControlTypeIds.UIA_HeaderControlTypeId:
                    type = "header";
                    break;
                case UIA_ControlTypeIds.UIA_HeaderItemControlTypeId:
                    type = "headeritem";
                    break;
                case UIA_ControlTypeIds.UIA_HyperlinkControlTypeId:
                    type = "hyperlink";
                    break;
                case UIA_ControlTypeIds.UIA_ImageControlTypeId:
                    type = "image";
                    break;
                case UIA_ControlTypeIds.UIA_ListControlTypeId:
                    type = "list";
                    break;
                case UIA_ControlTypeIds.UIA_ListItemControlTypeId:
                    type = "listitem";
                    break;
                case UIA_ControlTypeIds.UIA_MenuBarControlTypeId:
                    type = "menubar";
                    break;
                case UIA_ControlTypeIds.UIA_MenuControlTypeId:
                    type = "menu";
                    break;
                case UIA_ControlTypeIds.UIA_MenuItemControlTypeId:
                    type = "menuitem";
                    break;
                case UIA_ControlTypeIds.UIA_PaneControlTypeId:
                    type = "pane";
                    break;
                case UIA_ControlTypeIds.UIA_ProgressBarControlTypeId:
                    type = "progressbar";
                    break;
                case UIA_ControlTypeIds.UIA_RadioButtonControlTypeId:
                    type = "radiobutton";
                    break;
                case UIA_ControlTypeIds.UIA_ScrollBarControlTypeId:
                    type = "scrollbar";
                    break;
                case UIA_ControlTypeIds.UIA_SemanticZoomControlTypeId:
                    type = "semanticzoom";
                    break;
                case UIA_ControlTypeIds.UIA_SeparatorControlTypeId:
                    type = "separator";
                    break;
                case UIA_ControlTypeIds.UIA_SliderControlTypeId:
                    type = "slider";
                    break;
                case UIA_ControlTypeIds.UIA_SpinnerControlTypeId:
                    type = "spinner";
                    break;
                case UIA_ControlTypeIds.UIA_SplitButtonControlTypeId:
                    type = "splitbotton";
                    break;
                case UIA_ControlTypeIds.UIA_StatusBarControlTypeId:
                    type = "statusbar";
                    break;
                case UIA_ControlTypeIds.UIA_TabControlTypeId:
                    type = "tab";
                    break;
                case UIA_ControlTypeIds.UIA_TabItemControlTypeId:
                    type = "tabitem";
                    break;
                case UIA_ControlTypeIds.UIA_TableControlTypeId:
                    type = "table";
                    break;
                case UIA_ControlTypeIds.UIA_TextControlTypeId:
                    type = "text";
                    break;
                case UIA_ControlTypeIds.UIA_ThumbControlTypeId:
                    type = "thumb";
                    break;
                case UIA_ControlTypeIds.UIA_TitleBarControlTypeId:
                    type = "titlebar";
                    break;
                case UIA_ControlTypeIds.UIA_ToolBarControlTypeId:
                    type = "toolbar";
                    break;
                case UIA_ControlTypeIds.UIA_ToolTipControlTypeId:
                    type = "tooltip";
                    break;
                case UIA_ControlTypeIds.UIA_TreeControlTypeId:
                    type = "tree";
                    break;
                case UIA_ControlTypeIds.UIA_TreeItemControlTypeId:
                    type = "treeitem";
                    break;
                case UIA_ControlTypeIds.UIA_WindowControlTypeId:
                    type = "window";
                    break;
                default:
                    type = null;
                    break;
            }

            return type;
        }
    }
}
