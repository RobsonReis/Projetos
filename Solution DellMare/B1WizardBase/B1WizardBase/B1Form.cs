namespace B1WizardBase
{
    using System;

    public abstract class B1Form : B1Action
    {
        protected string FormType = "";

        protected B1Form()
        {
        }

        public sealed override string GetKey(bool before)
        {
            return EventTables.GetActionKey(this.FormType, "", before);
        }
    }
}

