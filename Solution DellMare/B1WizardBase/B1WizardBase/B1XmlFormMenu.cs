namespace B1WizardBase
{
    using SAPbouiCOM;
    using System;
    using System.IO;
    using System.Xml;

    public abstract class B1XmlFormMenu : B1Menu
    {
        private static int counter = 0;
        private static string formUID;
        private static string UIDPath = "Application/forms/action/form/@uid";
        private XmlDocument xmlDoc;

        protected B1XmlFormMenu()
        {
        }

        protected void LoadForm()
        {
            if (this.xmlDoc.HasChildNodes)
            {
                try
                {
                    this.xmlDoc.SelectSingleNode(UIDPath).Value = formUID + counter++;
                    string outerXml = this.xmlDoc.DocumentElement.OuterXml;
                    B1Connections.theAppl.LoadBatchActions(ref outerXml);
                    Form activeForm = B1Connections.theAppl.Forms.ActiveForm;

                    UserDataSource source = activeForm.DataSources.UserDataSources.Item("FolderDS");
                    if (source != null)
                    {
                        source.Value = "1";
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                B1Connections.theAppl.MessageBox("ERROR: XML File containing the form not found", -1, "", "", "");
            }
        }

        protected void LoadXml(string xmlFile)
        {
            this.xmlDoc = new XmlDocument();
            if (!File.Exists(xmlFile))
            {
                xmlFile = xmlFile.Insert(0, @"..\");
            }
            if (File.Exists(xmlFile))
            {
                this.xmlDoc.Load(xmlFile);
                formUID = this.xmlDoc.SelectSingleNode(UIDPath).Value;
            }
            else
            {
                B1Connections.theAppl.MessageBox("ERROR: File " + xmlFile + " not found", -1, "", "", "");
            }
        }
    }
}

