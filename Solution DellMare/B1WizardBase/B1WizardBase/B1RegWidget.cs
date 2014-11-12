namespace B1WizardBase
{
    using System;

    public abstract class B1RegWidget : B1Action
    {
        protected string WidgetType = "";

        protected B1RegWidget()
        {
        }

        public sealed override string GetKey(bool before)
        {
            return EventTables.GetActionKey(this.WidgetType, "", before);
        }
    }
}

