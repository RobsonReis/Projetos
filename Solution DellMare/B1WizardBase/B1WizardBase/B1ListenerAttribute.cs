namespace B1WizardBase
{
    using SAPbouiCOM;
    using System;

    [AttributeUsage(AttributeTargets.Method, Inherited=true, AllowMultiple=false)]
    public sealed class B1ListenerAttribute : Attribute
    {
        private bool before;
        private BoEventTypes eventType;
        private string[] formTypes;
        private BoWidgetEventTypes widgetEventType;

        public B1ListenerAttribute(BoEventTypes eventType)
        {
            this.eventType = eventType;
            this.before = false;
        }

        public B1ListenerAttribute(BoEventTypes eventType, bool before)
        {
            this.eventType = eventType;
            this.before = before;
        }

        public B1ListenerAttribute(BoWidgetEventTypes eventType, bool before)
        {
            this.widgetEventType = eventType;
            this.before = before;
        }

        public B1ListenerAttribute(BoEventTypes eventType, bool before, string[] formTypes)
        {
            this.eventType = eventType;
            this.before = before;
            this.formTypes = formTypes;
        }

        public bool GetBefore()
        {
            return this.before;
        }

        public string[] GetEventActionKeys(string beforeFlag)
        {
            if (this.GetEventType().Equals(BoEventTypes.et_MENU_CLICK))
            {
                return new string[] { ("*." + beforeFlag) };
            }
            string[] formTypes = this.GetFormTypes();
            if (formTypes == null)
            {
                return new string[] { ("*.*." + beforeFlag) };
            }
            int num = 0;
            string[] strArray = new string[formTypes.Length];
            foreach (string str in formTypes)
            {
                strArray[num++] = str + ".*." + beforeFlag;
            }
            return strArray;
        }

        public BoEventTypes GetEventType()
        {
            return this.eventType;
        }

        public string[] GetFormTypes()
        {
            return this.formTypes;
        }

        public BoWidgetEventTypes GetWidgetEventType()
        {
            return this.widgetEventType;
        }
    }
}

