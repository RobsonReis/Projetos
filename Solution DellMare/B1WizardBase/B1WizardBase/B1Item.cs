namespace B1WizardBase
{
    using System;

    public abstract class B1Item : B1Action
    {
        protected string FormType = "";
        protected string ItemUID = "";

        protected B1Item()
        {
        }

        public sealed override string GetKey(bool before)
        {
            return EventTables.GetActionKey(this.FormType, this.ItemUID, before);
        }
    }
}

