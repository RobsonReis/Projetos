namespace B1WizardBase
{
    using SAPbouiCOM;
    using System;
    using System.Threading;

    public class B1Info
    {
        private string msg;
        private Application theAppl;

        public B1Info(Application theAppl, string msg)
        {
            this.msg = msg;
            this.theAppl = theAppl;
            new Thread(new ThreadStart(this.displayMsg)).Start();
        }

        private void displayMsg()
        {
            this.theAppl.MessageBox(this.msg, -1, "", "", "");
        }
    }
}

