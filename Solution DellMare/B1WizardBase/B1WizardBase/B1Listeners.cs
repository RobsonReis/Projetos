namespace B1WizardBase
{
    using SAPbouiCOM;
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal class B1Listeners
    {
        private Hashtable listenersTable = new Hashtable();

        public B1Listeners()
        {
            Type attributeType = Type.GetType("B1WizardBase.B1ListenerAttribute");
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if ((!assembly.FullName.StartsWith("mscorlib") && !assembly.FullName.StartsWith("Interop")) && !assembly.FullName.StartsWith("Microsoft"))
                {
                    foreach (Type type2 in assembly.GetTypes())
                    {
                        if (type2.IsSubclassOf(Type.GetType("B1WizardBase.B1Action")) && !type2.IsAbstract)
                        {
                            B1Action action;
                            try
                            {
                                action = (B1Action) type2.GetConstructor(new Type[0]).Invoke(new B1AddOn[0]);
                            }
                            catch (Exception exception)
                            {
                                new B1Info(B1Connections.theAppl, "EXCEPTION: " + type2.Name + ".CTOR raised\n" + exception.InnerException.Message);
                                continue;
                            }
                            foreach (MethodInfo info2 in type2.GetMethods())
                            {
                                foreach (Attribute attribute in info2.GetCustomAttributes(attributeType, true))
                                {
                                    B1ListenerAttribute listener = (B1ListenerAttribute) attribute;
                                    if (type2.IsSubclassOf(Type.GetType("B1WizardBase.B1Event")))
                                    {
                                        foreach (string str in listener.GetEventActionKeys(action.GetKey(listener.GetBefore())))
                                        {
                                            this.registerListener(action, info2, listener, str);
                                        }
                                    }
                                    else if (type2.IsSubclassOf(Type.GetType("B1WizardBase.B1RegWidget")))
                                    {
                                        string key = action.GetKey(listener.GetBefore());
                                        this.registerWidgetListener(action, info2, listener, key);
                                    }
                                    else
                                    {
                                        string str3 = action.GetKey(listener.GetBefore());
                                        this.registerListener(action, info2, listener, str3);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public EventFilters BuildFilter()
        {
            if (this.listenersTable.Count == 0)
            {
                return null;
            }
            EventFilters filters = new EventFiltersClass();
            foreach (BoEventTypes types in this.listenersTable.Keys)
            {
                EventFilter filter = filters.Add(types);
                if ((!types.Equals(BoEventTypes.et_MENU_CLICK) && !types.ToString().Equals("90")) && !types.ToString().Equals("88"))
                {
                    Hashtable hashtable = new Hashtable();
                    Hashtable hashtable2 = (Hashtable) this.listenersTable[types];
                    bool flag = false;
                    foreach (string str in hashtable2.Keys)
                    {
                        if (str.Substring(0, str.IndexOf('.')).Equals("*"))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        foreach (string str2 in hashtable2.Keys)
                        {
                            string formType = str2.Substring(0, str2.IndexOf('.'));
                            if (hashtable[formType] == null)
                            {
                                filter.AddEx(formType);
                                hashtable[formType] = true;
                            }
                        }
                    }
                }
            }
            return filters;
        }

        public void FormDataHandler(ref BusinessObjectInfo pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;
            try
            {
                Hashtable hashtable = (Hashtable) this.listenersTable[pVal.EventType];
                if (hashtable != null)
                {
                    foreach (B1Listener listener in new B1Listener[] { (B1Listener) hashtable[EventTables.GetEventKey(pVal)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal, true)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal, false)] })
                    {
                        if ((listener != null) && !listener.Action.Action(listener.Method, pVal))
                        {
                            bubbleEvent = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: FormDataHandler raised\n" + exception.InnerException.Message);
            }
        }

        public void ItemHandler(string formUID, ref ItemEvent pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;
            try
            {
                Hashtable hashtable = (Hashtable) this.listenersTable[pVal.EventType];
                if (hashtable != null)
                {
                    foreach (B1Listener listener in new B1Listener[] { (B1Listener) hashtable[EventTables.GetEventKey(pVal)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal, true)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal, false)] })
                    {
                        if ((listener != null) && !listener.Action.Action(listener.Method, pVal))
                        {
                            bubbleEvent = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: ItemHandler raised\n" + exception.InnerException.Message);
            }
        }

        public void MenuHandler(ref MenuEvent pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;
            try
            {
                Hashtable hashtable = (Hashtable) this.listenersTable[BoEventTypes.et_MENU_CLICK];
                if (hashtable != null)
                {
                    foreach (B1Listener listener in new B1Listener[] { (B1Listener) hashtable[EventTables.GetEventKey(pVal)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal)] })
                    {
                        if ((listener != null) && !listener.Action.Action(listener.Method, pVal))
                        {
                            bubbleEvent = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: MenuHandler raised\n" + exception.InnerException.Message);
            }
        }

        public void PrintHandler(ref PrintEventInfo pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;
            try
            {
                Hashtable hashtable = (Hashtable) this.listenersTable[BoEventTypes.et_PRINT];
                if (hashtable != null)
                {
                    foreach (B1Listener listener in new B1Listener[] { (B1Listener) hashtable[EventTables.GetEventKey(pVal)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal, true)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal, false)] })
                    {
                        if ((listener != null) && !listener.Action.Action(listener.Method, pVal))
                        {
                            bubbleEvent = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: PrintHandler raised\n" + exception.InnerException.Message);
            }
        }

        private void registerListener(B1Action action, MethodInfo method, B1ListenerAttribute listener, string key)
        {
            BoEventTypes eventType = listener.GetEventType();
            Hashtable hashtable = (Hashtable) this.listenersTable[eventType];
            if (hashtable == null)
            {
                hashtable = new Hashtable();
                this.listenersTable[eventType] = hashtable;
            }
            if (EventTables.getMethodName(eventType, listener.GetBefore()) == null)
            {
                new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\nis not supported and will not be registered");
            }
            else if (!method.ReturnType.ToString().Equals(EventTables.GetMethodReturn(eventType, listener.GetBefore())))
            {
                new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\nhas wrong return type and will not be registered");
            }
            else if (method.GetParameters().Length != 1)
            {
                new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\nhas a wrong number of parameters and will not be registered");
            }
            else
            {
                ParameterInfo info = (ParameterInfo) method.GetParameters().GetValue(0);
                if ((eventType == BoEventTypes.et_MENU_CLICK) && !info.ParameterType.ToString().Equals("SAPbouiCOM.MenuEvent"))
                {
                    new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\ndoes not take MenuEvent as parameter and will not be registered");
                }
                else if ((eventType == BoEventTypes.et_PRINT) && !info.ParameterType.ToString().Equals("SAPbouiCOM.PrintEventInfo"))
                {
                    new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\ndoes not take PrintEventInfo as parameter and will not be registered");
                }
                else if ((eventType == BoEventTypes.et_PRINT_DATA) && !info.ParameterType.ToString().Equals("SAPbouiCOM.ReportDataInfo"))
                {
                    new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\ndoes not take ReportDataInfo as parameter and will not be registered");
                }
                else if ((eventType == BoEventTypes.et_RIGHT_CLICK) && !info.ParameterType.ToString().Equals("SAPbouiCOM.ContextMenuInfo"))
                {
                    new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\ndoes not take ContextMenuInfo as parameter and will not be registered");
                }
                else if ((((eventType == BoEventTypes.et_FORM_DATA_ADD) || (eventType == BoEventTypes.et_FORM_DATA_DELETE)) || ((eventType == BoEventTypes.et_FORM_DATA_LOAD) || (eventType == BoEventTypes.et_FORM_DATA_UPDATE))) && !info.ParameterType.ToString().Equals("SAPbouiCOM.BusinessObjectInfo"))
                {
                    new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\ndoes not take BusinessObjectInfo as parameter and will not be registered");
                }
                else if (((((eventType != BoEventTypes.et_MENU_CLICK) && (eventType != BoEventTypes.et_PRINT)) && ((eventType != BoEventTypes.et_PRINT_DATA) && (eventType != BoEventTypes.et_RIGHT_CLICK))) && (((eventType != BoEventTypes.et_FORM_DATA_ADD) && (eventType != BoEventTypes.et_FORM_DATA_DELETE)) && ((eventType != BoEventTypes.et_FORM_DATA_LOAD) && (eventType != BoEventTypes.et_FORM_DATA_UPDATE)))) && !info.ParameterType.ToString().Equals("SAPbouiCOM.ItemEvent"))
                {
                    new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\ndoes not take ItemEvent as parameter and will not be registered");
                }
                else if (hashtable[key] != null)
                {
                    new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\ndefines an already defined listener and will not be registered");
                }
                else
                {
                    hashtable.Add(key, new B1Listener(action, method));
                }
            }
        }

        private void registerWidgetListener(B1Action action, MethodInfo method, B1ListenerAttribute listener, string key)
        {
            BoWidgetEventTypes widgetEventType = listener.GetWidgetEventType();
            Hashtable hashtable = (Hashtable) this.listenersTable[widgetEventType];
            if (hashtable == null)
            {
                hashtable = new Hashtable();
                this.listenersTable[widgetEventType] = hashtable;
            }
            if (EventTables.getMethodName(widgetEventType, listener.GetBefore()) == null)
            {
                new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\nis not supported and will not be registered");
            }
            else if (!method.ReturnType.ToString().Equals(EventTables.GetMethodReturn(widgetEventType, listener.GetBefore())))
            {
                new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\nhas wrong return type and will not be registered");
            }
            else if (method.GetParameters().Length != 1)
            {
                new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\nhas a wrong number of parameters and will not be registered");
            }
            else
            {
                ParameterInfo info = (ParameterInfo) method.GetParameters().GetValue(0);
                if (!info.ParameterType.ToString().Equals("SAPbouiCOM.WidgetData"))
                {
                    new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\ndoes not take WidgetData as parameter and will not be registered");
                }
                else if (hashtable[key] != null)
                {
                    new B1Info(B1Connections.theAppl, "ERROR: listener " + method.DeclaringType.Name + "." + method.Name + "\ndefines an already defined listener and will not be registered");
                }
                else
                {
                    hashtable.Add(key, new B1Listener(action, method));
                }
            }
        }

        public void ReportDataHandler(ref ReportDataInfo pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;
            try
            {
                Hashtable hashtable = (Hashtable) this.listenersTable[BoEventTypes.et_PRINT_DATA];
                if (hashtable != null)
                {
                    foreach (B1Listener listener in new B1Listener[] { (B1Listener) hashtable[EventTables.GetEventKey(pVal)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal, true)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal, false)] })
                    {
                        if ((listener != null) && !listener.Action.Action(listener.Method, pVal))
                        {
                            bubbleEvent = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: ReportDataHandler raised\n" + exception.InnerException.Message);
            }
        }

        public void RightClickHandler(ref ContextMenuInfo ctxMenuInfo, out bool bubbleEvent)
        {
            bubbleEvent = true;
            try
            {
                Hashtable hashtable = (Hashtable) this.listenersTable[BoEventTypes.et_RIGHT_CLICK];
                if (hashtable != null)
                {
                    foreach (B1Listener listener in new B1Listener[] { (B1Listener) hashtable[EventTables.GetEventKey(ctxMenuInfo)], (B1Listener) hashtable[EventTables.GetGenericEventKey(ctxMenuInfo, true)], (B1Listener) hashtable[EventTables.GetGenericEventKey(ctxMenuInfo, false)] })
                    {
                        if ((listener != null) && !listener.Action.Action(listener.Method, ctxMenuInfo))
                        {
                            bubbleEvent = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: RightClickHandler raised\n" + exception.InnerException.Message);
            }
        }

        public void WidgetEventHandler(ref WidgetData pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;
            try
            {
                Hashtable hashtable = (Hashtable) this.listenersTable[pVal.EventType];
                if (hashtable != null)
                {
                    foreach (B1Listener listener in new B1Listener[] { (B1Listener) hashtable[EventTables.GetEventKey(pVal)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal, true)], (B1Listener) hashtable[EventTables.GetGenericEventKey(pVal, false)] })
                    {
                        if ((listener != null) && !listener.Action.Action(listener.Method, pVal))
                        {
                            bubbleEvent = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: WidgetEventHandler raised\n" + exception.InnerException.Message);
            }
        }
    }
}

