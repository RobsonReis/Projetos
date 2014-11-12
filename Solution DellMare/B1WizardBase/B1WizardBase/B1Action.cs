namespace B1WizardBase
{
    using System;
    using System.Reflection;

    public abstract class B1Action
    {
        private Type type;

        protected B1Action()
        {
            this.type = base.GetType();
        }

        public bool Action(MethodInfo method, object pVal)
        {
            try
            {
                if (method.ReturnType.ToString().Equals("System.Boolean"))
                {
                    return (bool) method.Invoke(this, new object[] { pVal });
                }
                method.Invoke(this, new object[] { pVal });
                return true;
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: " + this.type.Name + "." + method.Name + " raised\n" + exception.InnerException.Message);
                return true;
            }
        }

        public abstract string GetKey(bool before);
    }
}

