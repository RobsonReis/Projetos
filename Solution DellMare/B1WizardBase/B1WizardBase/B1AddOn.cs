namespace B1WizardBase
{
    using SAPbouiCOM;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;

    public abstract class B1AddOn
    {
        protected B1AddOn()
        {
            this.InitializeMenus();
            B1Listeners listeners = new B1Listeners();
            B1Connections.theAppl.MenuEvent += new _IApplicationEvents_MenuEventEventHandler(listeners.MenuHandler);
            B1Connections.theAppl.ItemEvent+= new _IApplicationEvents_ItemEventEventHandler(listeners.ItemHandler);
            B1Connections.theAppl.AppEvent+= new _IApplicationEvents_AppEventEventHandler(this.ApplHandler);
            B1Connections.theAppl.ProgressBarEvent+= new _IApplicationEvents_ProgressBarEventEventHandler(this.ProgressBarHandler);
            B1Connections.theAppl.StatusBarEvent+= new _IApplicationEvents_StatusBarEventEventHandler(this.StatusBarHandler);
            B1Connections.theAppl.RightClickEvent+= new _IApplicationEvents_RightClickEventEventHandler(listeners.RightClickHandler);
            B1Connections.theAppl.PrintEvent+= new _IApplicationEvents_PrintEventEventHandler(listeners.PrintHandler);
            B1Connections.theAppl.ReportDataEvent+= new _IApplicationEvents_ReportDataEventEventHandler(listeners.ReportDataHandler);
            B1Connections.theAppl.FormDataEvent += new _IApplicationEvents_FormDataEventEventHandler(listeners.FormDataHandler);
            B1Connections.theAppl.WidgetEvent+= new _IApplicationEvents_WidgetEventEventHandler(listeners.WidgetEventHandler);
            EventFilters filter = listeners.BuildFilter();
            B1Connections.theAppl.SetFilter(filter);
        }

        private void ApplHandler(BoAppEventTypes EventType)
        {
            try
            {
                switch (EventType)
                {
                    case BoAppEventTypes.aet_ShutDown:
                    case BoAppEventTypes.aet_ServerTerminition:
                        this.OnShutDown();
                        Environment.Exit(0);
                        return;

                    case BoAppEventTypes.aet_CompanyChanged:
                        this.OnCompanyChanged();
                        Environment.Exit(0);
                        return;

                    case BoAppEventTypes.aet_LanguageChanged:
                        this.OnLanguageChanged(B1Connections.theAppl.Language);
                        return;
                }
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: ApplHandler raised\n" + exception.InnerException.Message);
            }
        }

        private void batch(string fileName)
        {
            XmlDocument document = new XmlDocument();
            document.Load(fileName);
            string innerXml = document.InnerXml;
            B1Connections.theAppl.LoadBatchActions(ref innerXml);
            new B1BatchInfo(B1Connections.theAppl);
        }

        protected void InitializeMenus()
        {
            string path = "addMenus.xml";
            if (!File.Exists(path))
            {
                path = path.Insert(0, @"..\");
            }
            if (File.Exists(path))
            {
                this.batch(path);
            }
            path = "removeMenus.xml";
            if (!File.Exists(path))
            {
                path = path.Insert(0, @"..\");
            }
            if (File.Exists(path))
            {
                this.batch(path);
            }
            path = "updateMenus.xml";
            if (!File.Exists(path))
            {
                path = path.Insert(0, @"..\");
            }
            if (File.Exists(path))
            {
                this.batch(path);
            }
        }

        public abstract bool OnAfterProgressBarCreated();
        public abstract bool OnAfterProgressBarStopped(bool success);
        public abstract bool OnBeforeProgressBarCreated();
        public abstract bool OnBeforeProgressBarStopped(bool success);
        public abstract void OnCompanyChanged();
        public abstract void OnLanguageChanged(BoLanguages language);
        public abstract bool OnProgressBarReleased();
        public abstract void OnShutDown();
        public abstract void OnStatusBarErrorMessage(string text);
        public abstract void OnStatusBarNoTypedMessage(string text);
        public abstract void OnStatusBarSuccessMessage(string text);
        public abstract void OnStatusBarWarningMessage(string text);
        private void ProgressBarHandler(ref ProgressBarEvent pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;
            try
            {
                switch (pVal.EventType)
                {
                    case BoProgressBarEventTypes.pbet_ProgressBarCreated:
                        if ((pVal.BeforeAction && !this.OnBeforeProgressBarCreated()) || (!pVal.BeforeAction && !this.OnAfterProgressBarCreated()))
                        {
                            bubbleEvent = false;
                        }
                        return;

                    case BoProgressBarEventTypes.pbet_ProgressBarStopped:
                        if ((pVal.BeforeAction && !this.OnBeforeProgressBarStopped(pVal.ActionSuccess)) || (!pVal.BeforeAction && !this.OnAfterProgressBarStopped(pVal.ActionSuccess)))
                        {
                            bubbleEvent = false;
                        }
                        return;

                    case BoProgressBarEventTypes.pbet_ProgressBarReleased:
                        break;

                    default:
                        return;
                }
                if (!this.OnProgressBarReleased())
                {
                    bubbleEvent = false;
                }
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: ProgressBarHandler raised\n" + exception.InnerException.Message);
            }
        }

        private void StatusBarHandler(string text, BoStatusBarMessageType messageType)
        {
            try
            {
                switch (messageType)
                {
                    case BoStatusBarMessageType.smt_None:
                        this.OnStatusBarNoTypedMessage(text);
                        return;

                    case ~BoStatusBarMessageType.smt_None:
                        return;

                    case BoStatusBarMessageType.smt_Warning:
                        this.OnStatusBarWarningMessage(text);
                        return;

                    case BoStatusBarMessageType.smt_Error:
                        this.OnStatusBarErrorMessage(text);
                        return;

                    case BoStatusBarMessageType.smt_Success:
                        this.OnStatusBarSuccessMessage(text);
                        return;
                }
            }
            catch (Exception exception)
            {
                new B1Info(B1Connections.theAppl, "EXCEPTION: StatusBarHandler raised\n" + exception.InnerException.Message);
            }
        }
    }
}

