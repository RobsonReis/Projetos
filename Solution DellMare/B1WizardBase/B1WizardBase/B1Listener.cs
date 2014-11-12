namespace B1WizardBase
{
    using System;
    using System.Reflection;

    public class B1Listener
    {
        public B1Action Action;
        public MethodInfo Method;

        public B1Listener(B1Action action, MethodInfo method)
        {
            this.Action = action;
            this.Method = method;
        }
    }
}

