namespace B1WizardBase
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using System;

    public class B1CockpitsManager
    {
        protected B1Cockpit[] AddCockpits;
        protected B1Widget[] CreateWidgetInstances;
        protected string[] PublishCockpits;
        protected B1WidgetType[] RegisterWidgetTypes;

        public void Manage(Application uiApp, SAPbobsCOM.Company company)
        {
            if (this.AddCockpits != null)
            {
                foreach (B1Cockpit cockpit in this.AddCockpits)
                {
                    cockpit.Add(uiApp, company);
                }
            }
            if (this.CreateWidgetInstances != null)
            {
                foreach (B1Widget widget in this.CreateWidgetInstances)
                {
                    widget.CreateWidgetInstance(uiApp, company);
                }
            }
            if (this.PublishCockpits != null)
            {
                foreach (string str in this.PublishCockpits)
                {
                    B1Cockpit.GetCockpit(company, str).Publish(company);
                }
            }
            if (this.RegisterWidgetTypes != null)
            {
                foreach (B1WidgetType type in this.RegisterWidgetTypes)
                {
                    type.RegisterWidget(uiApp);
                }
            }
        }
    }
}

