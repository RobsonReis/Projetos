namespace B1WizardBase
{
    using SAPbouiCOM;
    using System;
    using System.Collections;

    public class EventTables
    {
        private Hashtable afterMethodNames = new Hashtable();
        private Hashtable beforeEnabled = new Hashtable();
        private Hashtable beforeMethodNames = new Hashtable();
        private static EventTables et;
        private BoEventTypes[] formEventTypes;
        private BoEventTypes[] genericEventTypes;
        private Hashtable itemEventTypes = new Hashtable();
        private Hashtable itemTypes = new Hashtable();
        private Hashtable returnTypes = new Hashtable();

        private EventTables()
        {
            this.beforeEnabled[BoEventTypes.et_MATRIX_LOAD] = true;
            this.beforeEnabled[BoEventTypes.et_DATASOURCE_LOAD] = true;
            this.beforeEnabled[BoEventTypes.et_MATRIX_COLLAPSE_PRESSED] = true;
            this.beforeEnabled[BoEventTypes.et_GRID_SORT] = true;
            this.beforeEnabled[BoEventTypes.et_MENU_CLICK] = true;
            this.beforeEnabled[BoEventTypes.et_CLICK] = true;
            this.beforeEnabled[BoEventTypes.et_DOUBLE_CLICK] = true;
            this.beforeEnabled[BoEventTypes.et_KEY_DOWN] = true;
            this.beforeEnabled[BoEventTypes.et_GOT_FOCUS] = false;
            this.beforeEnabled[BoEventTypes.et_LOST_FOCUS] = false;
            this.beforeEnabled[BoEventTypes.et_VALIDATE] = true;
            this.beforeEnabled[BoEventTypes.et_ITEM_PRESSED] = true;
            this.beforeEnabled[BoEventTypes.et_COMBO_SELECT] = true;
            this.beforeEnabled[BoEventTypes.et_MATRIX_LINK_PRESSED] = true;
            this.beforeEnabled[BoEventTypes.et_FORM_LOAD] = true;
            this.beforeEnabled[BoEventTypes.et_FORM_CLOSE] = true;
            this.beforeEnabled[BoEventTypes.et_FORM_UNLOAD] = true;
            this.beforeEnabled[BoEventTypes.et_FORM_ACTIVATE] = false;
            this.beforeEnabled[BoEventTypes.et_FORM_DEACTIVATE] = false;
            this.beforeEnabled[BoEventTypes.et_FORM_RESIZE] = false;
            this.beforeEnabled[BoEventTypes.et_FORM_KEY_DOWN] = true;
            this.beforeEnabled[BoEventTypes.et_FORM_MENU_HILIGHT] = false;
            this.beforeEnabled[BoEventTypes.et_FORM_DATA_ADD] = true;
            this.beforeEnabled[BoEventTypes.et_FORM_DATA_DELETE] = true;
            this.beforeEnabled[BoEventTypes.et_FORM_DATA_LOAD] = true;
            this.beforeEnabled[BoEventTypes.et_FORM_DATA_UPDATE] = true;
            this.beforeEnabled[BoEventTypes.et_CHOOSE_FROM_LIST] = true;
            this.beforeEnabled[BoEventTypes.et_RIGHT_CLICK] = true;
            this.beforeEnabled[BoEventTypes.et_PRINT] = true;
            this.beforeEnabled[BoEventTypes.et_PRINT_DATA] = true;
            this.beforeEnabled[BoEventTypes.et_Drag] = true;
            this.beforeEnabled[BoWidgetEventTypes.wet_content_save] = true;
            this.beforeEnabled[BoWidgetEventTypes.wet_created] = true;
            this.returnTypes[BoEventTypes.et_MATRIX_LOAD] = false;
            this.returnTypes[BoEventTypes.et_DATASOURCE_LOAD] = false;
            this.returnTypes[BoEventTypes.et_MATRIX_COLLAPSE_PRESSED] = false;
            this.returnTypes[BoEventTypes.et_GRID_SORT] = false;
            this.returnTypes[BoEventTypes.et_CLICK] = false;
            this.returnTypes[BoEventTypes.et_DOUBLE_CLICK] = false;
            this.returnTypes[BoEventTypes.et_KEY_DOWN] = false;
            this.returnTypes[BoEventTypes.et_GOT_FOCUS] = false;
            this.returnTypes[BoEventTypes.et_LOST_FOCUS] = false;
            this.returnTypes[BoEventTypes.et_ITEM_PRESSED] = false;
            this.returnTypes[BoEventTypes.et_COMBO_SELECT] = false;
            this.returnTypes[BoEventTypes.et_MATRIX_LINK_PRESSED] = false;
            this.returnTypes[BoEventTypes.et_FORM_UNLOAD] = false;
            this.returnTypes[BoEventTypes.et_FORM_ACTIVATE] = false;
            this.returnTypes[BoEventTypes.et_FORM_DEACTIVATE] = false;
            this.returnTypes[BoEventTypes.et_FORM_RESIZE] = false;
            this.returnTypes[BoEventTypes.et_FORM_KEY_DOWN] = false;
            this.returnTypes[BoEventTypes.et_FORM_MENU_HILIGHT] = false;
            this.returnTypes[BoEventTypes.et_VALIDATE] = false;
            this.returnTypes[BoEventTypes.et_FORM_LOAD] = false;
            this.returnTypes[BoEventTypes.et_FORM_CLOSE] = false;
            this.returnTypes[BoEventTypes.et_FORM_DATA_ADD] = false;
            this.returnTypes[BoEventTypes.et_FORM_DATA_DELETE] = false;
            this.returnTypes[BoEventTypes.et_FORM_DATA_LOAD] = false;
            this.returnTypes[BoEventTypes.et_FORM_DATA_UPDATE] = false;
            this.returnTypes[BoEventTypes.et_CHOOSE_FROM_LIST] = false;
            this.returnTypes[BoEventTypes.et_RIGHT_CLICK] = false;
            this.returnTypes[BoEventTypes.et_PRINT] = false;
            this.returnTypes[BoEventTypes.et_PRINT_DATA] = false;
            this.returnTypes[BoEventTypes.et_MENU_CLICK] = false;
            this.returnTypes[BoEventTypes.et_Drag] = false;
            this.returnTypes[BoWidgetEventTypes.wet_content_save] = false;
            this.returnTypes[BoWidgetEventTypes.wet_created] = false;
            this.genericEventTypes = new BoEventTypes[] { 
                BoEventTypes.et_FORM_LOAD, BoEventTypes.et_FORM_UNLOAD, BoEventTypes.et_FORM_ACTIVATE, BoEventTypes.et_FORM_DEACTIVATE, BoEventTypes.et_FORM_CLOSE, BoEventTypes.et_FORM_RESIZE, BoEventTypes.et_FORM_KEY_DOWN, BoEventTypes.et_FORM_MENU_HILIGHT, BoEventTypes.et_PRINT, BoEventTypes.et_PRINT_DATA, BoEventTypes.et_RIGHT_CLICK, BoEventTypes.et_FORM_DATA_ADD, BoEventTypes.et_FORM_DATA_DELETE, BoEventTypes.et_FORM_DATA_LOAD, BoEventTypes.et_FORM_DATA_UPDATE, BoEventTypes.et_CLICK, 
                BoEventTypes.et_DOUBLE_CLICK, BoEventTypes.et_ITEM_PRESSED, BoEventTypes.et_CHOOSE_FROM_LIST, BoEventTypes.et_KEY_DOWN, BoEventTypes.et_GOT_FOCUS, BoEventTypes.et_LOST_FOCUS, BoEventTypes.et_COMBO_SELECT, BoEventTypes.et_KEY_DOWN, BoEventTypes.et_VALIDATE, BoEventTypes.et_MATRIX_LINK_PRESSED, BoEventTypes.et_MATRIX_LOAD, BoEventTypes.et_DATASOURCE_LOAD, BoEventTypes.et_MATRIX_COLLAPSE_PRESSED, BoEventTypes.et_GRID_SORT, BoEventTypes.et_Drag
             };
            this.formEventTypes = new BoEventTypes[] { BoEventTypes.et_FORM_LOAD, BoEventTypes.et_FORM_UNLOAD, BoEventTypes.et_FORM_ACTIVATE, BoEventTypes.et_FORM_DEACTIVATE, BoEventTypes.et_FORM_CLOSE, BoEventTypes.et_FORM_RESIZE, BoEventTypes.et_FORM_KEY_DOWN, BoEventTypes.et_FORM_MENU_HILIGHT, BoEventTypes.et_PRINT, BoEventTypes.et_PRINT_DATA, BoEventTypes.et_RIGHT_CLICK, BoEventTypes.et_FORM_DATA_ADD, BoEventTypes.et_FORM_DATA_DELETE, BoEventTypes.et_FORM_DATA_LOAD, BoEventTypes.et_FORM_DATA_UPDATE };
            this.itemEventTypes.Add(BoFormItemTypes.it_BUTTON, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_DOUBLE_CLICK, BoEventTypes.et_ITEM_PRESSED, BoEventTypes.et_CHOOSE_FROM_LIST });
            this.itemEventTypes.Add(BoFormItemTypes.it_CHECK_BOX, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_DOUBLE_CLICK, BoEventTypes.et_ITEM_PRESSED, BoEventTypes.et_KEY_DOWN });
            this.itemEventTypes.Add(BoFormItemTypes.it_COMBO_BOX, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_DOUBLE_CLICK, BoEventTypes.et_GOT_FOCUS, BoEventTypes.et_LOST_FOCUS, BoEventTypes.et_COMBO_SELECT, BoEventTypes.et_RIGHT_CLICK });
            this.itemEventTypes.Add(BoFormItemTypes.it_EDIT, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_DOUBLE_CLICK, BoEventTypes.et_KEY_DOWN, BoEventTypes.et_GOT_FOCUS, BoEventTypes.et_LOST_FOCUS, BoEventTypes.et_VALIDATE, BoEventTypes.et_CHOOSE_FROM_LIST, BoEventTypes.et_RIGHT_CLICK, BoEventTypes.et_Drag });
            this.itemEventTypes.Add(BoFormItemTypes.it_FOLDER, new BoEventTypes[] { BoEventTypes.et_ITEM_PRESSED, BoEventTypes.et_CLICK });
            this.itemEventTypes.Add(BoFormItemTypes.it_MATRIX, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_GOT_FOCUS, BoEventTypes.et_LOST_FOCUS, BoEventTypes.et_VALIDATE, BoEventTypes.et_ITEM_PRESSED, BoEventTypes.et_COMBO_SELECT, BoEventTypes.et_MATRIX_LINK_PRESSED, BoEventTypes.et_MATRIX_LOAD, BoEventTypes.et_DATASOURCE_LOAD, BoEventTypes.et_MATRIX_COLLAPSE_PRESSED, BoEventTypes.et_CHOOSE_FROM_LIST, BoEventTypes.et_RIGHT_CLICK, BoEventTypes.et_GRID_SORT });
            this.itemEventTypes.Add(BoFormItemTypes.it_GRID, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_GOT_FOCUS, BoEventTypes.et_LOST_FOCUS, BoEventTypes.et_VALIDATE, BoEventTypes.et_ITEM_PRESSED, BoEventTypes.et_COMBO_SELECT, BoEventTypes.et_CHOOSE_FROM_LIST, BoEventTypes.et_RIGHT_CLICK, BoEventTypes.et_GRID_SORT });
            this.itemEventTypes.Add(BoFormItemTypes.it_OPTION_BUTTON, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_DOUBLE_CLICK, BoEventTypes.et_ITEM_PRESSED });
            this.itemEventTypes.Add(BoFormItemTypes.it_STATIC, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_DOUBLE_CLICK, BoEventTypes.et_ITEM_PRESSED, BoEventTypes.et_RIGHT_CLICK });
            this.itemEventTypes.Add(BoFormItemTypes.it_EXTEDIT, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_DOUBLE_CLICK, BoEventTypes.et_GOT_FOCUS, BoEventTypes.et_VALIDATE, BoEventTypes.et_RIGHT_CLICK });
            this.itemEventTypes.Add(BoFormItemTypes.it_PANE_COMBO_BOX, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_DOUBLE_CLICK, BoEventTypes.et_GOT_FOCUS, BoEventTypes.et_LOST_FOCUS });
            this.itemEventTypes.Add(BoFormItemTypes.it_PICTURE, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_RIGHT_CLICK });
            this.itemEventTypes.Add(BoFormItemTypes.it_BUTTON_COMBO, new BoEventTypes[] { BoEventTypes.et_CLICK, BoEventTypes.et_RIGHT_CLICK, BoEventTypes.et_ITEM_PRESSED, BoEventTypes.et_COMBO_SELECT });
            NodeType type = new NodeType {
                typeName = "Button",
                varName = "button",
                type = "Button"
            };
            this.itemTypes.Add(BoFormItemTypes.it_BUTTON, type);
            type = new NodeType {
                typeName = "CheckBox",
                varName = "checkbox",
                type = "CheckBox"
            };
            this.itemTypes.Add(BoFormItemTypes.it_CHECK_BOX, type);
            type = new NodeType {
                typeName = "ComboBox",
                varName = "combobox",
                type = "ComboBox"
            };
            this.itemTypes.Add(BoFormItemTypes.it_COMBO_BOX, type);
            type = new NodeType {
                typeName = "StaticText",
                varName = "statictext",
                type = "StaticText"
            };
            this.itemTypes.Add(BoFormItemTypes.it_STATIC, type);
            type = new NodeType {
                typeName = "EditText",
                varName = "edittext",
                type = "EditText"
            };
            this.itemTypes.Add(BoFormItemTypes.it_EDIT, type);
            type = new NodeType {
                typeName = "Folder",
                varName = "folder",
                type = "Folder"
            };
            this.itemTypes.Add(BoFormItemTypes.it_FOLDER, type);
            type = new NodeType {
                typeName = "Matrix",
                varName = "matrix",
                type = "Matrix"
            };
            this.itemTypes.Add(BoFormItemTypes.it_MATRIX, type);
            type = new NodeType {
                typeName = "Grid",
                varName = "grid",
                type = "Grid"
            };
            this.itemTypes.Add(BoFormItemTypes.it_GRID, type);
            type = new NodeType {
                typeName = "OptionButton",
                varName = "optbutton",
                type = "OptionBtn"
            };
            this.itemTypes.Add(BoFormItemTypes.it_OPTION_BUTTON, type);
            type = new NodeType {
                typeName = "Picture",
                varName = "picture",
                type = "PictureBox"
            };
            this.itemTypes.Add(BoFormItemTypes.it_PICTURE, type);
            type = new NodeType {
                typeName = "PaneComboBox",
                varName = "panecombobox",
                type = "PaneComboBox"
            };
            this.itemTypes.Add(BoFormItemTypes.it_PANE_COMBO_BOX, type);
            type = new NodeType {
                typeName = "ButtonCombo",
                varName = "buttoncombo",
                type = "ButtonCombo"
            };
            this.itemTypes.Add(BoFormItemTypes.it_BUTTON_COMBO, type);
            this.beforeMethodNames[BoEventTypes.et_MATRIX_LOAD] = "OnBeforeMatrixLoad";
            this.beforeMethodNames[BoEventTypes.et_DATASOURCE_LOAD] = "OnBeforeDatasourceLoad";
            this.beforeMethodNames[BoEventTypes.et_MATRIX_COLLAPSE_PRESSED] = "OnBeforeMatrixCollapsePressed";
            this.beforeMethodNames[BoEventTypes.et_GRID_SORT] = "OnBeforeGridSort";
            this.beforeMethodNames[BoEventTypes.et_CLICK] = "OnBeforeClick";
            this.beforeMethodNames[BoEventTypes.et_DOUBLE_CLICK] = "OnBeforeDoubleClick";
            this.beforeMethodNames[BoEventTypes.et_KEY_DOWN] = "OnBeforeKeyDown";
            this.beforeMethodNames[BoEventTypes.et_ITEM_PRESSED] = "OnBeforeItemPressed";
            this.beforeMethodNames[BoEventTypes.et_COMBO_SELECT] = "OnBeforeComboSelect";
            this.beforeMethodNames[BoEventTypes.et_MATRIX_LINK_PRESSED] = "OnBeforeMatrixLinkPressed";
            this.beforeMethodNames[BoEventTypes.et_FORM_UNLOAD] = "OnBeforeFormUnload";
            this.beforeMethodNames[BoEventTypes.et_FORM_ACTIVATE] = "OnBeforeFormActivate";
            this.beforeMethodNames[BoEventTypes.et_FORM_KEY_DOWN] = "OnBeforeFormKeyDown";
            this.beforeMethodNames[BoEventTypes.et_FORM_LOAD] = "OnBeforeFormLoad";
            this.beforeMethodNames[BoEventTypes.et_FORM_CLOSE] = "OnBeforeFormClose";
            this.beforeMethodNames[BoEventTypes.et_VALIDATE] = "OnBeforeValidate";
            this.beforeMethodNames[BoEventTypes.et_FORM_DATA_ADD] = "OnBeforeFormDataAdd";
            this.beforeMethodNames[BoEventTypes.et_FORM_DATA_DELETE] = "OnBeforeFormDataDelete";
            this.beforeMethodNames[BoEventTypes.et_FORM_DATA_LOAD] = "OnBeforeFormDataLoad";
            this.beforeMethodNames[BoEventTypes.et_FORM_DATA_UPDATE] = "OnBeforeFormDataUpdate";
            this.beforeMethodNames[BoEventTypes.et_MENU_CLICK] = "OnBeforeMenuClick";
            this.beforeMethodNames[BoEventTypes.et_PRINT] = "OnBeforePrint";
            this.beforeMethodNames[BoEventTypes.et_PRINT_DATA] = "OnBeforePrintData";
            this.beforeMethodNames[BoEventTypes.et_CHOOSE_FROM_LIST] = "OnBeforeChooseFromList";
            this.beforeMethodNames[BoEventTypes.et_RIGHT_CLICK] = "OnBeforeRightClick";
            this.beforeMethodNames[BoEventTypes.et_Drag] = "OnBeforeDrag";
            this.beforeMethodNames[BoWidgetEventTypes.wet_content_save] = "OnBeforeWidgetContentSave";
            this.beforeMethodNames[BoWidgetEventTypes.wet_created] = "OnBeforeWidgetCreated";
            this.afterMethodNames[BoEventTypes.et_MENU_CLICK] = "OnAfterMenuClick";
            this.afterMethodNames[BoEventTypes.et_CLICK] = "OnAfterClick";
            this.afterMethodNames[BoEventTypes.et_DOUBLE_CLICK] = "OnAfterDoubleClick";
            this.afterMethodNames[BoEventTypes.et_KEY_DOWN] = "OnAfterKeyDown";
            this.afterMethodNames[BoEventTypes.et_GOT_FOCUS] = "OnGotFocus";
            this.afterMethodNames[BoEventTypes.et_LOST_FOCUS] = "OnLostFocus";
            this.afterMethodNames[BoEventTypes.et_MATRIX_LOAD] = "OnAfterMatrixLoad";
            this.afterMethodNames[BoEventTypes.et_DATASOURCE_LOAD] = "OnAfterDatasourceLoad";
            this.afterMethodNames[BoEventTypes.et_MATRIX_COLLAPSE_PRESSED] = "OnAfterMatrixCollapsePressed";
            this.afterMethodNames[BoEventTypes.et_GRID_SORT] = "OnAfterGridSort";
            this.afterMethodNames[BoEventTypes.et_ITEM_PRESSED] = "OnAfterItemPressed";
            this.afterMethodNames[BoEventTypes.et_COMBO_SELECT] = "OnAfterComboSelect";
            this.afterMethodNames[BoEventTypes.et_MATRIX_LINK_PRESSED] = "OnAfterMatrixLinkPressed";
            this.afterMethodNames[BoEventTypes.et_FORM_UNLOAD] = "OnAfterFormUnload";
            this.afterMethodNames[BoEventTypes.et_FORM_ACTIVATE] = "OnAfterFormActivate";
            this.afterMethodNames[BoEventTypes.et_FORM_DEACTIVATE] = "OnFormDeactivate";
            this.afterMethodNames[BoEventTypes.et_FORM_RESIZE] = "OnFormResize";
            this.afterMethodNames[BoEventTypes.et_FORM_KEY_DOWN] = "OnAfterFormKeyDown";
            this.afterMethodNames[BoEventTypes.et_FORM_MENU_HILIGHT] = "OnFormMenuHilight";
            this.afterMethodNames[BoEventTypes.et_VALIDATE] = "OnAfterValidate";
            this.afterMethodNames[BoEventTypes.et_FORM_LOAD] = "OnAfterFormLoad";
            this.afterMethodNames[BoEventTypes.et_FORM_CLOSE] = "OnAfterFormClose";
            this.afterMethodNames[BoEventTypes.et_FORM_DATA_ADD] = "OnAfterFormDataAdd";
            this.afterMethodNames[BoEventTypes.et_FORM_DATA_DELETE] = "OnAfterFormDataDelete";
            this.afterMethodNames[BoEventTypes.et_FORM_DATA_LOAD] = "OnAfterFormDataLoad";
            this.afterMethodNames[BoEventTypes.et_FORM_DATA_UPDATE] = "OnAfterFormDataUpdate";
            this.afterMethodNames[BoEventTypes.et_PRINT] = "OnAfterPrint";
            this.afterMethodNames[BoEventTypes.et_PRINT_DATA] = "OnAfterPrintData";
            this.afterMethodNames[BoEventTypes.et_CHOOSE_FROM_LIST] = "OnAfterChooseFromList";
            this.afterMethodNames[BoEventTypes.et_RIGHT_CLICK] = "OnAfterRightClick";
            this.afterMethodNames[BoEventTypes.et_Drag] = "OnAfterDrag";
        }

        public static string GetActionKey(bool before)
        {
            return before.ToString();
        }

        public static string GetActionKey(string UID, bool before)
        {
            return (UID + "." + before);
        }

        public static string GetActionKey(string formType, string itemUID, bool before)
        {
            return string.Concat(new object[] { formType, ".", itemUID, ".", before });
        }

        public static string GetEventKey(BusinessObjectInfo pVal)
        {
            return (pVal.FormTypeEx + ".." + pVal.BeforeAction.ToString());
        }

        public static string GetEventKey(ContextMenuInfo pVal)
        {
            return (B1Connections.theAppl.Forms.Item(pVal.FormUID).TypeEx + "." + pVal.ItemUID + "." + pVal.BeforeAction.ToString());
        }

        public static string GetEventKey(ItemEvent pVal)
        {
            return string.Concat(new object[] { pVal.FormTypeEx, ".", pVal.ItemUID, ".", pVal.BeforeAction });
        }

        public static string GetEventKey(MenuEvent pVal)
        {
            return (pVal.MenuUID + "." + pVal.BeforeAction);
        }

        public static string GetEventKey(PrintEventInfo pVal)
        {
            return (B1Connections.theAppl.Forms.Item(pVal.FormUID).TypeEx + ".." + pVal.BeforeAction.ToString());
        }

        public static string GetEventKey(ReportDataInfo pVal)
        {
            return (B1Connections.theAppl.Forms.Item(pVal.FormUID).TypeEx + ".." + pVal.BeforeAction.ToString());
        }

        public static string GetEventKey(WidgetData pVal)
        {
            return (pVal.WidgetType + "..True");
        }

        public static string getEventsHandlerClassName()
        {
            return "EventsHandler";
        }

        public static string getFormClassName(string formType)
        {
            return ("Form__" + formType);
        }

        public static BoEventTypes[] getFormEvents()
        {
            if (et == null)
            {
                et = new EventTables();
            }
            return et.formEventTypes;
        }

        public static string GetGenericEventKey(MenuEvent pVal)
        {
            return ("*." + pVal.BeforeAction.ToString());
        }

        public static string GetGenericEventKey(BusinessObjectInfo pVal, bool formExists)
        {
            if (formExists)
            {
                return (pVal.FormTypeEx + ".*." + pVal.BeforeAction.ToString());
            }
            return ("*.*." + pVal.BeforeAction.ToString());
        }

        public static string GetGenericEventKey(ContextMenuInfo pVal, bool formExists)
        {
            if (formExists)
            {
                return (B1Connections.theAppl.Forms.Item(pVal.FormUID).TypeEx + ".*." + pVal.BeforeAction.ToString());
            }
            return ("*.*." + pVal.BeforeAction.ToString());
        }

        public static string GetGenericEventKey(ItemEvent pVal, bool formExists)
        {
            if (formExists)
            {
                return (pVal.FormTypeEx + ".*." + pVal.BeforeAction);
            }
            return ("*.*." + pVal.BeforeAction.ToString());
        }

        public static string GetGenericEventKey(PrintEventInfo pVal, bool formExists)
        {
            if (formExists)
            {
                return (B1Connections.theAppl.Forms.Item(pVal.FormUID).TypeEx + ".*." + pVal.BeforeAction.ToString());
            }
            return ("*.*." + pVal.BeforeAction.ToString());
        }

        public static string GetGenericEventKey(ReportDataInfo pVal, bool formExists)
        {
            if (formExists)
            {
                return (B1Connections.theAppl.Forms.Item(pVal.FormUID).TypeEx + ".*." + pVal.BeforeAction.ToString());
            }
            return ("*.*." + pVal.BeforeAction.ToString());
        }

        public static string GetGenericEventKey(WidgetData pVal, bool formExists)
        {
            if (formExists)
            {
                return (pVal.WidgetType + ".*.True");
            }
            return "*.*.True";
        }

        public static BoEventTypes[] getGenericEvents()
        {
            if (et == null)
            {
                et = new EventTables();
            }
            return et.genericEventTypes;
        }

        public static string getItemClassName(string itemType, string formType, string UID)
        {
            return (itemType + "__" + formType + "__" + UID);
        }

        public static BoEventTypes[] getItemEvents(BoFormItemTypes type)
        {
            if (et == null)
            {
                et = new EventTables();
            }
            return (BoEventTypes[]) et.itemEventTypes[type];
        }

        public static BoEventTypes[] getItemEvents(string itemTypeName)
        {
            if (et == null)
            {
                et = new EventTables();
            }
            foreach (BoFormItemTypes types in et.itemTypes.Keys)
            {
                NodeType type = (NodeType) et.itemTypes[types];
                if (type.typeName.Equals(itemTypeName))
                {
                    return getItemEvents(types);
                }
            }
            return null;
        }

        public static NodeType GetItemType(BoFormItemTypes itemType)
        {
            if (et == null)
            {
                et = new EventTables();
            }
            return (NodeType) et.itemTypes[itemType];
        }

        public static NodeType GetItemType(string itemTypeName)
        {
            if (et == null)
            {
                et = new EventTables();
            }
            foreach (BoFormItemTypes types in et.itemTypes.Keys)
            {
                NodeType type = (NodeType) et.itemTypes[types];
                if (type.typeName.Equals(itemTypeName))
                {
                    return type;
                }
            }
            return null;
        }

        public static string getMenuClassName(string UID)
        {
            return ("Menu__" + UID);
        }

        public static string getMethodName(BoEventTypes type, bool before)
        {
            if (et == null)
            {
                et = new EventTables();
            }
            if (!before)
            {
                return (string) et.afterMethodNames[type];
            }
            return (string) et.beforeMethodNames[type];
        }

        public static string getMethodName(BoWidgetEventTypes type, bool before)
        {
            if (et == null)
            {
                et = new EventTables();
            }
            if (!before)
            {
                return (string) et.afterMethodNames[type];
            }
            return (string) et.beforeMethodNames[type];
        }

        public static string GetMethodReturn(BoEventTypes type, bool before)
        {
            if (et == null)
            {
                et = new EventTables();
            }
            if (!before && !((bool) et.returnTypes[type]))
            {
                return "System.Void";
            }
            return "System.Boolean";
        }

        public static string GetMethodReturn(BoWidgetEventTypes type, bool before)
        {
            if (et == null)
            {
                et = new EventTables();
            }
            if (!before && !((bool) et.returnTypes[type]))
            {
                return "System.Void";
            }
            return "System.Boolean";
        }

        public static string getWidgetClassName(string widgetType)
        {
            return ("Widget__" + widgetType);
        }

        public static bool IsBeforeEnabled(BoEventTypes type)
        {
            if (et == null)
            {
                et = new EventTables();
            }
            return (bool) et.beforeEnabled[type];
        }

        public static bool IsFormAccessValid(BoEventTypes type, bool before)
        {
            return !getMethodName(type, before).Equals("OnAfterFormUnload");
        }

        public static bool IsSuccessAccessValid(BoEventTypes type, bool before)
        {
            return getMethodName(type, before).StartsWith("OnAfter");
        }

        public static bool IsValidEvent(BoFormItemTypes itemType, BoEventTypes eventType)
        {
            if (et == null)
            {
                et = new EventTables();
            }
            BoEventTypes[] typesArray = (BoEventTypes[]) et.itemEventTypes[itemType];
            if (typesArray != null)
            {
                foreach (BoEventTypes types in typesArray)
                {
                    if (types == eventType)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

