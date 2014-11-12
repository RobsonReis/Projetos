namespace B1WizardBase
{
    using System;

    public abstract class B1Menu : B1Action
    {
        protected string MenuUID = "";

        protected B1Menu()
        {
        }

        public sealed override string GetKey(bool before)
        {
            return EventTables.GetActionKey(this.MenuUID, before);
        }
    }
}

