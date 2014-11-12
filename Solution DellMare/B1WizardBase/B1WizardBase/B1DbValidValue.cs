namespace B1WizardBase
{
    using System;

    public class B1DbValidValue
    {
        public string Description;
        public string Val;

        public B1DbValidValue()
        {
        }

        public B1DbValidValue(string val, string description)
        {
            this.Val = val;
            this.Description = description;
        }
    }
}

