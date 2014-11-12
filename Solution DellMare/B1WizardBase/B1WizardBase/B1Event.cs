namespace B1WizardBase
{
    using System;

    public abstract class B1Event : B1Action
    {
        protected B1Event()
        {
        }

        public sealed override string GetKey(bool before)
        {
            return EventTables.GetActionKey(before);
        }
    }
}

